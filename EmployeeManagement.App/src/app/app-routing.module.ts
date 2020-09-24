import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RoleComponent } from './admin/role/role.component';
import { HomeComponent } from './home/home.component';


const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  {
    path: 'login',
    data: { preload: false },
    loadChildren: () =>
      import('./auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: 'home', component: HomeComponent,
    data: { preload: false },
    loadChildren: () =>
      import('./home/home.module').then(m => m.HomeModule)
  },
  { path: 'roles', component: RoleComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
