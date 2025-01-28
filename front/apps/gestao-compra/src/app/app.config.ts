import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, withInMemoryScrolling } from '@angular/router';
import { appRoutes } from './app.routes';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideNgxMask } from 'ngx-mask';

export const appConfig: ApplicationConfig = {
  providers: [
    // provideHttpClient(
    //   withInterceptors([TokenInterceptor, LoadingInterceptor]),
    //   withFetch()
    // ),
    provideAnimations(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideNgxMask(),
    provideRouter(
      appRoutes,
      withInMemoryScrolling({
        scrollPositionRestoration: 'top',
        anchorScrolling: 'enabled',
      }),
    ),
  ],
};


