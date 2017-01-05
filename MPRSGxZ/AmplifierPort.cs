using System.IO.Ports;
using System.Threading;
using System.Collections.Concurrent;
using System;
using System.Runtime.Serialization;
using MPRSGxZ.Events;

namespace MPRSGxZ
{
	[DataContract]
	internal class AmplifierPort
	{
		private ConcurrentQueue<string> m_SendQueue;
		private ConcurrentQueue<string> m_ReceiveQueue;

		private Thread m_PollThread;
		private Thread m_SendThread;
		private Thread m_ReceiveThread;

		private SerialPort m_SerialPort;

		private string m_PortName;
		private int m_PollFrequency;

		private bool m_RunThreads;

		private event SettingChangedEventHandler SettingChangedEvent;

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

		internal AmplifierPort()
		{
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			m_SerialPort = new SerialPort()
			{
				PortName = m_PortName,
				BaudRate = 9600,
				StopBits = StopBits.One,
				Parity = Parity.None,
				DataBits = 8,
				Handshake = Handshake.None,
				NewLine = "\r\n",
				ReadTimeout = 100
			};

			m_SerialPort.DataReceived += m_SerialPort_DataReceived;

			m_PollThread = new Thread(new ThreadStart(Poll));
			m_SendThread = new Thread(new ThreadStart(ProcessSendQueue));
			m_ReceiveThread = new Thread(new ThreadStart(ProcessReceiveQueue));
		}

		private void m_SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			SerialPort AmplifierSerialPort = (SerialPort)sender;
			string SerialReceived;

			try
			{
				while (AmplifierSerialPort.BytesToRead != 0)
				{
					SerialReceived = AmplifierSerialPort.ReadLine();

					m_ReceiveQueue.Enqueue(SerialReceived);
				}
			}
#warning Don't use try/catch for flow
			catch(TimeoutException)
			{

			}
		}

		internal void Open()
		{
			m_RunThreads = true;

			m_SendQueue = new ConcurrentQueue<string>();
			m_ReceiveQueue = new ConcurrentQueue<string>();

			m_SerialPort.Open();

			m_PollThread.Start();
			m_SendThread.Start();
			m_ReceiveThread.Start();
		}

		internal void Close()
		{
			m_RunThreads = false;
			m_SerialPort.Close();
		}

		private void ProcessSendQueue()
		{
			string DequeuedCommand;

			while(m_RunThreads)
			{
				if(!m_SendQueue.IsEmpty)
				{
					if(m_SendQueue.TryDequeue(out DequeuedCommand))
					{
						m_SerialPort.WriteLine(DequeuedCommand);
					}
				}
			}
		}

		private void ProcessReceiveQueue()
		{
			string DequeuedCommand;

			while(m_RunThreads)
			{
				if(!m_ReceiveQueue.IsEmpty)
				{
					if(m_ReceiveQueue.TryDequeue(out DequeuedCommand))
					{

					}
				}
			}
		}

		//
		// The Poll method simply adds a query to the Send queue
		//
		private void Poll()
		{
			while (m_RunThreads)
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
	}
}
