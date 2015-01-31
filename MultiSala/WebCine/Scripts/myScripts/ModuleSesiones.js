var Module = (function (_my) {
    
    _my.states["listadoSesionesSnippet"] = {
        title: "listado de Sesiones Seleccionadas",
        botones: [ _my.botones.btncorregir,_my.botones.btnvolver],
        handlers: [function () { return _my.handlers.corregir(); }, function () { return _my.handlers.volveralprincipio(); }],
        partialData: {
            items: null,
        }
    }
   
    _my.handlers.SeleccionaSesion = function () {
        var identificador = $("#fechadesesion").val();
        $.get("/api/sesion", "", function (data) {
            _my.states.listadoSesionesSnippet.partialData.items = data;
            _my.render('listadoSesionesSnippet', 'listadoSesionesSnippet');
        }, "json");
    }

    _my.handlers.corregir = function () {
        alert("Demasiado para mi");
    }


   

  

    return _my;
}

(Module || {}));