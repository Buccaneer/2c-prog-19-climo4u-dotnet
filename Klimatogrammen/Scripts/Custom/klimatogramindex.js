function clearAlert() {
    $(".alert-warning").hide();
    $("#container").html("");
    $("#container").removeAttr('style');
    $("#legend").html("");
}

function makeGraph(klimatogram) {
    clearAlert();
    $("#container").css({ "width": "100%", "height": "400px" });

    var temperaturen = klimatogram.GemiddeldeTemperatuur;
    var neerslagen = klimatogram.GemiddeldeNeerslag;

    var max;
    var maxNeerslag = Math.max.apply(Math, neerslagen);
    var maxTemperatuur = Math.max.apply(Math, temperaturen);
    if (maxTemperatuur > maxNeerslag) {
        max = maxTemperatuur * 2;
    } else {
        max = maxNeerslag;
    }

    var min = (Math.min.apply(Math, temperaturen) * 2);
    if (min > 0)
        min = 0;

    max += 10;
    var chart = new Highcharts.Chart({
        legend: {
            enabled: false
        },
        chart: {
            renderTo: 'container'
        },
        title: {
            text: klimatogram.Locatie + " - " + klimatogram.Land
        },
        subtitle: {
            text: 'Klimatologische gemiddelden ' + klimatogram.BeginJaar + " - " + klimatogram.EindJaar
        },
        xAxis: {
            categories: ['Jan', 'Feb', 'Maa', 'Apr', 'Mei', 'Jun', 'Jul', 'Aug', 'Sep', 'Okt', 'Nov', 'Dec']
        },
        yAxis: [
            {
                labels: {
                    format: '{value}'
                },
                title: {
                    text: 'Neerslag in mm'
                },
                max: max,
                min: min,
                endOnTick: false,
                minPadding: 0,
                maxPadding: 0,
                tickInterval: 10
            }, {
                labels: {
                    format: '{value}'
                },
                title: {
                    text: 'Temperatuur in °C'
                },
                opposite: true,
                max: max / 2,
                min: min / 2,
                endOnTick: false,
                minPadding: 0,
                maxPadding: 0,
                tickInterval: 5
            }
        ],
        tooltip: {
            shared: true
        },
        series: [
            {
                yAxis: 0,
                name: 'Neerslag',
                data: neerslagen,
                type: 'column'
            }, {
                color: 'red',
                yAxis: 1,
                name: 'Temperatuur',
                data: temperaturen,
                type: 'spline'
            }
        ]


    });

    var tabel = '<table class="legende table-bordered"><tr><td class=data>N in mm</td>';
    var neerslag = "";
    $.map(neerslagen, function (data) {
        neerslag += '<td class = data>' + data + '</td>';
    });
    tabel += neerslag;
    tabel += '</tr>';
    tabel += '<tr><td class=data>T in °C</td>';

    var temperatuur = "";
    $.map(temperaturen, function (data) {
        temperatuur += '<td class = data>' + data + '</td>';
    });

    tabel += temperatuur;
    tabel += '</tr>';

    tabel += '</table>';

    tabel += '<br/><p><b> Totale neerslag: </b>' + klimatogram.TotaalNeerslag + '</p>';
    tabel += '<p><b> Gemiddelde temperatuur: </b>' + klimatogram.TotaalGemiddeldeTemperatuur + '</p>';
    $("#legend").html(tabel);

    var actionButton = "<a href=/OefeningVragen/Index class='btn btn-primary'>Verder gaan</a>";
    $("#legend").append(actionButton);
}