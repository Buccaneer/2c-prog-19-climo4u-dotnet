$(function () {
    $items = $("#klimatogrammen");
    $items.attr({
        "class": "panel-group",
        "id": "klimatogrammen",
        "role": "tablist",
        "aria-multiselectable": "true"
        });
    url = $items.attr("data-url");
    $.getJSON(url, {}, function (data) {

        var pointers = $.map(data, function (val) {
            return {
                zoomLevel: 5,
                scale: 0.5,
                title: val.Locatie,
                latitude: val.Coordinaten.Latitude,
                longitude: val.Coordinaten.Longitute
            };
        });
        var map = AmCharts.makeChart("chartdiv", {
            type: "map",
            "theme": "light",
            pathToImages: "http://www.amcharts.com/lib/3/images/",
            dragMap: false,
            mouseWheelZoomEnabled: false,
            zoomOnDoubleClick: false,
            imagesSettings: {
                rollOverColor: "blue",
                rollOverScale: 3,
                selectedScale: 3,
                selectedColor: "#999999",
                color: "blue"
            },

            zoomControl: {
                panControlEnabled: false,
                zoomControlEnabled: false,
                buttonFillColor: "#15A892"
            },

            areasSettings: {
                unlistedAreasColor: "#15A892"
            },

            dataProvider: {
                map: "worldLow",
                images: pointers
            }
        });

        map.addListener("positionChanged", updateCustomMarkers);
        map.validateData();
        var resultaatSet = Array();
        var huidigeSelectie = null;
        function updateCustomMarkers(event) {
            // get map object
            var map = event.chart;

            // go through all of the images
            for (var x in map.dataProvider.images) {
                // get MapImage object
                var image = map.dataProvider.images[x];

                // check if it has corresponding HTML element
                if ('undefined' == typeof image.externalElement)
                    image.externalElement = createCustomMarker(image);

                // reposition the element according to coordinates
                image.externalElement.style.top = map.latitudeToY(image.latitude) + 'px';
                image.externalElement.style.left = map.longitudeToX(image.longitude) + 'px';
            }
        }

        var currentId = 0;
        // this function creates and returns a new marker element
        function createCustomMarker(image) {
            // create holder
            var holder = document.createElement('div');
            var tit = cleanup(image.title).toString();



            holder.setAttribute('titlefind', tit.toString());
            holder.onclick = function () {
                huidigeSelectie = image.title;
                $('#klimatogrammen').fadeOut(500, function() {
                    toonOpties();
                  
                    $(holder).find('.pulse').css('border-color', 'blue');
                });
            };
            holder.className = 'map-marker';
            holder.title = image.title;
            holder.style.position = 'absolute';
            holder.className += ' map-clickable';


            // create dot
            var dot = document.createElement('div');
            dot.className = 'dot';
            holder.appendChild(dot);

            // create pulse
            var pulse = document.createElement('div');
            pulse.className = 'pulse';
            holder.appendChild(pulse);

            // append the marker to the map container
            image.chart.chartDiv.appendChild(holder);

            return holder;
        }

        $.each(data, function(i,val) {
            var div = $(
                "<div class='panel panel-default' id='item" + i + "'>" +
                    "<div class='panel-heading' role='tab' id='heading" + i + "'>" +
                        "<h4 class='panel-title'>" +
                            "<a " + " class='nr" + i + "' data-toggle='collapse' data-parent='#klimatogrammen' href='#collapse" + i
                            + "' aria-expanded='" + "true" + "' aria-controls='collapse" + i + "'>" + "Klimatogram " + (i + 1) + "</a>" +
                        "</h4>" +
                    "</div>" +
                    "<div id='collapse" + i + "' class='panel-collapse collapse in nr" + i +  "' role='tabpanel' aria-labelledby='heading" +i + "'>" +
                        "<div class='class='container panel-body'>" +
                            "<div id='klim" + i + "'></div>" +
                        "</div>" +
                    "</div>" +
                 "</div>");
            $items.append(div);
            toonKlimatogram(val, 'klim' + i, i + 1);
            var button = $("<div><button class='btn'>Kies</button></div>");
            $(button).click(function() {
                resultaatSet[huidigeSelectie] = i;
                toonOpties();
            });
            $("#klim" + i).parent().append(button);
        });

        $('.panel-heading a').on('click', function (e) {
            var nr = $(this)[0].classList[0];
            console.log(nr);
            $.each($(this).parents('.panel-group').children('.panel').children('.panel-collapse'), function (i, val) {
                if ($(val).hasClass('in') && !$(val).hasClass(nr)) {
                    console.log("MAAK COLLAPSED");
                    $(val).removeClass('in');
                    $(val).addClass('collapse');
                }
            });
        });

        toonInfoMessage("Klik op een locatie en kies een klimatogram.");

        function toonOpties() {
            var item2Apreciate = resultaatSet[huidigeSelectie];
            $('.panel').addClass('panel-default');
            $('.panel').removeClass('panel-success');
            $('.panel-collapse.in').collapse('hide');
            $('.pulse').css('border-color', '#f7f14c');
            for (key in resultaatSet) {
                $('div[titlefind=' + cleanup(key).toString() + ']').find('.pulse').css('border-color', 'green');
            }
            if (item2Apreciate !== undefined) {
               

                $('#item' + item2Apreciate).removeClass('panel-default');
                $('#item' + item2Apreciate).addClass("panel-success");
                $('#collapse' + item2Apreciate).collapse('show');
            }
            $button = $('#submit');
            $button.hide();
            if (Object.size(resultaatSet) === pointers.length) {
                $button.show();
               
            }
            $info = $('#infomess');
            $klims = $('#klimatogrammen');
                $info.fadeOut(500, function() {
                    $klims.fadeIn(500);
                });
        }

        var $button = $('#submit');
        $button.click(function () {
            var action = $button.attr('data-action');

            var data = Array();

            var index = 0;
            var form = $("<form action='" + action + "' method='post'></div>");
            for (var i in resultaatSet) {
            
                form.append($("<input type='text' value='" + i + "' name='klimatogrammen[" + index + "]'" + ' ></input'));
                form.append($("<input type='text' value='" + resultaatSet[i] + "' name='locaties[" + index + "]'" + ' ></input'));
                console.log(form);
                ++index;
            }

            form.append($('<input type="submit" value="indienen" />'));
            $('body').append(form);
            $(form).submit();
        });
    });



});


Object.size = function (obj) {
    var size = 0, key;
    for (key in obj) {
        if (obj.hasOwnProperty(key)) size++;
    }
    return size;
};

function toonInfoMessage(messageText) {
    $info = $('#infomess');
    $klims = $('#klimatogrammen');
    $button = $("#submit");
    $button.hide();
    $info.text(messageText);
    $klims.fadeOut(500,function() {
        $info.fadeIn();
    });
    
}




function toonKlimatogram(klimatogram, container, number) {
    $("#" + container).css({ "width": "480px", "height": "400px" });

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
    if (min >= 0) {
        min = 0;
    }

    max =max + ( (max % 10) > 0 ? 10 : 0);
    max += (max / 10) % 2 != 0 ? 10 : 0;


    
     if (min >= 0) {
        if (min % 10 != 0) {
            min += 10 - (min % 10);
        }
    } else {
        if (min % 10 != 0) {
            min -= 10 + (min % 10);
        }
    }
        var tickInterval = max > 100 ? 20 : 10;

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
            text: "Klimatogram " + number
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
                    formatter: function () {
                        return this.value < 0 ? ' ' : this.value;
                    }
                },
                title: {
                    text: 'Neerslag in mm'
                },
                max: max,
                min: min,
                endOnTick: false,
                minPadding: 0,
                maxPadding: 0,
                tickInterval: tickInterval
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
                tickInterval: tickInterval / 2
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
                        label = 'Neerslag: ' + '<strong>' + point.y + 'mmN</strong><br>';
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
}

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

  function cleanup(val) {
    return val.toLowerCase().replace(/[^a-zA-Z0-9]+/g, "");
}