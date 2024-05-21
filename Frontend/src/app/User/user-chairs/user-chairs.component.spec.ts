import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserChairsComponent } from './user-chairs.component';

describe('UserChairsComponent', () => {
  let component: UserChairsComponent;
  let fixture: ComponentFixture<UserChairsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserChairsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserChairsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
