import { Injectable } from '@angular/core';
import axios, { AxiosResponse, AxiosError } from 'axios';
import { Observable, from } from 'rxjs';
import { map } from 'rxjs/operators';
import { Factura } from '../models';
import { FacturaDTO } from '../models';
import { HttpClient, HttpHeaders } from '@angular/common/http';

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
      axios
        .post<Factura>(
          `${this.apiUrl}?clienteId=${clienteId}&num=${numerofactura}`
        )
        .then((response: AxiosResponse<Factura>) => {
          observer.next(response.data);
          observer.complete();
        })
        .catch((error: AxiosError) => {
          observer.error(error);
        });
    });
  }
  getFacturaArchivo(identificador: number): Observable<Blob> {
    const url = `${this.apiUrl}/archivo?identificador=${identificador}`;

    // Realizar la solicitud GET utilizando Axios
    return new Observable((observer) => {
      axios.get(url, { responseType: 'blob' })
        .then((response) => {
          // Resolver la promesa con el blob de la respuesta
          observer.next(response.data);
          observer.complete();
        })
        .catch((error) => {
          // Manejar el error y notificar al observador
          observer.error(error);
        });
    });
  }

  async updateFactura(numFactura: number, id: number): Promise<void> {
    const url = `${this.apiUrl}/${numFactura}?id=${id}`;
    console.log(url);
    await axios.put(url);
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
