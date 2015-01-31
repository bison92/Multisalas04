var cambio = {
	venta_id: "",
	sesion_id: ""
};

			
var controlador = {
	id: 1,
	datos: cambio,
	descarga: function() {
		var formulario = $( '#'+this.id );
		formulario.find("input[name='venta_id']").val(this.datos.venta_id);
		formulario.find("input[name='sesion_id']").val(this.datos.sesion_id);
	},
	carga: function() {		
		var formulario = $( '#'+this.id );
		this.datos.venta_id = formulario.find("input[name='venta_id']").val();
		this.datos.sesion_id = formulario.find("input[name='sesion_id']").val();
	}
};

/*
$(document).ready(function() {
	controlador.carga();
	console.log(cambio.venta_id);
	console.log(cambio.sesion_id);
	
	cambio.venta_id = "2";
	cambio.sesion_id = "6";
	controlador.descarga();
})
*/