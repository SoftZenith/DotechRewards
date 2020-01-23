﻿$(document).ready(function () {
    $('.btnEditU').click(function () {
        $('#idUsuarioModal').val($(this).data('id'));
        $('#nombreModal').val($(this).data('nombre'));
        $('#usuarioModal').val($(this).data('usuario'));
        $('#puesto_asign').val($(this).data('puesto'));
        var fecha_entrada = $(this).data('entrada');
        fecha_entrada = fecha_entrada.split("/").reverse().join("-");
        $('#fecha_entrada').val(fecha_entrada);
        var fecha_cumple = $(this).data('cumple');
        fecha_cumple = fecha_cumple.split("/").reverse().join("-");
        $('#fecha_cumple').val(fecha_cumple);

        //Llenar tabla historial
        $.post("/AdminUsr/getHistorialUsuario", { usuario: $(this).data('usuario')}, function (json) {
            //console.log(json);
            $("#historialTable").find("tr").remove();
            for (var i = 0; i < json.length; i++) {
                $('#historialTable tbody').append(
                    '<tr>' +
                    '<td width="100px" style="text-align: center">' + json[i].fecha + '</td>' + //fecha
                    '<td width="500px" style="text-align: center">' + json[i].nombre + '</td>' + //nombre
                    '<td width="100px" style="text-align: center">' + json[i].puntos + '</td>' + //puntos
                    '</tr>');
            }
        }).done(function () {
            //llenar total de puntos
            /*
            $('#historialTable tfoot').append(
                '<tr>' +
                '<td width="100px" style="text-align: center">' + json[i].fecha + '</td>' + //fecha
                '<td width="500px" style="text-align: center">' + json[i].nombre + '</td>' + //nombre
                '<td width="100px" style="text-align: center">' + json[i].puntos + '</td>' + //puntos
                '</tr>');*/


        });
    });

    $('.btnDelU').click(function () {
        Swal.fire({
            title: '¿Deseas eliminar este usuario?',
            text: "Operación no revertible",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Eliminar'
        }).then((result) => {
            if (result.value) {
                $.post("/AdminUsr/DelUsuario", { idUsuario: $(this).data('id') })
                    .done(function (data) {
                        Swal.fire(
                            'Eliminado',
                            'El usuario ha sido eliminado',
                            'success'
                        ).then((result) => {
                            location.reload();
                        })
                    })
            }
        })
    });

    $('#btnAddU').click(function () {
        $('#idUsuarioModal').val(0);
        $('#nombreModal').val('');
        $('#usuarioModal').val('');
        $('#puesto_asign').val('');
        $('#fecha_entrada').val('');
        $('#fecha_cumple').val('');
    });

    $('#btnAsignar').click(function () {

        $.post("/AdminUsr/AsignarPuntos", { idUsuario: $('#idUsuarioModal').val(), descripcion: $('#descripcion').val(), puntos: $('#puntos').val() })
            .done(function (data) {
                //Llenar tabla historial
                $.post("/AdminUsr/getHistorialUsuario", { usuario: $('#usuarioModal').val() }, function (json) {
                    //console.log(json);
                    $("#historialTable").find("tr").remove();
                    for (var i = 0; i < json.length; i++) {
                        $('#historialTable tbody').append(
                            '<tr>' +
                            '<td width="100px" style="text-align: center">' + json[i].fecha + '</td>' + //fecha
                            '<td width="500px" style="text-align: center">' + json[i].nombre + '</td>' + //nombre
                            '<td width="100px" style="text-align: center">' + json[i].puntos + '</td>' + //puntos
                            '</tr>');
                    }
                }).done(function () {

                });
            })
    });

    $('#btnCobrar').click(function () {
        $.post("/AdminUsr/CobrarPuntos", { idUsuario: $('#idUsuarioModal').val(), descripcion: $('#descripcion').val(), puntos: $('#puntos').val() })
            .done(function (data) {
                //Llenar tabla historial
                $.post("/AdminUsr/getHistorialUsuario", { usuario: $('#usuarioModal').val() }, function (json) {
                    //console.log(json);
                    $("#historialTable").find("tr").remove();
                    for (var i = 0; i < json.length; i++) {
                        $('#historialTable tbody').append(
                            '<tr>' +
                            '<td width="100px" style="text-align: center">' + json[i].fecha + '</td>' + //fecha
                            '<td width="500px" style="text-align: center">' + json[i].nombre + '</td>' + //nombre
                            '<td width="100px" style="text-align: center">' + json[i].puntos + '</td>' + //puntos
                            '</tr>');
                    }
                    $('.selectpicker').selectpicker('refresh');
                }).done(function () {

                });
            })
    });
});