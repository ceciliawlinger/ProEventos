import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EventosComponent } from './componentes/eventos/eventos.component';
import { DashboardComponent } from './componentes/dashboard/dashboard.component';
import { ContatosComponent } from './componentes/contatos/contatos.component';
import { PalestrantesComponent } from './componentes/palestrantes/palestrantes.component';
import { PerfilComponent } from './componentes/perfil/perfil.component';


const routes: Routes = [
  {path : 'eventos', component: EventosComponent},
  {path : 'dashboard', component: DashboardComponent},
  {path : 'contatos', component: ContatosComponent},
  {path : 'palestrantes', component: PalestrantesComponent},
  {path : 'perfil', component: PerfilComponent},
  {path : '', redirectTo: 'dashboard', pathMatch: 'full'},
  {path : '**', redirectTo: 'dashboard', pathMatch: 'full'},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
