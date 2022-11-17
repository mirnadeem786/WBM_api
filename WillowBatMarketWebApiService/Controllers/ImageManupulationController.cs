using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WillowBatMarketWebApiService.DataLayer;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageManupulationController : ControllerBase
    {

        private IimageManupulation imageManupulation;
      
public ImageManupulationController(IimageManupulation imageManupulation)
        {
            this.imageManupulation = imageManupulation;
        }
        [HttpPost("upload_image")]
        public ActionResult uploadImage(ItemImages itemImage)
        {

            return Ok(imageManupulation.insertImage(itemImage));

        }

        [HttpGet("get-by-id")]

        public ActionResult getimage(Guid itemId)
        {

            return Ok(imageManupulation.getImageByItemId(itemId));
        }


    }
}
