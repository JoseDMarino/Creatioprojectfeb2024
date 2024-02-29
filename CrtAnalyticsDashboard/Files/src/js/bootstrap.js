(function() {
    require.config({
        paths: {
            "AnalyticsDashboard": Terrasoft.getFileContentUrl("CrtAnalyticsDashboard", "src/js/analytics-dashboard/main.js"),
            "AnalyticsDashboardStyles": Terrasoft.getFileContentUrl("CrtAnalyticsDashboard", "src/js/analytics-dashboard/styles.css"),
        },
        shim: {
            "AnalyticsDashboard": {
                deps: ["ng-core", "css-ltr!AnalyticsDashboardStyles"]
            }
        }
    });
})();
