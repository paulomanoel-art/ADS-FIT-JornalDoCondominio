import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { HeaderComponent } from "../../shared/header/header.component";
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { NoticiaService } from '../../services/noticia.service';
import { NoticiaResponse } from '../../models/NoticiaResponse.model';

@Component({
  selector: 'app-controle-noticias',
  standalone: true,
  imports: [HeaderComponent, CommonModule, FormsModule, RouterLink],
  templateUrl: './controle-noticias.component.html',
  styleUrl: './controle-noticias.component.css'
})
export class ControleNoticiasComponent implements OnInit {

  noticiaSelecionada?: NoticiaResponse;

  @Input() noticias: NoticiaResponse[] = [];

  placeholder = 'data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" width="200" height="120"><rect width="100%" height="100%" fill="%23e9ecef"/><text x="50%" y="50%" dominant-baseline="middle" text-anchor="middle" fill="%23999" font-size="14">Sem imagem</text></svg>';

  constructor(private noticiaService: NoticiaService) { }

  ngOnInit(): void {
    this.ExibirNoticias();
  }

  ExibirNoticias(): void {

    this.noticiaService.listarNoticias().subscribe({
      next: (data) => {
        this.noticias = data;
      },
      error: (erro) => { },
      complete: () => { }
    });
  }

  editarModalExcluir(noticia: NoticiaResponse) {
    this.noticiaSelecionada = noticia;
  }

  abrirModalExcluir(noticia: NoticiaResponse) {
    this.noticiaSelecionada = noticia;
  }

  fecharModal() {
    this.noticiaSelecionada = undefined;
  }

  confirmarExcluir() {
    if (!this.noticiaSelecionada) return;

    console.log(this.noticiaSelecionada);
    this.noticiaService.CancelarNoticia(this.noticiaSelecionada.id).subscribe({
      next: (data) => {
        this.noticias = data;
        this.fecharModal();
      },
      error: (err) => { },
      complete: () => { }
    });
  }

}
