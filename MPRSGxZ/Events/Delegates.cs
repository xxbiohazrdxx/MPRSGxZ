namespace MPRSGxZ.Events
{
	public delegate void ZoneChangedEventHandler(ZoneChangedEventArgs e);

	internal delegate void QueueCommandEventHandler(QueueCommandEventArgs e);
	internal delegate void ZonePollEventHandler(ZonePollEventArgs e);
	internal delegate void SettingChangedEventHandler();
}
