$("#form").on("submit", (e) => {
    e.preventDefault();

    var phone = $("#inputNumber").val();
    var pass = $("#inputPassword").val();

    console.log(phone)

    $.ajax({
        url: '/Service/loginUser',
        type: 'GET',
        data: { phone: phone, pass: pass },
        success: (res) => {
            if (res) {
                alert("Login Successfull!")
                window.location = '/User/Index';
            } else {
                alert("Incorrect credentials")
            }
        },
        error: () => alert("error")
    })
})