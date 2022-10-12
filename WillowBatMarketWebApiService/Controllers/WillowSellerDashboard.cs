using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WillowBatMarketWebApiService.BusinessLayer;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WillowSellerDashboard : ControllerBase

    {
        private IwillowSellerRepositoryDashboard iwillowSellerDashboard;

        public WillowSellerDashboard(IwillowSellerRepositoryDashboard iwillowSellerDashboard)
        {
            this.iwillowSellerDashboard = iwillowSellerDashboard;
        }

        [HttpPost("start_bidding")]

        public ResponseModel startbidding(Guid itemId, DateTime startingTime, DateTime endTime, decimal startingPrice)
        {

            return iwillowSellerDashboard.startBiddeing(itemId, startingTime, endTime, startingPrice);

        }
        [HttpPost("upload_Willow")]

        public ResponseModel upload(WillowModel willowModel)
        {

            return iwillowSellerDashboard.upload(willowModel);
 
        }

    }
}
