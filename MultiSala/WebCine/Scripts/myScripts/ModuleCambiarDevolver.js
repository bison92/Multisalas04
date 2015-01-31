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
        handlers: [function () { _my.handlers.cargaIndex(); }, function () { _my.rutas.venderPedirDatos(); },],
        hidden: [true, true, true, true],
        disabled: [true, true, true, true, true],
    }

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


    }
    _my.handlers.CargarCrearDevolucion = function () {
        var identificador = $("#ventaid").val();
        $.get("/api/venta/" + identificador, "", function (data) {
            if (data.VentaId == 0) {
                alert("No existe la venta con número: " + identificador);
                $("#ventaid").val("");
            } else {
                _my.render('venta', 'DatosDevolucion', data, _my.helpers.descargaVentas);
            }
        }, "json");
    };

    return _my;
}(Module || {}));