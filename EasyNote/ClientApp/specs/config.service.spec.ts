import { TestBed } from '@angular/core/testing';

import { ConfigService } from '../app/services/config.service';

describe('ConfigService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ConfigService]
    });
  });

  it('should be created', () => {
    const service: ConfigService = TestBed.get(ConfigService);
    expect(service).toBeTruthy();
  });

  it('should return correct base app addres', () =>{
    const service: ConfigService = TestBed.get(ConfigService);
    expect(service.getApiURI()).toEqual('http://localhost:64795');
  })
});
