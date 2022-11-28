using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using WillowBatMarketWebApiService.DataLayer;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.BusinessLayer
{
    public interface ICricketerDashboardRepository
    {

        public ResponseModel fetch(string quality, string bladded, int grains);
        // Object o
        public ResponseModel addTocart(Guid itemId, Guid cartId, short quantity);
        public ResponseModel removeFromCart(Guid itemId, Guid cartId);
        public ResponseModel ItemsInCats(Guid cartId);
        public ResponseModel clearCart(Guid cartId);
        public ResponseModel placeOrder(Guid caartId);
        //      object o,
        public ResponseModel buyNow(Guid itemId, Guid cartId,short quantity);
        public ResponseModel orderStaus(Guid orderId);
        public ResponseModel search(string parm);
        public ResponseModel BatRecomandation(float height);
       
    }
    public class CricketerDashboardRepository : ICricketerDashboardRepository
    {
        public Guid shopingCartId { get; set; }
        private readonly AppDbContext appDbContext;
        private readonly ResponseModel responseModel;
        IMapper mapper;
        public const string cartSessionKey = "cartId";
        public CricketerDashboardRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.mapper = mapper;
            responseModel = new ResponseModel();
            this.appDbContext = appDbContext;
        }
        //         Object o
        public ResponseModel addTocart(Guid itemId, Guid cartId, short quantity)
        {
            if(appDbContext.CartItems.FirstOrDefault(i=>i.itemId==itemId)!=null)
            {
                responseModel.Message = "item is already in cart";
                responseModel.Success = false;
                return responseModel;


            }
            CartItems cartItems = new CartItems();  

           var bat = appDbContext.Bat.FirstOrDefault(b => b.batId == itemId);

            Cart cart = appDbContext.Cart.Find(cartId);
            if (cart != null)
            {

                cartItems.cartId = cart.cartId;
                cartItems.quantity =   quantity;
                cartItems.amount = bat.sellingPrice*quantity;
                cartItems.itemId= itemId;
                cartItems.createdOn = DateTime.Now;
                cartItems.updatedOn = DateTime.Now;
                cart.totalAmount += cartItems.amount;
                cartItems.itemType = EntityType.BAT;
              
                try
                {
                    appDbContext.CartItems.Add(cartItems);
                    appDbContext.SaveChanges();
                    responseModel.Message = "item is added in cart";
                }



                catch (Exception ex)
                {
                    responseModel.Success = false;

                    responseModel.Message = ex.Message
    ;
                    return responseModel;

                }

                

            }
            else
            {
                responseModel.Success = false;
                responseModel.Message = "error";
            }



         
         
            return responseModel;
        }


        // object o,
        public ResponseModel buyNow(Guid itemId, Guid cricketerId,short quantity)
        {
            // Bat bat = new Bat();
            //if (o.GetType().Equals(bat))
            //{
            var stock = appDbContext.Bat.Where(b => b.batId == itemId).Select(bat => bat.quantity).FirstOrDefault();
            if(stock==0)
            {
                responseModel.Message = "out of stock";
                return responseModel;

            }
           if (stock < quantity)
            {
                responseModel.Message = "only"+stock+"bat is availble";
                return responseModel;


            }
            var item = appDbContext.Bat.FirstOrDefault(i => i.batId == itemId);
            // Bat b = (Bat)o;
            item.quantity-=quantity;

            OrderItems order = new OrderItems()
            {


                orderId = Guid.NewGuid(),
                cricketerId = cricketerId,
                createdOn = DateTime.Now,                      //b.createdOn,
                updatedOn = DateTime.Now,
                itemId = itemId,
                itemType = EntityType.BAT,
                quantity = quantity,
                amount = item.sellingPrice*quantity,                        //b.batPrice,
                discount = item.discount,                    //b.discount,
                orderDate = DateTime.Now,



            };
            try
            {

                appDbContext.OrderItems.Add(order);
                responseModel.Data = order;
                OrderStatus orderStatus = new OrderStatus()
                {
                    orderId = order.orderId,
                    status = OrderStatusInfo.PLACED,
                    date = order.orderDate
                };
                appDbContext.OrderStatus.Add(orderStatus);
                appDbContext.SaveChanges();

                return responseModel;
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.InnerException.ToString();
                responseModel.Success = false;
                return responseModel;

            }
            //}
            // return responseModel;


        }                                   //dbo.spr_Cricketer_Filter_Dashboard"
        public ResponseModel fetch(string quality, string bladded, int grains)         // FromSqlRaw("dbo.spr_Cricketer_Filter_Dashboard @batQuality={0},@batBladded={1},@batGrains={2}", quality, bladded, grains).ToList()
        {
            try
            {

                List<CricketerDashboard> dashboards = appDbContext.CricketerDashboard.FromSqlRaw("dbo.spr_Cricketer_Filter_Dashboard @batQuality={0},@batBladded={1},@batGrains={2}", quality, bladded, grains).ToList();
               if (dashboards==null)
                {
                    responseModel.Data = "some errors occurs";
                    return responseModel; 

                }
                responseModel.Data = dashboards;
                responseModel.Success = true;
                return responseModel;
            }
            catch (System.Exception ex)
            {

                responseModel.Success = false;
                responseModel.Message = ex.GetBaseException().ToString();
                return responseModel;
            }

        }

        public ResponseModel orderStaus(Guid orderId)
        {

            var record = appDbContext.OrderStatus.OrderByDescending(x => x.date).FirstOrDefault();
            responseModel.Data = record;
            responseModel.Success = true;
            return responseModel;

        }

        public ResponseModel placeOrder(Guid cartId)
        {
            try
            {

                Cart cart=appDbContext.Cart.Find(cartId);
                List<CartItems> items = appDbContext.CartItems.Where(c=>c.cartId==cartId).ToList();
                foreach (var item in items)
                {

                    OrderItems order = new OrderItems()
                    {
                        orderId = Guid.NewGuid(),
                        cricketerId = cart.cricketerId,
                        orderDate = DateTime.Now,
                        createdOn = item.createdOn,
                        updatedOn = item.updatedOn,
                        itemId = item.itemId,
                        itemType = item.itemType,
                        quantity = item.quantity,
                        amount = item.amount,
                        

                    };
                    OrderStatus orderStatus = new OrderStatus()
                    {
                        orderId = order.orderId,
                        status = OrderStatusInfo.PLACED,
                        date=DateTime.Now, 
                        


                    };
                    appDbContext.OrderItems.Add(order);
                    appDbContext.OrderStatus.Add(orderStatus);
                    appDbContext.SaveChanges();
                    clearCart( cartId);

                }

                responseModel.Success = true;
                responseModel.Message = "order has been palced";
                responseModel.Data = cartId;
                return responseModel;
            }
            catch (Exception ex)
            {

                responseModel.Message = ex.InnerException.ToString();
                responseModel.Success = false;
                return responseModel;
            }

        }

        public ResponseModel removeFromCart(Guid itemId, Guid cricketerId)
        {
            try
                
            {
               
                Bat bat = appDbContext.Bat.Find(itemId);
               Cart cart = appDbContext.Cart.FirstOrDefault(c=>c.cricketerId == cricketerId);
                CartItems item = appDbContext.CartItems.FirstOrDefault(c => c.itemId == itemId && c.cartId==cart.cartId);
                if (item == null)
                {
                    responseModel.Data = "item is  not in cart";
                    return responseModel;
                }
                appDbContext.CartItems.Remove(item);
                cart.totalAmount -= bat.sellingPrice;
                appDbContext.SaveChanges();
               
                responseModel.Message = "item removed from cart";
                return responseModel;
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                responseModel.Success = false;
                return responseModel;
            }

        }
        


        public ResponseModel search(string value)
        {

            List<Bat> bats = appDbContext.Bat.FromSqlRaw("dbo.spr_Search @parm={0}", value).ToList();

           if(bats.Count == 0)
            {
                responseModel.Success = false;
                responseModel.Message = "no result found";
                return responseModel;

            }
           responseModel.Data = bats;

            return responseModel;

        }


        public ResponseModel BatRecomandation(float height)
        {

            List<Bat> bats = appDbContext.Bat.FromSqlRaw("dbo. spr_BatRecomandation @parm={0}", height).ToList();

            if (bats.Count == 0)
            {
                responseModel.Success = false;
                responseModel.Message = "no result found";
                return responseModel;

            }
            responseModel.Data = bats;

            return responseModel;

        }

        public ResponseModel ItemsInCats(Guid cartId)
        {
            var items = appDbContext.Cart.Include(p => p.CartItems).FirstOrDefault(c=>c.cartId==cartId);
            //var items = (from cart in appDbContext.Set<Cart>() join cartItem in appDbContext.Set<CartItems>() on cart.cartId equals cartItem.cartId where (cart.customerId == customerId) select new { cartItem.amount,cartItem.cartId,cartItem.quantity,cartItem.updatedOn,cartItem.createdOn });
            if(items==null)
            {
                responseModel.Success = false;
                responseModel.Message = "no item in cart";
                return responseModel;

            }
            responseModel.Data = items;
            responseModel.Message = "succesfull";

            return responseModel;
        }

        public ResponseModel clearCart(Guid cartId)
        {
            Cart cart = appDbContext.Cart.Find(cartId);
            try
            {
                if (cart != null)
                {
               
                       var items=   appDbContext.CartItems.Where(x => x.cartId == cartId);
                    responseModel.Data = items;


                      appDbContext.CartItems.RemoveRange(items);
                    cart.totalAmount = 0;
                    appDbContext.SaveChanges();
                    responseModel.Message = "items removed";

                }
            }
            catch (Exception e)
            {
                responseModel.Success = false;
                responseModel.Message = "error";

            }
            

            return responseModel;     
                    }

       
        
    }
            

}



