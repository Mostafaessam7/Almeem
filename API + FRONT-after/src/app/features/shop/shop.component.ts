import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { MatCard } from '@angular/material/card';
import { ProductItemComponent } from './product-item/product-item.component';
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import {
  MatListOption,
  MatSelectionList,
  MatSelectionListChange,
} from '@angular/material/list';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { ShopParams } from '../../shared/models/shopParams';
import { Product } from '../../shared/models/product';
import { Pagination } from '../../shared/models/pagination';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [
    MatCard,
    ProductItemComponent,
    MatButton,
    MatIcon,
    MatMenu,
    MatSelectionList,
    MatListOption,
    MatMenuTrigger,
    MatPaginator,
    FormsModule,
  ],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss',
})
export class ShopComponent implements OnInit {
  private shopService = inject(ShopService);
  private dialogService = inject(MatDialog);
  private route = inject(ActivatedRoute);

  products?: Pagination<Product>;

  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low-High', value: 'priceAsc' },
    { name: 'Price: High-Low', value: 'priceDesc' },
  ];

  shopParams = new ShopParams();
  pageSizeOptions = [5, 10, 15, 20];

  receivedCategory: number = 0;
  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.receivedCategory = params['data'];
    });
    this.initialiseShop();
  }

  initialiseShop() {
    if (this.receivedCategory > 0) {
      this.shopParams.categories[0] = this.receivedCategory;
    }
    this.getProducts();
    this.shopService.getCategory();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: (response) => (this.products = response),
      error: (error) => console.log(error),
    });
  }

  onSearchChange() {
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  handlePageEvent(event: PageEvent) {
    this.shopParams.pageNumber = event.pageIndex + 1;
    this.shopParams.pageSize = event.pageSize;
    this.getProducts();
  }

  onSortChange(event: MatSelectionListChange) {
    const selectedOption = event.options[0];
    if (selectedOption) {
      this.shopParams.sort = selectedOption.value;
      this.shopParams.pageNumber = 1;
      this.getProducts();
    }
  }

  openFiltersDialog() {
    const dialogRef = this.dialogService.open(FiltersDialogComponent, {
      minWidth: '500px',
      data: {
        selectedNewArrival: this.shopParams.isNewArrival,
        selectedCategory: this.shopParams.categories,
        // selectedCategory: this.shopParams.categories.map(
        //   (category) => category.nameEn
        // ),
      },
    });
    dialogRef.afterClosed().subscribe({
      next: (result) => {
        if (result) {
          this.shopParams.isNewArrival = result.selectedNewArrival;
          this.shopParams.categories = result.selectedCategory;
          this.shopParams.pageNumber = 1;
          console.log(result.selectedCategory);
          this.getProducts();
        }
      },
    });
  }
}

// openFiltersDialog() {
//   const dialogRef = this.dialogService.open(FiltersDialogComponent, {
//     minWidth: '500px',
//     data: {
//       selectedNewArrival: this.shopParams.isNewArrival,
//       selectedCategory: this.shopParams.categories.map(
//         (category) => category.nameEn
//       ),
//     },
//   });

//   dialogRef.afterClosed().subscribe({
//     next: (result) => {
//       if (result) {
//         this.shopParams.isNewArrival = result.selectedNewArrival;
//         this.shopParams.categories = result.selectedCategory.map(name => ({ nameEn: name })); // Ensure this matches your expected structure
//         this.shopParams.pageNumber = 1;
//         this.getProducts(this.shopParams); // Call getProducts with updated params
//       }
//     },
//   });
// }
