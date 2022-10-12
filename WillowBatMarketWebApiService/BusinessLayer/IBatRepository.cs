using AutoMapper;
using System;
using System.Linq;
using WillowBatMarketWebApiService.DataLayer;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.BusinessLayer
{
    public interface IBatRepository
    {

        public ResponseModel Get(Guid id);
        public ResponseModel Create(BatModel bat);
        public ResponseModel Update(BatModel bat, Guid id);
        public ResponseModel Delete(Guid id);
        public ResponseModel GetAll();



    }
    public class BatRepository : IBatRepository
    {
        private readonly IMapper mapper;
        private readonly AppDbContext _appDbContext;
        private readonly ResponseModel responseModel;
        public BatRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.mapper = mapper;
            _appDbContext = appDbContext;
            responseModel = new ResponseModel();
        }

        public ResponseModel Create(BatModel batModel)
        {
            try
            {
                
                   

              
                Bat bat = mapper.Map<Bat>(batModel);
                bat.batId = Guid.NewGuid();

                _appDbContext.Bat.Add(bat);
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

                var bat = _appDbContext.Bat.Find(id);
                if (bat != null)
                {
                    _appDbContext.Bat.Remove(bat);
                    _appDbContext.SaveChanges();
                    responseModel.Data = id;
                    responseModel.Message = "sucessfully deleted";
                }
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
                responseModel.Data = _appDbContext.Bat.FirstOrDefault(x => x.batId== id);
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

        public ResponseModel Update(BatModel batModel, Guid id)
        {
            try
            {

                Bat bat = mapper.Map(batModel, _appDbContext.Bat.Find(id));
                _appDbContext.Update(bat);
                _appDbContext.SaveChanges();
                responseModel.Data = bat.batId;
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
