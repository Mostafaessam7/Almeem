import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  title = 'AlmeemFRONT';

  baseUrl = 'https://localhost:7051/api/Product/GetAllProducts';
  products: any[] = [];
  private http = inject(HttpClient); // same as injecting in the CTOR
  //best place to make Http req by angualr in this bit in LifeTime cycle
  ngOnInit(): void {
    this.http.get<any>(this.baseUrl).subscribe({
      next: (response) => (this.products = response),
      error: (error) => console.log(error),
      complete: () => console.log('Complete'),
    });
  }
}
