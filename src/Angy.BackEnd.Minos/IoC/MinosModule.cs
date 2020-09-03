using Autofac;
using Microsoft.Extensions.Configuration;

namespace Angy.BackEnd.Minos.IoC
{
    public class MinosModule : Module
    {
        readonly IConfiguration _configuration;

        public MinosModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
        }
    }
}