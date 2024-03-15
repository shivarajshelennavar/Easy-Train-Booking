$("form").submit(function (event) {
    // Prevent the default form submission
    event.preventDefault();

    // Get form data and create an object
    var formData = {
        Id: $("#trainId").val(),
        TrainNumber: $("#trainNumber").val(),
        FromLoc: $("#departurePlace").val(),
        ToLoc: $("#arrivalPlace").val(),
        AvailableWindowSeat: $("#windowSeats").val(),
        AvailableNormalSeat: $("#normalSeats").val(),
        WindowSeatPrice: $("#windowSeatPrice").val(),
        NormalSeatPrice: $("#normalSeatPrice").val(),
        AvailabiltyStatus: $("#status").val()
    };

    //ajax call
    $.ajax({
        url: '/Service/addTrain',
        type: 'POST',
        data: JSON.stringify(formData),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: (res) => {
            if (res) {
                alert("Updated successfully!");
                window.location = "/Admin/UpdateTrains";
            } else {
                alert("Train number is already there");
            }
        },
        error: () => alert("error")
    })
});