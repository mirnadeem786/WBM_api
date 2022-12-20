using AutoMapper;
using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WillowBatMarketWebApiService.DataLayer;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;
using static System.Net.WebRequestMethods;

namespace WillowBatMarketWebApiService.BusinessLayer
{
    public interface IBatRepository
    {

        public ResponseModel Get(Guid id);
        public ResponseModel Create(BatModel bat);
        public ResponseModel Update(BatModel bat, Guid id);
        public ResponseModel Delete(Guid id);
        public ResponseModel GetAll(Pagination pagination);





    }
    public class BatRepository : IBatRepository
    {
        private readonly IMapper mapper;
        private readonly AppDbContext _appDbContext;
        private readonly ResponseModel responseModel;
        private readonly IimageManupulation imageManupulation;

        public BatRepository(AppDbContext appDbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment, IimageManupulation imageManupulation)
        {
            this.mapper = mapper;
            _appDbContext = appDbContext;
            responseModel = new ResponseModel();
            this.imageManupulation = imageManupulation;

        }

        public ResponseModel Create(BatModel batModel)
        {


            Bat bat = mapper.Map<Bat>(batModel);
            bat.batId = Guid.NewGuid();
            try
            {
                _appDbContext.Bat.Add(bat);

                _appDbContext.SaveChanges();



                responseModel.Message = "succesfully inserted";
                responseModel.Success = true;
                responseModel.Data = bat.batId;
                return responseModel;

            }
            catch (Exception ex)
            {
                responseModel.Message = ex.InnerException.ToString();
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
                Bat bat = _appDbContext.Bat.FirstOrDefault(x => x.batId == id);
                bat.base64Image = imageManupulation.getImageByItemId(bat.batId);
                responseModel.Data = bat;
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

        public ResponseModel GetAll(Pagination pagination)
        {
            /* var bats = (from bat in _appDbContext.Set<Bat>()
                         join itemImages in _appDbContext.Set<ItemImages>() on bat.batId equals itemImages.itemId
                         select new
                         { bat
                                                           //itemImages

                         }).ToList();
            */
            responseModel.TotalRecords = _appDbContext.Bat.Count();
            var bats = _appDbContext.Bat
              .OrderBy(on => on.sellingPrice)
        .Skip((pagination.PageNumber - 1) * pagination.PageSize)
        .Take(pagination.PageSize)
        .ToList();

            // issue while fetching all images
          /*  foreach (var bat in bats)
            {
                if (imageManupulation.getImageByItemId(bat.batId) != null)
                    bat.base64Image = imageManupulation.getImageByItemId(bat.batId);

            }*/



            // List < Bat> bat = _appDbContext.Bat.ToList();
            try
            {
                responseModel.Data = bats;
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
