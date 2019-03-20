import Promise from 'promise-polyfill';

const apiGetRequest = (endpoint, params) => {

    if (params === undefined) {
        params = {};
    }

    return new Promise((resolve, reject) => {
        const keyValuePairs = Object.keys(params).map(
            (key) => key + '=' + encodeURIComponent(params[key])
        );
        const paramsStr = keyValuePairs.join('&');
        const path = endpoint + "?" + paramsStr;

        const request = new XMLHttpRequest();
        request.open("GET", path);

        request.onload = () => {

            //ViewLoadingScreen(false);
            if (request.status === 200) {
                resolve(JSON.parse(request.responseText));
            } else {
                console.warn('Error in request. Status: ' + request.status);
                console.warn('Response text: ' + request.responseText);

                let errors = new Array();
                if(request.getResponseHeader('content-type') === "application/json") {
                    errors = JSON.parse(request.responseText).errors;
                }

                reject(new Error());
            }
        };

        request.send();
    });
};

export default apiGetRequest;
