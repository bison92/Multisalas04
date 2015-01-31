
var Module = (function (_my) {
    // Constantes object 
    _my.Constantes = {};
    _my.Constantes.url = "/api/status"
    _my.Constantes.Precio = 0;
    _my.Constantes.DescuentoHumbral = 0;
    _my.Constantes.DescuentoGrupo = 0;
    _my.Constantes.DescuentoGrupoJoven = 0;
    _my.Constantes.loadStatus = function () {
        $.ajax({
            url: _my.Constantes.url,
            type: "get",
            dataType: "json",
            success: function (data) {
                for (property in data) {
                    console.log(property);
                    if (_my.Constantes.hasOwnProperty(property.toString())) {
                        _my.Constantes[property.toString()] = data[property];
                    }
                }
            },
        });
    };
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

    // State Object (Estado)
    // contienen la información que necesita render para pintar cada estado en un formulario o fragmento html.
    // cada estado es un objeto con la siguientes propiedades:
    //  + title: String, el título de la página.
    //  
    //  + botones : Array of HTMLString, listado ordenado de botones que tiene la vista.
    //  + handlers : Array of FunctionHandlers, listado ordenado de funciones (event handlers) que se disparan 
    //               cuando se hace click en los botones definidos.
    //// !! Para asociar el botón al event handler utilizamos su posición en el array, que ha de ser la misma.
    //// 

    _my.states = {};
    _my.states["home"] = {
        title: "CyC",
    };

    _my.states["venderPedirDatos"] = {
        title: "Crear venta",
        hidden : [false, false, false, false, false],
        disabled: [true, false, false, false, true],
        botones: [
            _my.botones.btncomprar,
            _my.botones.btnlimpiar,
            _my.botones.btnvolver,
        ],
        handlers: [
            function () {
                _my.handlers.comprobarVenta();
            },
            function () {
                _my.handlers.resetForm();
            },
        ],
    }
    _my.states["cambioDevolucionPedirDatos"] = {
        title: "Modificar venta",
        botones: [
            _my.botones.btndevolucionventa,
            _my.botones.btncambiar,
            _my.botones.btnvolver,
            _my.botones.btncomprar,
        ],
        handlers: [
            function () {
                _my.handlers.CargarCrearDevolucion();
            },
        ],
    };
    _my.states["seleccionaSesion"] = {
        title: "Seleccione la Sesion",
        botones: [_my.botones.btnversesion, _my.botones.btnvolver],
        handlers: [function () { _my.handlers.SeleccionaSesion(); }, function () { _my.handlers.volveralprincipio() }],
    };
    // handlers 
    // namespace para los

    _my.handlers = {};

    _my.handlers.volveralprincipio = function () {
        $("#actions").html("");
         _my.handlers.cargaIndex();
     
    }


    

    _my.handlers.cargaIndex = function () {

        _my.render('home', 'home', function () {
            $("#btn-create-venta").click(_my.rutas.venderPedirDatos);
            $("#btn-cambia-venta").click(_my.rutas.cambioDevolucionPedirDatos);
            $("#btn-listado-sesiones").click(_my.rutas.seleccionaSesion);
        });
    };

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

    // helpers, aquí metemos los cargadores y descargadores.

    _my.helpers = {};
    _my.helpers.descargaVentas = function (venta) {
        $("#ventaid").val(venta.ID),
        $("#sesionid").val(venta.SesionID),
        $("#nentradas").val(venta.NEntradas),
        $("#nentradasjoven").val(venta.NEntradasJoven),
        $("#precio").val(venta.Precio)
    };
    _my.helpers.cargaVentas = function () {
        var venta = {
            ID: $("#ventaid").val(),
            SesionID: $("#sesionid").val(),
            NEntradas: $("#nentradas").val(),
            NEntradasJoven: $("#nentradasjoven").val(),
            Precio: $("#precio").val(),
        };
        return venta;
    };


    _my.rutas["seleccionaSesion"] = function () {
        console.log("llamado");
        _my.render('seleccionaSesion', 'seleccionaSesion');
    }


    //render
    // la función render toma una vista, un estado y datos obtenidos a través de una petición AJAX para renderizar un formulario cargado.
    // el único parámetro obligatorio es la vista.
    // el parametro de estado contiene la información del estado de la pantalla, los botones y sus acciones, el título, el estado de los inputs (disabled)
    _my.render = function (view, state, dataorcb, descargador, cb) {
        console.log("Render Call: view=" + view + "; action=" + state + "; data=" + JSON.stringify(dataorcb) + ";");
        var actualAction = _my.states[state] || null;
        
        var responseCallback = function (result) {
            if (actualAction) {
                console.log("State :" + JSON.stringify(actualAction));
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
            if (dataorcb) {
                if (dataorcb !== Function) {

                    if (dataorcb.Precio) {
                        dataorcb.Precio = Number(dataorcb.Precio).toFixed(2);
                    }
                    if (descargador === Function) {
                        descargador(dataorcb);
                    }
                }
            }
            if (arguments[2] === Function || arguments[4] === Function) {
                if (arguments[2] === Function) {
                    arguments[2]();
                } else {
                    arguments[4]();
                }
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