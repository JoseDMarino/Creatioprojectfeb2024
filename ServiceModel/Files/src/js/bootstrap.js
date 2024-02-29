(function() {
	require.config({
		paths: {
			"ServiceModelNetworkComponent": Terrasoft.getFileContentUrl("ServiceModel", "src/js/service-model-network-component/main.js"),
			"ServiceModelNetworkComponentStyles": Terrasoft.getFileContentUrl("ServiceModel", "src/js/service-model-network-component/styles.css"),
		},
		shim: {
			"ServiceModelNetworkComponent": {
				deps: ["ng-core", "css-ltr!ServiceModelNetworkComponentStyles"]
			}
		}
	});
})();