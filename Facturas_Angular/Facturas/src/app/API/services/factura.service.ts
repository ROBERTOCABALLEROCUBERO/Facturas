import { Injectable } from '@angular/core';
import axios, { AxiosResponse, AxiosError } from 'axios';
import { Observable, from } from 'rxjs';
import { map } from 'rxjs/operators';
import { Factura } from '../models';
import { FacturaDTO } from '../models';

@Injectable({
  providedIn: 'root',
})
export class FacturaService {
  private readonly apiUrl = 'https://localhost:7030/api/facturas';

  constructor() {}

  getAllFacturas(): Observable<Factura[]> {
    return from(axios.get<Factura[]>(`${this.apiUrl}`)).pipe(
      map((response: AxiosResponse<Factura[]>) => response.data)
    );
  }

  getFacturaById(id: number): Observable<Factura> {
    return from(axios.get<Factura>(`${this.apiUrl}/${id}`)).pipe(
      map((response: AxiosResponse<Factura>) => response.data)
    );
  }

 
crearFactura(clienteId: number, numerofactura: number): Observable<Factura> {
  return new Observable<Factura>((observer) => {
    console.log(`${this.apiUrl}?clienteId=${clienteId}&num=${numerofactura}`);
    axios.post<Factura>(`${this.apiUrl}?clienteId=${clienteId}&num=${numerofactura}`)
      .then((response: AxiosResponse<Factura>) => {
        observer.next(response.data);
        observer.complete();
      })
      .catch((error: AxiosError) => {
        observer.error(error);
      });
  });
}
  
  updateFactura(id: number, factura: Factura): Observable<void> {
    return from(axios.put<void>(`${this.apiUrl}/${id}`, factura)).pipe(
      map(() => {})
    );
  }

  deleteFactura(id: number): Observable<void> {
    return from(axios.delete<void>(`${this.apiUrl}/${id}`)).pipe(map(() => {}));
  }

  getFacturasByClienteId(clienteId: number): Observable<Factura[]> {
    return from(
      axios.get<Factura[]>(`${this.apiUrl}/cliente/${clienteId}`)
    ).pipe(map((response: AxiosResponse<Factura[]>) => response.data));
  }

  public generarNumeroFacturaUnico(): Promise<number> {
    return axios
      .get<number>(`${this.apiUrl}/cliente/numero`)
      .then((response) => response.data);
  }

  public addFactura(clienteId: number, num: number): Observable<any> {
    const facturaDto: FacturaDTO = {
      fecha: new Date(),
      numeroFactura: num,
      clienteID: clienteId,
    };

    return new Observable((observer) => {
      axios
        .post(`${this.apiUrl}/add`, null, { params: { clienteId, num } })
        .then((response) => {
          observer.next(response.data);
          observer.complete();
        })
        .catch((error) => {
          observer.error(error);
        });
    });
  }
}
