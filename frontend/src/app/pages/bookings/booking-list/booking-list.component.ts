import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-booking-list',
  templateUrl: './booking-list.component.html',
  styleUrls: ['./booking-list.component.css']
})
export class BookingListComponent implements OnInit {
  bookings: any[] = [];
  loading = false;

  constructor() { }

  ngOnInit(): void {
    this.loadBookings();
  }

  loadBookings(): void {
    this.loading = true;
    // TODO: Call booking service
    console.log('Loading bookings...');
    this.loading = false;
  }
}
