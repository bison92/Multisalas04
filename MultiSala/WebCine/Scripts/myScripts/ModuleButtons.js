


var Module = (function (my) {
    
    

    var btnCancelar = function () {
        $(".btn-danger").click(function () {
            $.Reload();
        });
    };

   


    my.creaVentanaVenta = function () {

        $("#btn-create-venta").click(function () {
            
            $(".panel-heading").html("Crear Venta");

            $("#content").load("../../Content/snippets/createVentaSnippet.html", function () {
                $("#btn-crear-venta").click(function () {
                    $.post("../../api/venta",
                    {
                        SesionID: $("#inputIdSesion").val(),
                        NEntradas: $("#inputEntradas").val(),
                        NEntradasJoven: $("#inputEntradasJoven").val()
                    }, function (data) {
                        $("#precioCalculado").text(data.Precio + "\n Venta Realizada");
                    });
                });

                $("#btn-calcular-precio").click(function () {
                    $.get("../../api/venta/calculaPrecio/" + $('#inputIdSesion').val() + "/" + $('#inputEntradas').val() + "/" + $('#inputEntradasJoven').val(), function (data) {
                        $("#precioCalculado").text(data);
                    });
                });
                
                $("#content").append("<button type='button' style='margin-bottom: 10px; margin-left: 10px; float:right;' class='btn btn-danger'>Cancelar</button>");
                btnCancelar();
            });
        });   
    };

    my.creaVentanaBuscaVenta = function () {

        $("#btn-busca-venta").click(function () {
            $(".panel-heading").html("Buscar Ventas");

            $("#content").load("../../Content/snippets/buscaVentaSnippet.html", function () {
                $("#btn-buscar-venta").click(function () {
                    $.get("../../api/venta/" + $("#idInput").val(), function (data) {
                        $(".history").append("<tr><th scope='row'>" + data.ID + "</td>" +
                                                "<td>" + data.SesionID + "</td>" +
                                                "<td>" + data.NEntradas + "</td>" +
                                                "<td>" + data.NEntradasJoven + "</td>" +
                                                "<td>" + data.Precio + "</td></tr>" );
                    });
                });

                $("#content").append("<button type='button' style='margin-bottom: 10px; margin-left: 10px;float:right;' class='btn btn-danger'>Cancelar</button>");
                btnCancelar();

            });
        });
    };

    my.creaVentanaListadoSesiones = function () {
        $("#btn-listado-sesiones").click(function () {
            $(".panel-heading").html("Listado de Sesiones");
            
            $("#content").load("../../Content/snippets/listadoSesionesSnippet.html", function () {

                var entradas = [];
                $.ajaxSetup({ async: false });
                for (var i = 0; i < 9; i++) {
                    $.get("http://localhost:49208/api/venta/entradasDisponibles/" + (i + 1) + "", function (dataEntradas) {
                        entradas[i] = dataEntradas;
                        //alert(data);

                    });
                };

                $.ajaxSetup({ async: false });
                
                var getsesion = $.get("../../api/sesion/", function (data) {
                    
                    
                    for (var i = 0; i < data.length; i++) {
                        var success = "";
                        var estado = "";
                        var accion = "";
                        if (data[i].Abierto == true) {
                            success = "class ='success'";
                            estado = "Abierta";
                            accion = "Cerrar";
                        }
                        else {
                            success = "class ='danger'";
                            estado = "Cerrada";
                            accion = "Abrir";
                        };
                        
                        
                        $("#tablaSesiones").append("<tr " + success +  " id='tr-" + data[i].ID + "'><th scope='row'>" + data[i].ID + "</th>" +
                                          "<td>" + data[i].SalaID + "</td>" +
                                          "<td>" + ($.format.date(data[i].fecha, "dd-MM-yyyy HH:mm")) + "</td>" +
                                          "<td>" + entradas[i] + "</td>" +
                                          "<td id='estado-"+ data[i].ID + "'>" + estado + "</td>" +
                                          "<td><button type='button' class='btn btn-primary btn-block' id='"+ data[i].ID +"'>" + accion + "</button></td>" +
                                          "</tr>");
                        
                        
                    };
                    $(".btn-primary").click(function (event) {
                        //console.log(sesiones[i]);

                        var id = event.target.id;
                        $.post("http://localhost:49208/api/sesion/" + id + "/", function () {
                            $("#" + id).text("Cerrar");
                            $("#estado-" + id).text("Abierta");
                            $("#tr-" + id).removeClass("danger");
                            $("#tr-" + id).addClass("success");
                        });
                    });

                });

                $("#content").append("<button type='button' style='margin-bottom: 10px; margin-left: 10px;float:right;' class='btn btn-danger'>Cancelar</button>");
                btnCancelar();
            });

            
        });
    };

    my.creaVentanaListadoVentas = function () {
        $("#btn-listado-ventas").click(function () {
            $(".panel-heading").html("Listado de Ventas");

            $("#content").load("../../Content/snippets/listadoVentasSnippet.html", function () {
                
                $.get("../../api/venta/listado", function (data) {
                    for (i = 0; i <= data.length; i++) {
                        $("#tablaVentas").append("<tr><th scope='row'>" + data[i].ID + "</th>" +
                                          "<td>" + data[i].SesionID + "</td>" +
                                          "<td>" + data[i].NEntradas + "</td>" +
                                          "<td>" + data[i].NEntradasJoven + "</td>" +
                                          "<td>" + data[i].Precio + "</td>" +
                                          "</tr>")
                    };
                });

                $("#content").append("<button type='button' style='margin-bottom: 10px; margin-left: 10px;float:right;' class='btn btn-danger'>Cancelar</button>");
                btnCancelar();

            });
        });
    };


    return my;


}(Module || {}));

