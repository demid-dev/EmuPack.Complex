import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {Adaptor, DrugCell, DrugPack, MachineState, RegistredPrescription} from "../../models/machine-state";
import {MachineStateService} from "../../services/machine-state.service";

@Component({
  selector: 'app-machine-state',
  templateUrl: './machine-state.component.html',
  styleUrls: ['./machine-state.component.scss']
})
export class MachineStateComponent implements OnInit {
  // @ts-ignore
  public machineState: MachineState = new class implements MachineState {
    adaptor: Adaptor = new class implements Adaptor {
      adaptorInsideMachine: boolean = false;
      drugPack: DrugPack = new class implements DrugPack {
        drugCells: DrugCell[] = [];
      };
    };
    drawerLocked: boolean = false;
    registredPrescriptions: RegistredPrescription[] = [];
    warningCassettesIds: string[] = [];
  };
  // @ts-ignore
  public selectedPrescription: RegistredPrescription | undefined;
  // @ts-ignore
  @ViewChild('prescriptionModalButton') prescriptionModalButton: ElementRef<HTMLElement>;
  // @ts-ignore
  public machineStateRefreshedTime: string;

  constructor(private machineStateService: MachineStateService) {

  }

  ngOnInit(): void {
    this.refreshMachineState();
  }

  refreshMachineState(): void {
    this.machineStateService.getMachineState().subscribe(state => {
      console.log(state);
      this.machineState = state;
    });

    this.machineStateRefreshedTime = Date.now().toString();
  }

  getPrescriptionDetails(prescriptionId: number): void {
    this.selectedPrescription = this.machineState.registredPrescriptions
      .find(prescription => prescription.id == prescriptionId);

    this.openPrescriptionModal();
  }

  openPrescriptionModal(): void {
    this.prescriptionModalButton.nativeElement.click();
  }

  changeAdaptorState(): void {
    this.machineStateService.changeAdaptorState().subscribe(state => {
      this.machineState = state;
    });
  }


  clearDrugPack(): void {
    this.machineStateService.clearDrugPack().subscribe(state => {
      this.machineState = state;
    });
  }
}
