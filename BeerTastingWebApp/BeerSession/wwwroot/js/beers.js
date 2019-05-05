"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/tastinghub").build();

document.getElementById("beerbutton").disabled = true;

connection.on("removeBeer", function (id) {
    document.getElementById(id).remove();
});

connection.on("populateProducers", function (producers) {
    var array = JSON.parse(producers);
    autocomplete(document.getElementById("prodInput"), array);
});

connection.on("populateBeers", function (beers) {
    var array = JSON.parse(beers);
    listTheBeers(array);
});

connection.on("populateWithBeer", function (beer) {
    var array = JSON.parse(beer);
    populateInputFields(array);
});

connection.on("GetBeer", function (name, producer, country, price, sysnum, alcohol, id) {

    var wrapper = document.createElement("div");
    wrapper.id = name;
    wrapper.className = "card participant-card";
    var cardBody = document.createElement("div");
    cardBody.className = "card-body";
    wrapper.appendChild(cardBody)
    var nameHead = document.createElement("h5");
    nameHead.className = "card-title";
    nameHead.id = "beer-name";
    nameHead.textContent = name;
    cardBody.appendChild(nameHead);
    var subhead = document.createElement("p");
    subhead.innerHTML = producer + " - " + country;
    cardBody.appendChild(subhead);
    var priceAlco = document.createElement("p");
    priceAlco.innerHTML = price + " kr | " + alcohol + "%";
    cardBody.appendChild(priceAlco);
    var link = document.createElement("input");
    link.className = "btn btn-default";
    link.setAttribute("type", "button");
    link.setAttribute("value", "Remove beer");
    link.id = id;
    cardBody.appendChild(link);
    document.getElementById("beersList").appendChild(wrapper);
    link.onclick = function () {
        removeTheBeer(link)
    }
});

connection.start().then(function () {
    document.getElementById("beerbutton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

function removeTheBeer(link) {
    var id = link.id;
    var tastingId = document.getElementById("tastingId").value;
    connection.invoke("RemoveSelectedBeer", id, tastingId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
}

document.getElementById("beerbutton").addEventListener("click", function (event) {
    var sysnum = document.getElementById("sysNumb").value;
    var tastingId = document.getElementById("tastingId").value;

    connection.invoke("NewBeer", sysnum, tastingId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
})

document.getElementById("systemet-button").addEventListener("click", function (event) {
    connection.invoke("GetMeSomeProducers").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault;
});

document.getElementById("getBeersFromProd").addEventListener("click", function (event) {
    var prodName = document.getElementById("prodInput").value;
    connection.invoke("GetALlBeersFromProducer", prodName).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});


document.getElementById("beerChooseButton").addEventListener("click", function (event) {
    var inputValue = document.getElementById("inputGroupSelect01").value;
    var tastingId = document.getElementById("tastingId").value;
    connection.invoke("GetBeerInfo", inputValue, tastingId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
