﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WillowBatMarketWebApiService.BusinessLayer;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatControler : ControllerBase
    {

        private readonly IBatRepository _iBatRepository;
        private readonly IUsserRepository _uissserRepository;

        private readonly IWillowRepository willowRepository1;

        public BatControler(IBatRepository batRepository,IUsserRepository usserRepository)
        {
            this._iBatRepository = batRepository;
       
            _uissserRepository=usserRepository;
        }


        [HttpPost("create_Bat")]
        public ResponseModel create( BatModel bat)
        {
            //var image = Request.Form.Files[0];

            return _iBatRepository.Create(bat);


        }
       

        [HttpGet("fetch_By_Id")]

        public ResponseModel getById(Guid id)
        {

            return _iBatRepository.Get(id);

        }
        [HttpGet("fetchAll")]
        public ResponseModel fetchAll([FromQuery]Pagination pagination)
        {

            return _iBatRepository.GetAll(pagination);
        }

        [HttpDelete("delete")]
        public ResponseModel delete(Guid batId)
        {

            return _iBatRepository.Delete(batId);
        }
        [HttpPut("update")]
        public ResponseModel update(BatModel bat, Guid id)
        {
            return _iBatRepository.Update(bat, id);

        }



    }
}
