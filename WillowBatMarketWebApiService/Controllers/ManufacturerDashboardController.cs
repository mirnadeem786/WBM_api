using Microsoft.AspNetCore.Mvc;
using System;
using WillowBatMarketWebApiService.BusinessLayer;
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
        public ResponseModel fetchwillow(string willowType)
        {
            return imanufacturerDashboardRepository.fetch(willowType);



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

        [HttpGet("highest_bidder")]
        public ResponseModel highestBidder(Guid auctionId)
        {

            return imanufacturerDashboardRepository.highestBidder(auctionId);

        }

    }
}
