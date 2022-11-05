using AutoMapper;
using Microsoft.Extensions.Primitives;
using System;
using WillowBatMarketWebApiService.DataLayer;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.BusinessLayer
{
    public interface IwillowSellerRepositoryDashboard
    {
        public ResponseModel startBiddeing(Auction auction);
        public ResponseModel upload(WillowModel model);


    }

    public class willowSellerRepositoryDashboard : IwillowSellerRepositoryDashboard
    {


        private readonly AppDbContext _appDbContext;
        private ResponseModel responseModel;
        private readonly IMapper mapper;
        public willowSellerRepositoryDashboard(AppDbContext appDbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this._appDbContext = appDbContext;
            responseModel = new ResponseModel();
        }


        public ResponseModel startBiddeing(Auction auction)
        {
            try

            {
                
                auction.auctionId = Guid.NewGuid();
                auction.highestAmount = auction.startingPrice;
                _appDbContext.Auction.Add(auction);
                _appDbContext.SaveChanges();
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
                var willow= mapper.Map<Willow>(model);
                _appDbContext.Add(willow);
                _appDbContext.SaveChanges();
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
    }
}
