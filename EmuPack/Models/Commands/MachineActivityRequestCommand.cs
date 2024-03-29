﻿using EmuPack.Models.Machine;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmuPack.Models.Commands
{
    public class MachineActivityRequestCommand : Command
    {
        public string DataStartNotification { get; private set; }
        public string DrawerStatus { get; private set; }
        public string ClearingWarningsInitiated { get; private set; }

        public MachineActivityRequestCommand(string commandString) : base(commandString)
        {
            DataStartNotification = ReadCommandField(commandString,
                MachineActivityRequestValues.DataStartNotificationStartIndex,
                MachineActivityRequestValues.DataStartNotificationLength);
            DrawerStatus = ReadCommandField(commandString,
                MachineActivityRequestValues.DrawerStatusStartIndex,
                MachineActivityRequestValues.DrawerStatusLength);
            ClearingWarningsInitiated = ReadCommandField(commandString,
                MachineActivityRequestValues.ClearingWarningsInitiatedStartIndex,
                MachineActivityRequestValues.ClearingWarningsInitiatedLength);

            string command = CommandId + SendFrom + SendTo + DataLength
            + DataStartNotification + DrawerStatus + ClearingWarningsInitiated;
            IsCommandValid = ValidateCommand(commandString);
        }

        protected override bool ValidateCommand(string commandString)
        {
            if (!base.ValidateCommand(commandString))
                return false;
            if (DataStartNotification != MachineActivityRequestValues.DataStartNotification)
                return false;
            if (!MachineActivityRequestValues.DrawerStatusPossibleValues.Contains(DrawerStatus))
                return false;
            if (!MachineActivityRequestValues.ClearingWarningsInitiatedPossibleValues
                .Contains(ClearingWarningsInitiated))
                return false;

            return true;
        }

        public override async Task<CommandResponse> ExecuteAsync(MachineState machineState)
        {
            if (!IsCommandValid)
                return new MachineActivityRequestCommandResponse(CommandResponseCodes.WrongCommandFormat);
            await DelayCommand(MachineActivityRequestValues.ExecutionTime);

            if (DrawerStatus == MachineActivityRequestValues.DrawerStatusPossibleValues[0])
            {
                machineState.ChangeDrawerStatus(drawerLocked: true);
            }
            else
            {
                machineState.ChangeDrawerStatus(drawerLocked: false);
            }
            if (DrawerStatus == MachineActivityRequestValues.ClearingWarningsInitiatedPossibleValues[1])
            {
                machineState.ClearWarningCassettesIds();
            }

            return new MachineActivityRequestCommandResponse(CommandResponseCodes.Sucess);
        }
    }

    static class MachineActivityRequestValues
    {
        static public string CommandId { get; private set; }
        static public string DataStartNotification { get; private set; }
        static public int DataStartNotificationStartIndex { get; private set; }
        static public int DataStartNotificationLength { get; private set; }
        static public string[] DrawerStatusPossibleValues { get; private set; }
        static public int DrawerStatusStartIndex { get; private set; }
        static public int DrawerStatusLength { get; private set; }
        static public string[] ClearingWarningsInitiatedPossibleValues { get; private set; }
        static public int ClearingWarningsInitiatedStartIndex { get; private set; }
        static public int ClearingWarningsInitiatedLength { get; private set; }
        static public int ExecutionTime { get; private set; }

        static MachineActivityRequestValues()
        {
            CommandId = "MR";
            DataStartNotification = "D";
            DataStartNotificationLength = 1;
            DataStartNotificationStartIndex = 11;
            DrawerStatusPossibleValues = new string[] { "00", "01" };
            DrawerStatusStartIndex = 12;
            DrawerStatusLength = 2;
            ClearingWarningsInitiatedPossibleValues = new string[] { "00", "01" };
            ClearingWarningsInitiatedStartIndex = 14;
            ClearingWarningsInitiatedLength = 2;
            ExecutionTime = 0;
        }
    }

    public class MachineActivityRequestCommandResponse : CommandResponse
    {
        public MachineActivityRequestCommandResponse(CommandResponseCodes code) : base(code)
        {
            CommandId = MachineActivityRequestValues.CommandId;
            DataLength = "00002";
        }
    }
}
