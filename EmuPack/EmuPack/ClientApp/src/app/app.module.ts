import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {DashboardComponent} from './pages/dashboard/dashboard.component';
import {MainLayoutComponent} from './shared/components/main-layout/main-layout.component';
import {HttpClientModule} from "@angular/common/http";
import { MachineStateComponent } from './shared/components/machine-state/machine-state.component';
import { MedicationNamePipe } from './shared/pipes/medication-name.pipe';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    MainLayoutComponent,
    MachineStateComponent,
    MedicationNamePipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
