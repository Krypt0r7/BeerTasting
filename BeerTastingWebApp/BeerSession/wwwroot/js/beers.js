"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/tastinghub").build();

document.getElementById("beerbutton").disabled = true;

connection.on("removeBeer", function (id) {
    let cards = Array.from(document.querySelectorAll(".card"));
    let selectedCard = cards.filter(x => x.dataset.cardid == id);
    document.querySelector("#beer-list").removeChild(selectedCard[0]);
    // document.getElementById(id).remove();
});

connection.on("populateProducers", function (producers) {
    const array = JSON.parse(producers);
    const producerDatalist = document.getElementById("producers");
    populateProducerList(producerDatalist, array);
});

connection.on("populateBeers", function (beers) {
    var array = JSON.parse(beers);
    listTheBeers(array);
});

connection.on("populateWithBeer", function (beer) {
    var array = JSON.parse(beer);
    populateInputFields(array);
});

connection.on("getBeer", function (name, producer, country, price, sysnum, alcohol, id, image) {

    let wrapper = document.createElement("div");
    wrapper.className = "card";
    wrapper.dataset.cardid = id;
    let theImage = document.createElement("img");
    if (image != null) {
        theImage.src = image;
    }
    wrapper.appendChild(theImage);
    let nameHead = document.createElement("h4");
    nameHead.id = "beer-name";
    nameHead.textContent = name;
    wrapper.appendChild(nameHead);
    let subhead = document.createElement("p");
    subhead.innerHTML = producer + " - " + country;
    wrapper.appendChild(subhead);
    let priceAlco = document.createElement("p");
    priceAlco.innerHTML = price + " kr | " + alcohol + "%";
    wrapper.appendChild(priceAlco);
    let link = document.createElement("input");
    link.classList = "button-primary card-button";
    link.setAttribute("type", "button");
    link.setAttribute("value", "Remove beer");
    link.id = id;
    wrapper.appendChild(link);
    document.getElementById("beer-list").appendChild(wrapper);
    link.onclick = function () {
        removeTheBeer(link)
    }

    document.querySelector("#beer-input").value = "";
    document.querySelector("#producer-input").value = "";
    document.querySelector("#systemet-modal").style.display = "none";    
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
    const producerDatalist = document.querySelector("#producers");
    if (producerDatalist.childNodes.length == 0) {
        connection.invoke("GetMeSomeProducers").catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    }
    showModal();
});

document.getElementById("producer-button").addEventListener("click", function (event) {
    var prodName = document.querySelector("#producer-input").value;
    connection.invoke("GetALlBeersFromProducer", prodName).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});


document.querySelector("#beer-chooser-button").addEventListener("click", function (event) {
    let inputValue = document.getElementById("beer-input").value.split(',');
    let tastingId = document.getElementById("tastingId").value;
    let beerId = inputValue[0];
    connection.invoke("GetBeerInfo", beerId, tastingId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
