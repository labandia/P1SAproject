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


// Refresh Token
function refreshAccessToken() {
    var logout = localStorage.getItem('Logout') || window.AppConfig.loginUrl;
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