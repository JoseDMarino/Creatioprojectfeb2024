(function() {
    require.config({
        paths: {
            "ConfidenceLevelWidgetComponent": Terrasoft.getFileContentUrl("Opportunity", "src/js/confidence-level-widget/main.js"),
            "ConfidenceLevelWidgetComponentStyles": Terrasoft.getFileContentUrl("Opportunity", "src/js/confidence-level-widget/styles.css"),
        },
        shim: {
            "ConfidenceLevelWidgetComponent": {
                deps: ["ng-core", "chartjs", "css-ltr!ConfidenceLevelWidgetComponentStyles"]
            }
        }
    });
})();
