// GLOBAL GET FUNCTIONS WITH TOKEN AUTHENTICATION 
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
    console.clear();

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
    console.clear();
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

// Login check logic (unchanged, only arrow removed)
window.IsLoginUser = function (options) {
    options = options || {};
    var storageKey = options.storageKey;
    var expectedValue = options.expectedValue;
    var redirectUrl = options.redirectUrl;
    var redirectIfLoggedInUrl = options.redirectIfLoggedInUrl || null;
    var expirationKey = options.expirationKey || null;
    var maxHours = options.maxHours || null;

    var value = localStorage.getItem(storageKey);

    if (value === expectedValue && redirectIfLoggedInUrl) {
        if (expirationKey && maxHours) {
            var loginTimeStr = localStorage.getItem(expirationKey);
            if (loginTimeStr) {
                var loginTime = new Date(loginTimeStr);
                var now = new Date();
                var diffHours = Math.abs(now - loginTime) / 36e5;
                if (diffHours <= maxHours) {
                    window.location.href = redirectIfLoggedInUrl;
                    return;
                }
            }
        } else {
            window.location.href = redirectIfLoggedInUrl;
            return;
        }
    }

    if (value !== expectedValue) {
        var currentPath = window.location.pathname;
        if (currentPath !== redirectUrl) {
            if (typeof ActionRestrict === 'function') {
                var allowed = ActionRestrict();
                if (allowed !== false) {
                    window.location.href = redirectUrl;
                    return;
                }
            } else {
                window.location.href = redirectUrl;
                return;
            }
        }
        return;
    }

    if (expirationKey && maxHours) {
        var loginTimeStr2 = localStorage.getItem(expirationKey);
        var currentPath2 = window.location.pathname;
        var targetPath = new URL(redirectUrl, window.location.origin).pathname;

        if (loginTimeStr2) {
            var loginTime2 = new Date(loginTimeStr2);
            var now2 = new Date();
            var diffHours2 = Math.abs(now2 - loginTime2) / 36e5;
            if (diffHours2 > maxHours) {
                localStorage.clear();
                if (currentPath2 !== targetPath) {
                    window.location.href = redirectUrl;
                }
            }
        } else {
            localStorage.clear();
            if (currentPath2 !== targetPath) {
                window.location.href = redirectUrl;
            }
        }
    }
};

// Refresh Token
function refreshAccessToken() {
    var logout = localStorage.getItem('Logout');
    var refreshToken = localStorage.getItem('refreshToken');
    if (!refreshToken) return Promise.resolve(false);

    return fetch("/User/RefreshToken", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ refreshToken: refreshToken })
    })
        .then(function (response) {
            return response.json().then(function (result) {
                if (response.ok && result.accessToken) {
                    localStorage.setItem("accessToken", result.accessToken);
                    return true;
                } else {
                    console.warn("Refresh token failed. Logging out...");
                    localStorage.clear();
                    window.location.href = logout;
                    return false;
                }
            });
        })
        .catch(function (error) {
            console.error("Error refreshing token:", error);
            return false;
        });
}

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