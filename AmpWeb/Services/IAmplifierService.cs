using MPRSGxZ.Hardware;

namespace AmpWeb.Services
{
	public interface IAmplifierService
	{
		Amplifier[] Amplifiers { get; }
		Source[] Sources { get; }
	}
}
