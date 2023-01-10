import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TableHostComponent } from './table-host.component';

describe('TableHostComponent', () => {
  let component: TableHostComponent;
  let fixture: ComponentFixture<TableHostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TableHostComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TableHostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
