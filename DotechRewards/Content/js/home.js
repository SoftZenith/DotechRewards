

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
        swal("Te enviamos un correo", "Se envio un enlace a tu dirección de correo para poder reestablecer tu contraseña", "success");
    });
}