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
        public ResponseModel create(Ussers usser)
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
        public IActionResult editProfile(Guid usserId, Ussers usser)
        {
            var user = _iUsserRepository.editProfile(usserId,usser);
            return Ok(user);

        }
        [HttpGet("view_profile")]
        public IActionResult viewProfile(Guid usserId)
        {
            var usser=_iUsserRepository.viewProfile(usserId);
            return Ok(usser);



        }

    }
}
