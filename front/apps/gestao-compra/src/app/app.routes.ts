import { Route } from '@angular/router';

export const appRoutes: Route[] = [
  {
    path: '',
    loadChildren: () => import('./public/public.module').then(x => x.PublicModule)
  },
];
