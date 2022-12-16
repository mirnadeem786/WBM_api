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


        [HttpGet("fetch-willows")]
        public IActionResult fetchwillow(string willowType)
        {
            var res= imanufacturerDashboardRepository.fetch(willowType);
            return Ok(res);


        }
        [HttpGet("winning-bidder")]
        public IActionResult winning(Guid bidderId)
        {
            var res = imanufacturerDashboardRepository.winner(bidderId);
            return Ok(res);


        }

        [HttpGet("my-bids")]
        public IActionResult myBids(Guid bidderId)
        {
            var res = imanufacturerDashboardRepository.MyBidsDetails(bidderId);
            return Ok(res);


        }


        [HttpPost("participate_In_Auction")]

        public ResponseModel participateInAuction( Bidder bidder)
        {

            return imanufacturerDashboardRepository.partcipateInAuction(bidder);

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
        public IActionResult highestBidder(Guid auctionId)
        {

            return Ok(imanufacturerDashboardRepository.highestBidder(auctionId));

        }
        [HttpGet("auction-details-by-id")]
        public IActionResult aucionDetail(Guid auctionId)
        {

            var res=imanufacturerDashboardRepository.getAuctionDetails(auctionId);
            return Ok(res);

        }

        [HttpGet("get-auction-willow-by-id")]
        public IActionResult fetchAuctionWillow(Guid auctionId)
        {

            var res = imanufacturerDashboardRepository.getAuctionWillow(auctionId);
            return Ok(res);

        }

        [HttpGet("Order_received")]
        public IActionResult orderReceived(Guid manufacturerId)
        {



            return Ok(imanufacturerDashboardRepository.orderRecieved(manufacturerId));

        }

        [HttpPost("action_on_order")]

        public ResponseModel actionOnOrder(Guid orderId, string action)
        {

            return imanufacturerDashboardRepository.actionOnOrder(orderId, action);
        }
        [HttpGet("most-items-sold")]

        public IActionResult mostItemsSold(Guid manufacturerId)
        {

            return Ok(imanufacturerDashboardRepository.mostBatsSold(manufacturerId));

        }
        [HttpGet("items-close-to-out-of-stock")]

        public IActionResult itemsCloseToOutOfStock(Guid manufacturerId)
        {

            return Ok(imanufacturerDashboardRepository.batsCloseToOutOfStock(manufacturerId));

        }
        [HttpGet("fetch-bats")]

        public IActionResult fetch(Guid manufacturerId)
        {

            return Ok(imanufacturerDashboardRepository.fetchBats(manufacturerId));

        }


    }
}
