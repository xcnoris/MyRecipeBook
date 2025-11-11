using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.AutoMapper.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System.Runtime.CompilerServices;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public async Task<ResponsesRegisterUserJson> Execute(RequestRegisterUserJson request)
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
            await _userWriteOnlyRepository.Add(user);


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
