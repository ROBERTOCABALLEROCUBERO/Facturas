import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ClienteComponent } from './cliente/cliente.component';
import { InicioComponent } from './inicio/inicio.component';
import { CrearfacturaComponent } from './crearfactura/crearfactura.component';
import { EditarfacturaComponent } from './editarfactura/editarfactura.component';

@NgModule({
  declarations: [
    AppComponent,
    ClienteComponent,
    InicioComponent,
    CrearfacturaComponent,
    EditarfacturaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
