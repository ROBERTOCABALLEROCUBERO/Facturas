import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrearfacturaComponent } from './crearfactura.component';

describe('CrearfacturaComponent', () => {
  let component: CrearfacturaComponent;
  let fixture: ComponentFixture<CrearfacturaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CrearfacturaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CrearfacturaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
