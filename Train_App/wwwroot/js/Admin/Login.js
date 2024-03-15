$("#form").on("submit", (e) => {
    e.preventDefault();

    var id = $("#inputId").val();
    var pass = $("#inputPassword").val();

    $.ajax({
        url: '/Service/loginAdmin',
        type: 'GET',
        data: { id: id, password: pass },
        success: (res) => {
            if (res) {
                alert("Login Successfull!!");
                window.location = "/Admin/Index";
            } else {
                alert("Incorrect Credentials");
            }
        },
        error: () => alert("error")
    })
})