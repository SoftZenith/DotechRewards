$(document).ready(function () {
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
        $("#asigPuntos").show();
        //Llenar tabla historial
        $.post("/Rewards/AdminUsr/getHistorialUsuario", { usuario: $(this).data('usuario')}, function (json) {
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
            $.post("/Rewards/AdminUsr/getPuntosTotales", { usuario: $('#usuarioModal').val() }, function (puntosUsr) {
                //console.log(json);
                $('#totalHistUsr').html('Total ' + numberWithCommas(puntosUsr));
            })


        });
    });
function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
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
                $.post /Rewards/AdminUsr/DelUsuario", { idUsuario: $(this).data('id') })
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
        $("#historialTable").find("tr").remove();
        $("#asigPuntos").hide();
    });

    $('#btnAsignar').click(function () {
        if ($('#puntos').val() < 0) {
            Swal.fire(
                'Error en asignación',
                'Solo se aceptan montos positivos',
                'warning'
            );
            return;
        }
        var idUSur = $('#idUsuarioModal').val();
        $.post("/Rewards/AdminUsr/AsignarPuntos", { nUsuario: $('#idUsuarioModal').val(), descripcion: $('#descripcion').val(), puntos: $('#puntos').val() })
            .done(function (data) {
                //Llenar tabla historial
                $.post("/Rewards/AdminUsr/getHistorialUsuario", { usuario: $('#usuarioModal').val() }, function (json) {
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
                    $.post("/Rewards/AdminUsr/getPuntosTotales", { usuario: $('#usuarioModal').val() }, function (puntosUsr) {
                        //console.log(json);
                        $('#totalHistUsr').html('Total ' + numberWithCommas(puntosUsr));
                    })
                    $('#descripcion').val('');
                    $('#puntos').val('');
                });
            })
    });

    $('#btnCobrar').click(function () {
        if ($('#puntos').val() < 0) {
            Swal.fire(
                'Error en cobro',
                'Solo se aceptan montos positivos',
                'warning'
            );
            return;
        }
        $.post("/Rewards/AdminUsr/CobrarPuntos", { idUsuario: $('#idUsuarioModal').val(), descripcion: $('#descripcion').val(), puntos: $('#puntos').val() })
            .done(function (data) {
                if (data == -1) {
                    Swal.fire(
                        'Error en cobro',
                        'No puede cobrar más puntos de los que tiene el usuario',
                        'warning'
                    );
                }
                else if (data == -2) {
                    Swal.fire(
                        'Error en cobro',
                        'El usuario aun no activa sus puntos',
                        'warning'
                    );
                } else {
                    Swal.fire(
                        'Cobro exitoso',
                        'Se hizo el cobro de puntos exitosamente',
                        'success'
                    );
                    $('#descripcion').val('');
                    $('#puntos').val('');
                }

                //Llenar tabla historial
                $.post("/Rewards/AdminUsr/getHistorialUsuario", { usuario: $('#usuarioModal').val() }, function (json) {
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
                    //$('.selectpicker').selectpicker('refresh');
                }).done(function () {
                    $.post("/Rewards/AdminUsr/getPuntosTotales", { usuario: $('#usuarioModal').val() }, function (puntosUsr) {
                        //console.log(json);
                        $('#totalHistUsr').html('Total ' + numberWithCommas(puntosUsr));
                    })
                    $('#descripcion').val('');
                    $('#puntos').val('');
                });
            })
    });
});