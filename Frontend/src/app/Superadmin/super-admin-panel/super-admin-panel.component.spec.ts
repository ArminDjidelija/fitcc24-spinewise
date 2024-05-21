import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuperAdminPanelComponent } from './super-admin-panel.component';

describe('SuperAdminPanelComponent', () => {
  let component: SuperAdminPanelComponent;
  let fixture: ComponentFixture<SuperAdminPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SuperAdminPanelComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SuperAdminPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
