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
            throw new NotImplementedException();
        }

        public SharedLibs.DataContracts.Campaigns GetAllCampaign()
        {
            //before continuing is necessary to add Campaign to ServiceBusDatabaseEntities
            throw new NotImplementedException();
        }

        public SharedLibs.DataContracts.Result AddCampaign(Campaign campaign)
        {
            //before continuing is necessary to add Campaign to ServiceBusDatabaseEntities
            throw new NotImplementedException();
        }

        public SharedLibs.DataContracts.Result AddCampaign(Guid guid, string name, double discount)
        {
            //before continuing is necessary to add Campaign to ServiceBusDatabaseEntities
            throw new NotImplementedException();
        }

        public Result EditCampaign(Guid guid, string name, double discount)
        {
            //before continuing is necessary to add Campaign to ServiceBusDatabaseEntities
            throw new NotImplementedException();
        }

        public Result DeleteCampaign(Guid guid)
        {
            //before continuing is necessary to add Campaign to ServiceBusDatabaseEntities
            throw new NotImplementedException();
        }
    }
}