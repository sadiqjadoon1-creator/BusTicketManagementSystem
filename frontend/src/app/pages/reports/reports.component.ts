import { Component, OnInit } from '@angular/core';
import { Chart } from 'chart.js';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {
  selectedReport = 'dashboard';
  reportData: any = {};
  loading = false;

  ngOnInit(): void {
    this.loadDashboardReport();
  }

  loadDashboardReport(): void {
    this.loading = true;
    this.reportData = {
      totalBookings: 1250,
      totalRevenue: 3125000,
      activeSchedules: 45,
      occupancyRate: 82.5,
      recentBookings: [
        { id: 1, ref: 'BK001', amount: 2500, date: '2024-01-15', status: 'Confirmed' },
        { id: 2, ref: 'BK002', amount: 2000, date: '2024-01-14', status: 'Pending' }
      ]
    };
    this.loading = false;
  }

  loadRevenueReport(): void {
    this.loading = true;
    this.reportData = {
      daily: 125000,
      monthly: 3750000,
      annual: 45000000
    };
    this.loading = false;
  }

  exportReport(format: string): void {
    console.log(`Exporting ${this.selectedReport} as ${format}`);
    // Mock export functionality
    alert(`Report exported as ${format.toUpperCase()}`);
  }
}
