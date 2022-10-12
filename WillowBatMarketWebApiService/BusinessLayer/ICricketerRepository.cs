using AutoMapper;
using System;
using System.Linq;
using WillowBatMarketWebApiService.DataLayer;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.BusinessLayer
{
    public interface ICricketerRepository
    {
        public ResponseModel Get(Guid id);
        public ResponseModel Create(CricketerModel cricketerModel);
        public ResponseModel Update(CricketerModel cricketerModel, Guid id);
        public ResponseModel Delete(Guid id);
        public ResponseModel GetAll();



    }


    public class CricketerRepository : ICricketerRepository


    {
        private readonly AppDbContext _appDbContext;
        private readonly ResponseModel responseModel;
        private readonly IMapper mapper;
        public CricketerRepository(AppDbContext appDbContext, IMapper mapper)

        {
            this.mapper = mapper;
            _appDbContext = appDbContext;
            responseModel = new ResponseModel();

        }
        public ResponseModel Create(CricketerModel cricketerModel)
        {
            try
            {
                var cricketer = mapper.Map<Cricketer>(cricketerModel);
                responseModel.Data = _appDbContext.Cricketer.Add(cricketer);
                _appDbContext.SaveChanges();
                responseModel.Message = "succesfully inserted";
                responseModel.Success = true;
                return responseModel;

            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                responseModel.Error = ex.StackTrace;
                return responseModel;
            }
        }

        public ResponseModel Delete(Guid id)
        {
            try
            {

                responseModel.Data = _appDbContext.Remove(_appDbContext.Cricketer.FirstOrDefault(x => x.CRICKETER_ID == id));
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
                responseModel.Data = _appDbContext.Cricketer.FirstOrDefault(x => x.CRICKETER_ID == id);
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
                responseModel.Data = _appDbContext.Cricketer.ToList();
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

        public ResponseModel Update(CricketerModel cricketerModel, Guid id)
        {

            try
            {
                var cricketer = mapper.Map(_appDbContext.Cricketer.Find(id), cricketerModel);
                responseModel.Data = _appDbContext.Update(cricketer);
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
