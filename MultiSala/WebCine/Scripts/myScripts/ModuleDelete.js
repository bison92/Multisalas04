
var Module = (function (my) {
    
    var btnCancelar = function () {
        $(".btn-danger").click(function () {
            $.Reload();
        });
    };

    my.creaVentanaDevolucion = function () {

        $("#btndevolucionventa").click(function () {
            $(".panel-heading").html("Devolver venta");

            $("#content").load("../../Content/snippets/deleteVentaSnippet.html", function () {
                $("#btndevolucio-venta").click(function () {

                    $.post("../../api/venta/devolverVenta/" + $("#inputIdVenta").val(),
                    {
                        ID: $("#inputIdVenta").val()

                    }, function (data) {

                        $("#precioDevolucion").text(data + "\n Devolución realizada");
                    });
                });
                $("#content").append("<button type='button' style='margin-bottom: 10px; margin-left: 10px;float:right;' class='btn btn-danger'>Cancelar</button>");
                btnCancelar();
            });
            
        });
    };


    return my;
}(Module || {}));