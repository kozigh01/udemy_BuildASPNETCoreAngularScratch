using System.Security.Claims;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IAuthRepository _repository;

    public AuthController(IAuthRepository repository)
    {
      _repository = repository;
    }

    // [HttpPost("login")]
    // public async Task<IActionResult> Login(UserForLoginDto user)
    // {
    //   var userFromRepo = await _repository.Login(user.Username, user.Password);

    //   if(userFromRepo == null) {
    //     return Unauthorized();
    //   }

    //   var claims = new [] {
    //     new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
    //     new Claim(ClaimTypes.Name, userFromRepo.Username)
    //   };


    // }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserForRegisterDto user)
    {
      user.Username = user.Username.ToLower();

      if (await _repository.UserExists(user.Username))
      {
        return BadRequest("Username already exists");
      }

      var userToCreate = new User
      {
        Username = user.Username
      };

      var createdUser = await _repository.Register(userToCreate, user.Password);

      return StatusCode(201);
    }
  }
}