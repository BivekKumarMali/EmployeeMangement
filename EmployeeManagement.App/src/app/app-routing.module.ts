import { Component, NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RoleComponent } from './admin/role/role.component';
import { AuthGuard } from './core/auth/auth.guard';
import { HomeComponent } from './home/home.component';
import { PageNotFoundComponent } from './PageNotFound.component';


const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  {
    path: 'login',
    data: { preload: false },
    loadChildren: () =>
      import('./authentication/authentication.module').then(m => m.AuthenticationModule)
  },
  {
    path: 'home', component: HomeComponent, canActivate: [AuthGuard],
    data: { preload: false },
    loadChildren: () =>
      import('./home/home.module').then(m => m.HomeModule)
  },
  { path: 'roles', component: RoleComponent, canActivate: [AuthGuard], data: { permittedRoles: ['Admin'] } },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
