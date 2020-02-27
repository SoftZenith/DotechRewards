$(document).ready(function () {

    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    })

    

    $('.btnEditE').click(function () {

        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd
        }
        if (mm < 10) {
            mm = '0' + mm
        }

        today = yyyy + '-' + mm + '-' + dd + "T00:00";
        document.getElementById("fecha").setAttribute("min", today);

        $('#confirmacionEven').prop('checked', false);
        $('#confirmacionLink').prop('checked', false);
        $('#cantidadPersonas').prop('disabled', true);
        $('#h5confirmados').html($(this).data('confirmados') + ' confirmado(s)');
        $('#idEventoM').val($(this).data('id'));
        $('#edtFoto').val($(this).data('image'));
        $('#previewImg').attr('src', 'Content/images/'+$(this).data('image'));
        $('#nombre').val($(this).data('nombre'));
        $('#puntos').val($(this).data('puntos'));
        $('#lugar').val($(this).data('lugar'));
        $('#urlConfirmacion').val($(this).data('urle'));
        $('#descList').show();
        $('#previewImg').show();
        $('#Foto_Camp').removeAttr('required');

        if ($(this).data('urle')) {
            $('#confirmacionLink').prop('checked', true);
            $('#confirmacionEven').prop('checked', $(this).data('confirmacion').toLowerCase() === 'true');
        } else {
            $('#confirmacionEven').prop('checked', $(this).data('confirmacion').toLowerCase() === 'true');
            console.log($(this).data('confirmacion').toLowerCase() === 'true');
            if ($(this).data('asistentes') != 0) {
                $('#confirmacionAcom').prop('checked', true);
                $('#confirmacionAcom').prop('disabled', false);
                $('#cantidadPersonas').prop('disabled', false);
            } else {
                $('#confirmacionAcom').prop('checked', false);
                $('#cantidadPersonas').prop('disabled', true);
            }
        }

        $('#cantidadPersonas').val($(this).data('asistentes'));
        $('#downloadLista').attr('href', '/AdminEven/DescargarLista?idEvento=' + $(this).data('id'));
        var fecha = $(this).data('fecha');
        var fecha3 = fecha.split(" ");
        if (fecha3[2] == "p.") {

            var descomponer = fecha3[1].split(":");
            var recuperarInt = parseInt(descomponer[0]);
            recuperarInt = recuperarInt + 12;
            fecha = fecha3[0] + " " + recuperarInt+":" + descomponer[1];
        }


        var fecha2 = fecha.split("/");//.reverse().join("-");
        var hora = fecha2[2].split(" ")[1];
        var año = fecha2[2].split(" ")[0];
        var fecha_hora = año + "-" + fecha2[1] + "-" + fecha2[0] + "T" + hora;
        $('#fecha').val(fecha_hora);
    });

    $('.btnDelE').click(function () {
        Swal.fire({
            title: '¿Deseas eliminar este evento?',
            text: "Operación no revertible",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Eliminar',
        }).then((result) => {
            if (result.value) {
                $.post("/AdminEven/DelEvento", { idEvento: $(this).data('id') })
                    .done(function (data) {
                        Swal.fire(
                            'Eliminado',
                            'El evento ha sido eliminado correctamente',
                            'success'
                        ).then((result) => {
                            location.reload();
                        })
                    })
            }
        })
    });

    $('#confirmacionEven').change(function () {
        if ($('#confirmacionEven').is(':checked')) {
            $('#confirmacionAcom').prop('disabled', false);
            $('#confirmacionLink').prop('checked', false);
            $('#urlConfirmacion').prop('disabled', true);
            $('#urlConfirmacion').val('');
        } else {
            $('#cantidadPersonas').prop('disabled', true);
            $('#confirmacionAcom').prop('disabled', true);
            $('#confirmacionAcom').prop('checked', false);
            $('#cantidadPersonas').val(0);
        }
    });

    $('#confirmacionAcom').change(function () {
        if ($('#confirmacionAcom').is(':checked')) {
            $('#cantidadPersonas').prop('disabled', false);
        } else {
            $('#cantidadPersonas').prop('disabled', true);
            $('#cantidadPersonas').val(0);
        }
    });

    $('#confirmacionLink').change(function () {
        if ($('#confirmacionLink').is(':checked')) {
            $('#urlConfirmacion').prop('disabled', false);

            $('#confirmacionEven').prop('checked', false);
            $('#confirmacionAcom').prop('checked', false);
            $('#confirmacionAcom').prop('disabled', true);
            $('#cantidadPersonas').prop('disabled', true);
            $('#cantidadPersonas').val(0);
        } else {
            $('#urlConfirmacion').prop('disabled', true);
            $('#urlConfirmacion').val('');
        }
    });
    
    $('#btnAddE').click(function () {
        $('#idEventoM').val(0);
        $('#nombre').val('');
        $('#puntos').val('');
        $('#lugar').val('');
        $('#fecha').val('');
        $('#confirmacionAcom').prop('checked', false);
        $('#confirmacionAcom').prop('disabled', true);
        $('#cantidadPersonas').prop('disabled', true);
        $('#confirmacionLink').prop('checked', false);
        $('#cantidadPersonas').val($(this).data(''))
        $('#Foto_Camp').val('')
        $('#previewImg').attr('src', 'Content/images/baner_producto.png');
        $('#downloadLista').attr('href', '/AdminEven/DescargarLista?idEvento=0');
        $('#descList').hide();
        $('#previewImg').hide();
        $('#Foto_Camp').attr('required', 'required');
        //$('#urlConfirmacion').val('');
    });

    //validaciones de digitos
    $('#puntos').keyup(function () {
        if (parseInt($(this).val()) <= 0) {
            $(this).val('1');
        }
        if ($(this).val().length > 5) {
            $(this).val($(this).val().slice(0, -1));
        }
    });
    $('#puntos').change(function () {
        if ($(this).val().length > 5) {
            $(this).val($(this).val().slice(0, 5));
        }
    });
    $('#cantidadPersonas').keyup(function () {
        if (parseInt($(this).val()) <= 0) {
            $(this).val('1');
        }
        if ($(this).val().length > 2) {
            $(this).val($(this).val().slice(0, -1));
        }
    });
    $('#cantidadPersonas').change(function () {
        if ($(this).val().length > 5) {
            $(this).val($(this).val().slice(0, 3));
        }
        if ($(this).val().length > 2) {
            $(this).val($(this).val().slice(0, -1));
        }
    });
    //-------------------------
    $('#getLista').click(function () {
        $.post("/AdminEven/DescargarLista", { idEvento: $('#idEventoM').val() })
            .done(function (data) {

            })
    });

    $('#Lista_asis').change(function () {

        var fileName = document.getElementById("Lista_asis").value; //c:fakepath/imagen.jpg
        var fileButon = document.getElementById("btnListaGuardar");
        var idxDot = fileName.lastIndexOf(".") + 1;
        var extFile = fileName.substr(idxDot, fileName.length).toLowerCase(); //jpg




        if (extFile == "xlsx") {
            $('#btnListaGuardar').attr('disabled', false);
        } else {
            console.log("Error formato");
            Swal.fire(
                'Error en la lista',
                'Solo se permiten archivos de imágenes en formato xlsx.',
                'warning'
            );
            $('#btnListaGuardar').attr('disabled', true);
            return false;
        }
    });

    $('#subirLista').click(function () {
        $.post("/AdminEven/SubirLista").done(function () {
            window.Location.href = "adminEven";
        });
    });



    $('#Foto_Camp').change(function () {
        var fileName = document.getElementById("Foto_Camp").value; //c:fakepath/imagen.jpg
        var idxDot = fileName.lastIndexOf(".") + 1;
        var extFile = fileName.substr(idxDot, fileName.length).toLowerCase(); //jpg

        var $i = $('#Foto_Camp'), input = $i[0];

        console.log(document.querySelector('input[type=file]').files[0]);

        if (extFile == "jpg") {

            //var Fotofile = document.querySelector('input[type=file]').files[0];
            var Fotofile = $("*:file")[1].files[0];
            var nombreEspacio = Fotofile.name;
            var reader = new FileReader();
            reader.readAsDataURL(Fotofile);

            reader.onload = function (e) {
                var image = new Image();
                image.src = e.target.result;
                image.onload = function () {

                    nombreEspacio = nombreEspacio.substr(0, nombreEspacio.length - 4); //Obtener nombre sin extension .jpg

                    if (nombreEspacio[nombreEspacio.length - 1] == " ") {
                        Swal.fire(
                            'Error en imagen',
                            'La imagen no pueden contener espacios al final de su nombre.',
                            'warning'
                        );
                        $('#Foto_Camp').val('');
                        $('#previewImg').hide();
                        $('#btnGuardar').attr('disabled', true);
                        return false;
                    }
                    if (nombreEspacio[0] == " ") {
                        Swal.fire(
                            'Error en imagen',
                            'La imagen no pueden contener espacios al inicio de su nombre.',
                            'warning'
                        );
                        $('#Foto_Camp').val('');
                        $('#previewImg').hide();
                        $('#btnGuardar').attr('disabled', true);
                        return false;
                    }
                    var height = this.height;
                    var width = this.width;
                    if (width != 1290) {
                        if (width != 1290) {
                            console.log("Error ancho");
                            Swal.fire(
                                'Error en imagen',
                                'La imagen debe tener un ancho de 1290 pixeles.',
                                'warning'
                            );
                            $('#Foto_Camp').val('');
                            $('#previewImg').hide();
                            $('#btnGuardar').attr('disabled', true);
                            return false;
                        }
                    }
                    if (height != 300) {
                        if (height != 300) {
                            console.log("Error alto");
                            Swal.fire(
                                'Error en imagen',
                                'La imagen debe tener un alto de 300 pixeles.',
                                'warning'
                            );
                            $('#Foto_Camp').val('');
                            $('#previewImg').hide();
                            $('#btnGuardar').attr('disabled', true);
                            return false;
                        }
                    }
                    $('#btnGuardar').removeAttr('disabled');
                    $('#previewImg').show();
                    console.log("Buena Imagen");
                    readURL(this);
                };
            }
        } else {
            Swal.fire(
                'Error en imagen',
                'Solo se permiten archivos de imágenes en formato JPG.',
                'warning'
            );
            $('#Foto_Camp').val('');
            $('#previewImg').hide();
            $('#btnGuardar').attr('disabled', true);
            return false;
        }

        readURL(this);
    });

    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#previewImg').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

});