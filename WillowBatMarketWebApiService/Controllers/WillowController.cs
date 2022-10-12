using Microsoft.AspNetCore.Mvc;
using System;
using WillowBatMarketWebApiService.BusinessLayer;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WillowController : ControllerBase
    {
        private readonly IWillowRepository willowRepository;
        WillowController(IWillowRepository willowRepository)
        {
            this.willowRepository = willowRepository;


        }
        [HttpPost("create_willow")]

        public ResponseModel create(WillowModel willowModel)
        {

            return willowRepository.Create(willowModel);

        }
        [HttpGet("fetch_All_Willows")]
        public ResponseModel fetchAll()
        {

            return willowRepository.GetAll();

        }

        [HttpGet("fetch_all_willow_by_id")]
        public ResponseModel fetch(Guid id)
        {
            return willowRepository.Get(id);


        }
        [HttpPut("update_willow")]
        public ResponseModel update(WillowModel willowModel, Guid id)
        {


            return willowRepository.Update(willowModel, id);


        }
        [HttpDelete("delete_willow")]
        public ResponseModel delete(Guid id)
        {


            return willowRepository.Delete(id);


        }

    }
}
