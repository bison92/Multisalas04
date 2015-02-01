var Module = (function (_my) {

    _my.states["DatosDevolucion"] = {
        title: "Datos Devolucion",
        disabled: [true, true, true, true, true],
        botones: [_my.botones.btnconfirmardevolucion, _my.botones.btncomprar, _my.botones.btncancelar],
        handlers: [function () { _my.handlers.ConfirmarDevolucion(); }, function () { _my.rutas.venderPedirDatos(); }, function () { _my.rutas.cambioDevolucionPedirDatos() }],

    };

    _my.states["ConfirmarDevolucion"] = {
        title: "Venta Devuelta",
        botones: [_my.botones.btnvolver, _my.botones.btncomprar],
        handlers: [function () { _my.rutas.Index(); }, function () { _my.rutas.venderPedirDatos(); }, ],
        hidden: [true, true, true, true],
        disabled: [true, true, true, true, true],
    };

    _my.states["DatosCambio"] = {
        title: "Efectue los cambios",
        disabled: [true, false, false, false, true],
        botones: [_my.botones.btncambiar, _my.botones.btnvolver],
        handlers: [function () { _my.handlers.ComprobarCambio(); }, function () { _my.rutas.cambioDevolucionPedirDatos(); }, ],
    };

    _my.states["ConfirmarCambio"] = {
        title: "Confirme los cambios en la venta",
        hidden: [false, false, false, false, false],
        disabled: [true, true, true, true, true],
        botones: [
            _my.botones.btnconfirmarcambio,
            _my.botones.btncorregir,
            _my.botones.btncomprar,
        ],
        handlers: [
            function () {
                _my.handlers.ConfirmarCambio();
            },
            function () {
                _my.handlers.CorregirCambio();
            },
            function () {
                _my.rutas.venderPedirDatos();
            },
        ],
    };
    _my.states["CambioConfirmado"] = {
        title: "Cambios confirmados",
        botones: [_my.botones.btnvolver, _my.botones.btncomprar],
        handlers: [function () { _my.rutas.Index(); }, function () { _my.rutas.venderPedirDatos(); }, ],
        disabled: [true, true, true, true, true],
    };


    _my.handlers.CargarCrear = function (action) {
        if (action == "devolucion") {
            $("#btn-devolucion-venta").attr("disabled", true);
        }
        if (action == "cambiar") {
            $("#btn-cambiar").attr("disabled", true);
        }
        var identificador = $("#ventaid").val();
        if (identificador) {
            $.get("/api/venta/" + identificador, "", function (data) {
                if (data.VentaId == 0) {
                    alert("No existe la venta con número: " + identificador);
                    $("#ventaid").val("");
                } else {
                    if (action == "devolucion") {
                        _my.render('venta', 'DatosDevolucion', data, _my.helpers.descargaVentas);
                    }
                    if (action == "cambio") {
                        _my.render('venta', 'DatosCambio', data, _my.helpers.descargaVentas);
                    }
                }
            }, "json");

        } else {
            alert("Introduzca un identificador de venta válido.");
            $("#btn-devolucion-venta").attr("disabled", false);
            $("#btn-cambiar").attr("disabled", false);
        }
    };

    _my.handlers.ConfirmarDevolucion = function () {
        $("#btndevolucionventa").attr("disabled", true);
        var identificador = $("#ventaid").val();
        if (confirm("¿Seguro que desea devolver la venta #" + identificador + "?")) {
            $.post("../../api/venta/devolverVenta/" + identificador).done(function (data) {
                send = {
                    Precio: data,
                };
                _my.render('venta', 'ConfirmarDevolucion', send, _my.helpers.descargaVentas);
            });
        }
    };

    _my.handlers.ComprobarCambio = function () {
        $("#btn-confirmarcambio").attr("disabled", true);
        var model = _my.helpers.cargaVentas();
        $.when($.ajax({
            url: "/api/venta/" + model.VentaId,
            type: "get",
            success: function (data) {
                _my.helpers.comprobarVenta(model, function () {
                    model.Precio = _my.helpers.calculaPrecio(model);
                    _my.render('venta', 'ConfirmarCambio', model, _my.helpers.descargaVentas, function () {
                        $("#precio").parent().parent().append($("<div>").append("<label>Cambio</label>").attr({
                            "for": "cambio",
                        }).parent().append("<input>").attr({
                            id: "cambio",
                            type: "text",
                            value: (model.Precio - data.Precio),
                        }));
                    });
                }, function () {
                    $("#btn-confirmarcambio").attr("disabled", false);
                }, data);
            }
        }))
    }
    _my.handlers.CorregirCambio = function () {
        var model = _my.helpers.cargaVentas();
        model.Precio = _my.helpers.calculaPrecio(model);
        _my.render('venta', 'DatosCambio', model, _my.helpers.descargaVentas);
    }
    _my.handlers.ConfirmarCambio = function () {
        $("#btndevolucionventa").attr("disabled", true);
        var identificador = $("#ventaid").val();
        var model = _my.helpers.cargaVentas();
        if (confirm("¿Seguro que desea confirmar los cambios de la venta #" + identificador + "?")) {
            $.ajax({
                url: "/api/venta/" + identificador,
                type: "put",
                dataType: "json",
                data: model,
                success: function (data) {
                    _my.render("venta", "CambioConfirmado", data, _my.helpers.descargaVentas);
                },
                error: function (error) {
                    console.log(error);
                    alert("Error, " + error.responseJSON.ExceptionMessage);
                    _my.handlers.CorregirCambio();
                },
            });
        } else {

        }
    }
    return _my;
}(Module || {}));