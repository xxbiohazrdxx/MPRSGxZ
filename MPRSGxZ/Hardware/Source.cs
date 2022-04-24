using MPRSGxZ.Events;

namespace MPRSGxZ.Hardware
{
	public class Source
	{
		#region Software properties
		public int ID { get; internal set; }
		private string _Name;
		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				if (_Name != value)
				{
					_Name = value;
					SourceChangedEvent?.Invoke(new SourceChangedEventArgs(ID));
				}
			}
		}

		private bool _Enabled;
		public bool Enabled
		{
			get
			{
				return _Enabled;
			}
			set
			{
				if (_Enabled != value)
				{
					_Enabled = value;
					SourceChangedEvent?.Invoke(new SourceChangedEventArgs(ID));
				}
			}
		}
		#endregion

		private event SourceChangedEventHandler SourceChangedEvent;

		internal Source() { }

		internal Source(int ID, SourceChangedEventHandler SourceChangedEvent, string Name = null, bool Enabled = true) 
		{
			this.ID = ID;
			this.Name = Name ?? $"Source {ID}";
			this.Enabled = Enabled;
			this.SourceChangedEvent += SourceChangedEvent;
		}
	}
}