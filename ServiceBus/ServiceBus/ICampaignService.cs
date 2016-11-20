using System.ServiceModel;
using System;
using SharedLibs.DataContracts;

namespace ServiceBus
{
    /// <summary>
    /// This is a campaign service, which should serve as an entrypoint for everything that has
    /// something directly in common with campaigns.
    /// </summary>
    [ServiceContract]
    public interface ICampaignService
    {
        /// <summary>
        /// Returns campaign with specific Guid
        /// </summary>
        /// <param name="guid">guid of a campaign</param>
        /// <returns>requested campaign</returns>
        [OperationContract]
        Campaign GetCampaign(Guid guid);

        /// <summary>
        /// Returns all campaigns
        /// </summary>
        /// <returns>collection of all campaigns</returns>
        [OperationContract]
        Campaigns GetAllCampaigns();

        /// <summary>
        /// Adds campaign into database
        /// </summary>
        /// <param name="guid">ID of a campaign</param>
        /// <param name="name">Name of a campaign</param>
        /// <param name="discount">Discount of a campaign</param>
        /// <returns>Result object</returns>
        [OperationContract]
        Result AddCampaign(Guid guid, string name, double discount);

        /// <summary>
        /// Adds campaign into database
        /// </summary>
        /// <param name="campaign">Campaign object</param>
        /// <returns>Result object</returns>
        [OperationContract(Name = "AddCampaignByObject")]
        Result AddCampaign(SharedLibs.DataContracts.Campaign campaign);

        /// <summary>
        /// Edits campaign with specific Guid
        /// </summary>
        /// <param name="guid">ID of a campaign</param>
        /// <param name="name">New name for a campaign</param>
        /// <param name="discount">New discount of a campaign</param>
        /// <returns>Edited campaign</returns>
        [OperationContract]
        Campaign EditCampaign(Guid guid, string name, double discount);

        /// <summary>
        /// Delete campaign from database
        /// </summary>
        /// <param name="guid">ID of a campaign</param>
        /// <returns>Result object</returns>
        [OperationContract]
        Result DeleteCampaign(Guid guid);
    }
}
