import { Injectable } from '@angular/core';
import axios from 'axios';
import { Cliente } from '../models/cliente';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  private baseUrl = 'https://localhost:7030/api/clientes';

  async getAllClientes(): Promise<Cliente[]> {
    const response = await axios.get<Cliente[]>(this.baseUrl);
    return response.data;
  }

  async getClienteById(id: number): Promise<Cliente> {
    const response = await axios.get<Cliente>(`${this.baseUrl}/${id}`);
    return response.data;
  }

  async createCliente(cliente: Cliente): Promise<void> {
    await axios.post(this.baseUrl, cliente);
  }

  async updateCliente(id: number, cliente: Cliente): Promise<void> {
    await axios.put(`${this.baseUrl}/${id}`, cliente);
  }

  async deleteCliente(id: number): Promise<void> {
    await axios.delete(`${this.baseUrl}/${id}`);
  }
}
