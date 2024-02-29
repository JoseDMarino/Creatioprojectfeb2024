(function() {
	require.config({
		paths: {
			"TermCalculationComponent": Terrasoft.getFileContentUrl("CaseService", "src/js/term-calculation-component/main.js"),
			"TermCalculationComponentStyles": Terrasoft.getFileContentUrl("CaseService", "src/js/term-calculation-component/styles.css"),
		},
		shim: {
			"TermCalculationComponent": {
				deps: ["ng-core", "css-ltr!TermCalculationComponentStyles"]
			}
		}
	});
})();