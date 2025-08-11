import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {

  constructor(private router: Router){}


logout(): void {
  localStorage.removeItem('token');
  localStorage.removeItem('rol'); // si guardaste el rol
  this.router.navigate(['/login']);
}
}
