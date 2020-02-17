$(document).ready(function () {
    $('.btnEditE').click(function () {
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
        if ($(this).data('urle')) {
            $('#confirmacionLink').prop('checked', true);
            $('#confirmacionEven').prop('checked', $(this).data('confirmacion').toLowerCase() === 'true');
        } else {
            $('#confirmacionEven').prop('checked', $(this).data('confirmacion').toLowerCase() === 'true');
            console.log($(this).data('confirmacion').toLowerCase() === 'true');
            $('#cantidadPersonas').prop('disabled', false);
        }

        $('#cantidadPersonas').val($(this).data('asistentes'));
        $('#downloadLista').attr('href', '/AdminEven/DescargarLista?idEvento=' + $(this).data('id'));
        var fecha = $(this).data('fecha');
        var fecha2 = fecha.split("/").reverse().join("-");
        $('#fecha').val(fecha2);
    });

    $('.btnDelE').click(function () {
        Swal.fire({
            title: '¿Deseas eliminar este evento?',
            text: "Operación no revertible",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Eliminar'
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
            $('#cantidadPersonas').prop('disabled', false);

            $('#confirmacionLink').prop('checked', false);
            $('#urlConfirmacion').prop('disabled', true);
            $('#urlConfirmacion').val('');
        } else {
            $('#cantidadPersonas').prop('disabled', true);
            $('#cantidadPersonas').val(0);
        }
    });

    $('#confirmacionLink').change(function () {
        if ($('#confirmacionLink').is(':checked')) {
            $('#urlConfirmacion').prop('disabled', false);

            $('#confirmacionEven').prop('checked', false);
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
        $('#cantidadPersonas').val($(this).data(''))
        $('#Foto_Camp').val('')
        $('#previewImg').attr('src', 'Content/images/baner_producto.png');
        $('#downloadLista').attr('href', '/AdminEven/DescargarLista?idEvento=0');
        //$('#urlConfirmacion').val('');
    });
    
    $('#getLista').click(function () {
        $.post("/AdminEven/DescargarLista", { idEvento: $('#idEventoM').val() })
            .done(function (data) {

            })
    });

    $('#subirLista').click(function () {
        $.post("/AdminEven/SubirLista");
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
                        return false;
                    }
                    if (nombreEspacio[0] == " ") {
                        Swal.fire(
                            'Error en imagen',
                            'La imagen no pueden contener espacios al inicio de su nombre.',
                            'warning'
                        );
                        $('#Foto_Camp').val('');
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
                            $('#previewImg').attr('src', 'Content/images/baner_producto.png');
                            return false;
                        }
                    }
                    if (height != 210) {
                        if (height != 210) {
                            console.log("Error alto");
                            Swal.fire(
                                'Error en imagen',
                                'La imagen debe tener un alto de 210 pixeles.',
                                'warning'
                            );
                            $('#Foto_Camp').val('');
                            $('#previewImg').attr('src', 'Content/images/baner_producto.png');
                            return false;
                        }
                    }
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
            return false;
        }
        console.log("Fin Fn");
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