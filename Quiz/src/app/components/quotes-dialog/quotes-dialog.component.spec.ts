import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuotesDialogComponent } from './quotes-dialog.component';

describe('QuotesDialogComponent', () => {
  let component: QuotesDialogComponent;
  let fixture: ComponentFixture<QuotesDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuotesDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(QuotesDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
