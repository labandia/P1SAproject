// Global months array
window.Months = [
    "January", "February", "March", "April",
    "May", "June", "July", "August",
    "September", "October", "November", "December"
];

// Global function to populate month dropdown
window.initMonthSelect = function (selectId, setCurrent = false) {
    const select = document.getElementById(selectId);
    if (!select) return;

    select.innerHTML = ""; // clear existing options

    Months.forEach((month, index) => {
        const option = document.createElement("option");
        option.value = index + 1;   // 1–12
        option.text = month;
        select.appendChild(option);
    });

    // Optional: auto-select current month
    if (setCurrent) {
        select.value = new Date().getMonth() + 1;
    }
};


window.formatJsonDate = function (value) {
    if (!value) return "";

    const ms = parseInt(value.replace("/Date(", "").replace(")/", ""));
    if (isNaN(ms)) return "";

    const date = new Date(ms);
    return date.toLocaleDateString();
};


window.Loadingsteps = function(loadingTextId, callback){
    const steps = [
        "Fetching data from the database...",
        "Processing data...",
        "Finalizing table view..."
    ];

    let stepIndex = 0;

    const interval = setInterval(() => {
        const stepText = document.getElementById(loadingTextId);
        if (stepText) {
            stepText.innerText = steps[stepIndex];
            stepIndex++;
        } else {
            clearInterval(interval); // in case it's removed
        }

        if (stepIndex >= steps.length) {
            clearInterval(interval);
            if (typeof callback === "function") callback();
        }
    }, 800);
}


// Global loading display function
window.loadingDisplay = function (tableBodyId, col) {
    const loadingTextId = `loadingStepText_${Date.now()}`;
    const $table = $("#" + tableBodyId);

    if ($table.length === 0) {
        console.warn(`Table element with ID '${tableBodyId}' not found.`);
        return null;
    }

    const loadData = `
        <tr class="loading-row">
            <td colspan="${col}">
                <div class="Loadercontainer">
                    <span class="loader"></span>
                    <p id="${loadingTextId}" style='margin: 0;'>
                        Fetching data from the database..
                    </p>
                </div>
            </td>
        </tr>
    `;

    // Optional: remove previous loading rows
    $table.find(".loading-row").remove();

    $table.append(loadData);

    return loadingTextId;
};




// MERGE THE ARRAY WITH THE SAME MODEL NAME VALUE
function mergeArrayModels(array, modelname) {
    const newData = array.filter(item => item.Modelname === modelname);

    if (newData.length < 2) {
        return newData;
    } else {
        const keys = Object.keys(newData[0]).filter(key => typeof newData[0][key] === 'number');

        const sums = newData.reduce((acc, item) => {
            keys.forEach(key => {
                acc[key] = (acc[key] || 0) + (item[key] !== undefined ? item[key] : 0);
            });
            return acc;
        }, {});

        const averages = {};
        keys.forEach(key => {
            averages[key] = sums[key] / newData.length;
        });


    
        return averages;

    }
}


















// FOR FORMATTING NUMBERS
function getMonthname() {
    // Array of month names
    var monthNames = ["January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"];
    // Get the current date
    var currentDate = new Date();
    var currentMonthIndex = currentDate.getMonth();

    var currentMonthName = monthNames[currentMonthIndex];

    return currentMonthName
}
function getFormatComma(num) {
    if (num === null || num === undefined) {
        num = 0; 
    }

    var numberWithCommas = num.toLocaleString('en-US', { maximumFractionDigits: 0 });
    return numberWithCommas
}
function formattedNumber(num) {
    if (num === null || num === undefined) {
        num = 0;
    }

    let newnum = num.toLocaleString('en-US', { maximumFractionDigits: 0 }).replace('.', '');
    return newnum;
}
function RoundNumber(num) {
    if (num === null || num === undefined) {
        num = 0; 
    }

    var roundedNum = parseFloat(num.toFixed(0));
    return roundedNum;
}
function RoundNumberOne(num) {
    if (num === null || num === undefined) {
        num = 0;
    }

    var roundedNum = parseFloat(num.toFixed(1));
    return roundedNum;
}


function RoundNumberthree(num) {
    if (num === null || num === undefined) {
        num = 0;
    }

    var roundedNum = parseFloat(num.toFixed(3));
    return roundedNum;
}




// FOR SORTING TABLES
document.addEventListener('DOMContentLoaded', () => {
    const getCellValue = (tr, idx) => tr.children[idx].innerText || tr.children[idx].textContent;

    const comparer = (idx, asc) => (a, b) => ((v1, v2) =>
        v1 !== '' && v2 !== '' && !isNaN(v1) && !isNaN(v2) ? v1 - v2 : v1.toString().localeCompare(v2)
    )(getCellValue(asc ? a : b, idx), getCellValue(asc ? b : a, idx));

    document.querySelectorAll('.sortable').forEach(th => th.addEventListener('click', (() => {
        const table = th.closest('table');
        const tbody = table.querySelector('tbody');
        Array.from(tbody.querySelectorAll('tr'))
            .sort(comparer(Array.from(th.parentNode.children).indexOf(th), this.asc = !this.asc))
            .forEach(tr => tbody.appendChild(tr));
    })));
});




function searchArray(array, key, value) {
    var founditem = null;
    array.forEach(function (item) {
        if (item[key] === parseInt(value)) {
            founditem = item;
        }
    });
    return founditem;
}



// NUMBERS ONLY WHEN TYPE
function restrictChars(e) {
    var x = e.which || e.keycode;
    if ((x >= 48 && x <= 57) || x == 46) {
        return true;
    } else {
        return false;
    }
}


