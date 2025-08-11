import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'http://localhost:5206/api/auth';

  constructor(private http: HttpClient) {}

  login(datos: { correo: string; contrasena: string }) {
  return this.http.post<{ token: string, rol: string }>(`${this.apiUrl}/login`, datos);
}

  guardarToken(token: string) {
    localStorage.setItem('token', token);
  }

  obtenerToken(): string | null {
    return localStorage.getItem('token');
  }

  // eliminarToken() {
  //   localStorage.removeItem(this.tokenKey);
  // }

  eliminarToken() {
  localStorage.removeItem('token');
  localStorage.removeItem('rol');
}

  isLoggedIn(): boolean {
  const token = this.obtenerToken();
  if (!token) return false;

  const payload = JSON.parse(atob(token.split('.')[1]));
  const expiracion = payload.exp * 1000; // en milisegundos
  return Date.now() < expiracion;
}

  getRol(): string | null {
    return localStorage.getItem('rol');
  }
}

//codigo para mejorar el css sin el backend

/*import { HttpClient } from '@angular/common/http';
 import { Injectable } from '@angular/core';
 import { Observable, of, throwError } from 'rxjs';

 const useMock = true; // Cambia a false cuando quieras volver al backend real

 @Injectable({ providedIn: 'root' })
 export class AuthService {
   private apiUrl = 'http://localhost:5206/api/auth';

   constructor(private http: HttpClient) {}

   login(datos: { correo: string; contrasena: string }): Observable<{ token: string, rol: string }> {
     if (useMock) {
       // Lista de usuarios falsos para prueba
       const usuariosMock = [
         { correo: 'admin@correo.com', contrasena: 'admin123', rol: 'Admin' },
         { correo: 'usuario@correo.com', contrasena: 'usuario123', rol: 'Usuario' }
       ];

       const usuario = usuariosMock.find(
         u => u.correo === datos.correo && u.contrasena === datos.contrasena
       );

       if (usuario) {
         return of({
           token: this.generarTokenSimulado(usuario.correo, usuario.rol),
           rol: usuario.rol
         });
       } else {
         return throwError(() => ({
           status: 400,
           error: { mensaje: 'Correo o contraseña incorrectos.' }
         }));
       }
     }

     // Llamada real al backend
     return this.http.post<{ token: string, rol: string }>(`${this.apiUrl}/login`, datos);
   }

   guardarToken(token: string) {
     localStorage.setItem('token', token);
   }

   obtenerToken(): string | null {
     return localStorage.getItem('token');
   }

   eliminarToken() {
     localStorage.removeItem('token');
     localStorage.removeItem('rol');
   }

   isLoggedIn(): boolean {
     const token = this.obtenerToken();
     if (!token) return false;

     try {
       const payload = JSON.parse(atob(token.split('.')[1]));
       const expiracion = payload.exp * 1000;
       return Date.now() < expiracion;
     } catch (e) {
       return false; // token mal formado
     }
   }

   getRol(): string | null {
     return localStorage.getItem('rol');
   }

   // Simula un token JWT básico
   private generarTokenSimulado(correo: string, rol: string): string {
     const header = btoa(JSON.stringify({ alg: 'HS256', typ: 'JWT' }));
     const payload = btoa(JSON.stringify({
       correo: correo,
       rol: rol,
       exp: Math.floor(Date.now() / 1000) + 3600 // expira en 1 hora
     }));
     const signature = 'firma-falsa';
     return `${header}.${payload}.${signature}`;
   }
 }*/