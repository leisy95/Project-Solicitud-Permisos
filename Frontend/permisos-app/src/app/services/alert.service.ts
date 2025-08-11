import { Injectable } from '@angular/core';
import Swal, { SweetAlertIcon } from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  private animaciones = {
    showClass: { popup: 'animate__animated animate__fadeInDown' },
    hideClass: { popup: 'animate__animated animate__fadeOutUp' }
  };

  private estilos = {
    popup: 'mi-popup',
    title: 'mi-titulo',
    confirmButton: 'mi-boton-confirmar',
    cancelButton: 'mi-boton-cancelar'
  };

  mostrar(icon: SweetAlertIcon, titulo: string, mensaje: string) {
    Swal.fire({
      icon,
      title: titulo,
      text: mensaje,
      customClass: this.estilos,
      buttonsStyling: false,
      ...this.animaciones
    });
  }

  exito(titulo: string, mensaje: string) {
    this.mostrar('success', titulo, mensaje);
  }

  error(titulo: string, mensaje: string) {
    this.mostrar('error', titulo, mensaje);
  }

  advertencia(titulo: string, mensaje: string) {
    this.mostrar('warning', titulo, mensaje);
  }

  confirmar(titulo: string, mensaje: string, callback: () => void) {
    Swal.fire({
      icon: 'question',
      title: titulo,
      text: mensaje,
      showCancelButton: true,
      confirmButtonText: 'Sí',
      cancelButtonText: 'No',
      customClass: this.estilos,
      buttonsStyling: false,
      ...this.animaciones
    }).then((res) => {
      if (res.isConfirmed) callback();
    });
  }
}