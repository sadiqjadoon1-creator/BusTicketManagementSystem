import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BusSearchComponent } from './bus-search/bus-search.component';
import { BookingListComponent } from './booking-list/booking-list.component';
import { BookingsRoutingModule } from './bookings-routing.module';

@NgModule({
  declarations: [BookingListComponent, BusSearchComponent],
  imports: [CommonModule, FormsModule, BookingsRoutingModule]
})
export class BookingsModule { }
