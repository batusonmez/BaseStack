import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SwitchInputComponent } from './select-input.component';

describe('SelectInputComponent', () => {
  let component: SwitchInputComponent;
  let fixture: ComponentFixture<SwitchInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SwitchInputComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SwitchInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
