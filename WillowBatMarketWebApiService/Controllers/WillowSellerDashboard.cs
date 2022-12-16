using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WillowBatMarketWebApiService.BusinessLayer;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WillowSellerDashboard : ControllerBase

    {
        private IwillowSellerDashboardRepo iwillowSellerDashboard;

        public WillowSellerDashboard(IwillowSellerDashboardRepo iwillowSellerDashboard)
        {
            this.iwillowSellerDashboard = iwillowSellerDashboard;
        }

        [HttpPost("start_bidding")]

        public ResponseModel startbidding(Auction auction)
        {

            return iwillowSellerDashboard.startBiddeing(auction);

        }
        [HttpPost("upload_Willow")]

        public ResponseModel upload(WillowModel willowModel)
        {

            return iwillowSellerDashboard.upload(willowModel);

        }
        [HttpGet("fetch-auction-willow")]
        
        public IActionResult fetchAuctionWillow(Guid willowSellerId)
        {


            return Ok(iwillowSellerDashboard.fetchAuction(willowSellerId));
        }
        [HttpGet("fetch-willow")]

        public IActionResult fetchWillow(Guid willowSellerId)
        {


            return Ok(iwillowSellerDashboard.fetchWillows(willowSellerId));
        }
        [HttpDelete("delete-auction")]
        public ResponseModel deleteAuction(Guid auctionId)
        {
            return iwillowSellerDashboard.deleteAuction(auctionId);

        }


    }
}
