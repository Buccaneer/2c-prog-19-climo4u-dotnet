$(function () {
    behandelElement($("#Graad").val(), $("#jaar"));
    $("#Graad").change(function() {
        behandelElement($("#Graad").val(), $("#jaar"));
    });
});

function behandelElement(waarde, $elem) {
    if (waarde === "1") {
        $($elem).fadeIn(1500);
        
    } else {
        $($elem).hide();
    }
}