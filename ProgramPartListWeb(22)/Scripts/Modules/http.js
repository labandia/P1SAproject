import { DEFAULT_CONFIG } from "./config.js";
import { isTokenExpired, refreshAccessToken, clearTokens } from "./auth.js";

/* =========================
   URL BUILDER
========================= */

function buildUrl(url, params) {
    if (!params || !Object.keys(params).length) return url;
    return url + (url.includes("?") ? "&" : "?") + new URLSearchParams(params);
}

/* =========================
   FETCH WITH TIMEOUT
========================= */

async function fetchWithTimeout(url, options, timeout) {

    const controller = new AbortController();
    const timer = setTimeout(() => controller.abort(), timeout);

    try {
        return await fetch(url, { ...options, signal: controller.signal });
    }
    finally {
        clearTimeout(timer);
    }
}

/* =========================
   TOKEN PREPARE
========================= */

async function prepareToken(cfg) {

    let token = localStorage.getItem(cfg.accessTokenKey);
    const refresh = localStorage.getItem(cfg.refreshTokenKey);

    if (!token && !refresh)
        return null;

    if (token && isTokenExpired(cfg)) {

        const refreshed = await refreshAccessToken(cfg);

        if (!refreshed) {
            clearTokens(cfg);
            location.href = cfg.loginUrl;
            return null;
        }

        token = localStorage.getItem(cfg.accessTokenKey);
    }

    return token;
}

/* =========================
   GET METHOD (PROTECTED)
========================= */

export async function getMethod(url, params = {}, options = {}) {

    const cfg = { ...DEFAULT_CONFIG, ...options };

    const token = await prepareToken(cfg);

    const headers = { ...(options.headers || {}) };

    if (token)
        headers.Authorization = `Bearer ${token}`;

    try {

        const response = await fetchWithTimeout(
            buildUrl(url, params),
            {
                method: "GET",
                headers,
                credentials: "same-origin"
            },
            cfg.timeout
        );

        if (response.status === 401) {

            const refreshed = await refreshAccessToken(cfg);

            if (!refreshed) {
                clearTokens(cfg);
                location.href = cfg.loginUrl;
                return;
            }

            return getMethod(url, params, options);
        }

        if (!response.ok)
            return { success: false, status: response.status };

        return await response.json();
    }
    catch (err) {

        return {
            success: false,
            message: err.name === "AbortError"
                ? "Request timeout"
                : "Network error"
        };
    }
}

/* =========================
   GET METHOD (PUBLIC)
========================= */

export async function getMethodPublic(url, params = {}, options = {}) {

    const cfg = { ...DEFAULT_CONFIG, ...options };

    try {

        const response = await fetchWithTimeout(
            buildUrl(url, params),
            {
                method: "GET",
                headers: options.headers || {},
                credentials: "same-origin"
            },
            cfg.timeout
        );

        if (!response.ok)
            return { success: false, status: response.status };

        return await response.json();
    }
    catch (err) {

        return {
            success: false,
            message: err.name === "AbortError"
                ? "Request timeout"
                : "Network error"
        };
    }
}

/* =========================
   POST METHOD (PROTECTED)
========================= */

export async function postMethod(url, data, options = {}) {

    const cfg = { ...DEFAULT_CONFIG, ...options };

    const token = await prepareToken(cfg);

    const headers = { ...(options.headers || {}) };

    if (token)
        headers.Authorization = `Bearer ${token}`;

    if (!(data instanceof FormData)) {
        headers["Content-Type"] = "application/json";
        data = JSON.stringify(data);
    }

    try {

        const response = await fetchWithTimeout(
            url,
            {
                method: "POST",
                headers,
                body: data,
                credentials: "same-origin"
            },
            cfg.timeout
        );

        if (response.status === 401) {

            const refreshed = await refreshAccessToken(cfg);

            if (!refreshed) {
                clearTokens(cfg);
                location.href = cfg.loginUrl;
                return;
            }

            return postMethod(url, data, options);
        }

        const text = await response.text();

        if (!response.ok)
            return { success: false, status: response.status };

        try {
            return JSON.parse(text);
        }
        catch {
            return text;
        }
    }
    catch (err) {

        return {
            success: false,
            message: err.name === "AbortError"
                ? "Request timeout"
                : "Network error"
        };
    }
}

/* =========================
   POST METHOD (PUBLIC)
========================= */

export async function postMethodPublic(url, data, options = {}) {

    const cfg = { ...DEFAULT_CONFIG, ...options };

    const headers = { ...(options.headers || {}) };

    if (!(data instanceof FormData)) {
        headers["Content-Type"] = "application/json";
        data = JSON.stringify(data);
    }

    try {

        const response = await fetchWithTimeout(
            url,
            {
                method: "POST",
                headers,
                body: data,
                credentials: "same-origin"
            },
            cfg.timeout
        );

        const text = await response.text();

        if (!response.ok)
            return { success: false, status: response.status };

        try {
            console.log(JSON.parse(text));
            return JSON.parse(text);
        }
        catch {
            return text;
        }
    }
    catch (err) {

        return {
            success: false,
            message: err.name === "AbortError"
                ? "Request timeout"
                : "Network error"
        };
    }
}