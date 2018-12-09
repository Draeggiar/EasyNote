import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeComponent } from './home.component';
import { AppRoutingModule } from 'ClientApp/app/app-routing.module';
import { AccountModule } from '../account/account.module';
import { ConfigService } from 'ClientApp/app/services/config.service';
import { UserService } from 'ClientApp/app/services/user.service';
import { AppModule } from 'ClientApp/app/app.module';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [  ],
      imports:[
        AppRoutingModule,
        AccountModule,
        AppModule
      ],
      providers: [UserService, ConfigService]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
