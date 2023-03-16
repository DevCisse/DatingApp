import { TestBed } from '@angular/core/testing';

import { Message.Service.TsService } from './message.service.ts.service';

describe('Message.Service.TsService', () => {
  let service: Message.Service.TsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Message.Service.TsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
