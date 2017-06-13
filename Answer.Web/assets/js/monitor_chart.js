
function SetupCharts(moniterType, categoriesData, slotData, yText) {

        Highcharts.chart('monitor-chart', {
            title: {
                text: moniterType,
                x: -20 //center
            },
            subtitle: {
                text: 'Source: MSR-FSR Answer',
                x: -20
            },
            xAxis: {
                categories: categoriesData
            },
            yAxis: {
                title: {
                    text: yText
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                valueSuffix: '°C'
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'middle',
                borderWidth: 0
            },
            series: slotData
        });
    }
    
