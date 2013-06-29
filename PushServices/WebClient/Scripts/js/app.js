$(function (pushservices, $, undefined) {

    var endpoints = {
        apiUrl: "/api/artists/",
        serverUrl: "http://localhost:8080",
    };

    var artistsViewModel = function () {

        var artist = function (id, name) {
            var self = this;
            self.id = ko.observable(id);
            self.name = ko.observable(name);
        };

        var artists = ko.observableArray();

        var getData = function () {
            dataservices.getAll(endpoints.apiUrl).done(function (data) {
                artists.removeAll();
                $.each(data, function (i, a) {
                    artists.push(new artist(a.ArtistId, a.Name));
                });
            });
        };

        return {
            getData: getData,
            artists: artists,
        };
    }();

    // Apply Knockout binding
    artistsViewModel.getData();
    ko.applyBindings(artistsViewModel);

    // SignalR
    $.connection.hub.logging = true;
    
    var connection = $.hubConnection(endpoints.serverUrl);
    var proxy = connection.createHubProxy('monitor');

    proxy.on('refresh', function (artist) {
        console.log(artist);
        artistsViewModel.getData();
    });

    connection.start()
        .done(function () { console.log('connection started...'); })
        .fail(function () { console.log('connection failed...'); });

}(window.pushservices = window.pushservices || {}, jQuery));