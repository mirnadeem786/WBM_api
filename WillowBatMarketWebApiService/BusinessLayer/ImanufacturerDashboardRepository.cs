using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WillowBatMarketWebApiService.DataLayer;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;


namespace WillowBatMarketWebApiService.BusinessLayer
{
    public interface ImanufacturerDashboardRepository
    {
        public ResponseModel fetchAllAuctionWillows();
        public ResponseModel fetch(string type);
        public ResponseModel partcipateInAuction(Guid manufacturerId, Guid auctionId,decimal price);
        public ResponseModel ListOfParticipants(Guid auctionId);
     public ResponseModel highestBidder(Guid auctionId);


    }

    public class ManufacturerDashboardRepository : ImanufacturerDashboardRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly ResponseModel responseModel;
        public ManufacturerDashboardRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            this.responseModel = new ResponseModel();
        }

        public ResponseModel fetch(string willowType)
        {
            try
            {
                List<ManufacturerDashboard> manufacturerDashboards = appDbContext.ManufacturerDashboard.FromSqlRaw("dbo.spr_Manufacturer_Filter_Dashboard @willowType={0}", willowType).ToList();
                responseModel.Data = manufacturerDashboards;
                responseModel.Success = true;
                responseModel.Message = "sucessfully fetched";
                return responseModel;
            }
            catch (System.Exception ex)
            {
                responseModel.Success = false;
                responseModel.Message = ex.Message;
                return responseModel;
            }

        }

        public ResponseModel fetchAllAuctionWillows()
        {
            var querry = from auction in appDbContext.Set<Auction>()
                         join willow in appDbContext.Set<Willow>() on auction.itemId equals willow.willowId
                         select new
                         {
                             auction.auctionId,
                             auction.itemId,
                             auction.startingDateTime,
                             auction.endDateTime,
                             willow.willowPrice,
                             willow.WillowImage,
                             willow.willowType,
                             willow.willowSize,
                             willow.willowSellerId

                         };


            if (querry == null)

            {
                responseModel.Data = querry;
                return responseModel;
            }

            responseModel.Data = querry;
            return responseModel;


        }




    public ResponseModel highestBidder(Guid auctionId)
        {
            var record = appDbContext.Bidder.Where(a => a.auctionId == auctionId).OrderByDescending(x=>x.amount).FirstOrDefault();
            responseModel.Data = record;
            return responseModel;
   
        }

        public ResponseModel ListOfParticipants(Guid auctionId)
        {

            var querry = from auction in appDbContext.Set<Auction>()
                         join bidder in appDbContext.Set<Bidder>() on auction.auctionId equals bidder.auctionId
                         select new
                         {
                             auction.auctionId,
                             auction.itemId,
                             auction.startingDateTime,
                             auction.endDateTime,
                            bidder.amount,
                             bidder.bidderId
                         };


             if (querry==null)

            {
                responseModel.Data = querry;
                return responseModel;
            }

            responseModel.Data = querry;
            return responseModel;


        }

        public ResponseModel partcipateInAuction(Guid manufacturerId, Guid auctionId, decimal price)
        {
          
            Auction record = appDbContext.Auction.FirstOrDefault(i => i.auctionId == auctionId);
            if(record!=null &&record.startingPrice>price)
            {
                responseModel.Message = "price given is below the starting price";
                return responseModel;

            }
           else if (record!=null &&record.highestAmount < price)
            {
                record.highestAmount = price;

                appDbContext.Bidder.Add(new Bidder()
                {

                    bidderId = manufacturerId,
                    amount = price,
                    auctionId = auctionId



                }); ;
                appDbContext.SaveChanges();
              
            }
            else
            {
                responseModel.Message = "please mention price above the shown price";

            }
            return responseModel;



        }
    }





}

