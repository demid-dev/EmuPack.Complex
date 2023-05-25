import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {DashboardComponent} from "./pages/dashboard/dashboard.component";
import {MainLayoutComponent} from "./shared/components/main-layout/main-layout.component";

const routes: Routes = [{
  path: '', component: MainLayoutComponent, children:[
    {path:'', redirectTo:'/dashboard', pathMatch:'full'},
    {path: 'dashboard', component: DashboardComponent}
  ]
}];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
