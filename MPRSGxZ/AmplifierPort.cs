using System.IO.Ports;
using System.Threading;
using System.Collections.Concurrent;
using System;
using System.Runtime.Serialization;
using MPRSGxZ.Events;
using MPRSGxZ.Exceptions;
using System.Text;

namespace MPRSGxZ
{
	[DataContract]
	public class AmplifierPort
	{
		private ConcurrentQueue<string> m_SendQueue;
		private ConcurrentQueue<string> m_ReceiveQueue;

		private bool m_RunTXRXThreads;
		private bool m_RunPollThread;

		private Thread m_PollThread;
		private Thread m_SendThread;
		private Thread m_ReceiveThread;
		private Thread m_ReceiveQueueThread;

		private SerialPort m_SerialPort;

		private string m_PortName;
		private int m_PollFrequency;

		private event ZonePropertyChangedEventHandler ZonePropertyChangedEvent;
		private event SettingChangedEventHandler SettingChangedEvent;
		private event ZonePropertyPollEventHandler ZonePropertyPollEvent;

		private string m_BufferString;

		[DataMember]
		public string PortName
		{
			get
			{
				return m_PortName;
			}
			set
			{
				m_PortName = value;
				SettingChangedEvent?.Invoke();
			}
		}

		[DataMember]
		public int PollFrequency
		{
			get
			{
				return m_PollFrequency;
			}
			set
			{
				m_PollFrequency = value;
				SettingChangedEvent?.Invoke();
			}
		}

		[IgnoreDataMember]
		internal ZonePropertyChangedEventHandler ZoneChangedEvent
		{
			get
			{
				return ZonePropertyChangedEvent;
			}
		}

		internal AmplifierPort()
		{
			InitializePort();
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			InitializePort();
		}

		private void InitializePort()
		{
			m_BufferString = string.Empty;

			m_SerialPort = new SerialPort()
			{
				PortName = "COM6" /*m_PortName*/,
				BaudRate = 9600,
				StopBits = StopBits.One,
				Parity = Parity.None,
				DataBits = 8,
				Handshake = Handshake.None,
				NewLine = "\r\n",
				ReadTimeout = 100
			};

			m_SerialPort.ReceivedBytesThreshold = 8;

			m_PollThread = new Thread(new ThreadStart(Poll));
			m_SendThread = new Thread(new ThreadStart(ProcessSendQueue));
			m_ReceiveThread = new Thread(new ThreadStart(ReadSerialData));
			m_ReceiveQueueThread = new Thread(new ThreadStart(ProcessReceiveQueue));

			ZonePropertyChangedEvent += QueueCommand;
		}

		internal void AttachEvents(ZonePropertyPollEventHandler ZonePropertyPoll)
		{
			ZonePropertyPollEvent = ZonePropertyPoll;
		}

		public void Open()
		{
			m_RunTXRXThreads = true;
			m_RunPollThread = true;

			m_SendQueue = new ConcurrentQueue<string>();
			m_ReceiveQueue = new ConcurrentQueue<string>();

			m_SerialPort.Open();

			m_PollThread.Start();
			m_SendThread.Start();
			m_ReceiveThread.Start();
			m_ReceiveQueueThread.Start();
		}

		public void Close()
		{
			//
			// Disable polling so the send queue does not continue to be filled and
			// wait for the Poll thread to exit
			//
			m_RunPollThread = false;
			while (m_PollThread.IsAlive) ;

			//
			// Detach events
			//
			ZonePropertyPollEvent = null;

			//
			// Gracefully end TX and RX threads
			//
			m_RunTXRXThreads = false;

			while (m_SendThread.IsAlive ||
					m_ReceiveThread.IsAlive) ;

			//
			// Finally close the serial port
			//
			m_SerialPort.Close();
		}

		private void ProcessSendQueue()
		{
			string DequeuedCommand;

			while(m_RunTXRXThreads)
			{
				if(!m_SendQueue.IsEmpty)
				{
					if(m_SendQueue.TryDequeue(out DequeuedCommand))
					{
						lock (m_SerialPort)
						{
							byte[] encodedStr = m_SerialPort.Encoding.GetBytes(DequeuedCommand + m_SerialPort.NewLine);

							m_SerialPort.BaseStream.Write(encodedStr, 0, encodedStr.Length);
							m_SerialPort.BaseStream.Flush();

							//
							// If we write as fast as the serial port will allow us to, the echo and responses to commands (such as queries)
							// tend to get jumbled.
							//
							// One solution would be to re-write the I/O model so that we handle reads and writes in a single thread,
							// processing responses (and echos) as we write a command to the amplifier.
							//
							// An easier (but in my opinion, less elegant) solution is just to ensure that this loop doesn't execute
							// too quickly, hence the Thread.Sleep
							//
							Thread.Sleep(10);
						}
					}
				}
			}
		}

		private void ReadSerialData()
		{
			while(m_RunTXRXThreads)
			{
				byte[] buffer = new byte[1024];

				lock(m_SerialPort)
				{
					//
					// Check to see if there is any data available to read
					//
					if (m_SerialPort.BytesToRead > 0)
					{
						//
						// Read in up to 1k into the buffer
						//
						int BytesRead = m_SerialPort.BaseStream.Read(buffer, 0, 1024);

						//
						// Copy the actual amount of data read and place it in a string
						//
						byte[] result = new byte[BytesRead];
						Buffer.BlockCopy(buffer, 0, result, 0, BytesRead);

						string DecodedString = m_SerialPort.Encoding.GetString(result);
						m_BufferString += DecodedString;
					}
				}	

				//
				// Loop while the RX string has new lines
				//
				while(m_BufferString.IndexOf(m_SerialPort.NewLine) != -1)
				{
					//
					// Get the index of the first newline, and then add the length of the newline characters
					//
					int NewLineIndex = m_BufferString.IndexOf(m_SerialPort.NewLine) + m_SerialPort.NewLine.Length;

					//
					// Copy the first line from the RX string and place it in the receieve queue
					//
					var Token = m_BufferString.Substring(0, NewLineIndex);
					m_ReceiveQueue.Enqueue(Token);

					//
					// Remove the first line from teh RX string
					//
					m_BufferString = m_BufferString.Remove(0, NewLineIndex);
				}
			}
		}

		private void ProcessReceiveQueue()
		{
			string DequeuedResponse;

			while(m_RunTXRXThreads)
			{
				if(!m_ReceiveQueue.IsEmpty)
				{
					if(m_ReceiveQueue.TryDequeue(out DequeuedResponse))
					{
						if(DequeuedResponse.Equals(@"Command Error."))
						{
							throw new InvalidCommandException();
						}

						//
						// As the device echos, our responses could begin with 
						// #? - Echo of a query
						// #< - Echo of a command
						// #> - Response to a command
						//
						// We can simply ignore responses that are echos, and only parse query responses
						//
						if(DequeuedResponse.StartsWith(@"#>"))
						{
							DequeuedResponse = DequeuedResponse.Replace("\r", string.Empty);
							DequeuedResponse = DequeuedResponse.Replace(@"#>", string.Empty);

							int AmpID			= int.Parse(DequeuedResponse.Substring(0, 1));
							int ZoneID			= int.Parse(DequeuedResponse.Substring(1, 1));
							bool PublicAddress	= int.Parse(DequeuedResponse.Substring(2, 2)) == 1 ? true : false;
							bool Power			= int.Parse(DequeuedResponse.Substring(4, 2)) == 1 ? true : false;
							bool Mute			= int.Parse(DequeuedResponse.Substring(6, 2)) == 1 ? true : false;
							bool DoNotDisturb	= int.Parse(DequeuedResponse.Substring(8, 2)) == 1 ? true : false;
							int Volume			= int.Parse(DequeuedResponse.Substring(10, 2));
							int Treble			= int.Parse(DequeuedResponse.Substring(12, 2));
							int Bass			= int.Parse(DequeuedResponse.Substring(14, 2));
							int Balance			= int.Parse(DequeuedResponse.Substring(16, 2));
							int Source			= int.Parse(DequeuedResponse.Substring(18, 2));

							ZonePropertyPollEvent?.Invoke(new ZonePropertyPollEventArgs(AmpID, ZoneID, PublicAddress, Power, Mute, DoNotDisturb, Volume, Treble, Bass, Balance, Source));
						}
					}
				}
			}
		}

		//
		// The Poll method simply adds a query to the Send queue
		//
		private void Poll()
		{
			while (m_RunPollThread)
			{
				if (m_SerialPort.IsOpen)
				{
					string UnformattedAmplifierQuery = @"?{0}0";
					//
					// The amplifiers are addressed with a 1 based index, so we
					// start at 1 and go to 3. If there are not three amplifiers
					// the query returns no information for that amplifier
					//
					for (int i = 1; i <= 3; i++)
					{
						string Query = string.Format(UnformattedAmplifierQuery, i);

						m_SendQueue.Enqueue(Query);
					}
				}

				Thread.Sleep(m_PollFrequency);
			}
		}

		private void QueueCommand(ZonePropertyChangedEventArgs e)
		{
			string UnformattedAmplifierQuery = @"<{0}{1}{2}{3}";
			string Query = string.Format(UnformattedAmplifierQuery, e.AmpID, e.ZoneID, e.Command.CommandString, e.Value.ToString("00"));

			m_SendQueue.Enqueue(Query);
		}
	}
}
