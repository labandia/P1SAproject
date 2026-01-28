//// GLOBAL GET FUNCTIONS WITH TOKEN AUTHENTICATION 
window.FetchAuthenticate = function (url, fdata) {
    var token = localStorage.getItem('accessToken');
    var queryString = Object.keys(fdata || {})
        .map(function (k) { return encodeURIComponent(k) + '=' + encodeURIComponent(fdata[k]); })
        .join('&');
    var fullUrl = url + (queryString ? '?' + queryString : '');

    function makeRequest(tokenToUse) {
        return fetch(fullUrl, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + tokenToUse
            }
        });
    }

    return makeRequest(token).then(function (res) {
        var logout = localStorage.getItem('Logout') || window.AppConfig.loginUrl;

        if (res.status === 401) {
            return refreshAccessToken().then(function (refreshSuccess) {
                if (refreshSuccess) {
                    token = localStorage.getItem('accessToken');
                    return makeRequest(token).then(function (res2) {
                        if (res2.status === 401) {
                            localStorage.clear();
                            window.location.href = logout;
                            return null;
                        }
                        return res2.json();
                    });
                } else {
                    localStorage.clear();
                    window.location.href = logout;
                    return null;
                }
            });
        }
        return res.json();
    }).catch(function (error) {
        console.error('Error fetching data:', error);
    });
};

// GLOBAL GET FUNCTIONS WITHOUT TOKEN AUTHENTICATION 
window.fetchData = function (url, fdata) {
    if (fdata === void 0) fdata = {};

    var queryString = Object.keys(fdata || {})
        .map(function (k) { return encodeURIComponent(k) + '=' + encodeURIComponent(fdata[k]); })
        .join('&');
    var fullUrl = queryString ? (url + '?' + queryString) : url;

    return fetch(fullUrl, {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    })
        .then(function (res) { return res.json(); })
        .catch(function (error) {
            console.error('Fetch Error:', error);
            return null;
        });
};

window.postData = function (url, data) {
    return fetch(url, {
        method: 'POST',
        body: data
    })
        .then(function (response) {
            return response.json().then(function (json) {
                if (!response.ok || json.Success === false) {
                    console.error("Server error:", json.Message || "Unknown error");
                }
                return json;
            });
        })
        .catch(function (error) {
            console.error("Error posting data:", error);
            return null;
        });
};

window.postrawData = function (url, data) {
    return fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    })
        .then(function (response) {
            return response.json()
                .then(function (json) {
                    if (!response.ok || json.Success === false) {
                        console.error("Server error:", json.Message || "Unknown error");
                    }
                    return json;
                })
                .catch(function (parseErr) {
                    console.error("Error parsing JSON:", parseErr);
                    return response.text().then(function (text) {
                        console.error("Raw response:", text);
                        return { Success: false, Message: "Invalid JSON response", Raw: text };
                    });
                });
        })
        .catch(function (error) {
            console.error("Error posting data:", error);
            return null;
        });
};

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



window.GetMethod = function (url, fdata = {}, options = {}) {
    // Configuration
    const config = {
        loginUrl: localStorage.getItem('Logout') || window.AppConfig?.loginUrl || '/login',
        accessTokenKey: 'accessToken',
        refreshTokenKey: 'refreshToken',
        tokenExpiryKey: 'tokenExpiry',
        tokenRefreshThreshold: 5 * 60 * 1000, // 5 minutes before expiry
        ...options
    };

    // Build URL with query parameters
    function buildUrl(url, params) {
        if (!params || Object.keys(params).length === 0) return url;

        const queryString = Object.entries(params)
            .map(([key, value]) => `${encodeURIComponent(key)}=${encodeURIComponent(value)}`)
            .join('&');

        return `${url}${url.includes('?') ? '&' : '?'}${queryString}`;
    }

    // Check if token is expired or about to expire
    function isTokenExpiredOrNearExpiry() {
        const expiryTime = localStorage.getItem(config.tokenExpiryKey);
        if (!expiryTime) return true;

        const now = Date.now();
        const expiry = parseInt(expiryTime, 10);

        // Return true if expired or within refresh threshold
        return now >= (expiry - config.tokenRefreshThreshold);
    }

    // Make fetch request with token
    function makeRequest(tokenToUse, retryCount = 0) {
        const headers = {
            'Content-Type': 'application/json',
            ...options.headers
        };

        if (tokenToUse) {
            headers['Authorization'] = `Bearer ${tokenToUse}`;
        }

        return fetch(buildUrl(url, fdata), {
            method: 'GET',
            headers,
            credentials: 'same-origin',
            ...options,
            method: 'GET' // Ensure GET method
        });
    }

    // Handle response with token refresh logic
    async function handleResponse(response, token, retryCount = 0) {
        const logout = () => {
            localStorage.clear();
            window.location.href = config.loginUrl;
        };

        // If unauthorized and we have a token, try to refresh
        if (response.status === 401 && token && retryCount === 0) {
            try {
                const refreshSuccess = await refreshAccessToken();

                if (refreshSuccess) {
                    const newToken = localStorage.getItem(config.accessTokenKey);
                    // Retry the request with new token
                    const retryResponse = await makeRequest(newToken, 1);
                    return await handleResponse(retryResponse, newToken, 1);
                } else {
                    logout();
                    return null;
                }
            } catch (error) {
                console.error('Token refresh error:', error);
                logout();
                return null;
            }
        }

        // If still unauthorized after refresh, logout
        if (response.status === 401) {
            logout();
            return null;
        }

        // Check if response is OK
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        // Parse and return JSON response
        try {
            return await response.json();
        } catch (error) {
            console.error('Error parsing JSON:', error);
            return null;
        }
    }

    // Refresh token function
    async function refreshAccessToken() {
        const refreshToken = localStorage.getItem(config.refreshTokenKey);

        if (!refreshToken) {
            console.warn('No refresh token available');
            return false;
        }

        try {
            const response = await fetch("/User/RefreshToken", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                },
                body: JSON.stringify({ refreshToken: refreshToken })
            });

            if (!response.ok) {
                throw new Error(`Refresh token request failed: ${response.status}`);
            }

            const result = await response.json();

            if (result.accessToken) {
                // Store new access token
                localStorage.setItem(config.accessTokenKey, result.accessToken);

                // Store token expiry if provided
                if (result.expiresIn) {
                    const expiryTime = Date.now() + (result.expiresIn * 1000);
                    localStorage.setItem(config.tokenExpiryKey, expiryTime.toString());
                }

                return true;
            } else {
                console.warn('Refresh token response missing accessToken');
                return false;
            }
        } catch (error) {
            console.error("Error refreshing token:", error);
            return false;
        }
    }

    // Main execution flow
    return (async () => {
        try {
            let token = localStorage.getItem(config.accessTokenKey);

            // Check if token needs refresh before making request
            if (token && isTokenExpiredOrNearExpiry()) {
                const refreshSuccess = await refreshAccessToken();
                if (refreshSuccess) {
                    token = localStorage.getItem(config.accessTokenKey);
                } else if (token) {
                    // If we couldn't refresh but have an expired token, continue anyway
                    // The server will reject it and trigger the 401 flow
                }
            }

            // Make initial request
            const response = await makeRequest(token);
            return await handleResponse(response, token);

        } catch (error) {
            console.error('Error in FetchAuthenticate:', error);

            // Only logout for network errors if we have no token
            const token = localStorage.getItem(config.accessTokenKey);
            if (!token && error instanceof TypeError) {
                // Network error without token - could redirect to login
                // but we'll just return null to let the caller handle it
                return null;
            }

            throw error; // Re-throw for caller to handle
        }
    })();
};

// Optional: Add utility method for checking auth status
window.GetMethod.isAuthenticated = function () {
    const token = localStorage.getItem('accessToken');
    const expiryTime = localStorage.getItem('tokenExpiry');

    if (!token) return false;

    if (expiryTime) {
        const now = Date.now();
        const expiry = parseInt(expiryTime, 10);
        return now < expiry;
    }

    return true; // Assume valid if no expiry time
};

// Optional: Add method to manually refresh token
window.GetMethod.refreshToken = async function () {
    return await refreshAccessToken();
};

// Optional: Add method to clear tokens
window.GetMethod.clearTokens = function () {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('tokenExpiry');
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