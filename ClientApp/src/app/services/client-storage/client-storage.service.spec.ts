import { TestBed } from '@angular/core/testing';

import { ClientStorageService } from './client-storage.service';

describe('ClientStorageService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ClientStorageService = TestBed.get(ClientStorageService);
    expect(service).toBeTruthy();
  });
});
