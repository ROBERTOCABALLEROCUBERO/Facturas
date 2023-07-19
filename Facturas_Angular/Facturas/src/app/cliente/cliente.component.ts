import { Component, OnInit } from '@angular/core';
import { Cliente } from '../API/models';
import { DatosCompartidosService } from '../API/services/datos.compartidos.service';
import { ClienteService } from '../API/services/cliente.service';
import { Factura } from '../API/models/factura';
import { FacturaService } from '../API/services/factura.service';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnInit {
  cliente: Cliente = {};
  facturas: Factura[] = [];

  constructor(private clienteService: ClienteService, private facturaService: FacturaService, private datoscomp: DatosCompartidosService) {}

  ngOnInit(): void {
    const idCliente = localStorage.getItem('id');
    if (idCliente) {
      const id = Number(idCliente);
      this.obtenerClientePorId(id);
      this.obtenerFacturasPorClienteId(id);
    }
  }

  async obtenerClientePorId(id: number): Promise<void> {
    this.cliente = await this.clienteService.getClienteById(id);
  }
  descarga(numeroFactura: number) {
    // Aquí puedes agregar tu lógica para descargar el archivo correspondiente al número de factura
    // Puedes utilizar el número de factura como necesites, como en el nombre del archivo o en una solicitud a tu servidor

    // Ejemplo de cómo mostrar el número de factura en la consola
    console.log('Número de factura:', numeroFactura);
  }
  obtenerFacturasPorClienteId(clienteId: number): void {
    this.facturaService.getFacturasByClienteId(clienteId).subscribe(
      facturas => {
        this.facturas = facturas;
        console.log(facturas);
      },
      error => {
        console.log('Error al obtener las facturas:', error);
      }
    );
  }
}
