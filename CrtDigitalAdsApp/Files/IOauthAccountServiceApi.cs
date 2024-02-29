namespace CrtDigitalAdsApp
{
    using Models.Requests;
    using Models.Responses;

    #region IOAuthAccountServiceApi

    /// <summary>
    /// Provide information from account authentication service.
    /// </summary>
    public interface IOAuthAccountServiceApi
    {

        #region Methods: Public
		
        /// <summary>
        /// Action to start the authorization flow.
        /// </summary>
        /// <param name="request">Request that contain information about the app you want to authenticate to.</param>
        /// <returns>Instance of <see cref="StartOAuthFlowResponse"/>></returns>
        StartOAuthFlowResponse StartFlow(StartFlowRequest request);

        #endregion

    }

    #endregion
}