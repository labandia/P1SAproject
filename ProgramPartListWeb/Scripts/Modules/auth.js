import { DEFAULT_CONFIG } from "./config.js";

export function isTokenExpired(cfg = DEFAULT_CONFIG) {
    const expiry = localStorage.getItem(cfg.tokenExpiryKey);
    return !expiry || Date.now() >= (+expiry - cfg.tokenRefreshThreshold);
}

export async function refreshAccessToken(cfg = DEFAULT_CONFIG) {
    const refreshToken = localStorage.getItem(cfg.refreshTokenKey);
    if (!refreshToken) return false;

    try {
        const res = await fetch('/User/RefreshToken', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ refreshToken })
        });

        if (!res.ok) return false;

        const data = await res.json();
        if (!data.accessToken) return false;

        localStorage.setItem(cfg.accessTokenKey, data.accessToken);

        if (data.expiresIn) {
            localStorage.setItem(
                cfg.tokenExpiryKey,
                (Date.now() + data.expiresIn * 1000).toString()
            );
        }

        return true;
    } catch {
        return false;
    }
}

export function clearTokens(cfg = DEFAULT_CONFIG) {
    localStorage.removeItem(cfg.accessTokenKey);
    localStorage.removeItem(cfg.refreshTokenKey);
    localStorage.removeItem(cfg.tokenExpiryKey);
}

export function isAuthenticated(cfg = DEFAULT_CONFIG) {
    const token = localStorage.getItem(cfg.accessTokenKey);
    const expiry = localStorage.getItem(cfg.tokenExpiryKey);
    return !!token && (!expiry || Date.now() < +expiry);
}