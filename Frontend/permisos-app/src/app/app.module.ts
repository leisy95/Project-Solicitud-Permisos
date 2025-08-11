import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { SolicitudPermisoComponent } from './solicitud-permiso/solicitud-permiso.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './login/login.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { CrearUsuarioComponent } from './crear-usuario/crear-usuario.component';
import { DashboardComponent } from './admin/dashboard/dashboard.component';
import { MostrarUsuariosComponent } from './mostrar-usuarios/mostrar-usuarios.component';

@NgModule({
  declarations: [
    AppComponent,
    SolicitudPermisoComponent,
    AdminPanelComponent,
    LoginComponent,
    CrearUsuarioComponent,
    DashboardComponent,
    MostrarUsuariosComponent,
  ],
  imports: [
    BrowserModule,
    RouterModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
