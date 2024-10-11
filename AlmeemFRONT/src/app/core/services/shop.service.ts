import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ShopParams } from '../../shared/models/shopParams';
import { Pagination } from '../../shared/models/pagination';
import { category, Product } from '../../shared/models/product';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:7051/api/';

  private http = inject(HttpClient); // same as injecting in the CTOR
  //best place to make Http req by angualr in this bit in LifeTime cycle

  Categorys: category[] = [];
  GetNewArrival: boolean = false;

  // Color: any[] = [];
  // Size: any[] = [];

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();

    if (shopParams.categories.length > 0) {
      // Send only the first selected CategoryId
      params = params.append('CategoryId', shopParams.categories[0].toString());
    }

    // if (shopParams.categories.length > 0) {
    //   params = params.append(
    //     'categories',
    //     shopParams.categories.map((category) => category.nameEn).join(',')
    //   );
    // }

    if (shopParams.sort) {
      params = params.append('sort', shopParams.sort);
    }

    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }

    if (shopParams.isNewArrival !== undefined) {
      params = params.append('isNewArrival', String(shopParams.isNewArrival));
    }

    if (shopParams.isActive !== undefined) {
      params = params.append('isActive', String(shopParams.isActive));
    }

    params = params.append('pageSize', shopParams.pageSize);
    params = params.append('pageIndex', shopParams.pageNumber);

    return this.http.get<Pagination<Product>>(this.baseUrl + 'Products', {
      params,
    });
  }

  getProduct(id: number) {
    return this.http.get<Product>(this.baseUrl + 'products/' + id);
  }

  getCategory() {
    if (this.Categorys.length > 0) return;
    return this.http
      .get<category[]>(this.baseUrl + 'Products/categories')
      .subscribe({
        next: (response) => (this.Categorys = response),
      });
  }

  // getProducts(shopParams: ShopParams) {
  //   let params: string = 'Product/GetAllProducts';
  //   if ((shopParams.Category.length = 0 && !shopParams.IsNewArrival)) {
  //     params = 'Product/GetAllProducts';
  //   }

  //   if (shopParams.Category.length > 0) {
  //     params = 'Category';
  //   }

  //   if (shopParams.NewArrival) {
  //     params = 'Product/GetNewArrival';
  //   }

  //   return this.http.get<any>(this.baseUrl + params);
  // }

  //when using pagination

  // getProductsdry() {
  //   return this.http
  //     .get<Pagination<Product>>(this.baseUrl + 'Products')
  //     .subscribe({
  //       next: (response) => console.log(response),
  //     });
  // }
  // getCategory() {
  //   if (this.FilterByCategory.length > 0) return;
  //   return this.http.get<any>(this.baseUrl + 'Products/categories').subscribe({
  //     next: (response) => (this.FilterByCategory = response),
  //   });
  // }

  // getColor() {
  //   if (this.Color.length > 0) return;
  //   return this.http.get<any>(this.baseUrl + 'Color').subscribe({
  //     next: (response) => (this.Color = response),
  //   });
  // }

  // getSize() {
  //   if (this.Size.length > 0) return;
  //   return this.http.get<any>(this.baseUrl + 'Size').subscribe({
  //     next: (response) => (this.Size = response),
  //   });
  // }
}
