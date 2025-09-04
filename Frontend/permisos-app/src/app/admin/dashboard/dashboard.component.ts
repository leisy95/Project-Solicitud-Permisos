import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {

  constructor(private router: Router) { }

  isCollapsed = localStorage.getItem('sidebarCollapsed') === '1';

  toggleSidebar() {
    this.isCollapsed = !this.isCollapsed;
    localStorage.setItem('sidebarCollapsed', this.isCollapsed ? '1' : '0');
  }


  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('rol'); // si guardaste el rol
    this.router.navigate(['/login']);
  }
}
