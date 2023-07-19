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
  async descarga(numeroFactura: number) {
    try {
      const blob = await this.facturaService.getFacturaArchivo(numeroFactura).toPromise();
  
      // Verificar si el blob es válido antes de continuar
      if (!blob) {
        console.log('No se encontró la factura para el número de factura:', numeroFactura);
        return;
      }
  
      const fileName = `factura_${numeroFactura}.pdf`;
  
      // Crear un objeto URL a partir del blob
      const url = window.URL.createObjectURL(blob);
  
      // Crear un enlace temporal para descargar el archivo
      const link = document.createElement('a');
      link.href = url;
      link.download = fileName;
      link.target = '_blank';
      link.click();
  
      // Liberar el objeto URL
      window.URL.revokeObjectURL(url);
  
      console.log('Factura descargada con éxito');
    } catch (error) {
      console.log('Error al descargar la factura:', error);
    }
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
