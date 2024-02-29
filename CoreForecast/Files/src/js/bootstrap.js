(function() {
    require.config({
        paths: {
            "ForecastComponent": Terrasoft.getFileContentUrl("CoreForecast", "src/js/forecast-component/main.js"),
            "ForecastComponentStyles": Terrasoft.getFileContentUrl("CoreForecast", "src/js/forecast-component/styles.css"),
           
        },
        shim: {
            "ForecastComponent": {
                deps: ["ng-core", "css-ltr!ForecastComponentStyles"]
            }
        }
    });
})();
