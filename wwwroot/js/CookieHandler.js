function setCookie(key, value) {
    let expirationDate = new Date();
    expirationDate.setDate(expirationDate.getDate() + 1);
    document.cookie = key + "=" + value + ";expires=" + expirationDate.toUTCString();
}

function getCookie(key) {
    let cookies = document.cookie.split(";");
    for (let i = 0; i < cookies.length; i++) {
        let cookie = cookies[i].split("=");
        if (cookie[0].trim() == key) {
            return cookie[1];
        }
    }
    return null;
}

function deleteCookie(key) {
    let expirationDate = new Date();
    expirationDate.setDate(expirationDate.getDate() - 1);
    document.cookie = key + "=;expires=" + expirationDate.toUTCString();
}