import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
import { HeaderComponent } from '../../layout/header/header.component';
import { ShopService } from '../../core/services/shop.service';
import { ProductItemComponent } from '../shop/product-item/product-item.component';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Pagination } from '../../shared/models/pagination';
import { Product } from '../../shared/models/product';
import { MatCard } from '@angular/material/card';
import { MatIcon } from '@angular/material/icon';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { FormsModule } from '@angular/forms';
import { MatListOption, MatSelectionList } from '@angular/material/list';
import { ShopParams } from '../../shared/models/shopParams';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    MatButton,
    RouterLink,
    CommonModule,
    HeaderComponent,
    ProductItemComponent,
    MatPaginator,
    MatCard,
    MatIcon,
    MatMenu,
    FormsModule,
    MatSelectionList,
    MatListOption,
    MatMenuTrigger,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  private shopService = inject(ShopService);
  private router = inject(Router);

  products?: Pagination<Product>;

  shopParams = new ShopParams();
  pageSizeOptions = [5, 10, 15, 20];
  displayedProducts: Product[] = [];
  productsToShow = 5;

  // Array of slides (you can adjust this according to your slides)
  slides = [
    {
      image: 'images/banner-01.jpg',
      caption: 'Women Collection 2023',
      heading: 'New arrivals',
      link: '/product',
    },
    {
      image: 'images/banner-02.jpg',
      caption: 'Men New-Season',
      heading: 'Jackets & Coats',
      link: '/product',
    },
    {
      image: 'images/banner-03.jpg',
      caption: 'Men Collection 2023',
      heading: 'New Season',
      link: '/product',
    },
  ];

  currentSlide = 0; // Current active slide

  ngOnInit(): void {
    // Optional: Auto-slide functionality
    this.startAutoSlide();
    this.initialiseShop();
  }

  initialiseShop() {
    this.getProducts();
    this.shopService.getCategory();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: (response) => {
        this.products = response;
        this.displayedProducts = this.products.data.slice(
          0,
          this.productsToShow
        ); // Limit the number of products displayed
      },
      error: (error) => console.log(error),
    });
  }

  handlePageEvent(event: PageEvent) {
    this.shopParams.pageNumber = event.pageIndex + 1;
    this.shopParams.pageSize = event.pageSize;
    this.getProducts();
  }

  // Method to go to the next slide
  nextSlide(): void {
    this.currentSlide = (this.currentSlide + 1) % this.slides.length;
  }

  // Method to go to the previous slide
  prevSlide(): void {
    this.currentSlide =
      (this.currentSlide - 1 + this.slides.length) % this.slides.length;
  }

  // Auto-switch slides every 3 seconds
  startAutoSlide(): void {
    setInterval(() => {
      this.nextSlide();
    }, 5000); // You can adjust the time interval
  }

  goToSlide(index: number) {
    this.currentSlide = index;
  }

  goToShop(categroyId: number) {
    this.router.navigate(['/shop'], { queryParams: { data: categroyId } });
  }
}
