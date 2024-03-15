
var today = new Date().toISOString().split('T')[0];

document.getElementById("inputDate").value = today;

document.getElementById("inputDate").setAttribute("min", today);

function showCost(ns, ws, wp, np) {
    var seatType = $("#seatType").val();
    var seat = document.getElementById('enteredCount')
    var seat_count = seat.value;
    var avail_seat = -1
    var seat_price = -1

    if (seatType == 'Window') {
        avail_seat = parseInt(ws)
        seat_price = parseFloat(wp)
    }

    if (seatType == 'Normal') {
        avail_seat = parseInt(ns)

        seat_price = parseFloat(np)
    }
    
    var cost = document.querySelector(".cost");
    var btn = document.querySelector(".book");


    if (!seat.checkValidity()) {
        cost.classList.remove("text-success");
        cost.classList.add("text-danger");
        cost.textContent = "Please enter a postitive whole number greater than 0.";
        btn.disabled = true;
    } else if (seat_count <= 0) {
        cost.classList.remove("text-success");
        cost.classList.add("text-danger");
        cost.textContent = "Please enter a number greater than 0."
        btn.disabled = true;
    } else if (avail_seat < parseInt(seat_count) && avail_seat != -1) {
        cost.classList.remove("text-success");
        cost.classList.add("text-danger");
        cost.textContent = "These many Seats are not available."
        btn.disabled = true;
    } else {
        cost.classList.remove("text-danger");
        cost.classList.add("text-success");
        cost.textContent = "Total cost is : " + (parseInt(seat_count) * seat_price);
        btn.disabled = false;
    }
}

function bookTicket(trainNum) {

    var seatType = $("#seatType").val();
    var count = $("#enteredCount").val();
    var date = $("#inputDate").val();

    console.log({ Tnum: trainNum, seatType: seatType, count: count, date: date });

    $.ajax({
        url: '/Service/bookTicket',
        type: 'POST',
        data: { Tnum: trainNum, seatType: seatType, count: count, date: date },
        success: (res) => {
            if (res) {
                alert("Ticket booked!!");
                window.location = "/User/ShowTrains"
            } else {
                alert("You don't have enough balance")
            }
        },
        error: () => alert("error")
    })

}