using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Serialization;
using MPRSGxZ.Exceptions;
using System.Runtime.Serialization;
using MPRSGxZ.Events;
using System.Text;
using System.Xml;

namespace MPRSGxZ
{
	[DataContract]
    public class AmplifierSolution
    {
		private AmplifierPort m_AmplifierPort;
		private Amplifier[] m_Amplifiers;
		private Source[] m_Sources;

		private int m_AmplifierCount;

		private SettingChangedEventHandler SettingChangedEvent;
		private ZonePropertyPollEventHandler ZonePropertyPollEvent;

		/// <summary>
		/// Default blank constructor used for deserialization
		/// </summary>
		public AmplifierSolution()
		{
		}

		public AmplifierSolution(bool WithDefaults)
		{
			m_AmplifierPort = new AmplifierPort();
			m_AmplifierPort.PollFrequency = 200;
			m_AmplifierPort.PortName = "COM1";

			m_AmplifierCount = 3;

			//
			// All models have 6 zones, and when stacked the 6 zones of the first amp are shared with
			// all other amps.
			//
			m_Sources = new Source[6];

			for(int i = 0; i < 6; i++)
			{
				m_Sources[i] = new Source(i + 1);
			}

			m_Amplifiers = new Amplifier[3];

			for(int i = 0; i < 3; i++)
			{
				m_Amplifiers[i] = new Amplifier(i + 1);
			}
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			SettingChangedEvent += SerializeSolution;

			ZonePropertyPollEvent += UpdateZonesFromPoll;

			Port.AttachEvents(ZonePropertyPollEvent);

			for(int i = 0; i < 3; i++)
			{
				m_Amplifiers[i].AttachEvents(SettingChangedEvent, Port.ZoneChangedEvent);
			}
		}

		private void SerializeSolution()
		{
			var XMLFile = new FileStream("SerializationTest.xml", FileMode.Create);
			var Serializer = new DataContractSerializer(typeof(AmplifierSolution));
			Serializer.WriteObject(XMLFile, this);

			XMLFile.Close();
		}

		[DataMember]
		public AmplifierPort Port
		{
			get
			{
				return m_AmplifierPort;
			}
			set
			{
				m_AmplifierPort = value;
			}
		}

		[DataMember]
		public Source[] Sources
		{
			get
			{
				return m_Sources;
			}
			internal set
			{
				m_Sources = value;
			}
		}

		[DataMember]
		public Amplifier[] Amplifiers
		{
			get
			{
				return m_Amplifiers;
			}
			internal set
			{
				m_Amplifiers = value;
			}
		}

		//
		// Forwards the ZonePropertyPollEventArgs to the proper Amp/Zone
		//
		private void UpdateZonesFromPoll(ZonePropertyPollEventArgs e)
		{
			lock(Amplifiers[e.AmpID - 1].Zones[e.ZoneID - 1])
			{
				Amplifiers[e.AmpID - 1].Zones[e.ZoneID - 1].UpdateFromPollData(e);
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