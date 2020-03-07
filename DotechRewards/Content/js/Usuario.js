$(document).ready(function () {

    const eventoAct = new eventoActual();

    $('#txtImporteCargo1').change(function () {
        $("#txtImporteCargo1").val($("#txtImporteCargo1").val().replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ","));
    });

    $('#customRadio1').change(function () {
        var check = $("#customRadio1").is(":checked");
        if (check) {
            $("#asistentesModal").val(1);
        } else {
            $("#asistentesModal").val(null);
        }
        $("#asistentesModal").prop("disabled", !check);
    });
    $('#customRadio2').change(function () {
        var check = $("#customRadio2").is(":checked");
        if (check) {
            $("#asistentesModal").val(null);
        } else {
            $("#asistentesModal").val(1);
        }
        $("#asistentesModal").prop("disabled", check);
    });

    $("#asistentesModal").keypress(function (evt) {
        var key = evt.charCode;
        return key >= 48 && key <= 57;
    });

    $('#btnGuardarRegistro').click(function () {
        
        //Llenar tabla historial
        var asis = $(".aRegistro").data("asistente");
        var asis1 = $(".aRegistro").data("idUsr");
        var asist = $('#asistentesModal').val();
        var idUsr = user_name;
        var confPost = 0;
        if ($("#customRadio1").is(":checked")) {
            confPost = 1;
            if (asist > $('#asistentesModal').attr('max')) {
                swal("Error", "Excede el maximo de acompañantes", "error");
                $('#asistentesModal').val(0);
                return
            }
        }
        if (eventoAct.urlActual != "") {
            asist = 0;
        }

        $.post("/Rewards/Usuario/Confirmar", { confirmacion: confPost, asistentes: asist, idUsr: idUsr, idEventoF: eventoAct.idEventoActual }, function (json) {
            //console.log(json);
        }).done(function () {
            if (eventoAct.urlActual != "") {
                var check = $("#customRadio1").is(":checked");
                if (check) {
                    $("#sinUrlSiNo").hide();
                    $("#lblRegistroText").show();
                    $("#conUrl").show();
                    $("#linkUrl").show();
                    $("#btnGuardarRegistro").hide();
                    $("#btnFinalizar").show();
                }
            } else {
                $("#sinUrlSiNo").show();
                $("#lblRegistroText").hide();
                $("#conUrl").hide();
                $("#linkUrl").hide();
                $("#btnGuardarRegistro").show();
                $("#btnFinalizar").hide();
                    window.location.reload(true);
                }
        });
    });


    $(".aRegistro").click(function () {
        var y = $('#idEvent').val();
        $("#btnFinalizar").hide();
        var url = $(this).data("url");
        eventoAct.urlActual = url;
        eventoAct.idEventoActual = $(this).data("idevento");
        var z = $(this).data("usrasistentes");
        if (url == "") { //verifica si tiene url, si no tiene hace todo lo de abajo
            $('#conUrl').hide();
            $("#sinUrlSiNo").show();
            $("#lblRegistroText").hide();
            $('#sinUrl').show();
            if (z === 0) {
                $('#customRadio2').attr('checked', 'checked');
                $("#asistentesModal").prop("disabled", true);
            } else {
                $('#customRadio1').attr('checked', 'checked');
            }
            $('#asistentesModal').val(z);
            if ($(this).data("asistente") == 1) {
                $('#asistentesModal').val(z);
                $('#idEvent').val($(this).data("idevento"));
                $('#ModalLabelConfirmar').text('Registro-' + $(this).data("nombrevent"));
            } else {
                $('#asistentesModal').val(z);
                $('#asistentesModal').attr("max", $(this).data("asistente"));
                $("#sinUrlSiNo").show();
                $('#asistentesModal').show();
                $('#idEvent').val($(this).data("idevento"));
                $('#ModalLabelConfirmar').text('Registro-' + $(this).data("nombrevent"));
            }
        } else { //si, si tiene url
            $('#conUrl').show();
            $("#sinUrlSiNo").show();
            $('#sinUrl').hide();
            var confirmacion = $(this).data("confirmacion");
            if (z === 0) {
                $('#customRadio2').attr('checked', 'checked');
                $("#asistentesModal").prop("disabled", true);
            } else {
                $('#customRadio1').attr('checked', 'checked');
            }
            if (z === 0) {
                $("#linkUrl").hide();
                $("#sinUrlSiNo").show();
                $("#lblRegistroText").hide();
                $('#asistentesModal').val(z);
                $('#idEvent').val($(this).data("idevento"));
                $('#ModalLabelConfirmar').text('Registro-' + $(this).data("nombrevent"));
            } else {
                $("#lblRegistroText").hide();
                $("#linkUrl").hide();
                $("#lblRegistroText").hide();
                $('#asistentesModal').val(z);
                $('#idEvent').val($(this).data("idevento"));
                $('#ModalLabelConfirmar').text('Registro-' + $(this).data("nombrevent"));
            }
            $('#linkUrl').attr("href", url+"");

        }

    });
    $("#linkUrl").click(function () {
        window.location.reload(true);
    });
    $("#btnFinalizar").click(function () {
        window.location.reload(true);
    });
    $(".registroLink").click(function () {
        //alert('se registro el usuario: ' + $(this).data('usr') + 'Al evento: ' + $(this).data('idevento'));
        $.post("/Rewards/Usuario/ConfirmarLink", { idUsuario: $(this).data('usr'), idEvento: $(this).data('idevento')})
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
            var id = $(this).attr('id');
            if (!id) return undefined;
            $('#' + id + ' .nombrePrd').css('opacity', '1');
            $('#' + id + ' .puntosPrd').css('opacity', '1');
            $('#' + id + ' .banInfImg').css('opacity', '0.8');
        })
        .mouseout(function () {
            
            var id = $(this).attr('id');
            if (!id) return undefined;
            $('#' + id + ' .nombrePrd').css('opacity', '1');
            $('#' + id + ' .puntosPrd').css('opacity', '1');
            $('#' + id + ' .banInfImg').css('opacity', '0.80');
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
    urlc = "/Rewards/Usuario/Cambiar/";
    var nameuser = user_name;
    $.post(urlc, { nameuser , pass }, function (res) {

    }).done(function (res) {
        if (res == -1) {
            //alert('Usario y/o contraseña incorrectos')
            swal("Error", "Error al cambiar contraseña", "error");
        } else {
            window.location.href = "../Rewards"
        }
    });
}

class eventoActual {
    constructor(urlActual, idEventoActual) {
        this.urlActual = urlActual;
        this.idEventoActual = idEventoActual;
    }
}
