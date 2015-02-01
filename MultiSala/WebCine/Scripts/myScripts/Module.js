
var Module = (function (_my) {
    // obtiene el listado de sesiones.
    _my.listadoSesiones = new Array();
    $.ajax('/api/sesion', {
        success: function (data) {
            _my.listadoSesiones = data;
        },
    });
    // constantes object 
    _my.constantes = {};
    _my.constantes.url = "/api/status"
    _my.constantes.Precio = 0;
    _my.constantes.DescuentoHumbral = 0;
    _my.constantes.DescuentoGrupo = 0;
    _my.constantes.DescuentoGrupoJoven = 0;
    _my.constantes.loadStatus = function () {
        $.ajax({
            url: _my.constantes.url,
            type: "get",
            dataType: "json",
            success: function (data) {
                for (property in data) {
                    if (_my.constantes.hasOwnProperty(property)) {
                        _my.constantes[property] = data[property];
                    }
                }
            },
        });
    };
    _my.constantes.loadStatus();
    // botones 
    // colección de Markup de los distintos botones de la aplicación para maximizar su reutilización
    _my.botones = {};
    _my.botones["btndevolucionventa"] = "<button type=\"button\" class=\"btn btn-primary\" id=\"btn-devolucion-venta\">Devolver</button>&nbsp;";
    _my.botones["btncorregir"] = "<button type=\"button\" class=\"btn btn-primary\" id=\"btn-corregir\">Corregir</button>&nbsp;";
    _my.botones["btnvolver"] = "<button type=\"button\" class=\"btn btn-primary\" id=\"btn-volver\">Volver</button>&nbsp;";
    _my.botones["btncomprar"] = "<button type=\"button\" class=\"btn btn-primary\" id=\"btn-comprar\">Comprar</button>&nbsp;";
    _my.botones["btncancelar"] = "<button type=\"button\" class=\"btn btn-primary\" id=\"btn-cancelar\">Cancelar</button>&nbsp;";
    _my.botones["btncambiar"] = "<button type=\"button\" class=\"btn btn-primary\" id=\"btn-cambiar\">Cambiar</button>&nbsp;";
    _my.botones["btnlimpiar"] = "<button type=\"reset\"  class=\"btn btn-primary\"  id=\"btn-limpiar\">Limpiar</button>&nbsp;";
    _my.botones["btnconfirmarcompra"] = "<button type=\"button\" class=\"btn btn-primary\" id=\"btn-confirmarcompra\">Confirmar Compra</button>&nbsp;";
    _my.botones["btnconfirmarcambio"] = "<button type=\"button\" class=\"btn btn-primary\" id=\"btn-confirmarcambio\">Confirmar Cambio</button>&nbsp;";
    _my.botones["btnconfirmardevolucion"] = "<button type=\"button\" class=\"btn btn-primary\" id=\"btn-confirmardevolucion\">Confirmar Devolucion</button>&nbsp;";
    _my.botones["btnversesion"] = "<button type=\"button\" class=\"btn btn-primary\" id=\"btn-versesion\">Ver Sesion</button>&nbsp;";

    // State Object (Estado)
    // contienen la información que necesita render para pintar cada estado en un formulario o fragmento html.
    // cada estado es un objeto con la siguientes propiedades:
    //  + title: String, el título de la página.
    //
    //  + hidden : para cada etiqueta input decide si está o no escondida (el partial debe de tener la structura <div><input></div>
    //  + disabled : para cada etiqueta input decide si está o no hablitada.
    //
    //  + botones : Array of HTMLString, listado ordenado de botones que tiene la vista.
    //  + handlers : Array of FunctionHandlers, listado ordenado de funciones (event handlers) que se disparan 
    //               cuando se hace click en los botones definidos.
    //// !! Para asociar el botón al event handler utilizamos su posición en el array, que ha de ser la misma.
    //// 

    _my.states = {};

    // estado inicial
    _my.states["home"] = {
        title: "CyC",
    };
    // 1er estado crear venta.
    _my.states["venderPedirDatos"] = {

        title: "Crear venta",

        hidden: [true, false, false, true],
        disabled: [true, false, false, true],

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
                _my.handlers.limpiarVenta();
            },
            function () {
                _my.rutas.Index();
            },
        ],
    };
    // 1er estado cambiar / devolver venta
    _my.states["cambioDevolucionPedirDatos"] = {

        title: "Modificar venta",

        botones: [
            _my.botones.btndevolucionventa,
            _my.botones.btncambiar,
            _my.botones.btncomprar,
            _my.botones.btnvolver,
        ],
        handlers: [
            function () {
                _my.handlers.CargarCrear("devolucion");
            },
            function () {
                _my.handlers.CargarCrear("cambio");
            },
            function () {
                _my.rutas.venderPedirDatos();
            },
            function () {
                _my.rutas.Index();
            },
        ],
    };

    // 1er estado listado sesiones
    _my.states["seleccionaSesion"] = {
        title: "Seleccione la Sesion",
        botones: [
            _my.botones.btnversesion,
            _my.botones.btnvolver
        ],
        handlers: [
            function () {
                _my.handlers.SeleccionaSesion();
            },
            function () {
                _my.rutas.Index();
            }],
    };

    // handlers 
    // namespace para los manejadores de eventos, funciones que se ejecutan como resultado de los clicks/cambios del usuario en los controles de la aplicación.

    _my.handlers = {};

    // son los handlers de los botones de inicio ¬¬

    _my.rutas = {};
    _my.rutas.Index = function () {
        $("#actions").html("");
        _my.render('home', 'home', function () {
            $("#btn-create-venta").click(_my.rutas.venderPedirDatos);
            $("#btn-cambia-venta").click(_my.rutas.cambioDevolucionPedirDatos);
            $("#btn-listado-sesiones").click(_my.rutas.seleccionaSesion);
        });
    };
    _my.rutas["venderPedirDatos"] = function () {

        // procesamos las sesiones
        _my.states.venderPedirDatos.partialData = {};
        _my.states.venderPedirDatos.partialData.items = [];
        _.each(_my.listadoSesiones, function (element, key) {
            if (element.Abierto == true) {

                _my.states.venderPedirDatos.partialData.items.push(element);
            }
        });
        _my.render('preVenta', 'venderPedirDatos', function () {
            $.when(function () { return _my.helpers.entradasDisponibles($("#sesionid").val()); }()).then(function (disponibles) {
                $("#nentradasdisponibles").text(disponibles); // primera cargar
                $("#sesionid").change(function () {
                    $.when(function () { return _my.helpers.entradasDisponibles($("#sesionid").val()); }()).then(function (disponibles) {
                        $("#nentradasdisponibles").text(disponibles); // el resto de cambios.
                    });
                });
                $("#updatedisponibles").click(function () {
                    $("#updatedisponibles").attr("disabled", true);
                    $.when(function () { return _my.helpers.entradasDisponibles($("#sesionid").val()); }()).then(function (disponibles) {
                        $("#nentradasdisponibles").text(disponibles);
                        $("#updatedisponibles").attr("disabled", false);
                    });
                });
            });

        });
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
        $("#ventaid").val(venta.VentaId),
        $("#sesionid").val(venta.SesionId),
        $("#nentradas").val(venta.NEntradas),
        $("#nentradasjoven").val(venta.NEntradasJoven),
        $("#precio").val(venta.Precio)
    };
    _my.helpers.cargaVentas = function () {
        var venta = {
            VentaId: Number($("#ventaid").val()),
            SesionId: Number($("#sesionid").val()),
            NEntradas: Number($("#nentradas").val()),
            NEntradasJoven: Number($("#nentradasjoven").val()),
            Precio: $("#precio").val(),
        };
        return venta;
    };

    //render
    // la función render toma una vista, un estado y datos obtenidos a través de una petición AJAX para renderizar un formulario cargado.
    // el único parámetro obligatorio es la vista. Con ayuda de esta función nos podremos centrar en definir estados y transiciones dejando la parte del 
    // el parametro de estado contiene la información del estado de la pantalla, los botones y sus acciones, el título, el estado de los inputs (disabled)
    // el tercer parámetro es el JSON resultante de la petición AJAX desde la que llamamos a render, o bien el callback que se ejecutará cuando se complete el procesado de render.
    // el cuarto parámetro es la función utilizada para descargar los datos JSON a los inputs correspondientes var descargador = function(data) { $("#id").val(data.id); ...; };
    // el quinto parámetro es el callback que se ejecutará cuando se complete el procesado.
    // EJEMPLOS de _my.render SIN DATOS JSON.
    // _my.render('vista', 'estadovista');
    // _my.render('vista', 'estadovista', function() {
    //    $("#justRederedInput").change(...);
    // };
    // EJEMPLO de uso CON DATOS JSON obtenidos mediante una petición ajax.
    // _my.helpers.descargador = function(data) { $("#id").val(data.id); ...; };
    // $.ajax({
    //    type: "get",
    //    url: "/api/resource"
    //    dataType: "json",
    //    success: function(data) {
    //        _my.render('vista', 'estadovista', data, _my.helpers.descargador, function(){
    //          alert("complete :)");
    //        });
    //    },
    // });
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
                if (actualAction.hidden != null && actualAction.hidden.length != 0) {
                    for (var i = 0; i < actualAction.hidden.length; i++) {
                        if (actualAction.hidden[i]) {
                            inputs.eq(i).parent().attr("style", "display:none;");
                        }
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
                        //console.log(idboton);
                        //console.log(actualAction.handlers[i]);
                        $("#" + idboton).click(actualAction.handlers[i]);
                    }
                }
            } else {
                $(".site-title").text("");
            }
            var htmlFinal = source.html();
            $("#main").html(htmlFinal);
            if (dataorcb) {
                if (!_.isFunction(dataorcb)) {
                    if (dataorcb.Precio) {
                        dataorcb.Precio = Number(dataorcb.Precio).toFixed(2) + "€";
                    }
                    if (typeof descargador === 'function') {
                        descargador(dataorcb);
                    }
                    if (cb) {
                        cb();
                    }
                } else {
                    dataorcb();
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