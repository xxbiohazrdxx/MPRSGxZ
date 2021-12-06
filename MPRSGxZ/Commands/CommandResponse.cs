namespace MPRSGxZ.Commands
{
	internal class CommandResponse
	{
		public int AmpID			{ get; private set; }
		public int ZoneID			{ get; private set; }
		public bool PublicAddress	{ get; private set; }
		public bool Power			{ get; private set; }
		public bool Mute			{ get; private set; }
		public bool DoNotDisturb	{ get; private set; }
		public int Volume			{ get; private set; }
		public int Treble			{ get; private set; }
		public int Bass				{ get; private set; }
		public int Balance			{ get; private set; }
		public int Source			{ get; private set; }

		internal CommandResponse(string Response)
		{
			AmpID			= int.Parse(Response.Substring(0, 1));
			ZoneID			= int.Parse(Response.Substring(1, 1));
			PublicAddress	= int.Parse(Response.Substring(2, 2)) == 1 ? true : false;
			Power			= int.Parse(Response.Substring(4, 2)) == 1 ? true : false;
			Mute			= int.Parse(Response.Substring(6, 2)) == 1 ? true : false;
			DoNotDisturb	= int.Parse(Response.Substring(8, 2)) == 1 ? true : false;
			Volume			= int.Parse(Response.Substring(10, 2));
			Treble			= int.Parse(Response.Substring(12, 2));
			Bass			= int.Parse(Response.Substring(14, 2));
			Balance			= int.Parse(Response.Substring(16, 2));
			Source			= int.Parse(Response.Substring(18, 2));
		}

		public static implicit operator string(CommandResponse Response)
		{
			return $"{Response.AmpID:D1}{Response.ZoneID:D1}{(Response.PublicAddress ? 1 : 0):D2}{(Response.Power ? 1 : 0):D2}{(Response.Mute ? 1 : 0):D2}{(Response.DoNotDisturb ? 1 : 0):D2}{Response.Volume:D2}{Response.Treble:D2}{Response.Bass:D2}{Response.Balance:D2}{Response.Source:D2}";
		}
	}
}