(function() {
    require.config({
        paths: {
            "PivotTableComponent": Terrasoft.getFileContentUrl("PivotTable", "src/js/pivot-table-component/main.js"),
            "PivotTableComponentStyles": Terrasoft.getFileContentUrl("PivotTable", "src/js/pivot-table-component/styles.css"),
        },
        shim: {
            "PivotTableComponent": {
                deps: ["ng-core", "css-ltr!PivotTableComponentStyles"]
            }
        }
    });
})();
