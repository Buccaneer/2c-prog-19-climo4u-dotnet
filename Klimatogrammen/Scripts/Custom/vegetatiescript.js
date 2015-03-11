$(function() {
    $("#btnHint").on('click', function () {
        $("#hint").fadeIn(600);
        $(this).fadeOut(600,function() { $(this).remove(); });
    });

  
});