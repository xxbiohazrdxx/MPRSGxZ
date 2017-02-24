using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPRSGxZ.Events
{
	internal class QueueCommandEventArgs : EventArgs
	{
		private int m_AmpID;
		private int m_ZoneID;
		private Command m_Command;
		private int m_Value;

		public QueueCommandEventArgs(int AmpID, int ZoneID, Command Command, int value)
		{
			m_AmpID = AmpID;
			m_ZoneID = ZoneID;
			m_Command = Command;
			m_Value = value;
		}

		public int AmpID
		{
			get
			{
				return m_AmpID;
			}
		}

		public int ZoneID
		{
			get
			{
				return m_ZoneID;
			}
		}

		public Command Command
		{
			get
			{
				return m_Command;
			}
		}

		public int Value
		{
			get
			{
				return m_Value;
			}
		}
	}
}
