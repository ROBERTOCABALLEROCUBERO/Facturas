import { Injectable } from '@angular/core';
import { Cliente } from '../models';

@Injectable({
  providedIn: 'root'
})
export class DatosCompartidosService {
  cliente: Cliente = {} ;

  setCliente(cliente: Cliente) {
    this.cliente = cliente;
  }
}