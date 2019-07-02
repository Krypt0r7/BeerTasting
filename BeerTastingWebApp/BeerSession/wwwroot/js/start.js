"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/tastinghub").build();

connection.start()
    .catch(function (err) {
        return console.error(err.toString());
});
