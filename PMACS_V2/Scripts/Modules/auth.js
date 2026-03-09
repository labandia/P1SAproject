import { DEFAULT_CONFIG, PUBLIC_PAGES } from "./config.js";

export function getToken() {
    return localStorage.getItem(DEFAULT_CONFIG.accessTokenKey);
}

export function getRefreshToken() {
    return localStorage.getItem(DEFAULT_CONFIG.refreshTokenKey);
}

export function isTokenExpired(cfg = DEFAULT_CONFIG) {
    const expiry = localStorage.getItem(cfg.tokenExpiryKey);
    if (!expiry) return true;
    return Date.now() >= (+expiry - cfg.refreshThreshold);
}

export function clearTokens(cfg = DEFAULT_CONFIG) {
    localStorage.removeItem(cfg.accessTokenKey);
    localStorage.removeItem(cfg.refreshTokenKey);
    localStorage.removeItem(cfg.tokenExpiryKey);
}

export function isPublicPage() {
    const current = window.location.pathname.toLowerCase();
    return PUBLIC_PAGES.includes(current);
}

export function redirectLogin(cfg = DEFAULT_CONFIG) {
    window.location.href = cfg.loginUrl;
}

/* =========================
   INITIAL AUTH CHECK
========================= */

export function checkAuth(cfg = DEFAULT_CONFIG) {

    const token = getToken();
    const refresh = getRefreshToken();

    if (!token && !refresh && !isPublicPage()) {
        console.warn("No tokens → redirect login");
        redirectLogin(cfg);
    }
}

/* =========================
   REFRESH TOKEN
========================= */

export async function refreshAccessToken(cfg = DEFAULT_CONFIG) {

    const refresh = getRefreshToken();

    if (!refresh) return false;

    try {

        const res = await fetch("/User/RefreshToken", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ refreshToken: refresh })
        });

        if (!res.ok) return false;

        const data = await res.json();

        if (!data.accessToken) return false;

        localStorage.setItem(cfg.accessTokenKey, data.accessToken);

        if (data.expiresIn) {
            localStorage.setItem(
                cfg.tokenExpiryKey,
                Date.now() + data.expiresIn * 1000
            );
        }

        return true;

    } catch {
        return false;
    }
}