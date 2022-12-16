using AutoMapper;
using System;
using System.Linq;
using WillowBatMarketWebApiService.DataLayer;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.BusinessLayer
{
    public interface IwillowSellerDashboardRepo
    {

        public ResponseModel startBiddeing(Auction auction);
        public ResponseModel upload(WillowModel model);
        public ResponseModel fetchAuction(Guid willowSellerId);
        public ResponseModel fetchWillows(Guid willowSellerId);


    }



public class WillowSellerDashboardRepo : IwillowSellerDashboardRepo
{


    private readonly AppDbContext appDbContext;
    private ResponseModel responseModel;
    private readonly IMapper mapper;
    public WillowSellerDashboardRepo(AppDbContext appDbContext, IMapper mapper)
    {
        this.mapper = mapper;
        this.appDbContext = appDbContext;
        responseModel = new ResponseModel();
    }


    public ResponseModel startBiddeing(Auction auction)
    {
        try

        {

            auction.auctionId = Guid.NewGuid();
            auction.highestAmount = auction.startingPrice;
            appDbContext.Auction.Add(auction);
            appDbContext.SaveChanges();
            responseModel.Success = true;
            responseModel.Data = auction.itemId;
            return responseModel;
        }
        catch (Exception ex)
        {
            responseModel.Message = ex.InnerException.ToString();
            responseModel.Success = false;
            return responseModel;

        }









    }

    public ResponseModel upload(WillowModel model)
    {
        try
        {
            var willow = mapper.Map<Willow>(model);
            appDbContext.Add(willow);
            appDbContext.SaveChanges();
            responseModel.Success = true;
            responseModel.Data = willow;
            return responseModel;

        }
        catch (Exception ex)

        {
            responseModel.Success = false;
            responseModel.Message = ex.InnerException.ToString();
            return responseModel;
        }

    }

    public ResponseModel fetchAuction(Guid willowSellerId)
    {
            var querry = (from auction in appDbContext.Set<Auction>()
                         join   willow in appDbContext.Set<Willow>() on auction.itemId equals willow.willowId
                         where willow.willowSellerId == willowSellerId
                         select new
                         {

                             auction.startingDateTime,
                             auction.endDateTime,
                             auction.startingPrice,
                             willow.WillowImage,
                             willow.willowType,
                             willow.willowSize,


                         }).ToList();

            if(querry.Count<0)
            {
                responseModel.Success=false;
                responseModel.Message = "no auction willow is available";
                return responseModel;
            }
            responseModel.Data = querry;
            return responseModel;
    }
        public ResponseModel fetchWillows(Guid willowSellerId)
        {
            var querry = (from auction in appDbContext.Set<Auction>()
                          join willow in appDbContext.Set<Willow>() on auction.itemId !equals willow.willowId
                          where willow.willowSellerId == willowSellerId
                          select new
                          {
                       willow

                          }).ToList();

            if (querry.Count < 0)
            {
                responseModel.Success = false;
                responseModel.Message = "no  willow is available";
                return responseModel;
            }
            responseModel.Data = querry;
            return responseModel;
        }

    }
}

