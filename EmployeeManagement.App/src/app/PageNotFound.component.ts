import { Component } from '@angular/core';

@Component({
  template: `
    <h1>HTTP 404</h1>
    <p>Page Not Found</p>
    <a [routerLink]="['/home']" routerLinkActive="router-link-active" >Home</a>
    `
})
export class PageNotFoundComponent { }
