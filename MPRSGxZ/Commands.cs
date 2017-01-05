using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPRSGxZ
{
	public class Command
	{
		private string m_CommandString;
		private int m_MinValue;
		private int m_MaxValue;

		public static explicit operator string(Command Command)
		{
			return Command.m_CommandString;
		}

		public string CommandString
		{
			get
			{
				return m_CommandString;
			}
		}

		public int MinValue
		{
			get
			{
				return m_MinValue;
			}
		}

		public int MaxValue
		{
			get
			{
				return m_MaxValue;
			}
		}

		private Command(string CommandString, int MinValue, int MaxValue)
		{
			m_CommandString = CommandString;
			m_MinValue = MinValue;
			m_MaxValue = MaxValue;
		}

		public static Command Power { get; } = new Command(@"PR", 0, 1);
		public static Command Mute { get; } = new Command(@"MU", 0, 1);
		public static Command Volume { get; } = new Command(@"VO", 0, 38);
		public static Command Treble { get; } = new Command(@"TR", 0, 14);
		public static Command Bass { get; } = new Command(@"BS", 0, 14);
		public static Command Balance { get; } = new Command(@"BL", 0, 14);
		public static Command Source { get; } = new Command(@"CH", 1, 6);
		public static Command PublicAddress { get; } = new Command(@"PA", 0, 1);
		public static Command DoNotDisturb { get; } = new Command(@"DT", 0, 1);
	}
}
