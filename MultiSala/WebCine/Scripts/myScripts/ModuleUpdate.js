var Module = (function (my) {

    var btnCancelar = function () {
        $(".btn-danger").click(function () {
            $.Reload();
        });
    };

    my.creaVentanaCambiaVenta = function () {

        $("#btn-cambia-venta").click(function () {
            var generaHTML = _.template($("#vista").html());
            var datos = {
                campos: [
                    {
                        etiqueta: "ID. Venta",
                        identificador: "ID"
                    },
                    {
                        etiqueta: "ID. Sesion",
                        identificador: "SesionID"
                    },
/*                    {
                        etiqueta: "Nº Entradas",
                        identificador: "NEntradas"
                    },
                    {
                        etiqueta: "Nº Entradas Joven",
                        identificador: "NEntradasJoven"
                    },  
                    {
                        etiqueta: "Precio",
                        identificador: "Precio"
                    } */
                ],
                botones: [
                    {
                        etiqueta: "Cambiar Venta",
                        identificador: "btn-cambiar-venta"
                    }
                ]
            };
           
            $(".panel-heading").html("Cambiar Venta");

            $("#content").html(generaHTML(datos));

            $("#content").append("<button type='button' style='margin-bottom: 10px; margin-left: 10px; float:right;' class='btn btn-danger'>Cancelar</button>");
            btnCancelar();

            $("#btn-cambiar-venta").click(function () {
                $.ajax({
                    type: "put",
                    url: "../../api/venta/cambio/" + $("#ID").val(),
                    data: {
                        ID: $("#ID").val(),
                        SesionID: $("#SesionID").val()
                    },
                    success: function (data) { $("#resultado").text("Cambio realizado."); },
                    dataType: "json"
                });
            });
            
        });
    }

    return my;
}(Module || {}));