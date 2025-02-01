import { Injectable } from '@angular/core';
import {
    CanActivate,
} from '@angular/router';
import { Router } from '@angular/router';
import { AuthService } from '@front/services';
import { Observable } from 'rxjs';

@Injectable()
export class AuthenticatedGuard implements CanActivate {
    constructor(
        private authService: AuthService,
        private router: Router
    ) { }

    canActivate(): boolean | Observable<boolean> | Promise<boolean> {
        if (this.authService.isLoggedIn()) {
            return true;
        }

        this.router.navigate(['/login']);
        return false;
    }
}
