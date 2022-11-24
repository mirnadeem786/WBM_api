using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public ResponseModel addTocart(Guid itemId, Guid customerId, short quantity);
        public ResponseModel removeFromCart(Guid itemId, Guid customerId);
        public ResponseModel placeOrder(Guid customerId);
        //      object o,
        public ResponseModel buyNow(Guid itemId, Guid customerId,short quantity);
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
        public ResponseModel addTocart(Guid itemId, Guid customerId, short quantity)
        {

            var bat = appDbContext.Bat.FirstOrDefault(b => b.batId == itemId);

            var cartitem = appDbContext.Cart.FirstOrDefault(c => c.itemId == itemId && c.customerId == customerId);
            if (cartitem != null)
            {
                cartitem.quantity = Convert.ToInt16(cartitem.quantity + quantity);
                cartitem.amount = cartitem.amount + bat.sellingPrice;

            }



            else
            {
                //{
                // Bat bat = (Bat)o;

                Cart cart = new Cart
                {
                    cartId = Guid.NewGuid(),
                    createdOn = DateTime.Now,
                    customerId = customerId,
                    itemId = itemId,
                    itemType = EntityType.BAT,
                    //orderId = Guid.NewGuid(),
                    //orderDate = DateTime.Now,
                    amount = bat.sellingPrice * quantity,
                    quantity = quantity,
                    updatedOn = DateTime.Now,



                };


                appDbContext.Cart.Add(cart);
                responseModel.Data = cart;
            }

            //}
            //responseModel.Message = "only bat is added to cart";
            try
            {

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

            return responseModel;
        }


        // object o,
        public ResponseModel buyNow(Guid itemId, Guid customerId,short quantity)
        {
            // Bat bat = new Bat();
            //if (o.GetType().Equals(bat))
            //{
            var stock = appDbContext.Bat.Where(b => b.batId == itemId).Select(bat => bat.batStock).FirstOrDefault();
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
            item.batStock--;

            OrderItems order = new OrderItems()
            {


                orderId = Guid.NewGuid(),
                customerId = customerId,
                createdOn = DateTime.Now,                      //b.createdOn,
                updatedOn = DateTime.Now,
                itemId = itemId,
                itemType = EntityType.BAT,
                quantity = 1,
                amount = item.sellingPrice,                        //b.batPrice,
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
            responseModel.Data = record.status;
            responseModel.Success = true;
            return responseModel;

        }

        public ResponseModel placeOrder(Guid customerId)
        {
            try
            {

                List<Cart> items = appDbContext.Cart.Where(i => i.customerId == customerId).ToList();
                foreach (var item in items)
                {

                    OrderItems order = new OrderItems()
                    {
                        orderId = Guid.NewGuid(),
                        customerId = item.customerId,
                        orderDate = DateTime.Now,
                        createdOn = item.createdOn,
                        updatedOn = item.updatedOn,
                        itemId = item.itemId,
                        itemType = item.itemType,
                        quantity = item.quantity,
                        amount = item.amount,

                    };
                    appDbContext.OrderItems.Add(order);
                    appDbContext.SaveChanges();
                    removeFromCart(item.itemId, customerId);

                }

                responseModel.Success = true;
                responseModel.Message = "order has been palced";
                responseModel.Data = customerId;
                return responseModel;
            }
            catch (Exception ex)
            {

                responseModel.Message = ex.InnerException.ToString();
                responseModel.Success = false;
                return responseModel;
            }

        }

        public ResponseModel removeFromCart(Guid itemId, Guid customerId)
        {
            try
            {
                Cart item = appDbContext.Cart.FirstOrDefault(c => c.itemId == itemId && c.customerId == customerId);
                if (item == null)
                {
                    responseModel.Data = "item is  not in cart";
                    return responseModel;
                }
                appDbContext.Cart.Remove(item);
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


    }


}



