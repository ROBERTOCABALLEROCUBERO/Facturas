import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClienteComponent } from './cliente/cliente.component';
import { InicioComponent } from './inicio/inicio.component';
import { CrearfacturaComponent } from './crearfactura/crearfactura.component';
import { EditarfacturaComponent } from './editarfactura/editarfactura.component';

const routes: Routes = [ { path: '', component: InicioComponent },{ path: 'cliente', component: ClienteComponent } ,{ path: 'crear', component: CrearfacturaComponent },
{ path: 'editar/:numeroFactura', component: EditarfacturaComponent }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
