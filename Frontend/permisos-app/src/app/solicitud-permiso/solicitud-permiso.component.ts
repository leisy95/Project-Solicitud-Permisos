import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { AlertService } from '../services/alert.service';
import { environment } from 'src/environments/environment';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-solicitud-permiso',
  templateUrl: './solicitud-permiso.component.html',
  styleUrls: ['./solicitud-permiso.component.scss']
})
export class SolicitudPermisoComponent {
  permiso = {
    nombre: '',
    correo: '',
    motivo: '',
    archivo: null as File | null
  };

  mensaje = '';
  errores: any = {}; //Guardar errores por campo
  archivoSeleccionado?: File;

  constructor(private http: HttpClient,
    private router: Router,
    private alert: AlertService,
    private authService: AuthService) { }

  ngOnInit(): void {
    const user = this.authService.getUserData();
    if (user) {
      this.permiso.nombre = user.nombre;
      this.permiso.correo = user.email;
    }
  }


  onArchivoSeleccionado(event: any): void {
    const archivo = event.target.files[0];
    if (archivo) {
      this.permiso.archivo = archivo;
    }
  }

  enviarSolicitud(): void {
    this.mensaje = '';
    this.errores = {}; // Limpia errores anteriores

    const formData = new FormData();
    formData.append('Nombre', this.permiso.nombre);
    formData.append('Correo', this.permiso.correo);
    formData.append('Motivo', this.permiso.motivo);

    if (this.permiso.archivo) {
      formData.append('Archivo', this.permiso.archivo);
    }

    this.http.post<{ mensaje: string }>(
      `${environment.apiUrl}/permisos`,
      formData,
      {
        headers: {}
      }
    ).subscribe({
      next: (respuesta) => {
        this.alert.exito('Solicitud enviada', respuesta.mensaje);
        this.permiso = { nombre: '', correo: '', motivo: '', archivo: null };
        this.archivoSeleccionado = undefined;
      },
      error: (err) => {
        if (err.status === 400 && err.error) {
          this.errores = err.error;
        } else {
          this.alert.error('Error', 'Ocurrió un error inesperado al enviar la solicitud.');
        }
      }
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}