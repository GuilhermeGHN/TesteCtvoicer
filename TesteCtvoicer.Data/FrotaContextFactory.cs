using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TesteCtvoicer.Data
{
	public class FrotaContextFactory : IDesignTimeDbContextFactory<FrotaContext>
	{
		public FrotaContext CreateDbContext(string[] args)
		{
			var options = GetDbContextOptions();
			return new FrotaContext(options);
		}

		public static DbContextOptions GetDbContextOptions()
		{
			var builder = new DbContextOptionsBuilder<FrotaContext>();
			builder.UseInMemoryDatabase(databaseName: "Frota");
			return builder.Options;
		}
	}
}
