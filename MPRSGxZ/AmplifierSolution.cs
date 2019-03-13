using MPRSGxZ.Events;
using MPRSGxZ.Hardware;
using MPRSGxZ.Commands;
using System.Timers;

namespace MPRSGxZ
{
    public class AmplifierSolution
    {	
		public int AmplifierCount { get; private set; }
		public Amplifier[] Amplifiers { get; private set; }
		public Source[] Sources { get; private set; }

		private AmplifierPort Port;

		private Timer PollTimer;

		private QueueCommandEvent QueueCommand;
		public	ZoneChangedEvent ZoneChanged;

		public AmplifierSolution(string PortName, int PollFrequency = 250, int AmplifierCount = 1)
		{
			this.AmplifierCount = AmplifierCount;

			this.Port = new AmplifierPort(PortName);
			
			this.PollTimer = new Timer(PollFrequency);
			PollTimer.Elapsed += PollAmplifiers;

			QueueCommand += QueueAmplifierCommand;

			//
			// All models have 6 sources, and when stacked the sources of the first amp are shared with
			// all other amps.
			//
			Sources = new Source[6];

			for(int i = 0; i < 6; i++)
			{
				Sources[i] = new Source(i + 1);
			}

			Amplifiers = new Amplifier[AmplifierCount];

			for(int i = 0; i < AmplifierCount; i++)
			{
				Amplifiers[i] = new Amplifier(i + 1, QueueCommand, ZoneChanged);
			}
		}

		public void Open()
		{
			Port.Open();
			PollTimer.Enabled = true;
		}

		public void Close()
		{
			PollTimer.Enabled = false;
			Port.Close();
		}

		private void PollAmplifiers(object sender, ElapsedEventArgs e)
		{
			for(int i = 1; i <= AmplifierCount; i++)
			{
				var CurrentAmplifier = Amplifiers[i - 1];

				lock(CurrentAmplifier)
				{
					var CurrentAmpQuery = new Command(BaseCommand.Query, i, 0, 0);
					var CommandResults = Port.ExecuteCommand(CurrentAmpQuery);

					foreach (string CurrentResult in CommandResults)
					{
						int AmpID = int.Parse(CurrentResult.Substring(0, 1));
						int ZoneID = int.Parse(CurrentResult.Substring(1, 1));

						var CurrentZone = CurrentAmplifier.Zones[ZoneID - 1];
						var PublicAddress	= int.Parse(CurrentResult.Substring(2, 2)) == 1 ? true : false;
						var Power			= int.Parse(CurrentResult.Substring(4, 2)) == 1 ? true : false;
						var Mute			= int.Parse(CurrentResult.Substring(6, 2)) == 1 ? true : false;
						var DoNotDisturb	= int.Parse(CurrentResult.Substring(8, 2)) == 1 ? true : false;
						var Volume			= int.Parse(CurrentResult.Substring(10, 2));
						var Treble			= int.Parse(CurrentResult.Substring(12, 2));
						var Bass			= int.Parse(CurrentResult.Substring(14, 2));
						var Balance			= int.Parse(CurrentResult.Substring(16, 2));
						var Source			= int.Parse(CurrentResult.Substring(18, 2));
						CurrentZone.UpdateState(PublicAddress, Power, Mute, DoNotDisturb, Volume, Treble, Bass, Balance, Source);
					}
				}
			}
		}

		private void QueueAmplifierCommand(QueueCommandEventArgs e)
		{
			int AmpID = e.Command.AmpID;
			int ZoneID = e.Command.ZoneID;

			var CurrentZone = Amplifiers[AmpID - 1].Zones[ZoneID - 1];

			lock(CurrentZone)
			{
				var CommandResults = Port.ExecuteCommand(e.Command);
			}
		}

		/*public void LinkZone(int PrimaryZoneID, int SecondaryZoneID)
		{
			Zone Primary = Zones[PrimaryZoneID - 1];
			Zone Secondary = Zones[SecondaryZoneID - 1];

			//
			// Check to make sure both zones are enabled
			//
			string message = @"Zone {0} is not enabled and cannot be made into a link zone";

			if (!Primary.Enabled)
			{
				throw new ZoneNotEnabledException(string.Format(message, PrimaryZoneID));
			}

			if(!Secondary.Enabled)
			{
				throw new ZoneNotEnabledException(string.Format(message, SecondaryZoneID));
			}

			//
			// Check to make sure the primary zone is not already a secondary zone
			//
			message = @"Zone {0} is already a secondary zone and cannot be a primary";

			if(Primary.LinkStatus == ZoneLinkMode.Secondary)
			{
				throw new ZoneAlreadyLinkedException(string.Format(message, PrimaryZoneID));
			}

			//
			// Check to make sure the secondary zone is unlinked
			//
			message = @"Zone {0} is already a linked zone";

			if (Secondary.LinkStatus != ZoneLinkMode.Unlinked)
			{
				throw new ZoneAlreadyLinkedException(string.Format(message, SecondaryZoneID));
			}

			//
			// Go ahead and link the zones:
			// Set the primary zone LinkStatus to Primary
			// Set the secondary zone LinkStatus to Secondary
			// Add the secondary zone ID to the primary LinkedZone list
			//
			Primary.LinkStatus = ZoneLinkMode.Primary;
			Secondary.LinkStatus = ZoneLinkMode.Secondary;
			Zones[PrimaryZoneID - 1].LinkedZones.Add(SecondaryZoneID);
		}

		public void UnlinkZone(int PrimaryZoneID, int SecondaryZoneID)
		{
			Zone Primary = Zones[PrimaryZoneID - 1];
			Zone Secondary = Zones[SecondaryZoneID - 1];

			//
			// Check to make sure the primary zone is actually a primary zone
			//
			string message = @"Zone {0} is not a primary zone";

			if (Primary.LinkStatus != ZoneLinkMode.Primary)
			{
				throw new ZoneNotLinkedException(string.Format(message, PrimaryZoneID));
			}

			//
			// Check to make sure the secondary zone is actually a secondary zone
			//
			message = @"Zone {0} is not a secondary zone";

			if (Secondary.LinkStatus != ZoneLinkMode.Secondary)
			{
				throw new ZoneNotLinkedException(string.Format(message, SecondaryZoneID));
			}

			//
			// Check to make sure the secondary zone is a linked zone of the primary zone
			//
			message = @"Primary zone {0} is not linked to secondary zone {1}";

			if(!Primary.LinkedZones.Contains(SecondaryZoneID))
			{
				throw new ZoneNotLinkedException(string.Format(message, PrimaryZoneID, SecondaryZoneID));
			}

			//
			// Go ahead and link the zones:
			// Remove the secondary zone from the LinkedZones list on the primary
			// Set the secondary zone LinkStatus to Unlinked
			// Check to see if the primary is now unlinked to all zones, if it is, set it to Unlinked
			//
			Primary.LinkedZones.Remove(SecondaryZoneID);
			Secondary.LinkStatus = ZoneLinkMode.Unlinked;
			
			if(Primary.LinkedZones.Count == 0)
			{
				Primary.LinkStatus = ZoneLinkMode.Unlinked;
			}
		}*/
	}
}