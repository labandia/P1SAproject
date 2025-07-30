
//GLOBAL GET FUNCTIONS WITH TOKEN AUTHENTICATION 
window.FetchAuthenticate = async (url, fdata) => {
    console.clear();
    var token = localStorage.getItem('accessToken');
    const queryString = new URLSearchParams(fdata).toString();
    const fullUrl = `${url}?${queryString}`;  // Append parameters to URL

    const makeRequest = async (tokenToUse) => {
        return await fetch(fullUrl, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + tokenToUse
            }
        });
    };


    try {
        let res = await makeRequest(token);

        if (res.status === 401) {
            const refreshSuccess = await refreshAccessToken();
            if (refreshSuccess) {
                // Retry with new token
                token = localStorage.getItem('accessToken');
                res = await makeRequest(token);

                if (res.status === 401) {
                    // Still unauthorized even after refresh
                    window.location.href = '/Error/Unauthorized';
                    return;
                }
            } else {
                // Redirect if refresh fails
                window.location.href = '/Error/Unauthorized';
                return;
            }
        }


        //if (!res.ok) {
        //    const errorData = await res.json().catch(() => ({}));
        //    return { success: false, status: res.status, error: errorData.message || 'Request failed' };
        //}

       
        const result = await res.json();
        return result;

    } catch (error) {
        console.error('Error fetching data:', error);
        // Optionally, handle the error (e.g., show a notification to the user)
    }
}
//GLOBAL GET FUNCTIONS WITHOUT TOKEN AUTHENTICATION 
window.fetchData = async (url, fdata = {}) => {
    console.clear();
    try {
        const queryString = new URLSearchParams(fdata).toString();
        const fullUrl = queryString ? `${url}?${queryString}` : url;

        const res = await fetch(fullUrl, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
    
        const result = await res.json();

        //if (!res.ok) {
        //    //console.warn(`Error ${res.status}: ${result.Message}`);
        //    return result;
        //}

        return result;
    } catch (error) {
        console.error('Fetch Error:', error);
        return null; // Return null on failure
    }
};
window.postData = async (url, data) => {
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
window.postrawData = async (url, data) => {
    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        let json;
        try {
            json = await response.json();
            return json;
        } catch (parseErr) {
            console.error("Error parsing JSON:", parseErr);
            const text = await response.text(); // fallback to raw text
            console.error("Raw response:", text);
            return { Success: false, Message: "Invalid JSON response", Raw: text };
        }

        if (!response.ok || json.Success === false) {
            console.error("Server error:", json.Message || "Unknown error");
            return json;
        }
    } catch (error) {
        console.error("Error posting data:", error);
        return null;
    }
};


window.PullUserInformation = async () => {
    const token = localStorage.getItem('accessToken');

    const res = await fetch('/User/GetUserInformation', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json'
        }
    });

    if (res.status === 401) {
        console.warn("Unauthorized");
        return;
    }

    const result = await res.json();

    if (result.success) {
        return result;
        console.log("User ID:", result.userId);
        console.log("Name:", result.name);
        console.log("Role:", result.role);
    } else {
        console.warn(result.message || "Unknown error");
    }
};



window.IsLoginUser = (options = {}) => {
    const {
        storageKey,
        expectedValue,
        redirectUrl,
        redirectIfLoggedInUrl,
        expirationKey,
        maxHours
    } = {
        storageKey: 'isLoggedInPatrol',
        expectedValue: 'true',
        redirectUrl: '/PC/Patrol/index',
        redirectIfLoggedInUrl: null,
        expirationKey: null,
        maxHours: null,
        ...options
    };

    const value = localStorage.getItem(storageKey);
    //console.log(`[IsLoginUser] storageKey = "${storageKey}", value = "${value}", expected = "${expectedValue}"`);

    // 1. Redirect if already logged in and trying to access login page
    if (value === expectedValue && redirectIfLoggedInUrl) {
        //console.log(`[IsLoginUser] Already logged in. Redirecting to: ${redirectIfLoggedInUrl}`);

        //window.location.href = redirectIfLoggedInUrl;

        if (expirationKey && maxHours) {
            const loginTimeStr = localStorage.getItem(expirationKey);
            //console.log(loginTimeStr);
            if (loginTimeStr) {
                const loginTime = new Date(loginTimeStr);
                const now = new Date();
                const diffHours = Math.abs(now - loginTime) / 36e5;
                //console.log(`[IsLoginUser] Time since login: ${diffHours.toFixed(2)}h (max: ${maxHours})`);

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

    //// 2. Redirect if not logged in
    if (value !== expectedValue) {
        //console.log(`[IsLoginUser] Not logged in or invalid value. Redirecting to: ${redirectUrl}`);
        const currentPath = window.location.pathname;
        const shouldRedirect = currentPath !== redirectUrl;

        if (shouldRedirect) {
            //console.log(`[IsLoginUser] Redirecting to: ${redirectUrl}`);

            if (typeof ActionRestrict === 'function') {
                const allowed = ActionRestrict();
                //console.log(`[IsLoginUser] ActionRestrict returned: ${allowed}`);
                if (allowed !== false) {
                    window.location.href = redirectUrl;
                    return;
                }
            } else {
                window.location.href = redirectUrl;
                return;
            }
        } else {
            //console.log(`[IsLoginUser] Already on redirectUrl (${redirectUrl}). No redirect to prevent infinite loop.`);
        }

        return;
    }

    // 3. Check expiration if still logged in
    if (expirationKey && maxHours) {
        const loginTimeStr = localStorage.getItem(expirationKey);
        const currentPath = window.location.pathname;
        const targetPath = new URL(redirectUrl, window.location.origin).pathname;

        if (loginTimeStr) {
            const loginTime = new Date(loginTimeStr);
            const now = new Date();
            const diffHours = Math.abs(now - loginTime) / 36e5;
            //console.log(`[IsLoginUser] Time since login: ${diffHours.toFixed(2)}h`);

            if (diffHours > maxHours) {
                //console.log(`[IsLoginUser] Session expired. Logging out...`);
                localStorage.clear();

                if (currentPath !== targetPath) {
                    window.location.href = redirectUrl;
                } else {
                    //console.log(`[IsLoginUser] Already on redirectUrl. Avoiding infinite loop.`);
                }
            }
        } else {
            //console.log(`[IsLoginUser] No expiration key found. Logging out...`);
            localStorage.clear();

            if (currentPath !== targetPath) {
                window.location.href = redirectUrl;
            } else {
                //console.log(`[IsLoginUser] Already on redirectUrl. Avoiding infinite loop.`);
            }
        }
    }


    //console.log(`[IsLoginUser] Access granted. No redirect.`);
};





async function refreshAccessToken() {
    const logout = localStorage.getItem('Logout');
    const refreshToken = localStorage.getItem('refreshToken');
    if (!refreshToken) return false;

    try {
        const response = await fetch("/User/RefreshToken", {
            method: "POST",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ refreshToken: refreshToken })
        });
        console.warn("Refresh token");
        const result = await response.json();
        if (response.ok && result.accessToken) {
            localStorage.setItem("accessToken", data.accessToken);
        } else {
            console.warn("Refresh token failed. Logging out...");
            window.location.href = logout;
        }

        //const data = await response.json();

        //if (data.success) {
        //    localStorage.setItem("accessToken", data.accessToken);
        //} else {
        //    console.warn("Refresh token failed. Logging out...");
        //    window.location.href = logout;
        //}
    } catch (error) {
        console.error("Error refreshing token:", error);
        return false;
        // logout();
    }
}
window.logout = async () => {
    const logout = localStorage.getItem('Logout');
    try {
        //await fetch("/User/Logout", { method: "POST" });
        localStorage.clear();
        window.location.href = logout;
    } catch (error) {
        console.error("Logout failed:", error);
    }
}
// Restriction of typing characters
window.restrictChars = (e) => {
    var x = e.which || e.keycode;
    if ((x >= 48 && x <= 57) || x == 46) {
        return true;
    } else {
        return false;
    }
}


