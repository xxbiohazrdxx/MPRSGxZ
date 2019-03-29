using MPRSGxZ.Hardware;

namespace AmpAPI.Services
{
	public interface IAmplifierService
	{
		Amplifier[] Amplifiers { get; }
		Source[] Sources { get; }
	}
}
