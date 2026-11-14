import { HttpInterceptorFn } from '@angular/common/http';

export const demoOrganizationId = '11111111-1111-1111-1111-111111111111';

export const demoAuthInterceptor: HttpInterceptorFn = (request, next) =>
  next(
    request.clone({
      setHeaders: {
        'X-Organization-Id': demoOrganizationId,
        'X-User-Role': 'PayrollManager'
      }
    })
  );
