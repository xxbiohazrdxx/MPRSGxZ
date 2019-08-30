using System;
using System.IO.Ports;
using MPRSGxZ.Commands;

namespace MPRSGxZ.Ports
{
	internal class SerialAmplifierPort : IPort
	{
		private SerialPort Port;
		private object PortLock = new object();

		internal SerialAmplifierPort(string PortName)
		{
			Port = new SerialPort()
			{
				PortName = PortName,
				BaudRate = 9600,
				StopBits = StopBits.One,
				Parity = Parity.None,
				DataBits = 8,
				Handshake = Handshake.None,
				NewLine = "\r\n",
				ReadTimeout = 100
			};
		}

		public void Open()
		{
			Port.Open();

			// Get the port into a known state
			Port.WriteLine(string.Empty);
			Port.ReadLine();

			if (Port.BytesToRead != 1)
			{
				throw new InvalidOperationException("The serial port is in an unknown state.");
			}
		}

		public void Close()
		{
			Port.Close();
		}

		public CommandResponse[] ExecuteCommand(Command CommandToExecute)
		{
			lock(PortLock)
			{
				Port.WriteLine(CommandToExecute);
				var Echo = Port.ReadLine();

				if($"#{(string)CommandToExecute}" != Echo)
				{
					throw new InvalidOperationException("The serial port is in an unknown state.");
				}

				CommandResponse[] Response = new CommandResponse[CommandToExecute.ExpectedLines];
				for (int i = 0; i < CommandToExecute.ExpectedLines; i++)
				{
					var CurrentLine = Port.ReadLine();

					if (!CurrentLine.StartsWith(@"#>") && !CurrentLine.EndsWith("\r"))
					{
						throw new InvalidOperationException("The serial port is in an unknown state.");
					}

					CurrentLine = CurrentLine.Replace("\r", string.Empty);
					CurrentLine = CurrentLine.Replace(@"#>", string.Empty);

					Response[i] = new CommandResponse(CurrentLine);
				}

				return Response;
			}			
		}
	}
}