using System.Collections.Generic;

namespace EmuPack.Models.Machine
{
    public class RegistredPrescription
    {
        public int Id { get; private set; }
        public List<Cassette> RegistredCassettes { get; private set; }

        public RegistredPrescription(int id)
        {
            Id = id;
            RegistredCassettes = new List<Cassette>();
        }
    }
}
