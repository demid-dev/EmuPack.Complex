export interface MachineState {
  drawerLocked: boolean;
  adaptor: Adaptor;
  registredPrescriptions: RegistredPrescription[];
  warningCassettesIds: string[];
}

export interface Adaptor {
  adaptorInsideMachine: boolean;
  drugPack: DrugPack;
}

export interface RegistredPrescription {
  id: number;
  registredCassettes: Cassette[];
}

export interface Cassette {
  cassetteId: number;
  drugName: string;
  drugQuantity: number;
}

export interface DrugPack {
  drugCells: DrugCell[];
}

export interface DrugCell {
  cellName: string;
  drugsInCell: DrugInCell[];
}

export interface DrugInCell {
  drugName: string;
  drugQuantity: number;
}


