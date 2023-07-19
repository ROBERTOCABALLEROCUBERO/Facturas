import { Component } from '@angular/core';
import { Cliente } from '../API/models';
import { ClienteService } from '../API/services';
import { Router } from '@angular/router';
import { DatosCompartidosService } from '../API/services/datos.compartidos.service';

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.css']
})
export class InicioComponent {
  fechaActual = new Date();
  router = new Router;
  mostrarFormulario = false;
    cliente: Cliente = {
      codigoPostal: null,
      domicilio: null,
      fechaAlta: this.fechaActual,
      id: 0,
      nif: null,
      nombre: null,
      pais: null,
      poblacion: null,
      provincia: null
    };
  
    constructor(private clienteService: ClienteService, private routerService: Router, private datoscomp: DatosCompartidosService) {}
  
    async crearCliente(): Promise<void> {
      await this.clienteService.createCliente(this.cliente);
    }
  
    async obtenerClientePorId(id: number): Promise<void> {
      const cliente = await this.clienteService.getClienteById(id);
      if (cliente) {
        localStorage.setItem("id", cliente.id?.toString() ?? "");
        this.routerService.navigate(['/cliente']);
      }


    }

    toggleMostrarFormulario(): void {
      this.mostrarFormulario = !this.mostrarFormulario;
    }
  
}
