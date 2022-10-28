using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WillowBatMarketWebApiService.BusinessLayer;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerDashboardController : ControllerBase
    {

        private readonly ImanufacturerDashboardRepository imanufacturerDashboardRepository;
        public ManufacturerDashboardController(ImanufacturerDashboardRepository imanufacturerDashboardRepository)
        {
            this.imanufacturerDashboardRepository = imanufacturerDashboardRepository;
        }


        [HttpGet]
        public IActionResult fetchwillow(string willowType)
        {
            var res= imanufacturerDashboardRepository.fetch(willowType);
            return Ok(res);


        }
        [HttpPost("participate_In_Auction")]

        public ResponseModel participateInAuction( Guid auctionId,Guid manufacturerId,decimal price)
        {

            return imanufacturerDashboardRepository.partcipateInAuction(manufacturerId,auctionId, price);

        }

        [HttpGet("list_Of_Partcipants_In_Auction")]
        public ResponseModel bidders(Guid auctionId)
        {
            return imanufacturerDashboardRepository.ListOfParticipants(auctionId);


        }
        [HttpGet("fetch-all-Auction-willow")]
        public IActionResult fetchAllAuctionWillow()
        {


            var res = imanufacturerDashboardRepository.fetchAllAuctionWillows();

            return Ok(res);

        }

        [HttpGet("highest_bidder")]
        public ResponseModel highestBidder(Guid auctionId)
        {

            return imanufacturerDashboardRepository.highestBidder(auctionId);

        }

    }
}
