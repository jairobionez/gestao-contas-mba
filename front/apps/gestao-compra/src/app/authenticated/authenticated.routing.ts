import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticatedComponent } from './authenticated.component';

const routes: Routes = [
    {
      path: '',
      component: AuthenticatedComponent,
      children: [
        {
          path: '',
          pathMatch: 'full',
          redirectTo: 'inicio',
        },
        {
          path: 'inicio',
          loadChildren: () =>
            import('./inicio/inicio.module').then((x) => x.InicioModule)
        },
        {
          path: 'categoria',
          loadChildren: () =>
            import('./categoria/categoria.module').then((x) => x.CategoriaModule)
        },
      ],
    },
  ];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})

export class AuthenticatedRouting { }
