import { TestBed } from '@angular/core/testing';

import { SiteFormatterService } from './site-formatter.service';

describe('SiteFormatterService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SiteFormatterService = TestBed.get(SiteFormatterService);
    expect(service).toBeTruthy();
  });
});
