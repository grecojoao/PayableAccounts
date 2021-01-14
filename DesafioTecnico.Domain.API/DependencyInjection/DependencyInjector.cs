using System.Reflection;
using System.Threading.Tasks;
using DesafioTecnico.Domain.Infra.Storage.Data;
using DesafioTecnico.Domain.Infra.Storage.Repositories;
using DesafioTecnico.Domain.Infra.Storage.Repositories.Contracts;
using DesafioTecnico.Domain.Infra.Storage.Transactions;
using DesafioTecnico.Domain.Infra.Storage.Transactions.Contracts;
using DesafioTecnico.Domain.Services;
using DesafioTecnico.Domain.Services.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioTecnico.Domain.API.DependencyInjection
{
    public static class DependencyInjector
    {
        public static Task InjectDependencies(this IServiceCollection services)
        {
            InjectDomain(services);
            InjectInfrastructure(services);
            return Task.CompletedTask;
        }

        private static Task InjectDomain(IServiceCollection services)
        {
            services.AddMediatR(Assembly.Load("DesafioTecnico.Domain"));
            services.AddTransient<IPayableAccountService, PayableAccountService>();
            return Task.CompletedTask;
        }

        private static Task InjectInfrastructure(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("database"));
            services.AddTransient<IPayableAccountRepository, PayableAccountEFRepository>();
            services.AddTransient<IPaidAccountRepository, PaidAccountEFRepository>();
            services.AddTransient<IPaymentRuleRepository, PaymentRuleEFRepository>();
            services.AddTransient<IUnitOfWork, EFUnitOfWork>();
            return Task.CompletedTask;
        }
    }
}