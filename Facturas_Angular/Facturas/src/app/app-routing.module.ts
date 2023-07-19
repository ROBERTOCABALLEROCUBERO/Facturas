import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClienteComponent } from './cliente/cliente.component';
import { InicioComponent } from './inicio/inicio.component';
import { CrearfacturaComponent } from './crearfactura/crearfactura.component';

const routes: Routes = [ { path: '', component: InicioComponent },{ path: 'cliente', component: ClienteComponent } ,{ path: 'crear', component: CrearfacturaComponent }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
