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
                            'El producto ha sido eliminado correctamente',
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

        var fileName = document.getElementById("Foto_Prd").value; //c:fakepath/imagen.jpg
        var fileButon = document.getElementById("btnGuardarPrd");
        var idxDot = fileName.lastIndexOf(".") + 1;
        var extFile = fileName.substr(idxDot, fileName.length).toLowerCase(); //jpg

        var $i = $('#Foto_Prd'), input = $i[0];

        

        if (extFile == "jpg") {
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

                        $('#btnGuardarPrd').attr('disabled', true);
                        $('#Foto_Camp').val('');
                        return false;
                    }
                    if (nombreEspacio[0] == " ") {
                        Swal.fire(
                            'Error en imagen',
                            'La imagen no pueden contener espacios al inicio de su nombre.',
                            'warning'
                        );

                        $('#btnGuardarPrd').attr('disabled', true);
                        $('#Foto_Camp').val('');
                        return false;
                    }
                    var height = this.height;
                    var width = this.width;
                    if (width != 250) {
                        if (width != 250) {
                            console.log("Error ancho");
                            Swal.fire(
                                'Error en imagen',
                                'La imagen debe tener un ancho de 250 pixeles.',
                                'warning'
                            );
                            $('#Foto_Camp').val('');
                            $('#btnGuardarPrd').attr('disabled', true);
                            $('#previewImg').attr('src', 'Content/images/baner_producto.png');
                            return false;
                        }
                    }
                    if (height != 130) {
                        if (height != 130) {
                            console.log("Error alto");
                            Swal.fire(
                                'Error en imagen',
                                'La imagen debe tener un alto de 130 pixeles.',
                                'warning'
                            );
                            $('#Foto_Camp').val('');
                            $('#btnGuardarPrd').attr('disabled',true);
                            $('#previewImg').attr('src', 'Content/images/baner_producto.png');
                            return false;
                        }
                    }
                    console.log("Buena Imagen");
                    $('#btnGuardarPrd').removeAttr('disabled');
                    readURL(this);
                };
            }

        } else {
            console.log("Error formato");
            Swal.fire(
                'Error en imagen',
                'Solo se permiten archivos de imágenes en formato JPG.',
                'warning'
            );
            $('#btnGuardarPrd').attr('disabled',true);
            $('#Foto_Camp').val('');
            return false;
        }
        
        
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

    $('#exampleInputFile').change(function () {
        var fileName = document.getElementById("exampleInputFile").value; //c:fakepath/imagen.jpg
        var fileButon = document.getElementById("btnGuardarPrd");
        var idxDot = fileName.lastIndexOf(".") + 1;
        var extFile = fileName.substr(idxDot, fileName.length).toLowerCase(); //jpg

        var $i = $('#exampleInputFile'), input = $i[0];


        if (extFile == "pdf") {
            var Fotofile = $("*:file")[0].files[0];
            var nombreEspacio = Fotofile.name;
            var reader = new FileReader();
            //reader.readAsDataURL(Fotofile);
            //alert('Es pdf');
            

            // Checking whether FormData is available in browser  
            if (window.FormData !== undefined) {

                var fileUpload = $("#exampleInputFile").get(0);
                var files = fileUpload.files;

                // Create FormData object  
                var fileData = new FormData();

                // Looping over all files and add it to FormData object  
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }

                // Adding one more key to FormData object  
                //fileData.append('username', ‘Manas’);

                $.ajax({
                    url: '/AdminCat/UploadPDFCatalogo',
                    type: "POST",
                    contentType: false, // Not to set any content header  
                    processData: false, // Not to process data  
                    data: fileData,
                    success: function (result) {
                        Swal.fire(
                            'Catalogo actualizado',
                            'Se actualizo correctamente el archivo',
                            'success'
                        );
                    },
                    error: function (err) {
                        alert(err.statusText);
                    }
                });
            } else {
                alert("FormData is not supported.");
            }

        } else {
            console.log("Error formato");
            Swal.fire(
                'Error en imagen',
                'Solo se permiten archivos en formato PDF.',
                'warning'
            );
            
            return false;
        }
    });
    
});

