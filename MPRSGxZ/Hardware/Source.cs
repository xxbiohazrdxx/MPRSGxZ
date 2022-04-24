using MPRSGxZ.Events;

namespace MPRSGxZ.Hardware
{
	public class Source
	{
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
					SourceChanged?.Invoke(new SourceChangedEventArgs(ID));
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
					SourceChanged?.Invoke(new SourceChangedEventArgs(ID));
				}
			}
		}

		private event SourceChangedEvent SourceChanged;

		internal Source() { }

		internal Source(int ID, SourceChangedEvent SourceChanged, string Name = null, bool Enabled = true) 
		{
			this.ID = ID;
			this.Name = Name ?? $"Source {ID}";
			this.Enabled = Enabled;
			this.SourceChanged = SourceChanged;
		}

		internal void AttachEvents(SourceChangedEvent SourceChanged)
		{
			this.SourceChanged = SourceChanged;
		}
	}
}