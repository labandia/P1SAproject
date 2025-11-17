// =======================
// GLOBAL GET FUNCTIONS WITH TOKEN AUTHENTICATION
// =======================
window.FetchAuthenticate = function (url, fdata) {
    var token = localStorage.getItem('accessToken');
    var queryString = new URLSearchParams(fdata).toString();
    var fullUrl = url + "?" + queryString;  // Append parameters to URL

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
        var logout = localStorage.getItem('Logout');
        if (res.status === 401) {
            return refreshAccessToken().then(function (refreshSuccess) {
                if (refreshSuccess) {
                    token = localStorage.getItem('accessToken');
                    return makeRequest(token).then(function (retryRes) {
                        if (retryRes.status === 401) {
                            localStorage.clear();
                            window.location.href = logout;
                            return null;
                        }
                        return retryRes.json();
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

// =======================
// GLOBAL GET FUNCTIONS WITHOUT TOKEN AUTHENTICATION
// =======================
window.fetchData = function (url, fdata) {
    fdata = fdata || {};
    try {
        var queryString = new URLSearchParams(fdata).toString();
        var fullUrl = queryString ? (url + "?" + queryString) : url;

        return fetch(fullUrl, {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).then(function (res) {
            return res.json();
        }).catch(function (err) {
            console.error('Fetch Error:', err);
            return null;
        });
    } catch (error) {
        console.error('Fetch Error:', error);
        return Promise.resolve(null);
    }
};

// =======================
// POST JSON DATA
// =======================
window.postData = function (url, data) {
    return fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    }).then(function (response) {
        return response.json().then(function (json) {
            return json;
        }).catch(function (parseErr) {
            console.error("Error parsing JSON:", parseErr);
            return response.text().then(function (text) {
                console.error("Raw response:", text);
                return { Success: false, Message: "Invalid JSON response", Raw: text };
            });
        });
    }).catch(function (error) {
        console.error("Error posting data:", error);
        return null;
    });
};


window.postDataV2 = async function (url, data){
    console.clear();
    try {
        const response = await fetch(url, {
            method: 'POST',
            body: data
        });


        const json = await response.json(); // Parse response regardless of HTTP status

        if (!response.ok || json.Success === false) {
            // Server returned JSON error
            console.error("Server error:", json.Message || "Unknown error");
            return json; // or return null or throw if needed
        }

        return json;
    } catch (error) {
        console.error("Error posting data:", error);
        return null;
    }
};

// =======================
// POST PARTIAL VIEW DATA (TEXT RESPONSE)
// =======================
window.postPartialData = function (url, data) {
    return fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    }).then(function (response) {
        if (!response.ok) throw new Error("HTTP error! Status: " + response.status);
        return response.text();
    }).catch(function (error) {
        console.error("Error posting data:", error);
        return null;
    });
};

// =======================
// GET USER INFORMATION
// =======================
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
            return null;
        }
        return res.json();
    }).then(function (result) {
        if (result && result.success) {
            return result;
        } else if (result) {
            console.warn(result.message || "Unknown error");
        }
        return null;
    });
};

// =======================
// REFRESH TOKEN
// =======================
function refreshAccessToken() {
    var logout = localStorage.getItem('Logout');
    var refreshToken = localStorage.getItem('refreshToken');
    if (!refreshToken) return Promise.resolve(false);

    return fetch("/User/RefreshToken", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ refreshToken: refreshToken })
    }).then(function (response) {
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
    }).catch(function (error) {
        console.error("Error refreshing token:", error);
        return false;
    });
}

// =======================
// ACTION RESTRICT
// =======================
window.ActionRestrict = function () {
    var userRole = localStorage.getItem("UserRole");
    if (userRole === "Leader" || userRole === "Users") {
        return false;
    }
};

// =======================
// IS LOGIN USER
// =======================
window.IsLoginUser = function (options) {
    options = options || {};
    var storageKey = options.storageKey || 'isLoggedInPatrol';
    var expectedValue = options.expectedValue || 'true';
    var redirectUrl = options.redirectUrl || '/P1SA/PMACS/Mainpage';
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

// =======================
// LOGOUT
// =======================
window.logout = function () {
    var logout = localStorage.getItem('Logout');
    return fetch("/User/Logout", { method: "POST" })
        .then(function () {
            localStorage.clear();
            window.location.href = logout;
        }).catch(function (error) {
            console.error("Logout failed:", error);
        });
};

// =======================
// RESTRICT CHARS
// =======================
window.restrictChars = function (e) {
    var x = e.which || e.keyCode;
    return (x >= 48 && x <= 57) || x === 46;
};

// =======================
// UTILITIES
// =======================
function getMonthString(month) {
    var map = {
        "MonthUpload": "MonthUpload",
        "Total": "Total",
        "1": "Jan", "2": "Feb", "3": "Mar", "4": "Apr", "5": "May", "6": "Jun",
        "7": "Jul", "8": "Aug", "9": "Sep", "10": "Oct", "11": "Nov", "12": "Dec"
    };
    return map[month] || "";
}

function getRowMonths(month) {
    var intmonth = parseInt(month, 10);
    var names = [
        "January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    ];
    return names[intmonth - 1] || "";
}

function getMonthInteger(strmonth) {
    var map = {
        "January": 1, "February": 2, "March": 3, "April": 4, "May": 5, "June": 6,
        "July": 7, "August": 8, "September": 9, "October": 10, "November": 11, "December": 12
    };
    return map[strmonth] || 0;
}
