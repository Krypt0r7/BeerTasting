"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/tastinghub").build();

document.getElementById("participantButton").disabled = true;

connection.on("GetParticipant", function (name, email) {
    var newName = name.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var newEmail = email.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

    var encodePart = newName + "with email" + newEmail;
    var li = document.createElement("li");
    li.textContent = encodePart;

    document.getElementById("participants").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("participantButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
    });

document.getElementById("participantButton").addEventListener("click", function (event) {
    var name = document.getElementById("namePart").value;
    var email = document.getElementById("emailPart").value;
    var tasting = document.getElementById("tastingId").value;
    connection.invoke("NewParticipant", name, email, tasting).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
})