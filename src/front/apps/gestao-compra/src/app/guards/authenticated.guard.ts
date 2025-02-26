import { Injectable } from '@angular/core';
import {
  CanActivate,
} from '@angular/router';
import { Router } from '@angular/router';
import { AuthService } from '@front/services';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthenticatedGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  canActivate(): boolean | Observable<boolean> | Promise<boolean> {
    const url = this.authService.getUrl();

    if(!url) {
      this.authService.logout();
      this.router.navigate(['/login']);
      return false;
    }

    if (this.authService.isLoggedIn()) {
      return true;
    }

    this.router.navigate(['/inicio']);
    return false;
  }
}
