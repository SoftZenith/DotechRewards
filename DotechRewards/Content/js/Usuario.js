﻿$(document).ready(function () {

    $('#customRadio1').change(function () {
        var check = $("#customRadio1").is(":checked");
        if (check) {
            $("#asistentesModal").val(1);
        } else {
            $("#asistentesModal").val(0);
        }
        $("#asistentesModal").prop("disabled", !check);
    });
    $('#customRadio2').change(function () {
        var check = $("#customRadio2").is(":checked");
        if (check) {
            $("#asistentesModal").val(0);
        } else {
            $("#asistentesModal").val(1);
        }
        $("#asistentesModal").prop("disabled", check);
    });

    $("#asistentesModal").keypress(function (evt) {
        evt.preventDefault();
    });

    $(".aRegistro").click(function () {
        var y = $('#idEvent').val();
        var z = $(this).data("usrasistentes");
        if (z === 0) {
            $('#customRadio2').attr('checked', 'checked');
            $("#asistentesModal").prop("disabled", true);
        } else {
            $('#customRadio1').attr('checked', 'checked');
        }
        $('#asistentesModal').val(z);
        if ($(this).data("asistente") == 1) { 
            $('#asistentesModal').val(z);
            $('#lblAsistentes').hide();
            $('#asistentesModal').hide();
            $('#idEvent').val($(this).data("idevento"));
            $('#ModalLabelConfirmar').text('Registro-'+$(this).data("nombrevent"));
        } else {
            $('#asistentesModal').val(z);
            $('#asistentesModal').attr("max", $(this).data("asistente"));
            $('#lblAsistentes').show();
            $('#asistentesModal').show();
            $('#idEvent').val($(this).data("idevento"));
            $('#ModalLabelConfirmar').text('Registro-'+$(this).data("nombrevent"));
        }
    });

    $(".registroLink").click(function () {
        //alert('se registro el usuario: ' + $(this).data('usr') + 'Al evento: ' + $(this).data('idevento'));
        $.post("/Usuario/ConfirmarLink", { idUsuario: $(this).data('usr'), idEvento: $(this).data('idevento')})
            .done(function (data) {
                Swal.fire(
                    'Eliminado',
                    'El usuario ha sido eliminado',
                    'success'
                ).then((result) => {
                    location.reload();
                })
            })
    });

    $(".container")
        .mouseover(function () {
            $('#' + this.id + ' .nombrePrd').css('opacity', '0');
            $('#' + this.id + ' .puntosPrd').css('opacity', '0');
            $('#' + this.id + ' .banInfImg').css('opacity', '0');
        })
        .mouseout(function () {
            $('#'+this.id+' .nombrePrd').css('opacity', '1');
            $('#' + this.id + ' .puntosPrd').css('opacity', '1');
            $('#' + this.id + ' .banInfImg').css('opacity', '0.65');
        });
});



function myFunction() {
    // Declare variables 
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function myFunction2() {
    // Declare variables 
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("myInput2");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function cambiarContra(event) {
    var pass1 = document.getElementById("password1").value;  
    var pass2 = document.getElementById("password2").value;
    var pass = pass1;
    if (pass1 != pass2) {
        event.preventDefault();
        swal("Error", "Contraseñas no coinciden", "error");
        return
    }
    if (pass1.length < 6 || pass2.length < 6) {
        event.preventDefault();
        swal("Error", "Se necesitan almenos 6 caracteres", "error");
        return
    }
    urlc = "/Usuario/Cambiar/";
    var nameuser = user_name;
    $.post(urlc, { nameuser , pass }, function (res) {

    }).done(function (res) {
        if (res == -1) {
            //alert('Usario y/o contraseña incorrectos')
            swal("Error", "Error al cambiar contraseña", "error");
        } else {
            window.location.href = "../Home"
        }
    });
}
