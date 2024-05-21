import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewChairComponent } from './review-chair.component';

describe('ReviewChairComponent', () => {
  let component: ReviewChairComponent;
  let fixture: ComponentFixture<ReviewChairComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReviewChairComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReviewChairComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
