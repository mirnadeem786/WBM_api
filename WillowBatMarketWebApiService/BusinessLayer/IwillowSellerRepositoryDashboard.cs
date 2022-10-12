using AutoMapper;
using System;
using WillowBatMarketWebApiService.DataLayer;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.BusinessLayer
{
    public interface IwillowSellerRepositoryDashboard
    {
        public ResponseModel startBiddeing(Guid itemId, DateTime startingTime, DateTime endTime, decimal startingPrice);
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


        public ResponseModel startBiddeing(Guid itemId, DateTime startingTime, DateTime endTime, decimal startingPrice)

        {

            try
            {
                _appDbContext.Auction.Add(new Auction
                {

                    auctionId = Guid.NewGuid(),
                    startingDateTime = startingTime,
                    endDateTime = endTime,
                    startingPrice = startingPrice,
                    itemId = itemId,
                    highestAmount = 0

                });
                _appDbContext.SaveChanges();
                responseModel.Success = true;
                responseModel.Data = itemId;
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
