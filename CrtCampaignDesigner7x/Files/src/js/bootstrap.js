(function () {
	require.config({
		paths: {
			"SimpleDiagram": Terrasoft.getFileContentUrl("CrtCampaignDesigner7x", "src/js/cytoscape.min.js"),
			"CampaignDesignerComponent": Terrasoft.getFileContentUrl("CrtCampaignDesigner7x", "src/js/campaign-designer-component/main.js"),
			"CampaignDesignerComponentStyles": Terrasoft.getFileContentUrl("CrtCampaignDesigner7x", "src/js/campaign-designer-component/styles.css"),
			"SvgRenderer": Terrasoft.getFileContentUrl("CrtCampaignDesigner7x", "src/js/canvg.min.js"),
			"CampaignGallery": Terrasoft.getFileContentUrl("CrtCampaignDesigner7x", 	"src/js/campaign-gallery-element/main.js"),
			"CampaignGalleryStyles": Terrasoft.getFileContentUrl("CrtCampaignDesigner7x", 	"src/js/campaign-gallery-element/styles.css"),
		},
		shim: {
			"SimpleDiagram": {
				deps: [""]
			},
			"CampaignDesignerComponent": {
				deps: ["ng-core", "css-ltr!CampaignDesignerComponentStyles"]
			},
			"CampaignGallery": {
				deps: ["ng-core", "css-ltr!CampaignGalleryStyles"]
			},
			"SvgRenderer": {
				deps: [""]
			}
		}
	});
})();
