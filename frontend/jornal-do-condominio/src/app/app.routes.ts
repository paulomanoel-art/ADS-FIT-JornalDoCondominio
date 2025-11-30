import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { NoticiaCreateComponent } from './components/noticia/noticia-create.component';
import { NoticiaListComponent } from './components/noticia-list/noticia-list.component';
import { CancelarAssinaturaComponent } from './components/cancelar-assinatura/cancelar-assinatura.component';
import { ControleNoticiasComponent } from './components/controle-noticias/controle-noticias.component';
import { NoticiaEditComponent } from './components/noticia-edit/noticia-edit.component';

export const routes: Routes = [
    { path: '', component: LoginComponent },
    { path: 'login', component: LoginComponent },
    { path: 'noticia-criar', component: NoticiaCreateComponent },
    { path: 'noticia-editar/:id', component: NoticiaEditComponent },
    { path: 'controle-noticias', component: ControleNoticiasComponent },
    { path: 'noticias', component: NoticiaListComponent },
    { path: 'assinatura-cancelar/:id', component: CancelarAssinaturaComponent },
];
