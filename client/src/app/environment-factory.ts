import { APP_INITIALIZER } from '@angular/core';
import { HttpBackend, HttpClient, HttpClientModule } from '@angular/common/http';
import { EnvironmentService } from './services/environment/environment-service';

export function appInit(envService: EnvironmentService, backend: HttpBackend): () => Promise<any> {
  const http = new HttpClient(backend);
  return () => envService.load(http);
}

export const EnvServiceProvider = {
  provide: APP_INITIALIZER,
  useFactory: appInit,
  multi: true,
  deps: [EnvironmentService, HttpBackend],
  imports: [HttpClientModule],
};
