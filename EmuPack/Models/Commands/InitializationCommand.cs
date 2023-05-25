using EmuPack.Models.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuPack.Models.Commands
{
    public class InitializationCommand : Command
    {
        public InitializationCommand(string commandString) : base(commandString)
        {
            IsCommandValid = ValidateCommand(commandString);
        }

        protected override bool ValidateCommand(string commandString)
        {
            if (!base.ValidateCommand(commandString))
                return false;
            if (CommandId != InitializationCommandValues.CommandId)
                return false;

            return true;
        }

        public override CommandResponse Execute(MachineState machineState)
        {
            if (!IsCommandValid)
            {
                return new InitialiaztionCommandResposne(CommandResponseCodes.WrongCommandFormat);
            }

            machineState.ReinitilizeState();
            return new InitialiaztionCommandResposne(CommandResponseCodes.Sucess);
        }
    }

    static class InitializationCommandValues
    {
        static public string CommandId { get; private set; }

        static InitializationCommandValues()
        {
            CommandId = "IN";
        }
    }

    public class InitialiaztionCommandResposne : CommandResponse
    {
        public InitialiaztionCommandResposne(CommandResponseCodes code) : base(code)
        {
            CommandId = InitializationCommandValues.CommandId;
            DataLength = "00002";
        }
    }
}
