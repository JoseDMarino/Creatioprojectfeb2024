(function() {
	const config = {
		paths: {
			"SchemaViewComponent": Terrasoft.getFileContentUrl("CrtNUI", "src/js/schema-view-component/main.js"),
			"SchemaViewComponentStyles": Terrasoft.getFileContentUrl("CrtNUI", "src/js/schema-view-component/styles.css")
		},
		shim: {
			"StructureExplorerComponent": {
				deps: ["ng-core", "css-ltr!StructureExplorerComponentStyles"]
			},
			"ErrorListDialogComponent": {
				deps: ["ng-core", "css-ltr!ErrorListDialogComponentStyles"]
			},
			"SchemaViewComponent": {
				deps: ["ng-core", "chartjs", "css-ltr!SchemaViewComponentStyles"]
			}
		}
	};
	if (Terrasoft.isAngularHost) {
		define("StructureExplorerComponent",[],()=>{});
		define("ErrorListDialogComponent",[],()=>{});
		define("StructureExplorerComponentStyles",[],()=>{});
		define("ErrorListDialogComponentStyles",[],()=>{});
	} else {
		config.paths = {
			...config.paths,
			"StructureExplorerComponent": Terrasoft.getFileContentUrl("CrtNUI", "src/js/structure-explorer-component/main.js"),
			"StructureExplorerComponentStyles": Terrasoft.getFileContentUrl("CrtNUI", "src/js/structure-explorer-component/styles.css"),
			"ErrorListDialogComponent": Terrasoft.getFileContentUrl("CrtNUI", "src/js/error-list-dialog-component/main.js"),
			"ErrorListDialogComponentStyles": Terrasoft.getFileContentUrl("CrtNUI", "src/js/error-list-dialog-component/styles.css"),
		}
	}
	require.config(config);
})();
