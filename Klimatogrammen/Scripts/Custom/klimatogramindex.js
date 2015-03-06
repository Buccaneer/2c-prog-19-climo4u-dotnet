function clearAlert() {
    $(".alert-warning").hide();
    $("#container").html("");
    $("#container").removeAttr('style');
    $("#legend").html("");
    $("#verderGaan").remove();
}

function makeGraph(klimatogram) {
        clearAlert();
    if (klimatogram.Land !== undefined) {
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
        credits: {
            enabled:false
        },
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
             categories: ['Jan<br>'+temperaturen[0] + '°C<br>'+neerslagen[0]+'mm' , 'Feb<br>'+temperaturen[1] + '°C<br>'+neerslagen[1]+'mm', 'Maa<br>'+temperaturen[2] + '°C<br>'+neerslagen[2]+'mm', 'Apr<br>'+temperaturen[3] + '°C<br>'+neerslagen[3]+'mm', 'Mei<br>'+temperaturen[4] + '°C<br>'+neerslagen[4]+'mm', 'Jun<br>'+temperaturen[5] + '°C<br>'+neerslagen[5]+'mm', 'Jul<br>'+temperaturen[6] + '°C<br>'+neerslagen[6]+'mm', 'Aug<br>'+temperaturen[7] + '°C<br>'+neerslagen[7]+'mm', 'Sep<br>'+temperaturen[8] + '°C<br>'+neerslagen[8]+'mm', 'Okt<br>'+temperaturen[9] + '°C<br>'+neerslagen[9]+'mm', 'Nov<br>'+temperaturen[10] + '°C<br>'+neerslagen[10]+'mm', 'Dec<br>'+temperaturen[11] + '°C<br>'+neerslagen[11]+'mm']
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
             formatter: function() {
            var s = "";
            $.each(this.points, function(i, point) {
                var str;
                var label;
                if (i == 0) {
                    str = geefMaand(this.x.split("<br>")[0]) + '<br>';
                } else {
                    str = '';
                }
              
                
                if (point.series.name == "Neerslag") {
                    label = 'Neerslag: ' + '<strong>' + point.y + 'mm</strong><br>';
                } else {
                    label = "Temperatuur: " + '<strong>'+ point.y + '°C</strong>';
                }
                s+=('<span>' + str + label + '<span>');
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

    var html = '<br/><p><b> Totale neerslag: </b>' + klimatogram.TotaalNeerslag + '</p>';
    html += '<p><b> Gemiddelde temperatuur: </b>' + klimatogram.TotaalGemiddeldeTemperatuur + '</p>';
    $("#legend").html(html);

        $("#buttons").removeClass("col-md-offset-2").removeClass("col-md-10");

    var actionButton = "<a href=/OefeningVragen/Index class='btn btn-primary' id='verderGaan'>Verder gaan</a>";
    $("#buttons").prepend(actionButton);
    }
}

function geefMaand(maand) {
    switch(maand) {
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
                    default :
                        return maand;
                }
}