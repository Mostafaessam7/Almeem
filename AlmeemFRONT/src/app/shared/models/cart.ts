import { nanoid } from 'nanoid';

export type CartType = {
  id: string;
  items: CartItem[];
  deliveryMethodId?: number;
  paymentIntentId?: string;
  clientSecret?: string;
};

export type CartItem = {
  productId: number;
  nameEn: string;
  nameAr: string;
  price: number;
  quantity: number;
  imageUrl: string;
  categoryNameEn: string;
  categoryNameAr: string;
  size: string;
  color: string;
};
export class Cart implements CartType {
  id = nanoid();
  items: CartItem[] = [];
  deliveryMethodId?: number;
  paymentIntentId?: string;
  clientSecret?: string;
}
