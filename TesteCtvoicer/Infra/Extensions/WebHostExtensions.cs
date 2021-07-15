using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TesteCtvoicer.Data;
using TesteCtvoicer.Data.Seed;

namespace TesteCtvoicer.Infra.Extensions
{
	public static class WebHostExtensions
	{
		public static IHost ExecutarSeeds(this IHost host)
		{
			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var context = services.GetService<FrotaContext>();

				var seedSet = ListarSeeds();
				OrdenarPorDependencia(seedSet);

				foreach (var seed in seedSet)
					seed.Executar(context);
			}

			return host;
		}

		private static List<SeedBase> ListarSeeds()
		{
			var seedAssemblySet = Assembly
				.GetAssembly(typeof(SeedBase))
				.GetTypes()
				.Where(t => t.IsClass
					&& !t.IsAbstract
					&& t.IsSealed
					&& t.IsSubclassOf(typeof(SeedBase)))
				.ToList();

			var seedSet = new List<SeedBase>();

			foreach (var seedAssembly in seedAssemblySet)
			{
				var seed = (SeedBase)Activator.CreateInstance(seedAssembly);
				seedSet.Add(seed);
			}

			return seedSet;
		}

		private static void OrdenarPorDependencia(List<SeedBase> seedSet)
		{
			seedSet.Sort((a, b) =>
			{
				var dependenciaASet = a.ListarDependencias();
				var dependenciaBSet = b.ListarDependencias();

				if (dependenciaBSet == null && dependenciaASet == null)
					return 0;

				if (dependenciaASet?.Contains(b.GetType()) == true)
					return 1;

				if (dependenciaBSet?.Contains(a.GetType()) == true)
					return -1;

				if (dependenciaBSet != null && dependenciaASet != null)
					return dependenciaASet.Count - dependenciaBSet.Count;

				if (dependenciaASet != null && dependenciaBSet == null)
					return 1;

				if (dependenciaASet == null && dependenciaBSet != null)
					return -1;

				return 0;
			});
		}
	}
}
