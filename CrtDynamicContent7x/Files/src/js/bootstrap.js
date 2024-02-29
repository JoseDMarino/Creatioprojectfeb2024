(function() {
    require.config({
        paths: {
            "EmailTemplateLookupGalleryElement": Terrasoft.getFileContentUrl("CrtDynamicContent7x", "src/js/marketing-campaign-ng-elements/main.js"),
            "EmailTemplateLookupGalleryElementStyles": Terrasoft.getFileContentUrl("CrtDynamicContent7x", "src/js/marketing-campaign-ng-elements/styles.css")
		},
        shim: {
            "EmailTemplateLookupGalleryElement": {
                deps: ["ng-core", "css-ltr!EmailTemplateLookupGalleryElementStyles"]
            }
        }
    });
})();