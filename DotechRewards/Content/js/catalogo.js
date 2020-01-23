$(document).ready(function () {
    $('.btnEditP').click(function () {
        $('#Foto_Prd').val('');
        $('#edtFoto').val($(this).data('image'));
        $('#idPrdM').val($(this).data('idprd'));
        $('#previewImg').attr('src', 'Content/images/' + $(this).data('image'));
        $('#nombre').val($(this).data('nombre'));
        $('#descripcion').val($(this).data('desc'));
        $('#puntos').val($(this).data('puntos'));
    });

    $('.btnDelP').click(function () {
        Swal.fire({
            title: '¿Deseas eliminar este producto?',
            text: "Operación no revertible",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Eliminar'
        }).then((result) => {
            if (result.value) {
                $.post("/AdminCat/DelProducto", { idProducto: $(this).data('idprd') })
                    .done(function (data) {
                        Swal.fire(
                            'Eliminado',
                            'El producot ha sido eliminado correctamente',
                            'success'
                        ).then((result) => {
                            location.reload();
                        })
                    })
            }
        })
    });

    $('#btnAddP').click(function () {
        $('#idPrdM').val(0);
        $('#nombre').val($(this).data(''));
        $('#descripcion').val($(this).data(''));
        $('#puntos').val($(this).data(''));
        $('#Foto_Prd').val(null);
        $('#previewImg').attr('src', 'Content/images/baner_producto.png');
    });

    $('#Foto_Prd').change(function () {
        readURLPrd(this);
    });

    function readURLPrd(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#previewImg').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }
    
    $('#Foto_Prd').change(function () {
        var file = document.getElementById("Foto_Prd"); 
        var width = file.clientWidth;
        var height = file.clientHeight;
        console.log('el ancho es : ' + width + '   la altura es:' + height);
       // if (width != 358) {
            if (width != 358) {
                console.log("Error ancho");
                Swal.fire(
                    'Error en imagen',
                    'La imagen debe tener un ancho de 1290 pixeles.',
                    'warning'
                ).then((result) => {
                    $('#previewImg').attr('src', '');
                })
                $('#Foto_Prd').val('');
                $('#previewImg').attr('src', '');
                return false;
            }
        //}
        //if (height != 30) {
            if (height != 30) {
                console.log("Error alto");
                Swal.fire(
                    'Error en imagen',
                    'La imagen debe tener un alto de 350 pixeles.',
                    'warning'
                );
                $('#Foto_Prd').val('');
                $('#previewImg').attr('src', '');
                return false;
            }
        //}
    });

    $('#btnGuardarPrd').click(function () {
        //cachar todos los campos.
        //validar si son nulos.
        //mostrar mensaje en caso de.}
        var productName = $('#nombre').val();
        var productDesc = $('#descripcion').val();
        var productPoints = $('#puntos').val();
        var productImage = $('#Foto_Prd').val();
        if (productName == '') {
            Swal.fire(
                'Error en nombre de producto.',
                'Es necesario especificar un nombre para el producto que intenta registrar.',
                'warning'
            ).then((result) => {
                $('#nombre').focus();      //poner autofocus a este campo
            })
            return false;
        }
        else if (productDesc == '') {
            Swal.fire(
                'Error en descripción de producto.',
                'Es necesario especificar una descripción para el producto que intenta registrar.',
                'warning'
            ).then((result) => {
                $('#descripcion').focus();
            })
            return false;
        }
        else if (productPoints <= 0) {
            Swal.fire(
                'Error en cantidad de puntos.',
                'Es necesario especificar una cantidad mayor a 0.',
                'warning'
            ).then((result) => {
                $('#puntos').focus();
            })
            return false;
        }
        else if (productImage = '') {

            Swal.fire(
                'Error en imagen de producto.',
                'Se.',
                'warning'
            ).then((result) => {
                $('#puntos').focus();
            })
            return false;
        }
       // alert(productName + productDesc + productPoints +productImage);
    });
    
});

