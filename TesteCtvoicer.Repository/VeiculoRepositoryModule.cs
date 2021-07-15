using Autofac;

namespace TesteCtvoicer.Repositories
{
	public class VeiculoRepositoryModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<VeiculoRepository>()
				.As<IVeiculoRepository>()
				.InstancePerLifetimeScope();
		}
	}
}
