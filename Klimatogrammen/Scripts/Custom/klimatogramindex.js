$(function() {
    $('#Klimatogram_Continent').change(function() {
        $('#Klimatogram_Land').empty();
        $('#Klimatogram_Locatie').empty();
        $('#Klimatogram_Land').closest('.form-group').remove();
        $('#Klimatogram_Locatie').closest('.form-group').remove();
        $('#container').remove();
    });

    $('#Klimatogram_Land').change(function() {
        $('#Klimatogram_Locatie').empty();
        $('#Klimatogram_Locatie').closest('.form-group').remove();
        $('#container').remove();
    });

    $('#Klimatogram_Locatie').change(function() {
        $('#container').remove();
    });
})