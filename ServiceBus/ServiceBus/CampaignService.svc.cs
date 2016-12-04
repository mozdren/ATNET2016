using System;
using SharedLibs.DataContracts;

namespace ServiceBus
{
    public class CampaignService : ICampaignService
    {
        public Campaign GetCampaign(Guid guid)
        {
            //before continuing is necessary to add Campaign to ServiceBusDatabaseEntities
            return new Campaign
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
            try
            {

                if ((!string.IsNullOrWhiteSpace(name) && name.Length <= 50) && discount >= 0)
                {
                    using (var context = new EntityModels.ServiceBusDatabaseEntities())
                    {

                        context.Campaigns.Add(new EntityModels.Campaign() { Id=guid, Name = name, Discount=discount});
                        context.SaveChanges();

                        return Result.SuccessFormat("Product {0} | {1} has been added", guid, name);
                    }
                }
                else
                {
                    return Result.Error("Provided parameters are unacceptable.");
                }
            }
            catch (Exception ex)
            {
                return Result.FatalFormat("ProductService.AddProduct Exception: {0}", ex.Message);
            }
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