import { TestBed } from '@angular/core/testing';

import { BSfourConverterService } from './b-sfour-converter.service';

describe('BSfourConverterService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BSfourConverterService = TestBed.get(BSfourConverterService);
    expect(service).toBeTruthy();
  });
});
