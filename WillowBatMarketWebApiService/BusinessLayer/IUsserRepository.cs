using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using WillowBatMarketWebApiService.DataLayer;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.BusinessLayer
{
    public interface IUsserRepository
    {
        public ResponseModel createUsser(UsserModel usser);
        public ResponseModel login(LoginRequest loginRequest);
        public ResponseModel viewProfile(Guid usserId);
        public ResponseModel editProfile(Guid id, UsserModel usserModel);
        public ResponseModel resetPassword(ResetPassword resetPassword);
    }
    public class UsserRepository : IUsserRepository
    {
        Manufacturer manufacturer;
        WillowSeller willowSeller;
        Cricketer cricketer;
        AppDbContext _appDbContext;
        ResponseModel responseModel;
        private readonly IMapper _mapper;
        public UsserRepository(AppDbContext appDbContext, IMapper mapper)
        {
            manufacturer = new Manufacturer();
            willowSeller = new WillowSeller();
            cricketer = new Cricketer();
            this._appDbContext = appDbContext;
            responseModel = new ResponseModel();
            _mapper = mapper;
        }
        public ResponseModel createUsser(UsserModel usser)

        {
            Ussers ussers = new Ussers();
           
          
            _mapper.Map(usser, ussers);
            // type.usserTypeid = Guid.NewGuid();
            //usser.UsserTypeId = type.usserTypeid;
            //_appDbContext.UsserType.Add(type);
            ussers.usserId = Guid.NewGuid();
            ussers.createdOn = DateTime.Now;
            ussers.updatedOn = DateTime.Now;
            ussers.encriptedPassword = usser.password;
            ussers.userType=usser.usserType;
            try
            {



                if (usser.usserType == EntityType.MANUFACTURER)
                {
                    manufacturer.manufacturerId = Guid.NewGuid();
                    manufacturer.usserId = ussers.usserId;
                    _appDbContext.Manufacturer.Add(manufacturer);
                    responseModel.Data = manufacturer;

                }
                else if (usser.usserType == EntityType.CRICKETER)
                {
                    Cart cart = new Cart();
                   cart.cartId= Guid.NewGuid();
                    cricketer.cricketerId =  Guid.NewGuid();
                    cart.cricketerId = cricketer.cricketerId;
                    cricketer.usserId = ussers.usserId;
                    _appDbContext.Cricketer.Add(cricketer);
                    _appDbContext.Cart.Add(cart);
                    responseModel.Data = cricketer;

                }
                else
                {

                    willowSeller.willowSellerId = new Guid();
                    willowSeller.usserId = ussers.usserId;
                    _appDbContext.WillowSeller.Add(willowSeller);
                    responseModel.Data = willowSeller;


                }
                _appDbContext.Ussers.Add(ussers);
                _appDbContext.SaveChanges();
                responseModel.Message = "your have successfully registered";
                return responseModel;

            }
            catch (Exception e)
            {
                responseModel.Error = e.InnerException.ToString();
                responseModel.Success = false;
                responseModel.Status = 404;
                responseModel.Data = null;
                responseModel.Message = "failed to insert";
                return responseModel;
            }








        }

        public ResponseModel editProfile(Guid usserId, UsserModel usserModel)
        {
            var user=getUsser(usserId);

            try
            {
              _mapper.Map(usserModel,user);    
              
                user.updatedOn = DateTime.Now;
                _appDbContext.Update(user);
                _appDbContext.SaveChanges();
                responseModel.Message = "your details is succesfully edited";

                responseModel.Data = user.usserId;
            }
            catch (Exception e)
            {
                responseModel.Success = false;
                responseModel.Message = e.Message;

            }


            return responseModel;

        }

        public ResponseModel login(LoginRequest loginRequest)
        {

            Ussers usser = null;

            usser = _appDbContext.Ussers.FirstOrDefault(x => x.email == loginRequest.Username);


            if (usser != null && !usser.encriptedPassword.Equals(loginRequest.Password))

            {
                usser = null;
                responseModel.Success = false;
                responseModel.Message = "Password or UserName is incorrect";
                responseModel.Error = "Password or UserName is incorrect";
                return responseModel;

            }




            if (usser == null)

            { usser = null;
                responseModel.Success = false;
                responseModel.Message = "Password or UserName is incorrect";
                responseModel.Error = "Password or UserName is incorrect";


                return responseModel;


            }
            responseModel.Message = "login sucessful";



            if (usser.userType.Equals(EntityType.MANUFACTURER))
            {


                var querry = from ussers in _appDbContext.Set<Ussers>()
                             join manufacturer in _appDbContext.Set<Manufacturer>() on ussers.usserId equals manufacturer.usserId
                             where (loginRequest.Username == ussers.email)
                             select new
                             {
                                 usser.userType,
                                 usser.name,
                                 usser.email,
                                 usser.addressDetails,
                                 usser.usserId,
                                 manufacturer.manufacturerId

                             };

                responseModel.Data = querry;

            }
            else if (usser.userType.Equals(EntityType.WILLOWSELLER))
            {


                var querry = from ussers in _appDbContext.Set<Ussers>()
                             join willowSeller in _appDbContext.Set<WillowSeller>() on ussers.usserId equals willowSeller.usserId
                             where (loginRequest.Username == ussers.email)
                             select new
                             {
                                 usser.userType,
                                 usser.name,
                                 usser.email,
                                 usser.addressDetails,
                                 usser.usserId,
                                 willowSeller.willowSellerId

                             };

                responseModel.Data = querry;

            }
            else
            {



                var querry = (from ussers in _appDbContext.Set<Ussers>()
                             join cricketer in _appDbContext.Set<Cricketer>()  on ussers.usserId equals cricketer.usserId join cart in _appDbContext.Set<Cart>() on cricketer.cricketerId equals cart. cricketerId where (loginRequest.Username == ussers.email)
                             select new
                             {
                                 usser.userType,
                                 usser.name,
                                 usser.email,
                                 usser.addressDetails,
                                 usser.usserId,
                                 cricketer.cricketerId,
                                cart.cartId
                                 

                             });

                responseModel.Data = querry;

            }

            return responseModel;

        }

        public ResponseModel viewProfile(Guid usserId)
        {

            Ussers usser = _appDbContext.Ussers.FirstOrDefault(u => u.usserId == usserId);
            if (usser == null)
            {
                responseModel.Success = false;
                responseModel.Message = "user does not exist";
                return responseModel;


            }
            responseModel.Success = true;
            responseModel.Data = usser;
            responseModel.Message = "successfully fetched data";
            return responseModel;






        }

        private Ussers getUsser(Guid usserId)
        {
            Ussers usser = _appDbContext.Ussers.Find(usserId);
            if(usser == null)
            {
                throw new KeyNotFoundException("usser not found");
              
            }
            return usser;
   

    }

        public ResponseModel resetPassword(ResetPassword resetPassword)
        {

            Ussers user = getUsser((Guid)resetPassword.UserId);
            if (!user.encriptedPassword.Equals(resetPassword.OldPassword)) 
            {
                responseModel.Success = false;
                responseModel.Message = "old password is wrong";
                return responseModel;
            }
           
            try
            {
                user.encriptedPassword = resetPassword.NewPassword;
                _appDbContext.Update(user);
                _appDbContext.SaveChanges();
                responseModel.Data = user.usserId;
                responseModel.Message = "Password Changed Successfully";

            }
            catch(Exception e)
            {
                responseModel.Message = "error";
                responseModel.Success = false;


            }
            
                return responseModel;

            

        }
    }

}