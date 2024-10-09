import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ShopParams } from '../../shared/models/shopParams';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:44378/api/';

  private http = inject(HttpClient); // same as injecting in the CTOR
  //best place to make Http req by angualr in this bit in LifeTime cycle
  FilterByCategory: any[] = [];
  GetNewArrival: any[] = [];
  Color: any[] = [];
  Size: any[] = [];

  getProducts(shopParams: ShopParams) {
    let params: string = 'Product/GetAllProducts';
    if ((shopParams.Category.length = 0 && !shopParams.NewArrival)) {
      params = 'Product/GetAllProducts';
    }

    if (shopParams.Category.length > 0) {
      params = 'Category';
    }

    if (shopParams.NewArrival) {
      params = 'Product/GetNewArrival';
    }

    return this.http.get<any>(this.baseUrl + params);
  }

  getCategory() {
    if (this.FilterByCategory.length > 0) return;
    return this.http.get<any>(this.baseUrl + 'Category').subscribe({
      next: (response) => (this.FilterByCategory = response),
    });
  }
  getColor() {
    if (this.Color.length > 0) return;
    return this.http.get<any>(this.baseUrl + 'Color').subscribe({
      next: (response) => (this.Color = response),
    });
  }

  getSize() {
    if (this.Size.length > 0) return;
    return this.http.get<any>(this.baseUrl + 'Size').subscribe({
      next: (response) => (this.Size = response),
    });
  }
}
