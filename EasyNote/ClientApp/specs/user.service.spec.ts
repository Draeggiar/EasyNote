import { TestBed } from '@angular/core/testing';

import { UserService } from '../app/services/user.service';
import { ConfigService } from 'ClientApp/app/services/config.service';
import { AppModule } from 'ClientApp/app/app.module';

describe('UserService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports:[
      AppModule
    ],
    providers: [UserService, ConfigService]
  }));

  it('should be created', () => {
    const service: UserService = TestBed.get(UserService);
    expect(service).toBeTruthy();
  });
});
