import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookingListComponent } from './booking-list/booking-list.component';
import { BookingsRoutingModule } from './bookings-routing.module';

@NgModule({
  declarations: [BookingListComponent],
  imports: [CommonModule, BookingsRoutingModule]
})
export class BookingsModule { }
