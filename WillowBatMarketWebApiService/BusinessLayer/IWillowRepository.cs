using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using WillowBatMarketWebApiService.DataLayer;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WillowBatMarketWebApiService.BusinessLayer
{
    public interface IWillowRepository
    {

           public ResponseModel Get(Guid id);
            public ResponseModel Create( WillowModel willowMode);
            public ResponseModel Update(WillowModel willowModel, Guid id);
            public ResponseModel Delete(Guid id);
            public ResponseModel GetAll();


        }

    public class WillowRepository : IWillowRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ResponseModel responseModel;
        private readonly IMapper mapper;
        public WillowRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.mapper = mapper;
            _appDbContext = appDbContext;
            responseModel = new ResponseModel();
        }

        public ResponseModel Create([FromForm] WillowModel willowModel)
        {



            try
            {




                var willow = mapper.Map<Willow>(willowModel);

                if (willow.willowId == Guid.Empty)
                {

                    willow.willowId = Guid.NewGuid();

                }

              /*  using (var stream = new MemoryStream())
                {
                    image.CopyTo(stream);
                    willow.WillowImage = stream.ToArray();

              */
                
            
                _appDbContext.Add(willow);
                _appDbContext.SaveChanges();
                responseModel.Success = true;
                responseModel.Data = willow;
                return responseModel;

            }
            catch (Exception ex)

            {
                responseModel.Success = false;
                responseModel.Message = ex.GetBaseException().Message;
                return responseModel;
            }


        }
        public ResponseModel Delete(Guid id)
        {
            try
            {
                var willow = _appDbContext.Willow.Find(id);
                if (willow != null)
                {
                    _appDbContext.Willow.Remove(willow);
                    _appDbContext.SaveChanges();
                    responseModel.Data = id;
                    responseModel.Message = "sucessfully deleted";
                }
                return responseModel;
            }

            catch (Exception ex)
            {
                responseModel.Message = "error";
                responseModel.Error = ex.StackTrace;
                responseModel.Success = false;
                return responseModel;
            }

        }

        public ResponseModel Get(Guid id)
        {
            try
            {
                responseModel.Data = _appDbContext.Willow.FirstOrDefault(x => x.willowId == id);
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

                responseModel.Data = _appDbContext.Willow.ToList();
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

        public ResponseModel Update(WillowModel willowModel, Guid id)
        {
            try
            {
                var willow = mapper.Map(willowModel, _appDbContext.Willow.Find(id));
                _appDbContext.Update(willow);
                _appDbContext.SaveChanges();
                responseModel.Data =
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



       
    


