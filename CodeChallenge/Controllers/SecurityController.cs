using CodeChallenge.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class SecurityController
    {
        private JwtService _jwtService;


        public SecurityController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetToken")]
        public IResult GetToken()
        {
            try
            {
                var user = new IdentityUser("Test");

               return  _jwtService.GenerateJwtToken(user);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
