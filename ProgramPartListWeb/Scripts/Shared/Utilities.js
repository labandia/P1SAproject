(function () {

    const DEFAULT_CONFIG = {
        loginUrl: localStorage.getItem('Logout') || window.AppConfig?.loginUrl || '/login',
        accessTokenKey: 'accessToken',
        refreshTokenKey: 'refreshToken',
        tokenExpiryKey: 'tokenExpiry',
        tokenRefreshThreshold: 5 * 60 * 1000,
        timeout: 30000,
        errorMessages: {
            network: 'Network error. Please check your connection.',
            timeout: 'Request timeout. Please try again.',
            unauthorized: 'Session expired. Please log in again.',
            forbidden: 'You do not have permission to access this resource.',
            server: 'Server error. Please try again later.',
            notFound: 'Requested resource not found.',
            badRequest: 'Invalid request.',
            refreshFailed: 'Unable to refresh session.',
            jsonParse: 'Failed to parse server response.',
            general: 'An error occurred.'
        }
    };

    function buildUrl(url, params) {
        if (!params || Object.keys(params).length === 0) return url;
        return url + (url.includes('?') ? '&' : '?') + new URLSearchParams(params);
    }

    function isTokenExpired(cfg) {
        const expiry = localStorage.getItem(cfg.tokenExpiryKey);
        return !expiry || Date.now() >= (+expiry - cfg.tokenRefreshThreshold);
    }

    async function refreshAccessToken(cfg) {
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

    async function fetchWithTimeout(url, options, timeout) {
        const controller = new AbortController();
        const timer = setTimeout(() => controller.abort(), timeout);

        try {
            return await fetch(url, { ...options, signal: controller.signal });
        } finally {
            clearTimeout(timer);
        }
    }

    window.GetMethod = async function (url, fdata = {}, options = {}) {
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
                buildUrl(url, fdata),
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
                    localStorage.clear();
                    location.href = cfg.loginUrl;
                    return;
                }
                return GetMethod(url, fdata, options);
            }

            if (!response.ok) {
                return {
                    success: false,
                    status: response.status,
                    message: cfg.errorMessages.general
                };
            }

            const data = await response.json();

            return data;

        } catch (err) {
            if (err.name === 'AbortError') {
                return { success: false, message: cfg.errorMessages.timeout };
            }
            return { success: false, message: cfg.errorMessages.network };
        }
    };

    /* ---------- Utilities ---------- */

    window.GetMethod.isAuthenticated = function () {
        const token = localStorage.getItem(DEFAULT_CONFIG.accessTokenKey);
        const expiry = localStorage.getItem(DEFAULT_CONFIG.tokenExpiryKey);
        return !!token && (!expiry || Date.now() < +expiry);
    };

    window.GetMethod.refreshToken = () => refreshAccessToken(DEFAULT_CONFIG);

    window.GetMethod.clearTokens = function () {
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
        localStorage.removeItem('tokenExpiry');
    };

})();




window.PullUserInformation = function () {
    var token = localStorage.getItem('accessToken');
    return fetch('/User/GetUserInformation', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json'
        }
    }).then(function (res) {
        if (res.status === 401) {
            console.warn("Unauthorized");
            return;
        }
        return res.json().then(function (result) {
            if (result.success) {
                console.log("User ID:", result.userId);
                console.log("Name:", result.name);
                console.log("Role:", result.role);
                return result;
            } else {
                console.warn(result.message || "Unknown error");
            }
        });
    });
};


window.logout = function () {
    var logout = localStorage.getItem('Logout');
    try {
        localStorage.clear();
        window.location.href = logout;
    } catch (error) {
        console.error("Logout failed:", error);
    }
};

// Restriction of typing characters
window.restrictChars = function (e) {
    var x = e.which || e.keycode;
    return (x >= 48 && x <= 57) || x === 46;
};


// =======================
// ACTION RESTRICT
// =======================
window.ActionButtons = function () {
    var userRole = localStorage.getItem("UserRole");
    if (userRole === "Leader" || userRole === "Users") {
        return false;
    }
};



// =======================
// LAZY LOADING IMAGE
// =======================
document.addEventListener("DOMContentLoaded", function () {
    const lazyImages = document.querySelectorAll("img.lazy");

    if ("IntersectionObserver" in window) {
        let observer = new IntersectionObserver((entries, obs) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    const img = entry.target;
                    img.src = img.dataset.src;

                    img.onload = () => {
                        img.classList.add("loaded"); // trigger CSS transition
                    };

                    img.removeAttribute("data-src");
                    obs.unobserve(img);
                }
            });
        });

        lazyImages.forEach(img => observer.observe(img));
    } else {
        // fallback (older browsers)
        lazyImages.forEach(img => {
            img.src = img.dataset.src;
            img.onload = () => {
                img.classList.add("loaded");
            };
            img.removeAttribute("data-src");
        });
    }
    
});


window.formatJsonDate = function (value) {
    if (!value) return "";

    const ms = parseInt(value.replace("/Date(", "").replace(")/", ""));
    if (isNaN(ms)) return "";

    const date = new Date(ms);
    return date.toLocaleDateString();
};






window.postMethod = async function (url, data, options = {}) {

    if (!url) {
        console.error("postMethod: URL is required");
        return { Success: false, Message: "URL missing" };
    }

    const {
        headers = {},
        contentType = "application/json",
        throwOnError = false,
        includeCredentials = false,
        timeout = 30000,
        responseType = "json"
    } = options;

    const controller = new AbortController();
    const timeoutId = setTimeout(() => controller.abort(), timeout);

    const config = {
        method: "POST",
        headers: { ...headers },
        credentials: includeCredentials ? "include" : "same-origin",
        signal: controller.signal
    };

    // ---- Body handling (MVC friendly) ----
    if (data instanceof FormData) {
        config.body = data; // Let browser set multipart boundary
    }
    else if (typeof data === "object" && data !== null) {
        config.body = JSON.stringify(data);
        if (!config.headers["Content-Type"]) {
            config.headers["Content-Type"] = contentType;
        }
    }
    else if (typeof data === "string") {
        config.body = data;
        if (!config.headers["Content-Type"]) {
            config.headers["Content-Type"] = contentType;
        }
    }

    try {
        const response = await fetch(url, config);
        clearTimeout(timeoutId);

        let rawText = "";
        if (response.status !== 204) {
            rawText = await response.text(); // READ ONCE
        }

        if (!response.ok) {
            const err = new Error(`HTTP ${response.status}`);
            err.status = response.status;
            err.raw = rawText;
            if (throwOnError) throw err;
        }

        let result = rawText;

        if (responseType === "json") {
            try {
                result = rawText ? JSON.parse(rawText) : null;
            } catch {
                console.warn("Invalid JSON returned:", rawText);
                result = { Success: false, rawText };
            }
        }

        if (result && result.Success === false && throwOnError) {
            throw new Error(result.Message || "Server error");
        }

        return result;

    } catch (error) {

        if (error.name === "AbortError") {
            error.message = `Request timed out after ${timeout}ms`;
        }

        if (throwOnError) throw error;

        return {
            Success: false,
            Error: true,
            Message: error.message,
            Status: error.status || 0
        };
    }
};





