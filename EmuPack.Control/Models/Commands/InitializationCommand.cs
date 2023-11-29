namespace EmuPack.Control.Models.Commands
{
    public class InitializationCommand : Command
    {
        public InitializationCommand()
        {
            CommandID = InitializationCommandValues.CommandId;
            DataLength = NormalizeCommandField(0,
                CommandValues.DataLengthLength,
                CommandPadding.Zeroing);
            CommandString = FormCommand();
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
}
