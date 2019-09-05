"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/tastinghub").build();

document.getElementById("participantButton").disabled = true;

connection.on("RemoveParticipant", function (name) {
    document.getElementById(name).remove();
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
    var nameHead = document.createElement("h4");
    nameHead.className = "card-title";
    nameHead.id = "part-name";
    nameHead.textContent = newName;
    cardBody.appendChild(nameHead);
    var emailPart = document.createElement("p");
    emailPart.id = "email";
    emailPart.textContent = newEmail;
    cardBody.appendChild(emailPart);
    var link = document.createElement("input");
    link.className = "button-primary";
    link.setAttribute("type", "button");
    link.id = name;
    link.setAttribute("value", "Remove participant");
    cardBody.appendChild(link);
    document.getElementById("participantList").appendChild(wrapper);
    link.onclick = function () {
        removePart(link)
    }

    document.getElementById("namePart").value = "";
    document.getElementById("emailPart").value = "";
});

connection.on("ListUsers", function (users){
    let dataList = document.querySelector("#user-list");
    let objects = JSON.parse(users);
    objects.forEach(el => {
        let opt = document.createElement("option");
        opt.textContent = el.UserName;
        dataList.appendChild(opt);
    });
});

connection.start().then(function () {
    document.getElementById("participantButton").disabled = false;
    setupGroup();
    gettingUsers();
}).catch(function (err) {
    return console.error(err.toString());
    });

function removePart(link) {
    var name = link.id;
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
});

document.querySelector("#invitationSendButton").addEventListener("click", function (event){
    let user = document.querySelector("#user-list-input").value;
    let tasting = document.getElementById("tastingId").value;
    connection.invoke("AddUserInvitation", user, tasting).catch(function (err){
        return console.error(err.toString());
    });
    event.preventDefault();
});

function setupGroup() {
    var tastingId = document.getElementById("tastingId").value;
    connection.invoke("CreateRoom", tastingId).catch(function (err) {
        return console.error(err.toString());
    });
};

function gettingUsers(){
    connection.invoke("GetUsers").catch(function (err){
        return console.error(err.toString());
    });
};