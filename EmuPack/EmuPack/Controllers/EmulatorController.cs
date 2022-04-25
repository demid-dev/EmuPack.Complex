using EmuPack.Models.Machine;
using EmuPack.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmuPack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmulatorController : ControllerBase
    {
        private readonly ILogger<EmulatorController> _logger;
        private readonly EmulatedMachine _emulatedMachine;

        public EmulatorController(ILogger<EmulatorController> logger,
            EmulatedMachine emulatedMachine)
        {
            _logger = logger;
            _emulatedMachine = emulatedMachine;
        }

        [HttpGet]
        public ActionResult<MachineState> GetMachineState()
        {
            return Ok(_emulatedMachine.MachineState);
        }

        [HttpGet("prescriptions")]
        public ActionResult<IEnumerable<RegistredPrescription>> GetAllRegistredPrescriptions()
        {
            return Ok(_emulatedMachine.MachineState.RegistredPrescriptions);
        }

        [HttpGet("adaptor")]
        public ActionResult<Adaptor> GetAdaptorInformation()
        {
            return Ok(_emulatedMachine.MachineState.Adaptor);
        }

        [HttpPost("adaptor")]
        public ActionResult ChangeAdaptorState()
        {
            if (_emulatedMachine.MachineState.DrawerLocked)
            {
                return BadRequest("Adaptor state cannot be changed, machine drawer closed");
            }
            _emulatedMachine.MachineState.Adaptor.ChangeAdaptorState();
            return Ok(_emulatedMachine.MachineState);
        }

        [HttpPost("pack")]
        public ActionResult ChangeDrugPack()
        {
            if (_emulatedMachine.MachineState.DrawerLocked)
            {
                return BadRequest("Drug pack cannot be cleared, machine drawer closed");
            }
            _emulatedMachine.MachineState.Adaptor.ClearDrugPack();
            return Ok(_emulatedMachine.MachineState);
        }
    }
}
