import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgxEditorModule } from 'ngx-editor';
import { NoticiaResponse } from '../../models/NoticiaResponse.model';
import { NoticiaService } from '../../services/noticia.service';
import { Assinatura } from '../../models/assinatura.model';

@Component({
  selector: 'app-noticia-list',
  standalone: true,
  imports: [CommonModule, FormsModule, NgxEditorModule],
  templateUrl: './noticia-list.component.html',
  styleUrl: './noticia-list.component.css'
})
export class NoticiaListComponent implements OnInit {

  noticias : NoticiaResponse[] = []; 

  noticiaSelecionada: any;
  email: string = '';
  mensagem: string = '';

  constructor(private noticiaService : NoticiaService){
  }

  ngOnInit(): void {
    this.noticiaService.listarNoticias().subscribe({
      next: (data) => {
        this.noticias = data;
      },
      error: (erro) => {},
      complete: () => {}
    });
  }

  abrirModal(noticia: NoticiaResponse) {
    this.noticiaSelecionada = noticia;
    const modal = document.getElementById('noticiaModal');
    if (modal) {
      const bsModal = new (window as any).bootstrap.Modal(modal);
      bsModal.show();
    }
  }

  inscrever() {
    if (!this.email) {
      this.mensagem = 'Por favor, informe seu e-mail.';
      return;
    }
    const assinatura = new Assinatura();
    assinatura.email = this.email;
    this.noticiaService.assinatura(assinatura).subscribe({
      next: (data) => {
        this.mensagem = 'Obrigado! Você receberá novidades em breve.';
        this.email = '';
      },
      error: (erro) => {},
      complete: () => {}
    });


  }
}
