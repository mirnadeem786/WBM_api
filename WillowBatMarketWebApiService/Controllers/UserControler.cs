using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WillowBatMarketWebApiService.BusinessLayer;
using WillowBatMarketWebApiService.Entity;
using WillowBatMarketWebApiService.Models;

namespace WillowBatMarketWebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserControler : ControllerBase
    {
        private readonly IUsserRepository _iUsserRepository;

        public UserControler(IUsserRepository iUsserRepository)
        {
            _iUsserRepository = iUsserRepository;
        }

        [HttpPost("create_usser")]
        public ResponseModel create(UsserModel usser)
        {

            return _iUsserRepository.createUsser(usser);


        }
        [HttpPost("login")]
        public IActionResult login(LoginRequest loginRequest)
        {
            var res = _iUsserRepository.login(loginRequest);

            return Ok(res);

        }

        [HttpPut("edit_usser")]
        public IActionResult editProfile(Guid userId, UsserModel user)
        {
            var usser = _iUsserRepository.editProfile(userId,user);
            return Ok(usser);

        }
        [HttpGet("view_profile")]
        public IActionResult viewProfile(Guid userId)
        {
            var usser=_iUsserRepository.viewProfile(userId);
            return Ok(usser);



        }
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(ResetPassword obj)
        {
            var res =_iUsserRepository.resetPassword(obj);
            return Ok(res);
        }
    }




}
