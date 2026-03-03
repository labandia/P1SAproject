export const DEFAULT_CONFIG = {
    loginUrl: localStorage.getItem('Logout') || window.AppConfig?.loginUrl || '/login',
    accessTokenKey: 'accessToken',
    refreshTokenKey: 'refreshToken',
    tokenExpiryKey: 'tokenExpiry',
    tokenRefreshThreshold: 5 * 60 * 1000,
    timeout: 30000
};