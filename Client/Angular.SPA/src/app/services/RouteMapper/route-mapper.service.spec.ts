import { TestBed } from '@angular/core/testing';

import { RouteMapperService } from './route-mapper.service';

describe('RouteMapperService', () => {
  let service: RouteMapperService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RouteMapperService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
