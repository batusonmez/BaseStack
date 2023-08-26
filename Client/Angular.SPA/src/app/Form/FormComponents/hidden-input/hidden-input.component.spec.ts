import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HiddenInputComponent } from './hidden-input.component';

describe('HiddenInputComponent', () => {
  let component: HiddenInputComponent;
  let fixture: ComponentFixture<HiddenInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HiddenInputComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HiddenInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
