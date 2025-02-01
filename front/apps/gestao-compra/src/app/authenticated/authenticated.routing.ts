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
          redirectTo: 'dashboard',
        },
        {
          path: 'dashboard',
          loadChildren: () =>
            import('./dashboard/dashboard.module').then((x) => x.DashboardModule)
        },
        {
          path: 'categoria',
          loadChildren: () =>
            import('./categoria/categoria.module').then((x) => x.CategoriaModule)
        },
        {
          path: 'transacoes',
          loadChildren: () =>
            import('./transacao/transacao.module').then((x) => x.TransacaoModule)
        },
      ],
    },
  ];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})

export class AuthenticatedRouting { }
