using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SharedLibs.DataContracts;

namespace ServiceBus
{
    
    public class BasketService : IBasketService
    {
        /// <summary>
        /// Method is supposed to create new basket. This is a test for database FK implementation.
        /// </summary>
        /// <returns>DTO object basket WITHOUT basket items.</returns>
        public SharedLibs.DataContracts.Basket CreateBasekt()
        {
            try
            {
                using (var context = new ServiceBusDatabaseEntities())
                {
                    //Add new basket and two campaigns
                    //It shoudl not be possible to store into a db since
                    //campaings guid does not exists yet and there is a FK

                    Basket basket = new Basket()
                    {
                        Id = Guid.NewGuid()
                    };

                    var camp2 = context.Campaign.FirstOrDefault(p => p.Id == new Guid("509a8d88-8fa1-491a-94ba-fb8ed6aaf6fb"));
                    
                    //I kinda a thought when there are no records in campaign table it will throw error since
                    //FK is pointing into campaign table Id. Insted new campaign was created. That is sick
                    basket.Campaigns.Add(new Campaign() {Discount = 15, Name = "XYth campaign" , Id = Guid.NewGuid()});
                    //var camp4 = new Campaign() { Discount = 150, Name = "4st campaign", Id = Guid.NewGuid() };
                    //basket.Campaigns.Add(camp4);

                    //this is what I expect to do to create new campaign... 
                    //context.Campaign.Add( new Campaign() { Discount = 55, Name = "5st campaign", Id = Guid.NewGuid() });
                    
                    basket.Campaigns.Add(camp2);
                    context.Basket.Add(basket);
                    context.SaveChanges();

                    //basket is saved therefore I would like to return SharedLibs.DataContracts.Basket(); Is this the way how to create it


                    //TODO: Is there some C# function which can do this for me so I can use BasketCampaings = basket.Campaigns; like I tried below? Or I have to convert this always explicitly in some block of code?
                    var ciList = new List<SharedLibs.DataContracts.Campaign>();
                    foreach (var item in basket.Campaigns)
                    {
                        ciList.Add(new SharedLibs.DataContracts.Campaign() { Id = item.Id} /* and the rest of properties and maybe even results */);
                    }


                    var returnBasket = new SharedLibs.DataContracts.Basket()
                    {
                        Id = basket.Id,
                        Result = Result.Success("Yay!"),
                        //NO implicit conversion 
                        //BasketCampaings = basket.Campaigns;

                        // foreach is also not available in here  so I have to do all the stuff elswhere for example like ciList ?
                        //foreach (var item in basket.Campaigns) {}

                        BasketCampaings = ciList,
                        //BasketItems = not implemeted;
                    };


                    return returnBasket;

                }
            }
            catch (Exception exception)
            {
                //TODO catch it!
                return new SharedLibs.DataContracts.Basket() { Result = Result.FatalFormat(" Basket. Error {0}",exception.InnerException)};
            }
        }
    }
}
