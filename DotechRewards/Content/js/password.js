$(document).ready(function () {

    $("form").submit(function (event) {
        if ($('#idPassword').val() == "") {
            alert("Error poner un password");
            return;
        }
        if ($('#idPassword').val().length < 6) {
            alert("ERror en tamaño de password");
            return;
        }
    });

});