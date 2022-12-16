using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using WillowBatMarketWebApiService.Controllers;
using WillowBatMarketWebApiService.DataLayer;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;
using Willow = WillowBatMarketWebApiService.Entity.Willow;

namespace WillowBatMarketWebApiService.BusinessLayer
{
    public interface ImanufacturerDashboardRepository
    {
        public ResponseModel fetchAllAuctionWillows();
        public ResponseModel fetch(string type);
        public ResponseModel partcipateInAuction(Bidder bidder);
        public ResponseModel ListOfParticipants(Guid auctionId);
     public Bidder highestBidder(Guid auctionId);
        public ResponseModel MyBidsDetails(Guid BidderId);
        public ResponseModel getAuctionDetails(Guid auctionId);
        public ResponseModel getAuctionWillow(Guid id);
        public ResponseModel winner(Guid auctionId);
        public ResponseModel orderRecieved(Guid manufacturerId);
        public ResponseModel actionOnOrder(Guid orderId, string action);
        public ResponseModel mostBatsSold(Guid manufacturerId);
        public ResponseModel fetchBats(Guid manufacturerId);
   
        public ResponseModel batsCloseToOutOfStock(Guid manufacturerId);
    }

    public class ManufacturerDashboardRepository : ImanufacturerDashboardRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly ResponseModel responseModel;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IBatRepository batRepository;


        public ManufacturerDashboardRepository(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment, IBatRepository batRepository)
        {
            this.appDbContext = appDbContext;
            this.responseModel = new ResponseModel();
            this.webHostEnvironment = webHostEnvironment;
            this.batRepository = batRepository;
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
                             auction.highestAmount,
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
                responseModel.Success = false;
                responseModel.Message = "NO RECORD";
                return responseModel;
            }
            responseModel.Data = querry;
            return responseModel;


        }

        public ResponseModel getAuctionDetails(Guid auctionId)
        {
            Auction auction = appDbContext.Auction.FirstOrDefault(a => a.auctionId == auctionId);
           if(auction == null)
            {

                responseModel.Success = false;
                responseModel.Message = "no such kind of auction";
                return responseModel;
            }
            responseModel.Data = auction;
            return responseModel;
        }

        public Bidder highestBidder(Guid auctionId)
        {
            var record = appDbContext.Bidder.Where(a => a.auctionId == auctionId).OrderByDescending(x=>x.amount).FirstOrDefault();
            if(record != null)
            {
                return record;

            }
            return null;
           
           
   
        }

        public ResponseModel ListOfParticipants(Guid auctionId)
        {

            var querry =(from manufacturer in appDbContext.Set<Manufacturer>()
                         join bidder in appDbContext.Set<Bidder>() on manufacturer.manufacturerId equals bidder.bidderId where bidder.auctionId==auctionId
                         select new
                         {
                             bidder.amount,
                             bidder.bidderName,
                             bidder.bidDate,
                         }).ToList();


             if (querry==null)

            {
                responseModel.Message = "NO RECORD";
                responseModel.Success = false;
                return responseModel;
            }

            responseModel.Data = querry;
            return responseModel;


        }

        public ResponseModel MyBidsDetails(Guid BidderId)
        {
            try
            {

                var query = (from auction in appDbContext.Set<Auction>()
                             join bidders in appDbContext.Set<Bidder>() on auction.auctionId equals bidders.auctionId
                             join willow in appDbContext.Set<Willow>() on auction.itemId equals willow.willowId
                             where bidders.bidderId == BidderId
                             select new
                             {
                                 auction.startingDateTime,
                                 auction.endDateTime,
                                 bidders.amount,
                                 bidders.bidDate,
                                 willow.WillowImage,
                                 willow.willowType,
                                 willow.willowSize,
                                 willow.willowId,
                                  willow.base64Image

                             }).ToList();
              

                if (query.Count() == 0)
                {
                    responseModel.Success = false;
                    responseModel.Message = "no result";
                    return responseModel;
                }
              
                responseModel.Data = query;
                responseModel.Message = "sucessfully fetched";


            }
            catch (Exception e)
            {

                responseModel.Message = "error while fetching";
            }
           
            return responseModel;
        }
        public ResponseModel partcipateInAuction(Bidder bidder)
        {

            //   var record = db.Table.Where(p => p.Id == Id).OrderByDescending(x => x.ReceivedDateTime).FiBrstOrDefault();
            // if (record != null) { }
            

            Auction record = appDbContext.Auction.Where(i => i.auctionId == bidder.auctionId).OrderByDescending(x => x.highestAmount).FirstOrDefault();
            if(record==null)
            {
                responseModel.Success = false;
                responseModel.Message = "no such auction is going";
                return responseModel;
            }
           else if(DateTime.Compare(record.startingDateTime,DateTime.Now)>0)
            {
                responseModel.Message = "Bid is not yet started";
                responseModel.Success = false;
                return responseModel;


            }
           else if(DateTime.Compare(record.endDateTime,DateTime.Now)<0)
            {
                responseModel.Message = "bid has expired";
                responseModel.Success = false;
              return  responseModel;


            }
           else if(record.highestAmount>=bidder.amount)
            {
                responseModel.Message = "price given is below or equal the given bid ";
                responseModel.Success = false;
                return responseModel;

            }
            try
            {
                updateAuction(bidder.amount, bidder.auctionId);

                appDbContext.Bidder.Add(new Bidder()
                {

                    bidderId = bidder.bidderId,
                    amount = bidder.amount,
                    auctionId = bidder.auctionId,
                    bidDate=DateTime.Now,
                    bidderName=bidder.bidderName,



                }) ;
                appDbContext.SaveChanges();
                responseModel.Message = "bid has been placed ";
            }
            catch(Exception e)
            {
                responseModel.Message = "some error occured while inserting ";
                responseModel.Success = false;

            }
            
           
            
            return responseModel;



        }

        public ResponseModel winner(Guid ManufacturerId)
        {
            var record =( from bidder in appDbContext.Set<Bidder>()
                         join auction in appDbContext.Set<Auction>() on bidder.auctionId equals auction.auctionId
                         where bidder.bidderId == ManufacturerId
                         select new
                         {

                             auction

                         }).ToList(); 
            if(record==null)
            {
               
                
                responseModel.Message = "no such Auction Exist";
                responseModel.Success = false;
                return responseModel;
            }
            List<Auction> winings = new List<Auction>();
            foreach(var item in record)
            {

                if (DateTime.Compare(item.auction.endDateTime, DateTime.Now) < 0)
                {
                    
                   if( highestBidder(item.auction.auctionId).bidderId==ManufacturerId)
                    winings.Add(appDbContext.Auction.FirstOrDefault(a=>a.auctionId==item.auction.auctionId));
                }


            }
            responseModel.Data = winings;
          
            return responseModel;
        }

        private void updateAuction(decimal price,Guid auctionId)
        {
            Auction record = appDbContext.Auction.Find(auctionId);
            record.highestAmount = price;
            appDbContext.Update(record);
        }


        public string getImageByItemId(Guid itemId)

        {
            var path = Path.Combine(webHostEnvironment.ContentRootPath, "~/Uploads/bat/");
            string imagepath = path + "/" + itemId + ".png";                                      // + Path.GetExtension(file.FileName);
            byte[] b = System.IO.File.ReadAllBytes(imagepath);

            return "data:image/png;base64," + Convert.ToBase64String(b);

        }
        public ResponseModel getAuctionWillow(Guid id)
        {

            var querry = from auction in appDbContext.Set<Auction>()
                         join willow in appDbContext.Set<Willow>() on auction.itemId equals willow.willowId where auction.auctionId==id
                         select new
                         {
                             auction.highestAmount,
                             auction.auctionId,
                             auction.itemId,
                             auction.startingDateTime,
                             auction.endDateTime,
                             willow.willowPrice,
                             willow.WillowImage,
                             willow.willowType,
                             willow.willowSize,
                             willow.willowSellerId,
                             willow.address

                         };


            if (querry == null)

            {
                responseModel.Success = false;
                responseModel.Message = "NO RECORD";
                return responseModel;
            }
            responseModel.Data = querry;
            return responseModel;

        }

        public ResponseModel orderRecieved(Guid manufacturerId)
        {

            var OrderdItems = (from orderItems in appDbContext.Set<OrderItems>()
                               join bat in appDbContext.Set<Bat>() on orderItems.itemId equals bat.batId
                               where bat.manufacturerId == manufacturerId
                               select new
                               {
                                   bat,
                                   orderItems




                               }).ToList();


            if(OrderdItems==null)
            {

                responseModel.Message = "no order";
                responseModel.Success = false;
                return responseModel;

            }
            responseModel.Data = OrderdItems;
            responseModel.Message = "your recieved orders";
            return responseModel;






        }
        public ResponseModel actionOnOrder(Guid orderId,string action)
        {
            var orderStatus = appDbContext.OrderStatus.FirstOrDefault(o => o.orderId == orderId);
            orderStatus.status = action;
            orderStatus.date=DateTime.Now;
           
            try
            { 
              appDbContext.OrderStatus.Add(orderStatus);
                appDbContext.SaveChanges();
                responseModel.Message = "success";
                return responseModel;

            }
            catch(Exception)
            {
                responseModel.Message = "error";
                return responseModel;
            
            }



        }


        public ResponseModel mostBatsSold(Guid manufacturerId)
        {
            var query = (from orderItems in appDbContext.Set<OrderItems>()
                         join bats in appDbContext.Set<Bat>() on orderItems.itemId equals bats.batId
                         where (bats.manufacturerId == manufacturerId)
                         group orderItems by orderItems.itemId into g
                         select new
                         {
                             itemId = g.Key,

                             totalQuantity = g.Sum(orderItems => orderItems.quantity)

                         }
                        ).OrderByDescending(i => i.totalQuantity).Take(10).ToList();


            if (query.Count() < 0)
            {
                responseModel.Success = false;
                return responseModel;

            }

            List<Bat> bats1 = new List<Bat>();
            foreach(var item in query)
            {
                bats1.Add((Bat) batRepository.Get(item.itemId).Data);

            }
            responseModel.Data = bats1;
            responseModel.Message = "success";
            return responseModel;
        }

        public ResponseModel batsCloseToOutOfStock(Guid manufacturerId)
        {
            var query = appDbContext.Set<Bat>().Where(b => b.manufacturerId == manufacturerId  ).GroupBy(x => x.batId).Select(group => new
            {
                batId = group.Key,
                Quantity = group.Sum(o => o.quantity),


            }).Where(Q => Q.Quantity < 10).ToList();
            if(query.Count<0)
            {
                responseModel.Message="no items";
                return responseModel;

            }
            responseModel.Data=query;
            List<Bat> bats = new List<Bat>();
            foreach( var item in query)
            {
                bats.Add((Bat)batRepository.Get(item.batId).Data);


            }
            responseModel.Data = bats;
            return responseModel;
        }

        public ResponseModel fetchBats(Guid manufacturerId)
        {
          
           //  var bats = appDbContext.Manufacturer.Include(p => p.bats).Where(m =>m. manufacturerId == manufacturerId).ToList();
            var bats=appDbContext.Bat.Where(m => m.manufacturerId == manufacturerId).ToList();
            if (bats.Count<0)
            {
                responseModel.Message = "no bats available";
                return responseModel;

            }
            responseModel.Data = bats;
            responseModel.Message = "Success";
            return responseModel;
        }


       


    }

    



}
