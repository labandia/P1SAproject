
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



// =======================
// ACTION RESTRICT
// =======================
window.ActionButtons = function () {
    var userRole = localStorage.getItem("UserRole");
    if (userRole === "Leader" || userRole === "Users") {
        return false;
    }
};









window.postDataV2 = async (url, data, options = {}) => {
    const {
        headers = {},
        contentType = null,
        throwOnError = false,
        includeCredentials = false,
        timeout = 30000,
        responseType = 'json'
    } = options;

    // Configuration object
    const config = {
        method: 'POST',
        headers: { ...headers },
        credentials: includeCredentials ? 'include' : 'same-origin',
        signal: AbortSignal.timeout ? AbortSignal.timeout(timeout) : createTimeoutSignal(timeout)
    };

    // Handle different data types
    if (data instanceof FormData) {
        // FormData - let the browser set the Content-Type with boundary
        config.body = data;
    } else if (typeof data === 'object' && data !== null) {
        // Object data - stringify as JSON
        config.body = JSON.stringify(data);
        config.headers['Content-Type'] = contentType || 'application/json';
    } else if (typeof data === 'string') {
        // String data - assume it's already formatted
        config.body = data;
        config.headers['Content-Type'] = contentType || 'application/json';
    } else if (data !== undefined && data !== null) {
        // Other data types (ArrayBuffer, Blob, URLSearchParams, etc.)
        config.body = data;
        if (contentType) {
            config.headers['Content-Type'] = contentType;
        }
    }

    try {
        const response = await fetch(url, config);

        // Check if response is OK
        if (!response.ok) {
            const error = new Error(`HTTP ${response.status}: ${response.statusText}`);
            error.status = response.status;
            error.response = response;

            if (throwOnError) {
                throw error;
            }

            console.error(`HTTP error ${response.status} for ${url}:`, response.statusText);
        }

        // Parse response based on requested type
        let responseData;
        switch (responseType) {
            case 'text':
                responseData = await response.text();
                break;
            case 'blob':
                responseData = await response.blob();
                break;
            case 'arraybuffer':
                responseData = await response.arrayBuffer();
                break;
            case 'formdata':
                responseData = await response.formData();
                break;
            case 'json':
            default:
                try {
                    responseData = await response.json();
                } catch (e) {
                    // If JSON parsing fails, try to get text for debugging
                    const text = await response.text();
                    console.warn('Response is not valid JSON, received:', text.substring(0, 200));
                    responseData = { rawText: text };
                }
        }

        // Check for custom success flag if response is an object
        if (typeof responseData === 'object' && responseData !== null) {
            if (responseData.Success === false) {
                const error = new Error(responseData.Message || 'Operation failed');
                error.data = responseData;
                error.response = response;

                if (throwOnError) {
                    throw error;
                }

                console.error("Server reported failure:", responseData.Message || "Unknown error");
            }
        }

        return responseData;
    } catch (error) {
        // Handle timeout
        if (error.name === 'TimeoutError' || error.name === 'AbortError') {
            console.error(`Request timeout for ${url} after ${timeout}ms`);
            error.message = `Request timed out after ${timeout}ms`;
        }

        // Handle network errors
        if (error.name === 'TypeError' && error.message.includes('fetch')) {
            console.error('Network error or CORS issue:', error.message);
        }

        // Re-throw if throwOnError is true
        if (throwOnError) {
            throw error;
        }

        // Return structured error object
        return {
            Success: false,
            Error: true,
            Message: error.message || 'Request failed',
            Status: error.status,
            OriginalError: error.name
        };
    }
};




// Fallback timeout function for browsers without AbortSignal.timeout
function createTimeoutSignal(timeout) {
    const controller = new AbortController();
    setTimeout(() => controller.abort(new DOMException('Request timed out', 'TimeoutError')), timeout);
    return controller.signal;
}