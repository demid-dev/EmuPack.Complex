﻿using EmuPack.Models.Machine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuPack.Models.Commands
{
    public class StatusRequestCommand : Command
    {
        public StatusRequestCommand(string commandString) : base(commandString)
        {
            IsCommandValid = ValidateCommand(commandString);
        }

        protected override bool ValidateCommand(string commandString)
        {
            if (!base.ValidateCommand(commandString))
                return false;
            if (CommandId != StatusRequestCommandValues.CommandId)
                return false;

            return true;
        }

        public override async Task<CommandResponse> ExecuteAsync(MachineState machineState)
        {
            if (!IsCommandValid)
            {
                return new StatusRequestCommandResposne(CommandResponseCodes.WrongCommandFormat,
                    machineState);
            }
            await DelayCommand(StatusRequestCommandValues.ExecutionTime);

            return new StatusRequestCommandResposne(CommandResponseCodes.Sucess,
                machineState);
        }
    }

    static class StatusRequestCommandValues
    {
        static public string CommandId { get; private set; }
        static public int ExecutionTime { get; private set; }

        static StatusRequestCommandValues()
        {
            CommandId = "SR";
            ExecutionTime = 0;
        }
    }

    public class StatusRequestCommandResposne : CommandResponse
    {
        public string DrawerStatus { get; private set; }
        public string AdaptorStatus { get; private set; }
        public string RegistredPrescriptionsQuantity { get; private set; }
        public string RegistredPrescriptionsIds { get; private set; }
        public string WarningCassettesQuantity { get; private set; }
        public string WarningCassettesIds { get; private set; }

        public StatusRequestCommandResposne(CommandResponseCodes code,
            MachineState machineState) : base(code)
        {
            CommandId = StatusRequestCommandValues.CommandId;
            if (code == CommandResponseCodes.WrongCommandFormat)
            {
                DataLength = StatusRequestCommandResposneValues.DataLengthWrong;
                return;
            }
            DrawerStatus = Convert.ToInt32(!machineState.DrawerLocked).ToString();
            AdaptorStatus = Convert.ToInt32(machineState.Adaptor.AdaptorInsideMachine).ToString();
            RegistredPrescriptionsQuantity = PadWithZeroes(machineState.RegistredPrescriptions.Count.ToString(),
                StatusRequestCommandResposneValues.RegistredPrescriptionsQuantityLength);

            RegistredPrescriptionsIds = string.Join("",
                machineState.RegistredPrescriptions
                    .Select(pr =>
                        {
                            return PadWithZeroes(pr.Id.ToString(),
                                PrescriptionRegistrationCommandValues.PrescriptionIdLength);
                        }));

            WarningCassettesQuantity = PadWithZeroes(machineState.WarningCassettesIds.Count.ToString(),
                StatusRequestCommandResposneValues.WarningCassettesQuantityLength);
            WarningCassettesIds = string.Join("", machineState.WarningCassettesIds);
            Response = FormResponseData();
        }

        private string FormResponseData()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(DrawerStatus);
            stringBuilder.Append(AdaptorStatus);
            stringBuilder.Append(RegistredPrescriptionsQuantity);
            stringBuilder.Append(RegistredPrescriptionsIds);
            stringBuilder.Append(WarningCassettesQuantity);
            stringBuilder.Append(WarningCassettesIds);
            DataLength = PadWithZeroes((stringBuilder.Length + ResponseCode.Length).ToString(),
                CommandValues.DataLengthLength);

            return Response + stringBuilder.ToString();
        }

        private string PadWithZeroes(string fieldToPad, int fieldLength)
        {
            string paddedField = fieldToPad;

            for (int i = 0; i < fieldLength - fieldToPad.Length; i++)
            {
                paddedField = '0' + paddedField;
            }

            return paddedField;
        }

        static class StatusRequestCommandResposneValues
        {
            static public string DataLengthWrong { get; private set; }
            static public int RegistredPrescriptionsQuantityLength { get; private set; }
            static public int WarningCassettesQuantityLength { get; private set; }

            static StatusRequestCommandResposneValues()
            {
                RegistredPrescriptionsQuantityLength = 2;
                WarningCassettesQuantityLength = 2;
                DataLengthWrong = "00002";
            }
        }
    }
}
