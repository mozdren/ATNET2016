using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServiceBusTests
{
    /// <summary>
    /// Summary description for CampaignServiceTest
    /// </summary>
    [TestClass]
    public class CampaignServiceTest
    {
        [TestMethod]
        public void GetCampaignTest()
        {
            var client = new CampaignService.CampaignServiceClient();
            var campaignId = new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3301");

            var campaign = client.GetCampaign(campaignId);

            Assert.IsTrue(campaign.Result.ResultType == CampaignService.ResultType.Success);
        }

        [TestMethod]
        public void GetAllCampaignsTest()
        {
            var client = new CampaignService.CampaignServiceClient();

            var campaign = client.GetAllCampaigns();

            Assert.IsTrue(campaign.Result.ResultType == CampaignService.ResultType.Success);
        }

        [TestMethod]
        public void AddCampaignTest()
        {
            var client = new CampaignService.CampaignServiceClient();
            var campaignId = new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3301");

            var result = client.AddCampaign(campaignId, "Slevovy kupon", 5.2);

            Assert.IsTrue(result.ResultType == CampaignService.ResultType.Success);
        }

        [TestMethod]
        public void EditCampaignTest()
        {
            var client = new CampaignService.CampaignServiceClient();
            var campaignId = new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3301");

            var campaign = client.EditCampaign(campaignId, "Novy slevovy kupon", 6.5);

            Assert.IsTrue(campaign.Result.ResultType == CampaignService.ResultType.Success);
        }

        [TestMethod]
        public void DeleteCampaignTest()
        {
            var client = new CampaignService.CampaignServiceClient();
            var campaignId = new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3301");

            var result = client.DeleteCampaign(campaignId);

            Assert.IsTrue(result.ResultType == CampaignService.ResultType.Success);
        }
    }
}
