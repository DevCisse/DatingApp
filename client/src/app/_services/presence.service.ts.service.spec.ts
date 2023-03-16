import { TestBed } from '@angular/core/testing';

import { Presence.Service.TsService } from './presence.service.ts.service';

describe('Presence.Service.TsService', () => {
  let service: Presence.Service.TsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Presence.Service.TsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
