import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { SolicitudPermisoComponent } from './solicitud-permiso/solicitud-permiso.component';
import { LoginComponent } from './login/login.component';
import { CrearUsuarioComponent } from './crear-usuario/crear-usuario.component';
import { DashboardComponent } from './admin/dashboard/dashboard.component';
import { MostrarUsuariosComponent } from './mostrar-usuarios/mostrar-usuarios.component';
import { AuthGuard } from './services/AuthGuard';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'solicitud-permiso', component: SolicitudPermisoComponent, canActivate: [AuthGuard] },
  // { path: 'admin', component: AdminPanelComponent, canActivate: [AuthGuard] },
  {
    path: 'admin',
    component: DashboardComponent, canActivate: [AuthGuard],
    children: [
      { path: '', component: AdminPanelComponent }, // /admin
      // { path: 'crear-usuario', component: CrearUsuarioComponent }, // /admin/crear-usuario
      { path: 'mostrar-usuarios', component: MostrarUsuariosComponent }, // /admin/mostrar-usuarios
    ]
  },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
