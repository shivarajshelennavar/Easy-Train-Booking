function cancelTrain(id) {

    $.ajax({
        url: '/Service/cancelTrain',
        type: 'GET',
        data: { id: id },
        success: (res) => {
            if (res) {
                alert("Cancelled Successfully!!");
                window.location = '/User/CancelTrains'
            }
            else {
                alert("Something went wrong");
            }
        },
        error: () => alert("error")
    })
}