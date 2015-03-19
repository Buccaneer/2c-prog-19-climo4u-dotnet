window.AllesJuist = false;

function zetAllesJuist(val) {
    console.log(val);
    if (val == "True") {
        window.AllesJuist = true;
    }
}


function toonKlimatogram(klimatogram, container, number) {
    if (AllesJuist) {
        if(number == 0)
            $("#" + container).css({ "width": $(".col-md-7"), "height": "400px" });
        else {
            $("#" + container).css({ "width": $("#klim_0").width(), "height": "400px" });
        }
        $(".carousel-indicators").css({"bottom":"-30px"});
    } else {
        $("#" + container).css({ "width": $(".carousel-inner").width(), "height": "400px" });
    }

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
        credits: {
            enabled: false
        },
        legend: {
            enabled: false
        },
        chart: {
            renderTo: container
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
            shared: true,
            formatter: function () {
                var s = "";
                $.each(this.points, function (i, point) {
                    var str;
                    var label;
                    if (i == 0) {
                        str = geefMaand(this.x.split("<br>")[0]) + '<br>';
                    } else {
                        str = '';
                    }


                    if (point.series.name == "Neerslag") {
                        label = 'Neerslag: ' + '<strong>' + point.y + 'mmN</strong><br>';
                    } else {
                        label = "Temperatuur: " + '<strong>' + point.y + '°C</strong>';
                    }
                    s += ('<span>' + str + label + '<span>');
                });

                return s;
            },
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

    var html = '<br/><p><b> Totale neerslag: </b>' + klimatogram.TotaalNeerslag + ' millimeter</p>';
    html += '<p><b> Gemiddelde temperatuur: </b>' + klimatogram.TotaalGemiddeldeTemperatuur + '°C</p>';
    $("#legend_" + number).html(html);
}

$(function() {
    var url = $('#data').attr('data-url');
    $.post(url, {}, function(klimatogrammen) {
        $.each(klimatogrammen, function(i, val) {
            toonKlimatogram(val, 'klim_' + i, i);
        });
    });
});

function geefMaand(maand) {
    switch (maand) {
        case 'Jan':
            return 'Januari';
        case 'Feb':
            return 'Februari';
        case 'Maa':
            return 'Maart';
        case 'Apr':
            return 'April';
        case 'Mei':
            return 'Mei';
        case 'Jun':
            return 'Juni';
        case 'Jul':
            return 'Juli';
        case 'Aug':
            return 'Augustus';
        case 'Sep':
            return 'September';
        case 'Okt':
            return 'Oktober';
        case 'Nov':
            return 'November';
        case 'Dec':
            return 'December';
        default:
            return maand;
    }
}