


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
















// ###################### UTILITIES ###########################################

// DISABLE INPUT TEXT WHEN TYPING
const numberInputs = document.querySelectorAll('.num');
numberInputs.forEach(function (input) {
    // Add event listener for keypress
    input.addEventListener('keypress', function (event) {
        // Get the value of the input field
        // Prevent default behavior if max length is reached
        if (this.value.length >= 8 && event.key !== 'Backspace') {
            event.preventDefault();
            return;
        }



        const value = this.value + String.fromCharCode(event.keyCode);

        // Define regular expression to match only numbers and decimal points
        const regex = /^[0-9]*\.?[0-9]*$/;

        // Test the value against the regular expression
        if (!regex.test(value)) {
            // Prevent default behavior (typing the character)
            event.preventDefault();
        }
    });
});


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


