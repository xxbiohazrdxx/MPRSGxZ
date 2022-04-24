using MPRSGxZ.Hardware;

namespace WebAmp.Services
{
	public interface IAmplifierService
	{
		Amplifier[] Amplifiers { get; }
		Source[] Sources { get; }
	}
}
