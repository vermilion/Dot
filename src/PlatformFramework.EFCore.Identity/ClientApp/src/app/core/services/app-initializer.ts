import { AuthenticationService } from "./auth.service";

export function appInitializer(authenticationService: AuthenticationService) {
    return () => new Promise(resolve => {
        // attempt to refresh token on app start up to auto authenticate
        authenticationService.refreshToken()
            .subscribe()
            .add(resolve);
    });
}