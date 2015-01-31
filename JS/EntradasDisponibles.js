/*var datos = {
	id: "",
	entradas: ""
};
var controlador = {
	carga: function() {
		var res = {
			id: $("#id").attr("value") || ""
		};
		return res;	
	},
	descarga: function(d) {
		//$("#id").attr("value", d.id);
		$("#entradas").attr("value", d.entradas);
	}
};*/

var VentasModule = (function(my){

	var datosEntradasDisponibles = {
		id: "",
		entradas: ""
	};

	my.cargaEntradasDisponibles = function() {
		var res = {
			id: $("#id").attr("value") || ""
		};
		return res;	
	};

	my.descargaEntradasDisponibles = function(d) {
		//$("#id").attr("value", d.id);
		$("#entradas").attr("value", d.entradas);
	};

}(VentasModule || {}));


