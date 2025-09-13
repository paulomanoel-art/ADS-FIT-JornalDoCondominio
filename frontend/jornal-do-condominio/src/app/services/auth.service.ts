// services/auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Usuario } from '../models/usuario.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7140/Usuarios';

  constructor(private http: HttpClient) { }

  login(email: string, senha: string): Observable<Usuario> {
    return this.http.post<Usuario>(`${this.apiUrl}/login`, { email, senha });
  }

  // Guardar usuário logado
  setUsuarioLogado(usuario: Usuario) {
    localStorage.setItem('usuario', JSON.stringify(usuario));
  }

  getUsuarioLogado(): Usuario | null {
    const user = localStorage.getItem('usuario');
    return user ? JSON.parse(user) : null;
  }

  logout() {
    localStorage.removeItem('usuario');
  }
}
