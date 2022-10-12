using AutoMapper;
using System;
using System.Linq;
using WillowBatMarketWebApiService.DataLayer;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.BusinessLayer
{
    public interface IManufacturerRepository
    {
        public ResponseModel Create(ManufacturerModel manufacturer);
        public ResponseModel Update(ManufacturerModel manufacturer);
        public ResponseModel Delete(Guid id);
        public Manufacturer Get(Guid id);
        public ResponseModel GetAll();





    }

    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly ResponseModel responseModel;
        private readonly AppDbContext appDbContext;
        private readonly IMapper _mapper;
        public ManufacturerRepository(AppDbContext appDbContext, IMapper mapper)
        {
            responseModel = new ResponseModel();
            this.appDbContext = appDbContext;
            _mapper = mapper;
        }

        public ResponseModel Create(ManufacturerModel manufacturer)
        {
            try
            {
                Manufacturer m = _mapper.Map<Manufacturer>(manufacturer);
                m.manufacturerId= Guid.NewGuid();

                appDbContext.Manufacturer.Add(m);
                appDbContext.SaveChanges();
                responseModel.Data = m;
                responseModel.Message = "successfully added";
                responseModel.Status = 500;
                responseModel.Success = true;
            }
            catch (Exception ex)
            {
                responseModel.Success = false;
                responseModel.Message = "fail";
                responseModel.Data = ex.InnerException;

            }
            return responseModel;

        }

        public ResponseModel Delete(Guid id)
        {
            try
            {
                appDbContext.Remove(id);
                appDbContext.SaveChanges();
                responseModel.Data = id;
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

        public Manufacturer Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public ResponseModel GetAll()
        {
            try
            {

                responseModel.Data = appDbContext.Manufacturer.ToList();
                appDbContext.SaveChanges();
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



        public ResponseModel Update(ManufacturerModel manufacturer)
        {
            try
            {
                Manufacturer m = _mapper.Map<Manufacturer>(manufacturer);
                appDbContext.Manufacturer.Update(m);
                appDbContext.SaveChanges();
                responseModel.Data = m;
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

    }
}
