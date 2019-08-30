using MPRSGxZ.Commands;

namespace MPRSGxZ.Ports
{
    internal interface IPort
    {
        void Open();
        void Close();
        CommandResponse[] ExecuteCommand(Command CommandToExecute);
    }
}
