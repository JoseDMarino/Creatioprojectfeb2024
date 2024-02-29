(function() {
	require.config({
		paths: {
			"web-service-proxy-component": Terrasoft.getFileContentUrl("ServiceDesigner", "src/js/web-service-proxy-component/main.js"),
			"web-service-proxy-component-styles": Terrasoft.getFileContentUrl("ServiceDesigner", "src/js/web-service-proxy-component/styles.css"),
		},
		shim: {
			"web-service-proxy-component": {
				deps: ["ng-core", "css-ltr!web-service-proxy-component-styles"]
			}
		}
	});
}());
