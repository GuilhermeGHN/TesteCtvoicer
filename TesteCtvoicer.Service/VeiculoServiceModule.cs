using Autofac;

namespace TesteCtvoicer.Services
{
	public class VeiculoServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<VeiculoService>()
				.As<IVeiculoService>()
				.InstancePerLifetimeScope();
		}
	}
}
