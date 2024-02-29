(function() {
    require.config({
        paths: {
            "TrackingEventsFeedElement": Terrasoft.getFileContentUrl("Tracking", "src/js/tracking-ng-elements/main.js"),
            "TrackingEventsFeedElementStyles": Terrasoft.getFileContentUrl("Tracking", "src/js/tracking-ng-elements/styles.css"),
		},
        shim: {
            "TrackingEventsFeedElement": {
                deps: ["ng-core", "css-ltr!TrackingEventsFeedElementStyles"]
            }
        }
    });
})();