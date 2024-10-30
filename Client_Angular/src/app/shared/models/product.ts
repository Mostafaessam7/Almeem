type ProductImage = {
  url: string;
  isMain: boolean;
};

type variants = {
  size: string;
  color: string;
  quantityInStock: number;
};

export type category = {
  id: number;
  nameEn: string;
  nameAr: string;
};

export type Product = {
  id: number;

  nameEn: string;
  nameAr: string;
  descriptionEn: string;
  descriptionAr: string;
  price: number;
  isNewArrival: boolean;
  isActive: boolean;
  createdAt: Date;
  images: ProductImage[];
  variants: variants[];
  categoryId: number;
  categoryNameEn: string;
  categoryNameAr: string;
};
