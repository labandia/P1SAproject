(function () {

    const CONFIG = {
        loginUrl: window.AppConfig?.loginUrl || "/Index",
        accessTokenKey: "accessToken",
        refreshTokenKey: "refreshToken",
        tokenExpiryKey: "tokenExpiry",
        refreshThreshold: 5 * 60 * 1000,
        timeout: 30000
    };

    /* =========================
       AUTH STATE
    ========================= */

    function getToken() {
        return localStorage.getItem(CONFIG.accessTokenKey);
    }

    function getRefresh() {
        return localStorage.getItem(CONFIG.refreshTokenKey);
    }

    function isExpired() {
        const expiry = localStorage.getItem(CONFIG.tokenExpiryKey);
        if (!expiry) return true;
        return Date.now() >= (+expiry - CONFIG.refreshThreshold);
    }

    function redirectLogin() {
        window.location.href = CONFIG.loginUrl;
    }

    /* =========================
       TOKEN REFRESH
    ========================= */

    async function refreshToken() {

        const refresh = getRefresh();
        if (!refresh) return false;

        try {

            const res = await fetch("/User/RefreshToken", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ refreshToken: refresh })
            });

            if (!res.ok) return false;

            const data = await res.json();

            if (!data.accessToken)
                return false;

            localStorage.setItem(CONFIG.accessTokenKey, data.accessToken);

            if (data.expiresIn) {
                localStorage.setItem(
                    CONFIG.tokenExpiryKey,
                    Date.now() + data.expiresIn * 1000
                );
            }

            return true;

        } catch {
            return false;
        }
    }

    /* =========================
       FETCH WITH TIMEOUT
    ========================= */

    async function fetchTimeout(url, options, timeout = CONFIG.timeout) {

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
       GET METHOD (PROTECTED)
    ========================= */

    window.GetMethod = async function (url, params = {}, options = {}) {

        if (!getToken() && !getRefresh()) {
            redirectLogin();
            return { success: false, message: "Unauthorized" };
        }

        let token = getToken();

        if (token && isExpired()) {

            const refreshed = await refreshToken();

            if (!refreshed) {
                localStorage.clear();
                redirectLogin();
                return { success: false, message: "Session expired" };
            }

            token = getToken();
        }

        const query = new URLSearchParams(params).toString();
        const requestUrl = query ? `${url}?${query}` : url;

        const headers = {
            ...(options.headers || {})
        };

        if (token)
            headers.Authorization = `Bearer ${token}`;

        try {

            const res = await fetchTimeout(requestUrl, {
                method: "GET",
                headers,
                credentials: "same-origin"
            });

            if (res.status === 401) {

                const refreshed = await refreshToken();

                if (!refreshed) {
                    localStorage.clear();
                    redirectLogin();
                    return;
                }

                return GetMethod(url, params, options);
            }

            if (!res.ok) {
                return { success: false, status: res.status };
            }

            return await res.json();

        }
        catch (e) {

            if (e.name === "AbortError")
                return { success: false, message: "Request timeout" };

            return { success: false, message: "Network error" };
        }
    };

    /* =========================
       GET METHOD (PUBLIC)
    ========================= */

    window.GetMethodPublic = async function (url, params = {}, options = {}) {

        const query = new URLSearchParams(params).toString();
        const requestUrl = query ? `${url}?${query}` : url;

        const headers = {
            ...(options.headers || {})
        };

        try {

            const res = await fetchTimeout(requestUrl, {
                method: "GET",
                headers,
                credentials: "same-origin"
            });

            if (!res.ok) {
                return {
                    success: false,
                    status: res.status
                };
            }

            return await res.json();

        }
        catch (e) {

            if (e.name === "AbortError")
                return { success: false, message: "Request timeout" };

            return { success: false, message: "Network error" };
        }
    };

    /* =========================
       POST METHOD (PROTECTED)
    ========================= */

    window.postMethod = async function (url, data, options = {}) {

        if (!getToken() && !getRefresh()) {
            redirectLogin();
            return { success: false, message: "Unauthorized" };
        }

        const headers = options.headers || {};

        if (!(data instanceof FormData)) {
            headers["Content-Type"] = "application/json";
            data = JSON.stringify(data);
        }

        try {

            const res = await fetchTimeout(url, {
                method: "POST",
                body: data,
                headers,
                credentials: "same-origin"
            });

            const text = await res.text();

            if (!res.ok)
                return { success: false, status: res.status };

            try {
                return JSON.parse(text);
            }
            catch {
                return text;
            }

        }
        catch (err) {

            if (err.name === "AbortError")
                return { success: false, message: "Timeout" };

            return { success: false, message: "Network error" };
        }
    };

    /* =========================
       POST METHOD (PUBLIC / NO TOKEN)
    ========================= */
    window.postMethodPublic = async function (url, data, options = {}) {

        const headers = options.headers || {};

        if (!(data instanceof FormData)) {
            headers["Content-Type"] = "application/json";
            data = JSON.stringify(data);
        }

        try {

            const res = await fetchTimeout(url, {
                method: "POST",
                body: data,
                headers,
                credentials: "same-origin"
            });

            const text = await res.text();

            if (!res.ok) {
                return {
                    success: false,
                    status: res.status
                };
            }

            try {
                return JSON.parse(text);
            }
            catch {
                return text;
            }

        }
        catch (err) {

            if (err.name === "AbortError")
                return { success: false, message: "Timeout" };

            return { success: false, message: "Network error" };
        }
    };

    /* =========================
       LOGOUT
    ========================= */

    window.logout = function () {

        localStorage.clear();

        window.location.href = CONFIG.loginUrl;
    };

    /* =========================
       CROSS TAB LOGOUT
    ========================= */

    window.addEventListener("storage", function (e) {

        if (e.key === CONFIG.accessTokenKey && !e.newValue) {
            redirectLogin();
        }

    });

})();


/* =======================
   INPUT RESTRICTION
======================= */

window.restrictChars = function (e) {
    var x = e.which || e.keyCode;
    return (x >= 48 && x <= 57) || x === 46;
};


/* =======================
   ACTION RESTRICT
======================= */

window.ActionButtons = function () {
    var userRole = localStorage.getItem("UserRole");
    if (userRole === "Leader" || userRole === "Users") {
        return false;
    }
};


/* =======================
   LAZY IMAGE LOADING
======================= */

document.addEventListener("DOMContentLoaded", function () {

    const images = document.querySelectorAll("img.lazy");

    if ("IntersectionObserver" in window) {

        const observer = new IntersectionObserver((entries, obs) => {

            entries.forEach(entry => {

                if (entry.isIntersecting) {

                    const img = entry.target;

                    img.src = img.dataset.src;

                    img.onload = () => img.classList.add("loaded");

                    img.removeAttribute("data-src");

                    obs.unobserve(img);
                }

            });

        });

        images.forEach(img => observer.observe(img));
    }
});


/* =======================
   JSON DATE FORMAT
======================= */

window.formatJsonDate = function (value) {

    if (!value) return "";

    const ms = parseInt(value.replace("/Date(", "").replace(")/", ""));
    if (isNaN(ms)) return "";

    const date = new Date(ms);

    return date.toLocaleDateString();
};