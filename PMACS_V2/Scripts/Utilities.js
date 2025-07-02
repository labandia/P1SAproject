//GLOBAL GET FUNCTIONS WITH TOKEN AUTHENTICATION 
window.FetchAuthenticate = async (url, fdata) => {
    var token = localStorage.getItem('accessToken');
    // Convert parameters to a query string
    const queryString = new URLSearchParams(fdata).toString();
    const fullUrl = `${url}?${queryString}`;  // Append parameters to URL

    const makeRequest = async (tokenToUse) => {
        return await fetch(fullUrl, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + tokenToUse
            },
            dataType: 'json'
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
            } else {
                // Redirect if refresh fails
                window.location.href = '/Error/Unauthorized';
                return;
            }

            // Token expired or unauthorized, refresh the token
            //await refreshAccessToken();
            // Retry the original request
            //return FetchAuthenticate(url, fdata);
            //window.location.href = '/Error/Unauthorized';
        }


        const result = await res.json();

        //if (!res.ok) {
        //    //console.warn(`Error ${res.status}: ${result.Message}`);
        //    return result;
        //}

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
            body: data // Send FormData directly
        });

        if (!response.ok) throw new Error(`HTTP error! Status: ${response.status}`);

        return await response.json();
    } catch (error) {
        console.error("Error posting data:", error);
        return null;
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
    const refreshToken = localStorage.getItem("refreshToken");

    try {
        const response = await fetch("/User/RefreshToken", {
            method: "POST",
            headers: { "Content-Type": "application/x-www-form-urlencoded" },
            body: `_refreshToken=${refreshToken}`
        });

        const data = await response.json();

        if (data.success) {
            localStorage.setItem("accessToken", data.accessToken);
        } else {
            console.warn("Refresh token failed. Logging out...");
            window.location.href = logout;
        }
    } catch (error) {
        console.error("Error refreshing token:", error);
        // logout();
    }
}

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