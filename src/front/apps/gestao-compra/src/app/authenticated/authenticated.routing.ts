import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticatedComponent } from './authenticated.component';
import { AuthenticatedGuard } from '../guards/authenticated.guard';

const routes: Routes = [
    {
      path: '',
      component: AuthenticatedComponent,
      canActivate: [AuthenticatedGuard],
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
        {
          path: 'transacoes',
          loadChildren: () =>
            import('./transacao/transacao.module').then((x) => x.TransacaoModule)
        },
        {
          path: 'orcamentos',
          loadChildren: () =>
            import('./orcamento/orcamento.module').then((x) => x.OrcamentoModule)
        },
      ],
    },
  ];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})

export class AuthenticatedRouting { }
