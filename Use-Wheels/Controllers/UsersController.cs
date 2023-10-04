using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Use_Wheels.Controllers
{
    [ApiController]
    [Route("/")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        protected APIResponseDTO _response;

        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
            _response = new();
        }

        /// <summary>
        /// Method to log a user in
        /// </summary>
        /// <param name="LoginRequestDTO"></param>
        /// <returns>APIResponse object</returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _userRepo.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
                throw new BadHttpRequestException(Constants.LoginConstants.INVALID_CREDENTIALS, Constants.ResponseConstants.BAD_REQUEST);
            _response.Result = loginResponse;
            return Ok(_response);
        }

        /// <summary>
        /// Method to register a customer
        /// </summary>
        /// <param name="RegisterationRequestDTO"><see cref="RegisterationRequestDTO"/></param>
        /// <returns>APIResponse object</returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO model)
        { 
            var user = await _userRepo.Register(model);
            if (user == null)
                throw new BadHttpRequestException(Constants.RegisterConstants.INVALID_AGE, Constants.ResponseConstants.BAD_REQUEST);

            _response.Result = Constants.RegisterConstants.USER_REGISTERED_SUCCESSFULLY;
            return Ok(_response);
        }

        /// <summary>
        /// Method to unblack a seller by admin role
        /// </summary>
        /// <returns>APIResponse object</returns>
        [HttpPut("/unblack-seller")]
        [Authorize(Roles = Constants.Roles.ADMIN)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponseDTO>> UnblackSeller(string username)
        {
            int result = await _userRepo.UnblackSeller(username);

            if(result == -1)
                throw new BadHttpRequestException(Constants.UnblackSeller.USERNAME_NOT_FOUND, Constants.ResponseConstants.BAD_REQUEST);
            else if(result == 0)
                throw new BadHttpRequestException(Constants.UnblackSeller.USER_NOT_BLACKED, Constants.ResponseConstants.BAD_REQUEST);
            return Ok();            
        }

        /// <summary>
        /// Method to log a user out
        /// </summary>
        /// <returns>APIResponse object</returns>
        [HttpPost("/logout")]
        [Authorize] // Ensures only authenticated users can log out
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponseDTO>> Logout()
        {
            string username = HttpContext.User.Identity.Name;
            LogoutUtility logoutUtility = new LogoutUtility();

            var jwtToken = HttpContext.Request.Headers.Authorization.FirstOrDefault().Split(" ")[1];
            await logoutUtility.InvalidateToken(jwtToken);

            string role = HttpContext.User.Identities.First().Claims.ElementAt(1).Value;
            if (role == "customer")
               logoutUtility.DeleteUserFromWishlist(username);

            _response.Result = Constants.LogoutConstants.LOGOUT_SUCCESSFUL;
            return Ok(_response);
        }
    }
}

