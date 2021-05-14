import { initThemes, showThemeCenter } from "./themeManager";
import ApexCharts from "apexcharts";

initThemes();
showThemeCenter(false);

const testResults = G_TEST_RESULTS; // Global variable
console.log(testResults.map(x => x.date));

const options = {
    series: [{
        name: 'wpm',
        data: testResults.map(x => [x.date, x.wpm])
    }],
    chart: {
        height: 350,
        type: 'line',
        id: 'wpm-chart',
        toolbar: {
            show: false
        },
        zoom: {
            autoScaleYaxis: false
        }
    },
    dataLabels: {
        enabled: false
    },
    tooltip: {
        x: {
            format: 'dd MMM yyyy'
        }
    },
    stroke: {
        curve: 'straight'
    },
    title: {
        text: 'test results',
        align: 'left'
    },
    xaxis: {
	type: 'datetime',
        tickAmount: 8,
        min: new Date(testResults[0].date).getTime(),
        max: new Date(testResults[testResults.length - 1].date).getTime(),
        labels: {
            rotate: -15,
            rotateAlways: true,
            formatter: (_, timestamp) => {
                return new Date(timestamp).toLocaleDateString("en-US");
            }
        }
    },
    yaxis: {
        title: {
            text: 'wpm',
            align: 'left'
        }
    }
};

const chart = new ApexCharts(document.querySelector("#chart"), options);
chart.render();
