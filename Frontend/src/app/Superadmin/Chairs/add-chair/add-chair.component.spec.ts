import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddChairComponent } from './add-chair.component';

describe('AddChairComponent', () => {
  let component: AddChairComponent;
  let fixture: ComponentFixture<AddChairComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddChairComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddChairComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
