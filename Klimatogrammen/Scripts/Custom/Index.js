$(function () {
    behandelElement($("#Graad").val(), $("#jaar"));
    $("#Graad").change(function() {
        behandelElement($("#Graad").val(), $("#jaar"));
    });
});

function behandelElement(waarde, $elem) {
    if (waarde === "2") {
        $($elem).fadeIn(1500);
        
    } else {
        $("#jaar input").val(null);
        $($elem).hide();
    }
}