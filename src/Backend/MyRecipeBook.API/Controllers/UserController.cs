using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponsesRegisterUserJson), StatusCodes.Status201Created)]
        public IActionResult Register(
            [FromServices] IRegisterUserUseCase useCase,
            [FromBody] RequestRegisterUserJson request)
        {
            try
            {
                var result = useCase.Execute(request);

                return Created(string.Empty, result);   
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
