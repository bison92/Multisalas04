var Module = (function (_my) {
    // metodos de validación y cálculo de precio.
    _my.helpers.calculaPrecio = function(model){
        if(_my.constantes.Precio == 0){
            _my.constantes.loadStatus();
        }
        var entradasSinDescuento = model.NEntradas - model.NEntradasJoven;
        var precioEntradas = model.NEntradasJoven * _my.constantes.Precio * _my.constantes.DescuentoGrupoJoven;
        var precioEntradasNormales = entradasSinDescuento * _my.constantes.Precio;
        if (entradasSinDescuento >= _my.constantes.DescuentoHumbral) {
            precioEntradasNormales *= _my.constantes.DescuentoGrupo;
        }
        precioEntradas += precioEntradasNormales;
        return precioEntradas;
    }
    var validaFormulario = function (model) {
        valido = true;
        if (model.SesionId == "" || model.NEntradas == "" || model.NEntradas <= 0) {
            alert("Debe escoger una sesión y un número de entradas mayor a 0.");
            valido = false;
        }
        if (valido && model.NEntradasJoven > model.NEntradas) {
            alert("Revise el número de entradas: las entradas totales deben incluir las entradas con carnet joven.");
            valido = false;
        }
        if (isNaN(model.NEntradas) || isNaN(model.NEntradasJoven)) {
            alert("Los número de entradas solo pueden ser números");
            if (isNaN(model.NEntradas)) {
                $("#nentradas").val("");
            }
            if (isNaN(model.NEntradasJoven)) {
                $("#nentradasjoven").val("");
            }
            valido = false;
        }
        return valido;
    };
    _my.helpers.entradasDisponibles = function (sesionId) {
        return $.ajax({
            type: "get",
            url: "/api/venta/entradasDisponibles/" + sesionId,
        });
    };
    var validaSesionAbierta = function (sesionId) {
        return $.ajax({
            type: "get",
            url: "/api/sesion/" + sesionId,
        });
    }
    // estados, 2a pantalla de venta.
    _my.states.confirmarVenta = {
        title : "Confirme la venta",
        hidden: [true, false, false, false, false],
        disabled: [true, true, true, true, true],
        botones: [
            _my.botones.btnconfirmarcompra,
            _my.botones.btncorregir,
            _my.botones.btnlimpiar,
            _my.botones.btnvolver,
        ],
        handlers: [
            function () {
                _my.handlers.confirmarVenta();
            },
            function () {
                _my.handlers.corregirVenta();
            },
            function () {
                _my.handlers.limpiarVenta();
            },
            function () {
                _my.rutas.Index();
            },
        ],
    };
    // estados 3a pantalla de venta.
    _my.states.ventaConfirmada = {
        title: "Venta confirmada",
        hidden: [], //defaults to false.
        disabled: [true, true, true, true, true],
        botones: [
            _my.botones.btndevolucionventa,
            _my.botones.btnvolver
        ],
        handlers: [
            function () {
                _my.handlers.CargarCrear("devolucion");
            },
            function () {
                _my.handlers.limpiarVenta();
            },
        ],
    };

    _my.helpers.comprobarVenta = function (model, cb, errcb, oldmodel) {
        if (validaFormulario(model)) {
            $.when(validaSesionAbierta(model.SesionId), _my.helpers.entradasDisponibles(model.SesionId))
                .then(function (sesion, entradasDisponibles) {
                    if (sesion[1] == "success") {
                        if (sesion[0].Abierto) {
                            if (entradasDisponibles[1] == "success") {
                                if (oldmodel && oldmodel.SesionId == model.SesionId)  {
                                    entradasDisponibles[0] += oldmodel.NEntradas;
                                }
                                if (Number(entradasDisponibles[0]) < model.NEntradas) {
                                    alert("No hay suficientes butacas disponibles para la sesión solicitada (quedan "+entradasDisponibles[0]+" butacas disponibles)");
                                    if (errcb) {
                                        errcb();
                                    }
                                } else {
                                    cb();
                                }
                            }
                        } else {
                            alert("La sesión seleccionada está cerrada");
                            if (errcb) {
                                errcb();
                            }
                        }
                    }
                });
        } else {
            if (errcb) {
                errcb();
            }
        }
    };
    _my.handlers.comprobarVenta = function () {
        $("#btncomprar").attr("disabled", true);
        var model = _my.helpers.cargaVentas();
        _my.helpers.comprobarVenta(model, function () {
            model.Precio = _my.helpers.calculaPrecio(model);
            _my.render('venta', 'confirmarVenta', model, _my.helpers.descargaVentas);
        });                 
    };
    _my.handlers.confirmarVenta = function() {
        $("#btn-confirmarcompra").attr("disabled", true);
        var model = _my.helpers.cargaVentas();
        model.VentaId = 0;
        model.Precio = 0;
        $.post("/api/venta", model, function(data) {
            _my.render('venta', 'ventaConfirmada', data, _my.helpers.descargaVentas);
        }).error(function(error){
            console.log(error);
            alert("Error, " + error.responseJSON.ExceptionMessage);
            _my.rutas.venderPedirDatos();
        });
    }
    _my.handlers.corregirVenta = function () {
        var model = _my.helpers.cargaVentas();
        model.VentaId = 0;
        model.Precio = 0;
        _my.render('preVenta', 'venderPedirDatos', model, _my.helpers.descargaVentas);
    }
    _my.handlers.limpiarVenta = function () {
        _my.rutas.venderPedirDatos();
    }
    return _my;
}(Module || {}));