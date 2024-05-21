import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuperAdminChairsComponent } from './super-admin-chairs.component';

describe('SuperAdminChairsComponent', () => {
  let component: SuperAdminChairsComponent;
  let fixture: ComponentFixture<SuperAdminChairsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SuperAdminChairsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SuperAdminChairsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
