// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function listTheBeers(array) {
    var listOfBeer = document.querySelector("#beer-listing");
    listOfBeer.innerHTML = "";
    document.querySelector("#beer-chooser").classList.remove("no-show");

    array.forEach(x => {
        let option = document.createElement("option");
        option.value = x;
        listOfBeer.appendChild(option)
    });
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


function goBack() {
    window.history.back();
}

function populateProducerList(dataList, array){
    array.forEach(x => {
        let option = document.createElement("option");
        option.textContent = x;
        dataList.appendChild(option);
    });
}

function showModal(){
    let modal = document.querySelector("#systemet-modal");
    let span = document.querySelector(".close");
    modal.style.display = "block";
    document.querySelector("#producer-input").focus();

    window.onclick = event =>{
        if (event.target === modal) {
            modal.style.display = "none";
        }
    }

    span.onclick = () =>{
        modal.style.display = "none";
    }
}

function openTab(event, name) {
    let tabContent = document.querySelectorAll(".tabContent");
    for (let i = 0; i < tabContent.length; i++) {
        tabContent[i].style.display = "none"
    }

    let links = document.querySelectorAll(".tablinks");
    for (let i = 0; i < links.length; i++) {
        links[i].classList.remove("active");
    }

    document.getElementById(name).style.display = "block";
    event.currentTarget.classList.add("active");
}

