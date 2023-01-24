import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
//import { AuthGuard } from './auth/auth.guard';


const routes: Routes = [
  {
    path: 'auth',
    canLoad: [],
    loadChildren: () => import('./auth/auth.module').then(mod => mod.AuthModule)
   // loadChildren: () => import('./inbox/inbox.module').then(mod => mod.InboxModule)
  },
  {
    path: '',
    canLoad: [],
    loadChildren: () => import('./home/home.module').then(mod => mod.HomeModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
