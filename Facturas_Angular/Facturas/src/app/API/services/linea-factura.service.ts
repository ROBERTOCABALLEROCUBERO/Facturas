import axios from 'axios';
import { LineaFactura } from '../models';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LineaFacturaService {
  private apiUrl = 'https://localhost:7030/api'; // Reemplaza con la URL de tu API

  constructor() { }

  public getAllLineasFactura(): Promise<LineaFactura[]> {
    return axios.get<LineaFactura[]>(`${this.apiUrl}/LineaFactura`)
      .then(response => response.data);
  }

  public getLineaFacturaById(id: number): Promise<LineaFactura> {
    return axios.get<LineaFactura>(`${this.apiUrl}/LineaFactura/${id}`)
      .then(response => response.data);
  }

  public addLineaFactura(lineaFactura: LineaFactura): Promise<void> {
    return axios.post(`${this.apiUrl}/LineaFactura`, lineaFactura);
  }

  public updateLineaFactura(id: number | undefined, lineaFactura: LineaFactura): Promise<void> {
    return axios.put(`${this.apiUrl}/LineaFactura/${id}`, lineaFactura);
  }

  public deleteLineaFactura(id: number): Promise<void> {
    return axios.delete(`${this.apiUrl}/${id}`);
  }

  
 public getLineasFactura(facturaId: number): Observable<LineaFactura[]> {
    const url = `${this.apiUrl}/LineaFactura/editar/${facturaId}`;
    return new Observable<LineaFactura[]>(observer => {
      axios.get<LineaFactura[]>(url)
        .then(response => {
          observer.next(response.data);
          observer.complete();
        })
        .catch(error => {
          observer.error(error);
        });
    });
  }
}
