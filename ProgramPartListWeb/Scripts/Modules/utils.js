export function restrictChars(e) {
    const x = e.which || e.keyCode;
    return (x >= 48 && x <= 57) || x === 46;
}


// FORMAT DATE CHANGE 
export function formatDate(dateString) {
    if (!dateString) return '';

    const match = String(dateString).match(/\d+/);
    if (!match) return '';

    const timestamp = Number(match[0]);
    const date = new Date(timestamp);

    if (isNaN(date.getTime())) return '';

    return date.toLocaleDateString('en-US');
}

export function formatJsonDate(value) {
    if (!value) return "";
    const ms = parseInt(value.replace("/Date(", "").replace(")/", ""));
    if (isNaN(ms)) return "";
    return new Date(ms).toLocaleDateString();
}

export function actionButtonsRestricted() {
    const role = localStorage.getItem("UserRole");
    return role === "Leader" || role === "Users";
}

export function getDepartmentName(id) {
    switch (Number(id)) {
        case 1: return 'Molding';
        case 2: return 'Press';
        case 3: return 'Rotor';
        case 4: return 'Winding';
        case 5: return 'Circuit';
        case 6: return 'Process Control';
        default: return 'Unknown Department';
    }
}