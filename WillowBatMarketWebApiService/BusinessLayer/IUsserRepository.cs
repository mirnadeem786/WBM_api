using Realms.Sync;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using WillowBatMarketWebApiService.DataLayer;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.BusinessLayer
{
    public interface IUsserRepository
    {
        public ResponseModel createUsser(Ussers usser);
        public ResponseModel login(LoginRequest loginRequest);
    }
    public class UsserRepository : IUsserRepository
    {
        Manufacturer manufacturer;
        WillowSeller willowSeller;
        Cricketer cricketer;
        AppDbContext _appDbContext;
        ResponseModel responseModel;
        public UsserRepository(AppDbContext appDbContext)
        {
            manufacturer = new Manufacturer();
            willowSeller = new WillowSeller();
            cricketer = new Cricketer();
            this._appDbContext = appDbContext;
            responseModel = new ResponseModel();

        }
        public ResponseModel createUsser(Ussers usser)

        {
            // type.usserTypeid = Guid.NewGuid();
            //usser.UsserTypeId = type.usserTypeid;
            //_appDbContext.UsserType.Add(type);
            usser.usserId = Guid.NewGuid();
            usser.createdOn = DateTime.Now;
            usser.updatedOn = DateTime.Now;

            try
            {



                if (usser.usserType == "Manufacturer")
                {
                    manufacturer.manufacturerId = new Guid();
                    manufacturer.usserId = usser.usserId;
                    _appDbContext.Manufacturer.Add(manufacturer);
                    responseModel.Data = manufacturer;

                }
                else if (usser.usserType == "Cricketer")
                {

                    cricketer.cricketerId = new Guid();
                    cricketer.usserId = usser.usserId;
                    _appDbContext.Cricketer.Add(cricketer);

                    responseModel.Data = cricketer;

                }
                else
                {

                    willowSeller.willowSellerId = new Guid();
                    willowSeller.usserId = usser.usserId;
                    _appDbContext.WillowSeller.Add(willowSeller);
                    responseModel.Data = willowSeller;


                }
                _appDbContext.Ussers.Add(usser);
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

        public ResponseModel login(LoginRequest loginRequest)
        {

            Ussers usser = null;
        
                usser = _appDbContext.Ussers.FirstOrDefault(x => x.email == loginRequest.Username);


                if (usser != null && !usser.password.Equals(loginRequest.Password))

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



            if (usser.usserType.Equals("Manufacturer"))
            {


                var querry = from ussers in _appDbContext.Set<Ussers>()
                             join manufacturer in _appDbContext.Set<Manufacturer>() on ussers.usserId equals manufacturer.usserId
                             where (loginRequest.Username == ussers.email)
                             select new
                             {
                                 usser.usserType,
                                 usser.name,
                                 usser.email,
                                 usser.addressDetails,
                                 usser.usserId,
                                 manufacturer.manufacturerId

                             };

                responseModel.Data = querry;
               
            }
            else if (usser.usserType.Equals("WillowSeller"))
            {


                var querry = from ussers in _appDbContext.Set<Ussers>()
                             join willowSeller in _appDbContext.Set<WillowSeller>() on ussers.usserId equals willowSeller.usserId
                             where (loginRequest.Username == ussers.email)
                             select new
                             {
                                 usser.usserType,
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


                var querry = from ussers in _appDbContext.Set<Ussers>()
                             join cricketer in _appDbContext.Set<Cricketer>() on ussers.usserId equals cricketer.usserId where(loginRequest.Username==ussers.email)
                             select new
                             {
                                 usser.usserType,
                                 usser.name,
                                 usser.email,
                                 usser.addressDetails,
                                 usser.usserId,
                                 cricketer.cricketerId

                             };

                responseModel.Data = querry;
               
            }

            return responseModel;

        }

    }
}
