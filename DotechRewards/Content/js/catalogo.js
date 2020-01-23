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

});

