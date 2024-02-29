namespace CrtDigitalAdsApp.Models.Requests
{
	using System.Runtime.Serialization;

	/// <summary>
	/// Represents a request to start the authentication flow.
	/// </summary>
	[DataContract]
    public class BaseRequest 
    {

	    #region Properties: Public
    
    	/// <summary>
    	/// The application identifier.
    	/// </summary>
    	[DataMember(Name = "application")]
    	public string Application { get; set; }

        /// <summary>
        /// The platform name.
        /// </summary>
        [DataMember(Name = "platform")]
        public string Platform { get; set; }
    
    	#endregion
    
    }
}

