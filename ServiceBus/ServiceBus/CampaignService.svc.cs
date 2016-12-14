using SharedLibs.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using SharedLibs.Enums;

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
                        Result = Result.SuccessFormat("Campaign {0} found", guid),
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
            try
            {

                if ((!string.IsNullOrWhiteSpace(name) && name.Length <= 50) && discount >= 0)
                {
                    using (var context = new EntityModels.ServiceBusDatabaseEntities())
                    {

                        context.Campaigns.Add(new EntityModels.Campaign() { Id = guid, Name = name, Discount = discount });
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

        /// <summary>
        /// This method edits campaing in case campaing exists
        /// </summary>
        /// <param name="guid">ID of a product</param>
        /// <param name="name">New name for a product</param>
        /// <param name="discount">New discount for a campaign</param>
        /// <returns>Modified campaign</returns>
        public Campaign EditCampaign(Guid guid, string name, double discount)
        {
            try
            {
                var originalCampaign = GetCampaign(guid);

                if (originalCampaign.Result.ResultType == ResultType.Success)
                {
                    var editableCampaign = new EntityModels.Campaign()
                    {
                        Id = originalCampaign.Id,
                        Name = originalCampaign.Name,
                        Discount = originalCampaign.Discount
                    };

                    using (var context = new EntityModels.ServiceBusDatabaseEntities())
                    {
                        context.Campaigns.Attach(editableCampaign);

                        //is new name valid and different than the stored one?
                        if (!String.IsNullOrWhiteSpace(name) && name.Length <= 50 && name != editableCampaign.Name)
                        {
                            editableCampaign.Name = name;
                        }

                        //is price valid and different from the stored one?
                        if (discount != editableCampaign.Discount && discount >= 0)
                        {
                            editableCampaign.Discount = discount;
                        }

                        //No changes? No need to update
                        if (context.Entry(editableCampaign).State == EntityState.Unchanged)
                        {
                            originalCampaign.Result = Result.WarningFormat("Campaign {0} was not modified.", originalCampaign.Id);
                            return originalCampaign;
                        }
                        //Otherwise update db and return what you should return
                        else
                        {
                            context.SaveChanges();
                            return new Campaign()
                            {
                                Id = editableCampaign.Id,
                                Name = editableCampaign.Name,
                                Discount = editableCampaign.Discount.HasValue ? editableCampaign.Discount.Value : 0.0,
                                Result = Result.Success()
                            };
                        }
                    }

                }
                else
                {
                    return originalCampaign;
                }
            }
            catch (Exception exception)
            {
                return new Campaign()
                {
                    Result =
                        Result.FatalFormat("CampaignService.EditCampaign exception has occured : {0}", exception.Message)
                };
            }
        }

        /// <summary>
        /// Delete campaign with specific Guid
        /// </summary>
        /// <param name="guid">guid of a campaign</param>
        /// <returns>Result object</returns>
        public Result DeleteCampaign(Guid guid)
        {
            try
            {
                using (var context = new EntityModels.ServiceBusDatabaseEntities())
                {
                    var campaign = context.Campaigns.FirstOrDefault(p => p.Id == guid);

                    if (campaign != null)
                    {
                        context.Campaigns.Remove(campaign);
                        context.SaveChanges();

                        return Result.SuccessFormat("Campaign {0} - {1} has been deleted.", campaign.Id, campaign.Name);
                    }

                    return Result.Error("Campaign was not found. Please make sure campaign ID is valid.");
                }
            }
            catch (Exception exception)
            {
                return Result.FatalFormat("CampaignService.DeleteCampaign exception has occured : {0}", exception.Message);
            }
        }
    }
}