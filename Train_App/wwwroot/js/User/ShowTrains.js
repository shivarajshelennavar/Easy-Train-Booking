function filterTrains() {
    var selectedFrom = $("#fromDropdown").val().toLowerCase();
    var selectedTo = $("#toDropdown").val().toLowerCase();

    var trains = document.getElementsByClassName("train");
    var from = document.getElementsByClassName("from");
    var to = document.getElementsByClassName("to");

    for (var i = 0; i < trains.length; i++) {

        var showtrains = (selectedFrom == 'all' || selectedFrom == from[i].textContent.toLowerCase()) && (selectedTo == "all" || selectedTo == to[i].textContent.toLowerCase());

        trains[i].style.display = showtrains ? "" : "none";
    }
}

// Attach change event to 'From' and 'To' dropdowns
$("#fromDropdown, #toDropdown").change(function () {
    filterTrains();
});