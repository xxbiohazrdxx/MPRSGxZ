using MPRSGxZ.Commands;
using System;

namespace MPRSGxZ.Ports
{
	internal class VirtualAmplifierPort : IPort
	{
		private object PortLock = new();
		private string[] VirtualAmplifierState;

		internal VirtualAmplifierPort()
		{
			VirtualAmplifierState = new string[18];

			for (int AmplifierID = 1; AmplifierID <= 3; AmplifierID++)
			{
				for (int ZoneID = 1; ZoneID <= 6; ZoneID++)
				{
					int Index = ((AmplifierID - 1) * 6) + ZoneID - 1;

					VirtualAmplifierState[Index] = $"{AmplifierID}{ZoneID}000000001907070701";
				}
			}
		}

		public void Open()
		{
		}

		public void Close()
		{
		}

		public CommandResponse[] ExecuteCommand(Command CommandToExecute)
		{
			lock (PortLock)
			{
				CommandResponse[] Response = new CommandResponse[CommandToExecute.ExpectedLines];
				string[] SimulatedReads = new string[CommandToExecute.ExpectedLines];

				int Index = ((CommandToExecute.AmpID - 1) * 6) + Math.Max(0, CommandToExecute.ZoneID - 1);

				//
				// Set commands don't return any data
				//
				if (CommandToExecute.Type == CommandType.Set)
				{
					var ZoneState = VirtualAmplifierState[Index];
					var ParsedState = new CommandResponse(ZoneState);

					var NewValue = $"{CommandToExecute.Value:D2}";
					ZoneState = ZoneState.Remove(CommandToExecute.ResponseIndex, 2).Insert(CommandToExecute.ResponseIndex, NewValue);
					VirtualAmplifierState[Index] = ZoneState;
				}
				//
				// If the command is not a set command it is a query
				// So return 1 response (if querying a single zone) or 6 responses (if querying a whole amplifier)
				//
				else
				{
					Array.Copy(VirtualAmplifierState, Index, SimulatedReads, 0, CommandToExecute.ExpectedLines);

					for (int i = 0; i < CommandToExecute.ExpectedLines; i++)
					{
						var CurrentLine = SimulatedReads[i];
						Response[i] = new CommandResponse(CurrentLine);
					}
				}

				return Response;
			}
		}
	}
}