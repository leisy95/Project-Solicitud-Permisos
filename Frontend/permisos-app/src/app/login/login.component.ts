import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  correo = '';
  contrasena = '';
  error = '';

  ngOnInit(): void {
    const token = localStorage.getItem('token');
    const rol = localStorage.getItem('rol');

    if (token) {
      if (rol === 'Admin') {
        this.router.navigate(['/admin']);
      } else {
        this.router.navigate(['/solicitud']);
      }
    }
  }

  constructor(private authService: AuthService, private router: Router) { }

  login(): void {
    this.error = ''; // limpia errores anteriores

    const usuario = { correo: this.correo, contrasena: this.contrasena };

    this.authService.login(usuario).subscribe({
      next: (res) => {
        this.authService.guardarToken(res.token);
        localStorage.setItem('rol', res.rol);

        if (res.rol === 'Admin') {
          this.router.navigate(['/admin']);
        } else {
          this.router.navigate(['/solicitud']);
        }
      },
      error: (err) => {
        console.error('Error completo:', err);
        if (err.status === 400 && err.error) {
          this.error = err.error.mensaje || 'Correo o contraseña incorrectos.';
        } else {
          this.error = 'Datos Incorrectos. Contacta al administrador si necesitas ayuda';
        }
      }
    });
  }
}