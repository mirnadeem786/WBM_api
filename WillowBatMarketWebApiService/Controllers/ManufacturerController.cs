using Microsoft.AspNetCore.Mvc;
using System;
using WillowBatMarketWebApiService.BusinessLayer;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WillowBatMarketWebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        public ManufacturerController(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        [HttpPost("create_manufacturer")]
        public ResponseModel create(ManufacturerModel manufacturer)
        {
            return _manufacturerRepository.Create(manufacturer);
        }
        [HttpDelete("delete_by_Id")]
        public ResponseModel delete(Guid id)
        {

            return _manufacturerRepository.Delete(id);



        }
        [HttpGet("fetch_all")]
        public ResponseModel getall()
        {

            return _manufacturerRepository.GetAll();

        }
        [HttpPut("update")]
        public ResponseModel update(ManufacturerModel manufacturer)
        {

            return _manufacturerRepository.Update(manufacturer);


        }
       
    }
}
