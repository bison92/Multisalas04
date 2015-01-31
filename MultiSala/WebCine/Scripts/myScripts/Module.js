
var Module = (function (_my) {
    // botones    
    _my.botones = {};
    _my.botones["btndevolucionventa"] = "<button type=\"button\" class=\"btn btn-info\" id=\"btn-devolucion-venta\">Devolver</button>";
    _my.botones["btncorregir"] = "<button type=\"button\" class=\"btn btn-info\" id=\"btn-corregir\">Corregir</button>";
    _my.botones["btnvolver"] = "<button type=\"button\" class=\"btn btn-info\" id=\"btn-volver\">Volver</button>";
    _my.botones["btncomprar"] = "<button type=\"button\" class=\"btn btn-info\" id=\"btn-comprar\">Comprar</button>";
    _my.botones["btncancelar"] = "<button type=\"button\" class=\"btn btn-info\" id=\"btn-cancelar\">Cancelar</button>";
    _my.botones["btncambiar"] = "<button type=\"button\" class=\"btn btn-info\" id=\"btn-cambiar\">Cambiar</button>";
    _my.botones["btnlimpiar"] = "<button type=\"reset\"  class=\"btn btn-info\"  id=\"btn-limpiar\">Limpiar</button>";
    _my.botones["btnconfirmarcompra"] = "<button type=\"button\" class=\"btn btn-info\" id=\"btn-confirmarcompra\">ConfirmarCompra</button>";
    _my.botones["btnconfirmarcambio"] = "<button type=\"button\" class=\"btn btn-info\" id=\"btn-confirmarcambio\">ConfirmarCambio</button>";
    _my.botones["btnconfirmardevolucion"] = "<button type=\"button\" class=\"btn btn-info\" id=\"btn-confirmardevolucion\">ConfirmarDevolucion</button>";
    _my.botones["btnversesion"] = "<button type=\"button\" class=\"btn btn-info\" id=\"btn-versesion\">VerSesion</button>";
    // estados
    // estados
    _my.states = {};
    _my.states["home"] = {
        title: "CyC",
    };

    _my.states["venderPedirDatos"] = {
        title: "Crear venta",
        botones: [_my.botones.btncomprar, _my.botones.btnlimpiar, _my.botones.btnvolver],
        disabled: [true, false, false, false, true],
        handlers: [function () { _my.handlers.comprobarVenta(); }],
    }
    _my.states["cambioDevolucionPedirDatos"] = {
        title: "Modificar venta",
        botones: [_my.botones.btndevolucionventa, _my.botones.btncambiar, _my.botones.btnvolver, _my.botones.btncomprar],
        handlers: [function () {  _my.handlers.CargarCrearDevolucion(); }],
    };
    _my.states["seleccionaSesion"] = {
        title: "Seleccione la Sesion",
        botones: [_my.botones.btnversesion, _my.botones.btnvolver],
        handlers: [function () {_my.handlers.SeleccionaSesion(); }, function () {_my.handlers.volveralprincipio()}],
    }
   
    _my.cargaIndex = function () {

        this.render('home', 'home', null, null, function () {
            $("#btn-create-venta").click(_my.rutas.venderPedirDatos);
            $("#btn-cambia-venta").click(_my.rutas.cambioDevolucionPedirDatos);
            $("#btn-listado-sesiones").click(_my.rutas.seleccionaSesion);
        });
    };

    // handlers 

    _my.handlers = {};

    _my.handlers.volveralprincipio = function () {
        $("#actions").html("");
         _my.cargaIndex();
     
    }


    // rutas 
    
    _my.rutas = {};

    _my.rutas["venderPedirDatos"] = function () {
        _my.render('venta', 'venderPedirDatos');
    }

    _my.rutas["cambioDevolucionPedirDatos"] = function () {
        _my.render('cambioDevolucionPedirDatos', 'cambioDevolucionPedirDatos');
    }
    _my.rutas["seleccionaSesion"] = function () {
        _my.render('seleccionaSesion', 'seleccionaSesion');
    }

    // cargadores / descargadores

    _my.helpers = {};
    _my.helpers.descargaVentas = function (data) {
        $("#ventaid").val(data.ID),
        $("#sesionid").val(data.SesionID),
        $("#nentradas").val(data.NEntradas),
        $("#nentradasjoven").val(data.NEntradasJoven),
        $("#precio").val(data.Precio)
    };
    _my.helpers.cargaVentas = function () {
        var objeto = {
            ID: $("#ventaid").val(),
            SesionID: $("#sesionid").val(),
            NEntradas: $("#nentradas").val(),
            NEntradasJoven: $("#nentradasjoven").val(),
            Precio: $("#precio").val(),
        };
        return objeto;
    };


    _my.rutas["seleccionaSesion"] = function () {
        console.log("llamado");
        _my.render('seleccionaSesion', 'seleccionaSesion');
    }


    //render

    _my.render = function (view, state, data, descargador, cb) {
        console.log("Render Call: view=" + view + "; action=" + state + "; data=" + JSON.stringify(data) + ";");
        var actualAction = _my.states[state] || null;
        var responseCallback = function (result) {
            if (actualAction) {
                if (typeof (actualAction.partialData) != 'undefined' && actualAction.partialData != null) {
                    result = _.template(result)(actualAction.partialData);
                }
            }
            var source = $(result);
            if (actualAction) {
                $(".site-title").text(actualAction.title);
                var inputs = source.find("input");
                if (actualAction.disabled != null && actualAction.disabled.length != 0) {
                    for (var i = 0; i < actualAction.disabled.length; i++) {
                        inputs.eq(i).attr("disabled", actualAction.disabled[i]);
                    }
                }
                if (actualAction.botones != null && actualAction.botones.length != 0) {
                    $("#actions").html("");
                    for (var i = 0; i < actualAction.botones.length; i++) {
                        $("#actions").append(actualAction.botones[i]);
                    }
                }
                if (actualAction.handlers != null && actualAction.handlers.length != 0) {
                    for (var i = 0; i < actualAction.handlers.length; i++) {
                        var idboton = $(actualAction.botones[i]).attr("id");
                        console.log(idboton);
                        console.log(actualAction.handlers[i]);
                        $("#" + idboton).click(actualAction.handlers[i]);
                    }
                }
            } else {
                $(".site-title").text("");
            }
            var htmlFinal = source.html();
            $("#main").html(htmlFinal);
            if (data) {
                if (data.Precio) {
                    data.Precio = Number(data.Precio).toFixed(2);
                }
                descargador(data);
            }
            if (cb) {
                cb();
            }
        };
        $.ajax({
            url: "../Content/snippets/" + view + ".html",
            success: responseCallback,
            dataType: "html",
        });
    };

    return _my;


}(Module || {}));