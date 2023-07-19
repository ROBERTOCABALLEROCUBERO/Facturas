/* tslint:disable */
/* eslint-disable */
import { Factura } from './factura';
export interface LineaFactura {
  concepto?: null | string;
  factura?: Factura;
  facturaId?: number;
  importe?: number;
  lineaFacturaId?: number;
  precio?: number;
  unidades?: number;
}
