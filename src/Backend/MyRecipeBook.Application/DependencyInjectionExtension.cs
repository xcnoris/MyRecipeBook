using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.AutoMapper.Cryptography;
using MyRecipeBook.Application.UseCases.User.Register;

namespace MyRecipeBook.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddAplication(this IServiceCollection service)
        {
            AddAutoMapper(service);
            AddUseCases(service);
            AddPasswordEncrypted(service);
        }

        private static void AddAutoMapper(IServiceCollection service)
        {
            service.AddScoped(options => new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper());
        }

        private static void AddUseCases(IServiceCollection service)
        {
            service.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();

        }
        private static void AddPasswordEncrypted(IServiceCollection service)
        {
            service.AddScoped(options => new PasswordCripter());

        }
    }
}
