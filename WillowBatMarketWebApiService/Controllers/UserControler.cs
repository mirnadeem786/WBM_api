using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
