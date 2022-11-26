using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WillowBatMarketWebApiService.BusinessLayer;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CricketerDashboard : ControllerBase
    {
            private readonly ICricketerDashboardRepository cricketerDashBoardRepository;
            public CricketerDashboard(ICricketerDashboardRepository cricketerDashBoardRepository)
            {
                this.cricketerDashBoardRepository = cricketerDashBoardRepository;

            }
            [HttpGet("fetch_bat")]
            public ResponseModel fetch(string quality, string bladded, int grains)
            {
                return cricketerDashBoardRepository.fetch(quality, bladded, grains);


            }
            [HttpPost("addToCart")]
            // Object o
            public ResponseModel addToCat(Guid itemId, Guid cartId, short quantity)
            {
                return cricketerDashBoardRepository.addTocart(itemId, cartId, quantity);


            }

            [HttpPost("place_order")]
            public ResponseModel placeOrder(Guid cartId)
            {

                return cricketerDashBoardRepository.placeOrder(cartId);


            }



            [HttpDelete("removeFromCart")]

            public ResponseModel removeFromCart(Guid itemId, Guid cartId)
            {

                return cricketerDashBoardRepository.removeFromCart(itemId, cartId);

            }
            [HttpPost("buyNow")]
            //Object o
            public ResponseModel buyNow(Guid itemId, Guid customerId,short quantity)
            {
                return cricketerDashBoardRepository.buyNow(itemId, customerId,quantity);


            }
            [HttpGet("order_status")]
            public ResponseModel orderStatus(Guid orderid)
            {

                return cricketerDashBoardRepository.orderStaus(orderid);


            }
        [HttpGet("Search")]
        public IActionResult search(string parm)
        {

            return Ok( cricketerDashBoardRepository.search(parm));


        }
        [HttpGet("bat_recomendation")]
        public IActionResult batRecomendation(float height)
        {

            return  Ok (cricketerDashBoardRepository.BatRecomandation(height));


        }
        [HttpGet("items_in_cart")]
        public IActionResult itemsInCart(Guid cartId)
        {

            return Ok(cricketerDashBoardRepository.ItemsInCats(cartId));


        }



        [HttpDelete("clear-cart")]
        public ResponseModel clearCart(Guid cartId)
        {

            return cricketerDashBoardRepository.clearCart(cartId);


        }




    }
}






