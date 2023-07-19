import { LineaFactura } from '../API/models';
import { LineaFacturaService } from '../API/services';
import { Component } from '@angular/core';
import { FacturaService } from '../API/services';
import { Factura } from '../API/models';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-crearfactura',
  templateUrl: './crearfactura.component.html',
  styleUrls: ['./crearfactura.component.css'],
})
export class CrearfacturaComponent {
  nuevaLineaFactura: LineaFactura = {}; // Asegúrate de que LineaFactura tenga los atributos correctos
  numeroFactura: number = 0;
  LineaFacturaColl: LineaFactura[] = [];

  constructor(
    private lineaService: LineaFacturaService,
    private facturaService: FacturaService
  ) {
    this.generarNumeroFactura();
  }

  generarNumeroFactura() {
    this.facturaService
      .generarNumeroFacturaUnico()
      .then((numero) => {
        this.numeroFactura = numero;
      })
      .catch((error) => {
        console.log(error);
      });
  }

  agregarLineaFactura(): void {
    const nuevaLineaFactura = Object.assign({}, this.nuevaLineaFactura); // Crear una copia del objeto
    nuevaLineaFactura.facturaId = this.numeroFactura;
    this.LineaFacturaColl.push(nuevaLineaFactura);
    console.log(nuevaLineaFactura);
    this.nuevaLineaFactura = {};
    console.log( this.LineaFacturaColl); // Reiniciar el objeto para borrar los campos del formulario
  }

  async crearFactura(): Promise<void> {
    const clienteId = Number(localStorage.getItem('id'));
  
    try {
      await this.facturaService.addFactura(clienteId, this.numeroFactura).toPromise();
      console.log('Factura creada' + this.numeroFactura +"  "+ clienteId);
  
      for (const lineaFactura of this.LineaFacturaColl) {
        await this.lineaService.addLineaFactura(lineaFactura);
        console.log("Se ha añadido" + lineaFactura);
      }
  console.log("Este es el numero de factura otra vez" + this.numeroFactura);
      const factura = await this.facturaService.crearFactura(clienteId, this.numeroFactura).toPromise();
      console.log("Este es el numero de factura otra vez    " + this.numeroFactura);
      console.log('Factura creada:', factura);
  
      this.LineaFacturaColl = [];
    } catch (error) {
      console.log('Error al crear la factura:', error);
    }
  }
  
  
}
