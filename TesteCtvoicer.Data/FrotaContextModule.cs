using Autofac;

namespace TesteCtvoicer.Data
{
	public class FrotaContextModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<FrotaContext>()
				.WithParameter("options", FrotaContextFactory.GetDbContextOptions())
				.AsSelf()
				.InstancePerLifetimeScope();
		}
	}
}
