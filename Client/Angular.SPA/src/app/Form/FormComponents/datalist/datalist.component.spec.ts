import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DataListComponent } from './datalist.component';

describe('TextInputComponent', () => {
  let component: DataListComponent;
  let fixture: ComponentFixture<DataListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DataListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DataListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
