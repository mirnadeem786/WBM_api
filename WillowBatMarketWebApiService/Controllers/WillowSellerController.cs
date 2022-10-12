using Microsoft.AspNetCore.Mvc;
using System;
using WillowBatMarketWebApiService.BusinessLayer;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WillowSellerController : ControllerBase
    {
        private readonly IWillowSellerRepository willowSellerRepository;
        WillowSellerController(IWillowSellerRepository willowSellerRepository)
        {
            this.willowSellerRepository = willowSellerRepository;


        }
        [HttpPost("create_Willow_Seller")]

        public ResponseModel create(WillowSellerModel willowSellerModel)
        {

            return willowSellerRepository.create(willowSellerModel);

        }
        [HttpGet("fetch_All_Willow_Seller")]
        public ResponseModel fetchAll()
        {

            return willowSellerRepository.GetAll();
        }

        [HttpGet("fetch_all_willow_Seller_by_id")]
        public ResponseModel fetch(Guid id)
        {
            return willowSellerRepository.Get(id);


        }
        [HttpPut("update_willow")]
        public ResponseModel update(WillowSellerModel willowSellerModel, Guid id)
        {


            return willowSellerRepository.Update(willowSellerModel, id);


        }
        [HttpDelete("delete_willow_seller")]
        public ResponseModel delete(Guid id)
        {


            return willowSellerRepository.Delete(id);


        }
       

    }
}
