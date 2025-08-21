
//GLOBAL GET FUNCTIONS WITH TOKEN AUTHENTICATION 
window.FetchAuthenticate = async (url, fdata) => {
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
        const logout = localStorage.getItem('Logout');
        if (res.status === 401) {
            const refreshSuccess = await refreshAccessToken();
            if (refreshSuccess) {
                // Retry with new token
                token = localStorage.getItem('accessToken');
                res = await makeRequest(token);
                if (res.status === 401) {
                    localStorage.clear();
                    window.location.href = logout;
                    return;
                }

                // Still 401 after refresh attempt
                if (res.status === 401) {
                    //localStorage.clear();
                    if (logout) {
                        localStorage.clear();
                        window.location.href = logout;
                    }
                    return null;
                }
            } else {
                // Redirect if refresh fails
                localStorage.clear();
                window.location.href = logout;
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


window.postPartialData = async (url, data) => {
    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data) 
        });

        if (!response.ok) throw new Error(`HTTP error! Status: ${response.status}`);

        return await response.text(); 
    } catch (error) {
        console.error("Error posting data:", error);
        return null;
    }
};


async function refreshAccessToken() {
    const logout = localStorage.getItem('Logout');
    const refreshToken = localStorage.getItem('refreshToken');
    if (!refreshToken) return false;

    try {
        const response = await fetch("/User/RefreshToken", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ refreshToken })
        });
        const result = await response.json();
        if (response.ok && result.accessToken) {
            localStorage.setItem("accessToken", result.accessToken);
            return true;
        } else {
            console.warn("Refresh token failed. Logging out...");
            localStorage.clear();
            window.location.href = logout;
        }

    } catch (error) {
        console.error("Error refreshing token:", error);
        return false;
    }
}

window.ActionRestrict = () => {
    userRole = localStorage.getItem("UserRole");
    if (userRole === "Leader" || userRole === "Users") {
        return false;
    }
}


window.IsLoginUser = (options = {}) => {
    const {
        storageKey,
        expectedValue,
        redirectUrl,
        redirectIfLoggedInUrl,
        expirationKey,
        maxHours
    } = {
        storageKey: 'isLoggedInPMACS',
        expectedValue: 'true',
        redirectUrl: '/P1SA/PMACS/Mainpage',              
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



window.logout = async () => {
    const logout = localStorage.getItem('Logout');
    try {
        await fetch("/User/Logout", { method: "POST" });
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








function getMonthString(month) {
  
    if (month === "MonthUpload") {
        return "MonthUpload";
    } else if (month === "Total") {
        return "Total";
    } else if (month === "1") {
        return "Jan";
    } else if (month === "2") {
        return "Feb";
    } else if (month === "3") {
        return "Mar";
    } else if (month === "4") {
        return "Apr";
    } else if (month === "5") {
        return "May";
    } else if (month === "6") {
        return "Jun";
    } else if (month === "7") {
        return "Jul";
    } else if (month === "8") {
        return "Aug";
    } else if (month === "9") {
        return "Sep";
    } else if (month === "10") {
        return "Oct";
    }
    else if (month === "11") {
        return "Nov";
    } else {
        return "Dec";
    }
}



function getRowMonths(month) {
    let intmonth = parseInt(month);
    //switch (intmonth) {
    //    case 1: return "January";
    //    case 2: return "February";
    //    case 3: return "March";
    //    case 4: return "April";
    //    case 5: return "May";
    //    case 6: return "June";
    //    case 7: return "July";
    //    case 8: return "August";
    //    case 9: return "September";
    //    case 10: return "October";
    //    case 11: return "November";
    //    case 12: return "December";
    //}

    if (intmonth === 1) {
        return "January";
    } else if (intmonth === 2) {
        return "February";
    } else if (intmonth === 3) {
        return "March";
    } else if (intmonth === 4) {
        return "April";
    } else if (intmonth === 5) {
        return "May";
    } else if (intmonth === 6) {
        return "June";
    } else if (intmonth === 7) {
        return "July";
    } else if (intmonth === 8) {
        return "August";
    } else if (intmonth === 9) {
        return "September";
    } else if (intmonth === 10) {
        return "October";
    }
    else if (intmonth === 11) {
        return "November";
    } else {
        return "December";
    }
}


function getMonthInteger(strmonth){
    if (strmonth === "January") {
        return 1;
    } else if (strmonth === "February") {
        return 2;
    } else if (strmonth === "March") {
        return 3;
    } else if (strmonth === "April") {
        return 4;
    } else if (strmonth === "May") {
        return 5;
    } else if (strmonth === "June") {
        return 6;
    } else if (strmonth === "July") {
        return 7;
    } else if (strmonth === "August") {
        return 8;
    } else if (strmonth === "September") {
        return 9;
    } else if (strmonth === "October") {
        return 10;
    }
    else if (strmonth === "November") {
        return 11;
    } else {
        return 12;
    }
}






