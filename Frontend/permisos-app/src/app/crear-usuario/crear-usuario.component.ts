import { Component, EventEmitter, Output } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertService } from '../services/alert.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-crear-usuario',
  templateUrl: './crear-usuario.component.html',
  styleUrls: ['./crear-usuario.component.scss']
})
export class CrearUsuarioComponent {
  usuarioForm: FormGroup;
  mensaje = '';
  error = '';

  constructor(private fb: FormBuilder, private http: HttpClient, private alert: AlertService) {
    this.usuarioForm = this.fb.group({
      nombre: ['', Validators.required],
      correo: ['', [Validators.required, Validators.email]],
      rol: ['Usuario', Validators.required]
    });
  }

  crearUsuario(): void {
    this.mensaje = '';
    this.error = '';

    if (this.usuarioForm.invalid) {
      this.error = 'Por favor completa todos los campos correctamente.';
      return;
    }

    const datos = this.usuarioForm.value;
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', 'Bearer ' + token);

    // Validación: obtener usuarios existentes y revisar si el correo ya está registrado
    this.http.get<any[]>(`${environment.apiUrl}/usuarios`, { headers }).subscribe({
      next: (usuarios) => {
        const correoExistente = usuarios.some(u => u.correo === datos.correo);
        if (correoExistente) {
          this.alert.error('Error', 'El correo ya está registrado');
          return;
        }

        // Crear usuario si el correo no existe
        this.http.post(`${environment.apiUrl}/auth/crear-usuario`, datos, { headers }).subscribe({
          next: () => {
            this.alert.exito('Éxito', 'Usuario creado y correo enviado correctamente');
            this.usuarioForm.reset({ rol: 'Usuario' });
          },
          error: (err) => {
            console.error(err);
            if (err.error && err.error.mensaje) {
              this.alert.error('Error', err.error.mensaje);
            } else {
              this.alert.error('Error', 'Error al crear el usuario');
            }
          }
        });
      },
      error: (err) => {
        console.error(err);
        this.alert.error('Error', 'No se pudo verificar si el correo existe');
      }
    });
  }

  @Output() cerrar = new EventEmitter<void>();

  cerrarFormulario() {
    this.cerrar.emit();
  }
}