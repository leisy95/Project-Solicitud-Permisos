import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AlertService } from '../services/alert.service';

interface Usuario {
  id: number;
  nombre: string;
  correo: string;
  rol: string;
}

@Component({
  selector: 'app-mostrar-usuarios',
  templateUrl: './mostrar-usuarios.component.html',
  styleUrls: ['./mostrar-usuarios.component.scss']
})
export class MostrarUsuariosComponent implements OnInit {
  usuarios: Usuario[] = [];
  error = '';
  mensaje = '';

  constructor(private http: HttpClient, private alert: AlertService) {}

 mostrarCrear: boolean = false;

  ngOnInit(): void {
    this.obtenerUsuarios();
  }

  obtenerUsuarios(): void {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    this.http.get<Usuario[]>('http://localhost:5206/api/usuarios', { headers })
      .subscribe({
        next: (data) => {
          this.usuarios = data;
          this.error = '';
        },
        error: (err) => {
          this.alert.error('Oops','Error al obtener los usuarios.')
          this.usuarios = [];
        }
      });
  }

  eliminarUsuario(id: number): void {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    this.http.delete(`http://localhost:5206/api/usuarios/${id}`, { headers })
      .subscribe({
        next: () => {
          this.alert.exito('perfecto','Usuario eliminado correctamente.')
          this.error = '';
          this.obtenerUsuarios(); // refrescar lista
        },
        error: (err) => {
          this.alert.error('Oops','Error al eliminar el usuario.')
          this.mensaje = '';
        }
      });
  }
}