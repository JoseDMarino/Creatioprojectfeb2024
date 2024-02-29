(function() {
	require.config({
		paths: {
			"RelationshipDiagramComponent": Terrasoft.getFileContentUrl("RelationshipDesigner", "src/js/relationship-diagram-component/main.js"),
			"RelationshipDiagramComponentStyles": Terrasoft.getFileContentUrl("RelationshipDesigner", "src/js/relationship-diagram-component/styles.css")
		},
		shim: {
			"RelationshipDiagramComponent": {
				deps: ["ng-core", "css-ltr!RelationshipDiagramComponentStyles"]
			}
		}
	});
}());
