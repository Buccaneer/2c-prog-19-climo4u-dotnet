$(function() {
    $("#btnHint").on('click', function () {
        $("#hint").show();
        $(this).remove();
    });
});