// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function listTheBeers(array) {
    var listOfBeer = document.getElementById("inputGroupSelect01");
    clearListofBeers(listOfBeer);

    for (var i = 0; i < array.length; i++) {
        var option = document.createElement("option");
        option.value = array[i];
        option.innerHTML = array[i];
        listOfBeer.appendChild(option);
    }
}

function clearListofBeers(list) {
    $(list).empty();
}

function populateInputFields(array) {
    document.getElementById("beerName").value = array.namn + ", " + array.namn2;
    document.getElementById("producer").value = array.producent;
    document.getElementById("country").value = array.land;
    document.getElementById("price").value = array.prisInkMoms;
    document.getElementById("sysNumb").value = array.artikelId;
    document.getElementById("alcohol").value = array.alkoholhalt;
}

$("#myModal").on('hidden.bs.modal', function () {
    clearListofBeers(document.getElementById("inputGroupSelect01"));
    document.getElementById("prodInput").value = "";
})

function goBack() {
    window.history.back();
}