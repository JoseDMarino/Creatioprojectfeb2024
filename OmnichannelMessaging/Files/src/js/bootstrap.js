(function() {
	require.config({
		paths: {
			"OmnichannelMessagingComponent": Terrasoft.getFileContentUrl("OmnichannelMessaging", "src/js/omnichannel-messaging-component/main.js"),
			"OmnichannelMessagingComponentStyles": Terrasoft.getFileContentUrl("OmnichannelMessaging", "src/js/omnichannel-messaging-component/styles.css"),
		},
		shim: {
			"OmnichannelMessagingComponent": {
				deps: ["ng-core", "css-ltr!OmnichannelMessagingComponentStyles"]
			}
		}
	});
})();