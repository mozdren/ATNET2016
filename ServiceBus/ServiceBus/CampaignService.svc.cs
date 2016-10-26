using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using SharedLibs.DataContracts;

namespace ServiceBus
{
    public class CampaignService : ICampaignService
    {
        public SharedLibs.DataContracts.Campaign GetCampaign(Guid guid)
        {
            //before continuing is necessary to add Campaign to ServiceBusDatabaseEntities
            return new SharedLibs.DataContracts.Campaign
            {
                Result = Result.Fatal("Not Implemented")
            };
        }

        public SharedLibs.DataContracts.Campaigns GetAllCampaigns()
        {
            //before continuing is necessary to add Campaign to ServiceBusDatabaseEntities
            return new SharedLibs.DataContracts.Campaigns
            {
                Result = Result.Fatal("Not Implemented")
            };
        }

        public SharedLibs.DataContracts.Result AddCampaign(Campaign campaign)
        {
            //before continuing is necessary to add Campaign to ServiceBusDatabaseEntities
            return Result.Fatal("Not Implemented");
        }

        public SharedLibs.DataContracts.Result AddCampaign(Guid guid, string name, double discount)
        {
            //before continuing is necessary to add Campaign to ServiceBusDatabaseEntities
            return Result.Fatal("Not Implemented");
        }

        public Campaign EditCampaign(Guid guid, string name, double discount)
        {
            //before continuing is necessary to add Campaign to ServiceBusDatabaseEntities
            return new SharedLibs.DataContracts.Campaign
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