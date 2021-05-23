import { TestBed } from '@angular/core/testing';

import { InformacionIPService } from './informacion-ip.service';

describe('InformacionIPService', () => {
  let service: InformacionIPService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InformacionIPService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
