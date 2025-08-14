import { Component, EventEmitter, Output } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertService } from '../services/alert.service';
import { environment } from 'src/environments/environments';

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

    this.http.post(`${environment.apiUrl}/auth/crear-usuario`, datos, { headers }).subscribe({
      next: () => {
        this.alert.exito('Exito', 'Usuario creado y correo enviado correctamente' );
        this.usuarioForm.reset({ rol: 'Usuario' });
      },
      error: (err) => {
        console.error(err);
        this.alert.error('Error', 'Error al crear el usuario' );
      }
    });
  }

  @Output() cerrar = new EventEmitter<void>();

  cerrarFormulario() {
    this.cerrar.emit();
  }
}