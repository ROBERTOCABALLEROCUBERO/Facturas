/* tslint:disable */
/* eslint-disable */
import { Cliente } from './cliente';
import { LineaFactura } from './linea-factura';
export interface Factura {
  cliente?: Cliente;
  clienteID?: number;
  fecha?: string;
  lineasFactura?: null | Array<LineaFactura>;
  numeroFactura?: number;
  totalFactura?: number;
}
