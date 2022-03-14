using Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace PolicyBasedAuthanticationNet6.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IJwtSetting _jwtSetting;
    public UserController(IJwtSetting jwtSetting)
    {
        _jwtSetting = jwtSetting;
    }

    [AllowAnonymous]
    [HttpPost]
    public ActionResult<ResponseToken> Login(Login model)
    {
        var user = Users.GetUsers().FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
        if (user == null)
        {
            return BadRequest("Invalid email or password");
        }

        ResponseToken responseToken = JwtComputeService.Compute(_jwtSetting, user);
        return Ok(responseToken);
    }


    [Authorize(Policy = "Account")]
    [Authorize(Policy = "Admin")]
    [HttpGet]
    public ActionResult<IEnumerable<User>> Get()
    {
        return Ok(Users.GetUsers());
    }

}