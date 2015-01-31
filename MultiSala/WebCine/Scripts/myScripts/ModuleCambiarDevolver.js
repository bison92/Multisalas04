var Module = (function (_my) {

    var btnCancelar = function () {
        $(".btn-danger").click(function () {
            $.Reload();
        });
    };

    _my.states["DatosDevolucion"] = {
        title: "DatosDevolucion",
        disabled: [true, true, true, true, true],
        botones: [_my.botones.btnconfirmardevolucion, _my.botones.btncancelar, _my.botones.btncomprar],
        handlers: [function () { _my.handlers.ConfirmarDevolucion(); }, function () { _my.handlers.SeleccionaSesion; }, function () { _my.handlers.SeleccionaSesion; }, ],

    };
    _my.helpers.descargaTotalDevolucion = function (data) {
        $("#precioDevolucion").val(data);
    }
    _my.states["ConfirmarDevolucion"] = {
        title: "ConfirmarDevolucion",
        botones: [_my.botones.btnvolver, _my.botones.btncomprar],
        handlers: [function () { _my.handlers.ConfirmarDevolucion(); }],
        disabled : [true],
    }
    _my.handlers.DatosDevolucion = function () {
        $("#")
        $("DatosDevolucion").html(_my.helpers.descargaVentas);
        $("_my.botones.btcancelar").click(function () { _my.handlers.DatosDevolucion(); });
    };

    _my.handlers.ConfirmarDevolucion = function () {
        var identificador = $("#ventaid").val();
        $.post("../../api/venta/devolverVenta/" + identificador,
            {
                ID: identificador,

            }, function (data) {
                alert("As devuelto tu venta");
                $("#precioDevolucion").val();
                $("#precio").val();
                $("precioDevolucion").text(data);
                _my.render('cambiarDevolverSnippet', 'ConfirmarDevolucion', data, _my.helpers.descargaTotalDevolucion);

            });


    }
    _my.handlers.CargarCrearDevolucion = function () {
        var identificador = $("#ventaid").val();
        $.get("/api/venta/" + identificador, "", function (data) {
            _my.render('venta', 'DatosDevolucion', data, _my.helpers.descargaVentas);
        }, "json");








    };




    _my.handlers.RealizarCambario = $("#btnconfirmarcambio").click(function () {
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




    return _my;
}(Module || {}));