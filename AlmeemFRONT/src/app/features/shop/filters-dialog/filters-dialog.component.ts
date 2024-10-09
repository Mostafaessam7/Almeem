import { Component, inject } from '@angular/core';
import { ShopService } from '../../../core/services/shop.service';
import { MatDivider } from '@angular/material/divider';
import { MatListOption, MatSelectionList } from '@angular/material/list';
import { MatButton } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-filters-dialog',
  standalone: true,
  imports: [
    MatDivider,
    MatSelectionList,
    MatListOption,
    MatButton,
    FormsModule,
  ],
  templateUrl: './filters-dialog.component.html',
  styleUrl: './filters-dialog.component.scss',
})
export class FiltersDialogComponent {
  shopService = inject(ShopService);

  private dialogRef = inject(MatDialogRef<FiltersDialogComponent>);
  data = inject(MAT_DIALOG_DATA);

  selectedNewArrival: boolean = this.data.selectedNewArrival;
  selectedCategory: any = this.data.selectedCategory;
  selectedColor: any = this.data.selectedColor;
  selectedSize: any = this.data.selectedSize;

  applyFilters() {
    this.dialogRef.close({
      selectedNewArrival: this.selectedNewArrival,
      selectedCategory: this.selectedCategory,
      selectedColor: this.selectedColor,
      selectedSize: this.selectedSize,
    });
  }
}
