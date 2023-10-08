using ContactBookAPI.Core.Interfaces;
using ContactBookAPI.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ContactBookAPI.Controllers.AUTH
{
    [Route("api/[controller]")]
    [ApiController]
    public class AUTHController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AUTHController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model, string role)
        {

            var registerResult = await _authRepository.RegisterUserAsync(model, ModelState, role);

            if (!registerResult)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(new
                {
                    Message = "user registration successful"
                });
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _authRepository.LoginAsync(model);
            if (token == null)
            {
                return Unauthorized(new
                {
                    Message = "Invalid Credentials"
                });
            }
            return Ok(new
            {
                Token = token
            });
        }
    }
}
