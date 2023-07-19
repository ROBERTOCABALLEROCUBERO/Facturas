import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LineaFactura } from '../API/models';
import { FacturaService, LineaFacturaService } from '../API/services';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-editarfactura',
  templateUrl: './editarfactura.component.html',
  styleUrls: ['./editarfactura.component.css']
})
export class EditarfacturaComponent implements OnInit {
  lineasFactura: LineaFactura[] = [];
  lineaFacturaForms: FormGroup[] = [];
  numeroFactura: number = 0;
  lineasFacturaActualizadas: LineaFactura[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private lineaFacturaService: LineaFacturaService,
    private route: ActivatedRoute,
    private facturaservice: FacturaService

  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.numeroFactura = Number(params.get('numeroFactura'));
      console.log(this.numeroFactura);
    });

    this.loadLineasFactura();
  }

  private loadLineasFactura(): void {
    this.lineaFacturaService.getLineasFactura(this.numeroFactura).subscribe(
      (lineasFactura: LineaFactura[]) => {
        this.lineasFactura = lineasFactura;
        console.log(this.lineasFactura);
        this.fillLineaFacturaForms();
      },
      (error) => {
        console.log('Error al obtener las líneas de factura:', error);
      }
    );
  }

  private fillLineaFacturaForms(): void {
    this.lineaFacturaForms = this.lineasFactura.map((lineaFactura) =>
      this.formBuilder.group({
        lineaFacturaId: [lineaFactura?.lineaFacturaId],
        concepto: [lineaFactura?.concepto, Validators.required],
        unidades: [lineaFactura?.unidades, Validators.required],
        precio: [lineaFactura?.precio, Validators.required],
      })
    );
    console.log(this.lineaFacturaForms);
  }

  getLineaFacturaForm(index: number): FormGroup {
    return this.lineaFacturaForms[index];
  }

  onSubmit(index: number): void {
    const lineaFacturaForm = this.getLineaFacturaForm(index);
    const lineaFacturaId = lineaFacturaForm?.get('lineaFacturaId')?.value;
    const concepto = lineaFacturaForm?.get('concepto')?.value;
    const unidades = lineaFacturaForm?.get('unidades')?.value;
    const precio = lineaFacturaForm?.get('precio')?.value;

    const lineaFactura: LineaFactura = {
      lineaFacturaId: lineaFacturaId ?? 0, // Asigna 0 como valor predeterminado si lineaFacturaId es undefined
      concepto: concepto,
      unidades: unidades,
      precio: precio
    };

    this.lineasFacturaActualizadas.push(lineaFactura);
  }

  async actualizarFactura(): Promise<void> {
    try {
      // Guardar los cambios en la base de datos
      for (const lineaFactura of this.lineasFacturaActualizadas) {
        await this.lineaFacturaService.updateLineaFactura(lineaFactura.lineaFacturaId, lineaFactura);
      }
      const idCliente = Number (localStorage.getItem('id'));
      // Actualizar la factura en el servicio
      await this.facturaservice.updateFactura(this.numeroFactura, idCliente);
  
      console.log('Factura actualizada con éxito');
      // Realizar acciones adicionales después de actualizar la factura
    } catch (error) {
      console.log('Error al actualizar la factura:', error);
    }
  }
}