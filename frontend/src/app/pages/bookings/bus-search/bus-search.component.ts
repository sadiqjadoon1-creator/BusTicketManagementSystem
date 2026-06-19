import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-bus-search',
  templateUrl: './bus-search.component.html',
  styleUrls: ['./bus-search.component.css']
})
export class BusSearchComponent implements OnInit {
  searchForm!: FormGroup;
  buses: any[] = [];
  loading = false;
  searched = false;

  cities = [
    { id: 1, name: 'Hyderabad' },
    { id: 2, name: 'Karachi' },
    { id: 3, name: 'Multan' },
    { id: 4, name: 'Lahore' },
    { id: 5, name: 'Islamabad' }
  ];

  constructor(private formBuilder: FormBuilder, private apiService: ApiService) { }

  ngOnInit(): void {
    this.searchForm = this.formBuilder.group({
      source: ['', Validators.required],
      destination: ['', Validators.required],
      date: ['', Validators.required],
      passengers: [1, [Validators.required, Validators.min(1)]]
    });
  }

  get f() { return this.searchForm.controls; }

  onSearch(): void {
    if (this.searchForm.invalid) return;

    this.loading = true;
    this.searched = true;

    const { source, destination, date } = this.searchForm.value;
    // Mock data - Replace with actual API call
    this.buses = [
      {
        scheduleId: 1,
        busNo: 'BUS001',
        routeName: `${source} to ${destination}`,
        departure: '08:00 AM',
        arrival: '06:00 PM',
        fare: 2500,
        availableSeats: 15,
        type: 'AC'
      },
      {
        scheduleId: 2,
        busNo: 'BUS002',
        routeName: `${source} to ${destination}`,
        departure: '02:00 PM',
        arrival: '10:00 PM',
        fare: 2000,
        availableSeats: 8,
        type: 'Non-AC'
      }
    ];

    this.loading = false;
  }

  selectBus(schedule: any): void {
    console.log('Selected schedule:', schedule);
    // Navigate to seat selection
  }
}
