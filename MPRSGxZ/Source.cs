using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MPRSGxZ
{
	[DataContract]
	public class Source
	{
		private int m_ID;
		private string m_Name;
		private bool m_Enabled;

		/// <summary>
		/// Default blank constructor used for deserialization
		/// </summary>
		internal Source()
		{
		}

		/// <summary>
		/// Default constructor for missing configuration
		/// </summary>
		/// <param name="ID">The ID of the source</param>
		internal Source(int ID)
		{
			m_ID = ID;
			m_Name = string.Format(@"Source {0}", m_ID);
			m_Enabled = false;
		}

		[DataMember]
		public int ID
		{
			get
			{
				return m_ID;
			}
			internal set
			{
				m_ID = value;
			}
		}

		[DataMember]
		public string Name
		{
			get
			{
				return m_Name;
			}
			set
			{
				m_Name = value;
			}
		}

		[DataMember]
		public bool Enabled
		{
			get
			{
				return m_Enabled;
			}
			set
			{
				m_Enabled = value;
			}
		}
	}
}