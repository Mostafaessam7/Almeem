import { category } from './product';

export class ShopParams {
  sort = 'name';
  pageNumber = 1;
  pageSize = 10;
  search = '';

  // categoryId = 1;

  categories: number[] = [];
  isNewArrival = true;
  isActive = true;
}
