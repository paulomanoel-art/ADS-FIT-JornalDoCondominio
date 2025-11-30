import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Editor, NgxEditorModule } from 'ngx-editor';
import { HeaderComponent } from '../../shared/header/header.component';
import { NoticiaService } from '../../services/noticia.service';
import { AuthService } from '../../services/auth.service';
import { Noticia } from '../../models/noticia.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-noticia-edit',
  standalone: true,
  imports: [CommonModule, FormsModule, NgxEditorModule, HeaderComponent],
  templateUrl: './noticia-edit.component.html',
  styleUrl: './noticia-edit.component.css'
})
export class NoticiaEditComponent {
  titulo = '';
  conteudoHtml = '';
  imagemUrl = '';
  sucesso = '';
  erro = '';
  editor!: Editor;

  noticiaId : number = 0;

  constructor(private route : ActivatedRoute, private router : Router, private noticiaService: NoticiaService, private authService: AuthService) { }

  ngOnInit(): void {
    this.editor = new Editor();
    this.CarregarNoticia();
  }

  CarregarNoticia() : void {
    const idParam = this.route.snapshot.paramMap.get('id');
    this.noticiaId = Number(idParam);
    
    this.noticiaService.ObterNoticia(this.noticiaId).subscribe({
      next:(data) => {
        this.titulo = data.titulo;
        this.conteudoHtml = data.conteudoHtml;
        this.imagemUrl = data.imagemUrl;
      },
      error:(err) => {},
      complete:() => {}
    });
  }

  ngOnDestroy(): void {
    this.editor.destroy();
  }

  editarNoticia() {
    const usuario = this.authService.getUsuarioLogado();
    if (!usuario) {
      this.erro = 'Usuário não autenticado';
      return;
    }

    const noticia: Noticia = {
      id : this.noticiaId,
      titulo: this.titulo,
      fotoPath: this.imagemUrl,
      texto: this.conteudoHtml,
      usuarioCriacaoId: usuario.id
    };

    this.noticiaService.editarNoticia(noticia).subscribe({
      next: () => {
        this.sucesso = 'Alteração realizada com sucesso!';
        this.erro = '';
        this.titulo = '';
        this.imagemUrl = '';
        this.conteudoHtml = '';
        this.router.navigate(['/controle-noticias']);
      },
      error: (erro) => {
        console.log(erro.error);
        this.erro = erro.error;
      }
    });
  }
}
