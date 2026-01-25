


window.formatJsonDate = function (value) {
    if (!value) return "";

    const ms = parseInt(value.replace("/Date(", "").replace(")/", ""));
    if (isNaN(ms)) return "";

    const date = new Date(ms);
    return date.toLocaleDateString();
};