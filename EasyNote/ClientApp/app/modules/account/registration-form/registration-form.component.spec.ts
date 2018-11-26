import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationFormComponent } from './registration-form.component';
import { AppModule } from 'ClientApp/app/app.module';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../../shared/shared.module';

describe('RegistrationFormComponent', () => {
  let component: RegistrationFormComponent;
  let fixture: ComponentFixture<RegistrationFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ],
      imports: [
        FormsModule,
        SharedModule,
        AppModule
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrationFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
