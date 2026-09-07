export function restrictChars(e) {
    const x = e.which || e.keyCode;
    return (x >= 48 && x <= 57) || x === 46;
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