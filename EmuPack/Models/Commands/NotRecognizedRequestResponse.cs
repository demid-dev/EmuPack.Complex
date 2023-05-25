using EmuPack.Models.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuPack.Models.Commands
{
    public class NotRecognizedRequestResponse : CommandResponse
    {
        public NotRecognizedRequestResponse(CommandResponseCodes code =
            CommandResponseCodes.WrongCommandFormat) : base(code)
        {
            DataLength = NotRecognizedRequestResponseValues.DataLength;
            CommandId = NotRecognizedRequestResponseValues.CommandId;
        }
    }

    public static class NotRecognizedRequestResponseValues
    {
        static public string CommandId { get; private set; }
        static public string DataLength { get; private set; }
        static public string ResponseCode { get; private set; }

        static NotRecognizedRequestResponseValues()
        {
            CommandId = "NR";
            DataLength = "00002";
            ResponseCode = "00";
        }
    }
}
