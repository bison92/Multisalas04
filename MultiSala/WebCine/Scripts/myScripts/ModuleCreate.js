var Module = (function (_my) {
    _my.states.confirmarVenta = {
        title : "Confirme la venta",
        disabled: [true, true, true, true, true],
        botones: [_my.botones.btnconfirmarcompra, _my.botones.btncorregir, _my.botones.btnlimpiar, _my.botones.btnvolver],
        //handlers: [function() { _my.botones.},]

    };
    _my.handlers.comprobarVenta = function () {
        var model = _my.helpers.cargaVentas();
        console.log(model);
        $.get("/api/venta"+model.SesionID+"/"+model.NEntradas+"/"+model.NEntradasJoven, model, function (data) {
            model.Precio = data;
            _my.render('venta', 'confirmarVenta' )
        }, "json");
    };
    return _my;
}(Module || {}));