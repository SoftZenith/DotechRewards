$(document).ready(function () {

    $("form").submit(function (event) {
        if ($('#password1').val() == "") {
            //swal("Error", "Ingresa tu nueva contraseña", "error");
            alert("Ingresa tu nueva contraseña");
            event.preventDefault();
            return;
        }
        if ($('#password2').val().length < 6) {
            //swal("Error", "Ingresa una contraseña de al menos 6 caracteres", "error");
            alert("Ingresa una contraseña de al menos 6 caracteres")
            event.preventDefault();
            return;
        }

    });

});

function olvideContrasena() {
    var user = document.getElementById("usuario").value;

    if (user == "") {
        swal("Error", "Ingresa tu dirección de correo", "error");
        return;
    }

    urlC = "Home/SendEmail";
    $.post(urlC, { user }, function (res) {

    }).done(function (res) {
        if (user == "") {
            return;
        }
        swal("Te enviamos un correo", "Se envió un enlace a tu dirección de correo para poder reestablecer tu contraseña", "success");
    });
}