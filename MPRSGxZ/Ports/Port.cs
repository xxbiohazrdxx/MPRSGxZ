using MPRSGxZ.Commands;

namespace MPRSGxZ.Ports
{
    internal interface Port
    {
        void Open();
        void Close();
        CommandResponse[] ExecuteCommand(Command CommandToExecute);
    }
}
