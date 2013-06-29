http = {
    GET: "get",
    PUT: "put",
    POST: "post",
    DELETE: "delete"
};

data = {
    JSON: "json",
    XML: "xml",
    HTML: "html"
};

contentType = {
    JSON: "application/json; charset=utf-8"
};

var dataservices = (function () {

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
        }
    };
})();


$(function (datagrid, $, undefined) {

    var getBaseUrl = function () {
        return 'http://localhost:49250/';
    };

    var endpoints = {
        customerapi: getBaseUrl() + 'api/customerapi',
    };

    // Declare a proxy to reference the hub. 
    var customerHubProxy = $.connection.customerHub;

    // Create a function that the hub can call to broadcast messages.
    customerHubProxy.client.broadcastMessage = function (customer) {
        console.log('received message:' + customer.Id);
        var tr = $("tr[data-edit-row-id='" + customer.Id + "']");
        tr.effect("highlight", { color: 'red' }, 3000);
        tr.find('td.firstname').html(customer.FirstName);
        tr.find('td.surname').html(customer.Surname);
        tr.find('td.housenumber').html(customer.HouseNumber);
        tr.find('td.street').html(customer.Street);
        tr.find('td.town').html(customer.Town);
        tr.find('td.postcode').html(customer.postcode);
    };
    
    $.connection.hub.start().done(function () {
        $('.edit').on('click', function () {
            var tr = $(this).closest('tr');
            var i = $(this).find('i');
            if (i.attr('class') == 'icon-edit') {
                i.removeClass('icon-edit').addClass('icon-ok');
                tr.addClass('info').css('font-weight', 'bold').css('font-style', 'italic');
                tr.find('.editable').each(function () {
                    $(this).attr('contenteditable', 'true');
                });
            } else {
                i.removeClass('icon-ok').addClass('icon-edit');
                tr.removeClass('info').css('font-weight', 'normal').css('font-style', 'normal');
                tr.find('.editable').each(function () {
                    $(this).attr('contenteditable', 'false');
                });

                var customer = {
                    Id: tr.data('edit-row-id'),
                    FirstName: tr.find('.firstname').html().trim(),
                    Surname: tr.find('.surname').html().trim(),
                    HouseNumber: tr.find('.housenumber').html().trim(),
                    Street: tr.find('.street').html().trim(),
                    Town: tr.find('.town').html().trim(),
                    PostCode: tr.find('.postcode').html().trim(),
                };

                dataservices.update(endpoints.customerapi, JSON.stringify(customer));
                customerHubProxy.server.send(customer).done(function () {
                    console.log('update complete');
                });
            }
        });
    });

}(window.datagrid = window.datagrid || {}, jQuery));