using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmuPack.Models.Machine
{
    public class MachineState
    {
        public bool DrawerLocked { get; private set; }
        public Adaptor Adaptor { get; private set; }
        public List<RegistredPrescription> RegistredPrescriptions { get; private set; }
        public List<string> WarningCassettesIds { get; private set; }

        public MachineState()
        {
            Adaptor = new Adaptor();
            RegistredPrescriptions = new List<RegistredPrescription>();
            WarningCassettesIds = new List<string>();
            DrawerLocked = false;
        }

        public void ReinitilizeState()
        {
            RegistredPrescriptions = new List<RegistredPrescription>();
            DrawerLocked = false;
            WarningCassettesIds = new List<string>();
        }

        public void ChangeDrawerStatus(bool drawerLocked)
        {
            DrawerLocked = drawerLocked;
        }

        public void ClearWarningCassettesIds()
        {
            WarningCassettesIds = new List<string>();
        }
    }
}
