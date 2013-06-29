var dataservices = (function () {

    var http = {
        GET: "get",
        PUT: "put",
        POST: "post",
        DELETE: "delete"
    };

    var data = {
        JSON: "json",
        XML: "xml",
        HTML: "html"
    };

    var contentType = {
        JSON: "application/json; charset=utf-8"
    };

    function handleError(error) {
        console.log("Error in dataservices: " + error.message);
    };

    return {
        create: function (service, item) {
            console.log("Dataservices: create: " + service);
            return $.ajax({
                url: service,
                type: http.POST,
                data: item,
                contentType: contentType.JSON,
                dataType: data.JSON
            }).fail(function (error) {
                handleError(error);
            });
        },

        update: function (service, item) {
            console.log("Dataservices: update: " + service + item);
            return $.ajax({
                url: service,
                type: http.PUT,
                dataType: data.JSON,
                contentType: contentType.JSON,
                data: item
            }).fail(function (error) {
                handleError(error);
            });
        },

        getAll: function (service) {
            console.log("Dataservices: getAll: " + service);
            return $.ajax({
                url: service,
                type: http.GET,
                dataType: data.JSON
            }).fail(function (error) {
                handleError(error);
            });
        }
    };
})();