import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditarfacturaComponent } from './editarfactura.component';

describe('EditarfacturaComponent', () => {
  let component: EditarfacturaComponent;
  let fixture: ComponentFixture<EditarfacturaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditarfacturaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditarfacturaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
