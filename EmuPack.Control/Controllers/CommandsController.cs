using EmuPack.Control.DTOs;
using EmuPack.Control.Services;
using EmuPack.Control.Services.Operations;
using Microsoft.AspNetCore.Mvc;

namespace EmuPack.Control.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommandsController : ControllerBase
    {
        private readonly MachineClient _machineClient;
        private readonly InitializationOperationHandler _initHandler;
        private readonly DispensingOperationHandler _dispenseHandler;
        private readonly StatusOperationHandler _statusHandler;

        public CommandsController(MachineClient machineClient,
            InitializationOperationHandler initHandler,
            DispensingOperationHandler dispenseHandler,
            StatusOperationHandler statusHandler)
        {
            _machineClient = machineClient;
            _initHandler = initHandler;
            _dispenseHandler = dispenseHandler;
            _statusHandler = statusHandler;
        }

        [HttpPost("reinitialize")]
        public ActionResult ReinitializeMachine()
        {
            _initHandler.InitializeMachine();

            return Ok();
        }

        [HttpPost("dispense")]
        public ActionResult Dispense(DispensingOperationDTO dto)
        {
            _dispenseHandler.Dispense(dto);

            return Ok();
        }
    }
}
