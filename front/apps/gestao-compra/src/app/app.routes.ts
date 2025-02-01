import { AuthenticatedModule } from './authenticated/authenticated.module';
import { Route } from '@angular/router';

export const appRoutes: Route[] = [
  {
    path: '',
    loadChildren: () => import('./public/public.module').then(x => x.PublicModule)
  },
  {
    path: '',
    loadChildren: () => import('./authenticated/authenticated.module').then(x => x.AuthenticatedModule)
  },
];
