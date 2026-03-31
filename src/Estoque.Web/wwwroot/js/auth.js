window.codexAuth = {
    getToken: function (tokenKey) {
        return window.localStorage.getItem(tokenKey)
            ?? window.sessionStorage.getItem(tokenKey);
    },

    saveToken: function (tokenKey, tokenValue) {
        window.localStorage.setItem(tokenKey, tokenValue);
        window.sessionStorage.setItem(tokenKey, tokenValue);
    },

    removeToken: function (tokenKey) {
        window.localStorage.removeItem(tokenKey);
        window.sessionStorage.removeItem(tokenKey);
    },

    clearSessionAndRedirect: function (tokenKey, redirectUrl) {
        window.localStorage.removeItem(tokenKey);
        window.sessionStorage.removeItem(tokenKey);
        window.location.replace(redirectUrl || "/");
    }
};
