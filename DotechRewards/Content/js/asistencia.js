var listaInvitados = [];
var selected = 0;

$(document).ready(function () {

    $('#mensaje').hide();

    $('#customRadio1').change(function () {
        if ($("#customRadio1").is(":checked")) {
            $('#puntosEvt').prop("disabled", true);
            $('#divEventos').show();
            $('#txtEvento').hide();
            $('#puntosEvt').val($(this).find(':selected').data('puntos'));
        }
    });

    $('#customRadio2').change(function () {
        if ($("#customRadio2").is(":checked")) {
            $('#puntosEvt').prop("disabled", false);
            $('#divEventos').hide();
            $('#txtEvento').show();
            $('#puntosEvt').val(0);
        }
    });

    $('#btnGuardar').click(function () {

        if ($("#customRadio2").is(":checked")) {
            $.post("/AdminUsr/AsignarPuntos", { nUsuario: $('#txtInvitado').val(), descripcion: $('#txtEvento').val(), puntos: $('#puntosEvt').val() })
                .done(function (data) {
                    //alert('Se asigno');
                    $('#mensaje').show();
                    selected = 0;
                })
        } else {
            $.post("/AdminUsr/AsignarPuntosE", { nUsuario: $('#txtInvitado').val(), idEvento: $('#selectedclick2').val(), puntos: $('#puntosEvt').val() })
                .done(function (data) {
                    //alert('Se asigno');
                    $('#mensaje').show();
                    selected = 0;
                })
        }

        //alert($('#selectedclick').val());
        //alert($('#selectedclick2').val());
    });

    $('#selectedclick2').change(function () {
        //alert($(this).find(':selected').data('puntos'));
        $('#puntosEvt').val($(this).find(':selected').data('puntos'))
    });

    $('#selectedclick').change(function () {
        //alert($(this).find(':selected').data('puntos'));
        $('#mensaje').hide();
    });

    $('#txtEvento').hide();

    //var targetDropdown = $("#selectedclick");
    //targetDropdown.empty();

    $.get("/AdminPrin/getUsuarios", function (json) {
        //console.log(json);
        for (var i = 0; i < json.length; i++) {
            //$('#selectedclick').append('<option value="' + json[i].idUsuario + '">' + json[i].nombre + '</option>');
            listaInvitados.push(json[i].nombre);
        }
    }).done(function () {
        $('#txtInvitado').autocomplete({
            source: listaInvitados,
            messages: {
                noResults: '',
            },
            select: function (event, ui) {
                selected = 1;
            },
            open: function (event, ui) {
                $('#mensaje').hide();
            }
        });
    });

    var targetDropdown2 = $("#selectedclick2");
    targetDropdown2.empty();
    //"/AdminPrin/getEventos"
    $.get("/AdminPrin/getEventos", function (json) {
        //console.log(json);
        for (var i = 0; i < json.length; i++) {
            $('#selectedclick2').append('<option value="' + json[i].idEvento + '" data-puntos="' + json[i].puntos + '">' + json[i].nombre + '</option>');
        }
        $('.selectpicker').selectpicker('refresh');
    }).done(function () {

    });

    /*
    $.post("/AdminPrin/getEventos", function (json) {
        //console.log(json);
        for (var i = 0; i < json.length; i++) {
            $('#selectedclick2').append('<option value="' + json[i].idEvento + '">' + json[i].nombre + '</option>');
        }
        //$("#selectedclick").selectpicker("refresh");
    }).done(function () {

    });*/
});