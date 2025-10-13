// services/noticia.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Noticia } from '../models/noticia.model';
import { AuthService } from './auth.service';
import { Assinatura } from '../models/assinatura.model';

@Injectable({
  providedIn: 'root'
})
export class NoticiaService {
  private apiUrl = 'https://localhost:7140/Noticias';

  constructor(private http: HttpClient, private authService: AuthService) { }

  criarNoticia(noticia: Noticia): Observable<Noticia> {
    const usuario = this.authService.getUsuarioLogado();
    if (!usuario) throw new Error('Usuário não autenticado');

    const headers = new HttpHeaders()
      .set('X-User-Email', usuario.emailLogin)
      .set('X-User-Password', usuario.pwdLogin);

    return this.http.post<Noticia>(this.apiUrl, noticia, { headers });
  }

  listarNoticias(): Observable<any> {
    const headers = new HttpHeaders()
    return this.http.get<any>(this.apiUrl);
  }

  assinatura(assinatura : Assinatura): Observable<any> {

    return this.http.post<any>(this.apiUrl + '/assinatura', assinatura);

  }
}
