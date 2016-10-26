using System;
using System.Collections.Generic;
using SharedLibs.DataContracts;

namespace ServiceBus.Mock
{
    /// <summary>
    /// This is just a mock of a Campaign service to provide the developers samples of outputs and interfaces they
    /// might encounter when using the service. This is definitely not intended to simulate whole functionality!
    /// </summary>
    public class CampaignService : ICampaignService
    {
        /// <summary>
        /// Adds campaign
        /// </summary>
        /// <param name="campaign">campaign to be added</param>
        /// <returns>success if added sucessfuly</returns>
        public Result AddCampaign(SharedLibs.DataContracts.Campaign campaign)
        {
            return Result.SuccessFormat("Campaign added");
        }

        /// <summary>
        /// Adds campaign
        /// </summary>
        /// <param name="guid">ID of a campaign</param>
        /// <param name="name">Name of a campaign</param>
        /// <param name="discount">Discount of a campaign</param>
        /// <returns>success if added successfully</returns>
        public Result AddCampaign(Guid guid, string name, double discount)
        {
            return Result.SuccessFormat("Campaign added");
        }

        /// <summary>
        /// Deletes a campaign from a database (just a mock - does nothing)
        /// </summary>
        /// <param name="guid">identifier of a product</param>
        /// <returns></returns>
        public Result DeleteCampaign(Guid guid)
        {
            return Result.SuccessFormat("Campaign deleted");
        }

        /// <summary>
        /// Edits a campaign
        /// </summary>
        /// <param name="guid">product identifier</param>
        /// <param name="name">product name</param>
        /// <param name="discount">produt price</param>
        /// <returns>edited campaign</returns>
        public SharedLibs.DataContracts.Campaign EditCampaign(Guid guid, string name, double discount)
        {
            return new SharedLibs.DataContracts.Campaign
            {
                Result = Result.SuccessFormat("Campaign Edited"),
                Id = guid,
                Name = name,
                Discount = discount
            };
        }

        /// <summary>
        /// Returns all campaigns
        /// </summary>
        /// <returns></returns>
        public Campaigns GetAllCampaigns()
        {
            return new Campaigns
            {
                Result = Result.Success("Campaigns found"),
                Items = new List<SharedLibs.DataContracts.Campaign> {
                    new SharedLibs.DataContracts.Campaign { Id = Guid.Empty, Name = "Mock 1", Discount = 1.5 },
                    new SharedLibs.DataContracts.Campaign { Id = Guid.Empty, Name = "Mock 2", Discount = 2.0 },
                    new SharedLibs.DataContracts.Campaign { Id = Guid.Empty, Name = "Mock 3", Discount = 3.5 },
                    new SharedLibs.DataContracts.Campaign { Id = Guid.Empty, Name = "Mock 4", Discount = 4.0 },
                    new SharedLibs.DataContracts.Campaign { Id = Guid.Empty, Name = "Mock 5", Discount = 5.5 },
                    new SharedLibs.DataContracts.Campaign { Id = Guid.Empty, Name = "Mock 6", Discount = 6.0 }
                }
            };
        }

        /// <summary>
        /// returns a specific campaign
        /// </summary>
        /// <param name="guid">guid of the campaign</param>
        /// <returns>campaign with specific Guid if found</returns>
        public SharedLibs.DataContracts.Campaign GetCampaign(Guid guid)
        {
            return new SharedLibs.DataContracts.Campaign { Result = Result.Success("Campaign found"), Id = guid, Name = "Mock", Discount = 1.5 };
        }
    }
}
