import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-cancelar-assinatura',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cancelar-assinatura.component.html',
  styleUrl: './cancelar-assinatura.component.css'
})
export class CancelarAssinaturaComponent implements OnInit {
  assinaturaId!: number;
  carregando = false;
  sucesso = false;
  erro = false;

  private apiUrl = 'https://localhost:7140/Noticias/cancelar-assinatura';

  constructor(private route: ActivatedRoute, private http: HttpClient) {}

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.assinaturaId = Number(idParam);
    } else {
      this.erro = true;
    }
  }

  cancelarAssinatura(): void {
    if (!this.assinaturaId) return;

    this.carregando = true;

    this.http.delete(`${this.apiUrl}/${this.assinaturaId}`).subscribe({
      next: () => {
        this.carregando = false;
        this.sucesso = true;
      },
      error: () => {
        this.carregando = false;
        this.erro = true;
      }
    });
  }
}
