function deleteProduct(id) {

    $.ajax({
        url: '/Service/deleteTrain',
        type: 'GET',
        data: { id: id },
        success: (res) => {
            if (res) {
                alert("Train deleted successfully");
                window.location = "/Admin/UpdateTrains";
            } else {
                alert("something went wrong");
            }
        },
        error: () => alert("error")
    })
}