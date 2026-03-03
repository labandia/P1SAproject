import { DEFAULT_CONFIG } from "./config.js";
import { isTokenExpired, refreshAccessToken, clearTokens } from "./auth.js";

function buildUrl(url, params) {
    if (!params || !Object.keys(params).length) return url;
    return url + (url.includes('?') ? '&' : '?') + new URLSearchParams(params);
}

async function fetchWithTimeout(url, options, timeout) {
    const controller = new AbortController();
    const timer = setTimeout(() => controller.abort(), timeout);

    try {
        return await fetch(url, { ...options, signal: controller.signal });
    } finally {
        clearTimeout(timer);
    }
}

export async function getMethod(url, params = {}, options = {}) {
    const cfg = { ...DEFAULT_CONFIG, ...options };
    let token = localStorage.getItem(cfg.accessTokenKey);

    if (token && isTokenExpired(cfg)) {
        await refreshAccessToken(cfg);
        token = localStorage.getItem(cfg.accessTokenKey);
    }

    const headers = { ...(options.headers || {}) };
    if (token) headers.Authorization = `Bearer ${token}`;

    try {
        const response = await fetchWithTimeout(
            buildUrl(url, params),
            {
                method: 'GET',
                headers,
                credentials: 'same-origin'
            },
            cfg.timeout
        );

        if (response.status === 401) {
            const refreshed = await refreshAccessToken(cfg);
            if (!refreshed) {
                clearTokens();
                location.href = cfg.loginUrl;
                return;
            }
            return getMethod(url, params, options);
        }

        if (!response.ok) {
            return { success: false, status: response.status };
        }

        return await response.json();

    } catch (err) {
        return {
            success: false,
            message: err.name === 'AbortError'
                ? "Request timeout"
                : "Network error"
        };
    }
}

export async function postMethod(url, data, options = {}) {
    const {
        headers = {},
        contentType = "application/json",
        timeout = 30000,
        responseType = "json"
    } = options;

    const controller = new AbortController();
    const timeoutId = setTimeout(() => controller.abort(), timeout);

    const config = {
        method: "POST",
        headers: { ...headers },
        credentials: "same-origin",
        signal: controller.signal
    };

    if (data instanceof FormData) {
        config.body = data;
    } else {
        config.body = JSON.stringify(data);
        config.headers["Content-Type"] = contentType;
    }

    try {
        const response = await fetch(url, config);
        clearTimeout(timeoutId);

        const rawText = response.status !== 204
            ? await response.text()
            : "";

        if (!response.ok) {
            return { Success: false, Status: response.status };
        }

        if (responseType === "json") {
            return rawText ? JSON.parse(rawText) : null;
        }

        return rawText;

    } catch (error) {
        return {
            Success: false,
            Error: true,
            Message: error.message
        };
    }
}