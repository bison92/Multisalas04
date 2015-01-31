/*var datos = {
	id: "1",
	estado: "abierto"
};

var controlador = {
	carga: function() {
		var res = {
			id: $("#id").attr("value") || ""
		};
		return res;	
	},
	descarga: function(d) {
		$("#id").attr("value", d.id);
		$("#estado").attr("value", d.estado);
	}
};*/

var VentasModule = (function(my){

	var datosGestionSesion = {
		id: "1",
		estado: "abierto"
	};

	my.cargaGestionSesion = function() {
		var res = {
			id: $("#id").attr("value") || ""
		};
		return res;	
	};

	my.descargaGestionSesion = function(d) {
		$("#id").attr("value", d.id);
		$("#estado").attr("value", d.estado);
	};

}(VentasModule || {}));