import Promise from 'promise-polyfill';

const apiPostRequest = (endpoint, params) => {

    return new Promise((resolve, reject) => {
        const request = new XMLHttpRequest();

        request.open("POST", endpoint);
        request.setRequestHeader("Content-Type", "application/json");

        request.onload = () => {
            if (request.status >= 200 && request.status < 300) {
                resolve(JSON.parse(request.responseText));
            } else {
                let errors = [];
                if(request.getResponseHeader('content-type') === "application/json") {
                    errors = JSON.parse(request.responseText).errors;
                } 

                reject({statusCode: request.status, errors: errors});
            }
        };
        request.send(JSON.stringify(params));
    });
};

export default apiPostRequest;