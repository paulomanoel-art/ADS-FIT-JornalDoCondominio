import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { NoticiaCreateComponent } from './components/noticia/noticia-create.component';

export const routes: Routes = [
    { path: '', component: LoginComponent },
    { path: 'login', component: LoginComponent },
    { path: 'noticia-criar', component: NoticiaCreateComponent }
];
