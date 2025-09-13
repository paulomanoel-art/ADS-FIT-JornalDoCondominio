import { Component, OnDestroy, OnInit } from '@angular/core';
import { NoticiaService } from '../../services/noticia.service';
import { AuthService } from '../../services/auth.service';
import { Noticia } from '../../models/noticia.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Editor, NgxEditorModule } from 'ngx-editor';
import { HeaderComponent } from "../../shared/header/header.component";

@Component({
  selector: 'app-noticia',
  standalone: true,
  imports: [CommonModule, FormsModule, NgxEditorModule, HeaderComponent],
  templateUrl: './noticia-create.component.html',
  styleUrl: './noticia-create.component.css'
})
export class NoticiaCreateComponent implements OnInit, OnDestroy {
  titulo = '';
  conteudoHtml = '';
  imagemUrl = '';
  sucesso = '';
  erro = '';
  editor!: Editor;

  constructor(private noticiaService: NoticiaService, private authService: AuthService) { }

  ngOnInit(): void {
    this.editor = new Editor();
  }

  ngOnDestroy(): void {
    this.editor.destroy();
  }

  criarNoticia() {
    const usuario = this.authService.getUsuarioLogado();
    if (!usuario) {
      this.erro = 'Usuário não autenticado';
      return;
    }

    const noticia: Noticia = {
      titulo: this.titulo,
      fotoPath: this.imagemUrl,
      texto: this.conteudoHtml,
      usuarioCriacaoId: usuario.id
    };

    this.noticiaService.criarNoticia(noticia).subscribe({
      next: () => {
        this.sucesso = 'Notícia criada com sucesso!';
        this.erro = '';
        this.titulo = '';
        this.imagemUrl = '';
        this.conteudoHtml = '';
      },
      error: (erro) => {
        console.log(erro.error);
        this.erro = erro.error;
      }
    });
  }

}
