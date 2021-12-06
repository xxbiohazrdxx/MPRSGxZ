using MPRSGxZ.Commands;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace MPRSGxZ.Ports
{
	internal class IPAmplifierPort : IPort
	{
		private TcpClient Client;
		private IPEndPoint ServerEndpoint;
		private NetworkStream Stream;
		private StreamWriter Writer;
		private StreamReader Reader;
		private object PortLock = new object();

		internal IPAmplifierPort(IPEndPoint ServerEndpoint)
		{
			Client = new TcpClient();
			this.ServerEndpoint = ServerEndpoint;
		}

		public void Open()
		{
			Client.Connect(ServerEndpoint);
			Stream = Client.GetStream();

			Writer = new StreamWriter(Stream, System.Text.Encoding.ASCII);
			Writer.NewLine = "\r\n";
			Writer.AutoFlush = true;

			Reader = new StreamReader(Stream, System.Text.Encoding.ASCII);

			Writer.WriteLine();
			Reader.ReadLine();

			if (Reader.Peek() != '#')
			{
				throw new InvalidOperationException("The serial port is in an unknown state.");
			}
		}

		public void Close()
		{
			Reader.Close();
			Writer.Close();
			Stream.Close();
			Client.Close();
		}

		public CommandResponse[] ExecuteCommand(Command CommandToExecute)
		{
			lock (PortLock)
			{
				Writer.WriteLine(CommandToExecute);
				var Echo = Reader.ReadLine();

				if ($"#{(string)CommandToExecute}" != Echo)
				{
					throw new InvalidOperationException("The serial port is in an unknown state.");
				}

				CommandResponse[] Response = new CommandResponse[CommandToExecute.ExpectedLines];
				for (int i = 0; i < CommandToExecute.ExpectedLines; i++)
				{
					var CurrentLine = Reader.ReadLine();

					//
					// Read the additional CR
					//
					if (Reader.Peek() != 13)
					{
						throw new InvalidOperationException("The serial port is in an unknown state.");
					}

					var ExtraNewline = Reader.ReadLine();

					if (ExtraNewline != string.Empty)
					{
						throw new InvalidOperationException("The serial port is in an unknown state.");
					}

					if (!CurrentLine.StartsWith(@"#>") && !CurrentLine.EndsWith("\r"))
					{
						throw new InvalidOperationException("The serial port is in an unknown state.");
					}

					CurrentLine = CurrentLine.Replace(@"#>", string.Empty);

					Response[i] = new CommandResponse(CurrentLine);
				}

				return Response;
			}
		}
	}
}