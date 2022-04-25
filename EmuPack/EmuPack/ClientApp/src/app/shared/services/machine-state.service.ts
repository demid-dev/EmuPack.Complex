import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {MachineState} from "../models/machine-state";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class MachineStateService {

  constructor(private http: HttpClient) {
  }

  public getMachineState(): Observable<MachineState> {
    return this.http.get<MachineState>(`${environment.api}/Emulator`);
  }

  public changeAdaptorState(): Observable<MachineState> {
    return this.http.post<MachineState>(`${environment.api}/Emulator/adaptor`, null);
  }

  public clearDrugPack(): Observable<MachineState> {
    return this.http.post<MachineState>(`${environment.api}/Emulator/pack`, null);
  }
}
