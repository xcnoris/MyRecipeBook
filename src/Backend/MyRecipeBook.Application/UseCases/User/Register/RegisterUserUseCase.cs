using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.AutoMapper.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        public ResponsesRegisterUserJson Execute(RequestRegisterUserJson request)
        {
            var cryprograpy = new PasswordCripter();
            var autoMapper = new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper();



            //mapear a request em uma entidade
            Validate(request);
            var user = autoMapper.Map<Domain.Entities.User>(request);

            // criptrografa da senha

            user.Password = cryprograpy.Emcrypt(request.Password);

            //Salvar no banco de dados

            return new ResponsesRegisterUserJson
            {
                Name = request.Name,
            };
        }

        private void Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
