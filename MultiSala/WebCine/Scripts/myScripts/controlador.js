$(document).ready(function () {
    $.Reload = function () {
        Module.handlers.cargaIndex();
        Module.constantes.loadStatus();
    }
    $.Reload();
});