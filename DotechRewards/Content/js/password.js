$(document).ready(function () {

    $("form").submit(function (event) {
        if ($('#idPassword').val() == "") {
            //swal("Error", "Ingresa tu nueva contraseña", "error");
            alert("Ingresa tu nueva contraseña");
            event.preventDefault();
            return;
        }
        if ($('#idPassword').val().length < 6) {
            //swal("Error", "Ingresa una contraseña de al menos 6 caracteres", "error");
            alert("Ingresa una contraseña de al menos 6 caracteres")
            event.preventDefault();
            return;
        }

    });

});