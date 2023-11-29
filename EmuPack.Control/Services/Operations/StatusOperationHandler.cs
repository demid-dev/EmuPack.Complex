using EmuPack.Control.Models.Commands;
using System.Threading;

namespace EmuPack.Control.Services.Operations
{
    public class StatusOperationHandler
    {
        private readonly MachineClient _machineClient;

        public StatusOperationHandler(MachineClient machineClient)
        {
            _machineClient = machineClient;
        }

        public void UpdateMachineState()
        {
            RequestStatus();
            WaitUntilMachineStateUpdated();
        }

        private void RequestStatus()
        {
            StatusRequestCommand command = new StatusRequestCommand();
            _machineClient.SendCommand(command);
        }

        private void WaitUntilMachineStateUpdated()
        {
            Thread.Sleep(200);
        }
    }
}
