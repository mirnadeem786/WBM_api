using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Linq;
using WillowBatMarketWebApiService.DataLayer;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.BusinessLayer
{
    public interface IWillowSellerRepository
    {


        public ResponseModel Get(Guid id);
        public ResponseModel create(WillowSellerModel model);
      

        public ResponseModel Update(WillowSellerModel willowSellerModel ,Guid id);
        public ResponseModel Delete(Guid id);
        public ResponseModel GetAll();

    }

    public class WillowSellerRepository : IWillowSellerRepository
    {

        private readonly AppDbContext _appDbContext;
        private ResponseModel responseModel;
        private readonly IMapper mapper;
        public WillowSellerRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this._appDbContext = appDbContext;
            responseModel = new ResponseModel();
        }
        public ResponseModel create(WillowSellerModel Model)
        {
            try
            {
                var willowSeller = mapper.Map<WillowSeller>(Model);
                _appDbContext.Add(willowSeller);
                _appDbContext.SaveChanges();
                responseModel.Success = true;
                responseModel.Data = willowSeller;
                return responseModel;

            }
            catch (Exception ex)

            {
                responseModel.Success = false;
                responseModel.Message = ex.InnerException.ToString();
                return responseModel;
            }


        }

        

        public ResponseModel Delete(Guid id)
        {
            try
            {
               _appDbContext.Remove(_appDbContext.WillowSeller.Find(id));
                responseModel.Data = id;
               _appDbContext.SaveChanges();
                responseModel.Message = "sucessfully deleted";
                return responseModel;
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                responseModel.Error = ex.StackTrace;
                responseModel.Success = true;
                return responseModel;
            }

        }

        public ResponseModel Get(Guid id)
        {
            try
            {
                responseModel.Data = _appDbContext.WillowSeller.FirstOrDefault(x => x.willowSellerId==id);
                _appDbContext.SaveChanges();
                responseModel.Message = "sucess";
                return responseModel;
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                responseModel.Error = ex.StackTrace;
                responseModel.Success = false;
                return responseModel;
            }
        }

        public ResponseModel GetAll()
        {
            try
            {

                responseModel.Data = _appDbContext.Bat.ToList();
                _appDbContext.SaveChanges();
                responseModel.Success = true;
                return responseModel;


            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                responseModel.Error = "error";
                responseModel.Success = false;
                return responseModel;
            }
        }

        public ResponseModel Update(WillowSellerModel willowModel , Guid id)
        {
            try
            {
                var willoSeller=mapper.Map(_appDbContext.WillowSeller.Find(id),willowModel);
            _appDbContext.Update(id);
                responseModel.Data = id;
                _appDbContext.SaveChanges();
                responseModel.Message = "sucess";
                return responseModel;
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                responseModel.Error = ex.StackTrace;
                responseModel.Success = false;
                return responseModel;
            }
        }

        }
    }

