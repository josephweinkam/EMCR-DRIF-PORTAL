import { BreakpointObserver } from '@angular/cdk/layout';
import { CommonModule } from '@angular/common';
import { Component, Input, inject } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { RxFormBuilder, RxFormControl } from '@rxweb/reactive-form-validators';

@Component({
  selector: 'drr-textarea',
  templateUrl: './drr-textarea.component.html',
  styleUrl: './drr-textarea.component.scss',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatInputModule,
  ],
})
export class DrrTextareaComponent {
  formBuilder = inject(RxFormBuilder);

  breakpointObserver = inject(BreakpointObserver);

  isMobile = false;

  @Input() label = '';
  @Input() id = '';
  @Input() maxlength = 0;
  @Input() rows = 3;

  private _formControl = this.formBuilder.control('', []) as RxFormControl;
  @Input()
  set rxFormControl(rxFormControl: any) {
    this._formControl = rxFormControl as RxFormControl;
  }

  ngOnInit() {
    this.breakpointObserver
      .observe('(min-width: 768px)')
      .subscribe(({ matches }) => {
        this.isMobile = !matches;
      });
  }

  get rxFormControl() {
    return this._formControl;
  }

  getCount(): number {
    return this.rxFormControl?.value?.length ?? 0;
  }
}
