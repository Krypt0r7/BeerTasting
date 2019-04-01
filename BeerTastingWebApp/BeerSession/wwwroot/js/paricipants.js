"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/tastinghub").build();

document.getElementById("participantButton").disabled = true;

connection.on("RemoveParticipant", function () {
    
});

connection.on("GetParticipant", function (name, email) {
    var newName = name.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var newEmail = email.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

    var wrapper = document.createElement("div");
    wrapper.id = newName;
    wrapper.className = "card participant-card";
    var cardBody = document.createElement("div");
    cardBody.className = "card-body";
    wrapper.appendChild(cardBody)
    var nameHead = document.createElement("h5");
    nameHead.className = "card-title";
    nameHead.id = "part-name";
    nameHead.textContent = newName;
    cardBody.appendChild(nameHead);
    var emailHidden = document.createElement("input");
    emailHidden.id = "email";
    emailHidden.setAttribute("type", "hidden");
    emailHidden.setAttribute("value", newEmail);
    cardBody.appendChild(emailHidden);
    var link = document.createElement("input");
    link.className = "btn btn-default";
    link.setAttribute("type", "button");
    link.id = name;
    link.setAttribute("value", "Remove participant");
    cardBody.appendChild(link);
    document.getElementById("participantList").appendChild(wrapper);
    link.onclick = removePart;
});

connection.start().then(function () {
    document.getElementById("participantButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
    });

function removePart() {
    var name = this.id;
    var tastingId = document.getElementById("tastingId").value;
    connection.invoke("RemoveThePart", name, tastingId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
}

document.getElementById("participantButton").addEventListener("click", function (event) {
    var name = document.getElementById("namePart").value;
    var email = document.getElementById("emailPart").value;
    var tasting = document.getElementById("tastingId").value;
    connection.invoke("NewParticipant", name, email, tasting).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
})

