using System;
using SharedLibs.DataContracts;

namespace ServiceBus
{
    public class CampaignService : ICampaignService
    {
        public Campaign GetCampaign(Guid guid)
        {
            //before continuing is necessary to add Campaign to ServiceBusDatabaseEntities
            return new SharedLibs.DataContracts.Campaign
            {
                Result = Result.Fatal("Not Implemented")
            };
        }

        public Campaigns GetAllCampaigns()
        {
            //before continuing is necessary to add Campaign to ServiceBusDatabaseEntities
            return new Campaigns
            {
                Result = Result.Fatal("Not Implemented")
            };
        }

        public Result AddCampaign(Campaign campaign)
        {
            //before continuing is necessary to add Campaign to ServiceBusDatabaseEntities
            return Result.Fatal("Not Implemented");
        }

        public Result AddCampaign(Guid guid, string name, double discount)
        {
            //before continuing is necessary to add Campaign to ServiceBusDatabaseEntities
            return Result.Fatal("Not Implemented");
        }

        public Campaign EditCampaign(Guid guid, string name, double discount)
        {
            //before continuing is necessary to add Campaign to ServiceBusDatabaseEntities
            return new Campaign
            {
                Result = Result.Fatal("Not Implemented")
            };
        }

        public Result DeleteCampaign(Guid guid)
        {
            //before continuing is necessary to add Campaign to ServiceBusDatabaseEntities
            return Result.Fatal("Not Implemented");
        }
    }
}