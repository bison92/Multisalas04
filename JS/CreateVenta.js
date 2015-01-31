var venta = {
	sesion_id: "",
	n_entradas: "",
	n_joven: "",
	precio: ""
};

			
var controlador = {
	id: 1,
	datos: venta,
	descarga: function() {
		var formulario = $( '#'+this.id );
		formulario.find("input[name='sesion_id']").val(this.datos.sesion_id);
		formulario.find("input[name='n_entradas']").val(this.datos.n_entradas);
		formulario.find("input[name='n_joven']").val(this.datos.n_joven);
		formulario.find("input[name='precio']").val(this.datos.precio);
	},
	carga: function() {
		
		var formulario = $( '#'+this.id );
		this.datos.sesion_id = formulario.find("input[name='sesion_id']").val()
		this.datos.n_entradas = formulario.find("input[name='n_entradas']").val();
		this.datos.n_joven = formulario.find("input[name='n_joven']").val();
		this.datos.precio = formulario.find("input[name='precio']").val();		
	}
};