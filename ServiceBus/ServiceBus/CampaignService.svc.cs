using System;
using System.Linq;
using System.Collections.Generic;
using SharedLibs.DataContracts;

namespace ServiceBus
{
    public class CampaignService : ICampaignService
    {
        public Campaign GetCampaign(Guid guid)
        {
            try
            {
                using (var context = new EntityModels.ServiceBusDatabaseEntities())
                {
                    var campaign = context.Campaigns.FirstOrDefault(p => p.Id == guid);

                    if (campaign == null)
                    {
                        return new Campaign()
                        {
                            Result = Result.ErrorFormat("Campaign {0} not found.", guid)
                        };
                    }

                    return new Campaign()
                    {
                        Result =  Result.SuccessFormat("Campaign {0} found", guid),
                        Id = guid,
                        Name = campaign.Name,
                        Discount = campaign.Discount.HasValue ? campaign.Discount.Value : 0.0
                    };
                }
            }
            catch (Exception exception)
            {
                return new Campaign()
                {
                    Result = Result.FatalFormat("ProductService.GetCampaign Exception: {0}", exception.Message)
                };
            }
        }

        public Campaigns GetAllCampaigns()
        {
            try
            {
                using (var context = new EntityModels.ServiceBusDatabaseEntities())
                {
                    var returnCollection = new List<Campaign>();

                    foreach (var campaign in context.Campaigns)
                    {
                        returnCollection.Add(new Campaign
                        {
                            Id = campaign.Id,
                            Name = campaign.Name,
                            Discount = campaign.Discount.HasValue ? campaign.Discount.Value : 0.0
                        });
                    }

                    return new Campaigns()
                    {
                        Result = SharedLibs.DataContracts.Result.Success("All products returned"),
                        Items = returnCollection
                    };
                }
            }
            catch (Exception exception)
            {
                return new Campaigns()
                {
                    Result = Result.FatalFormat("ProductService.GetAllCampaigns Exception: {0}", exception.Message)
                };
            }
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