using Microsoft.EntityFrameworkCore;
using TesteCtvoicer.Data.DbConfig;
using TesteCtvoicer.Entities;

namespace TesteCtvoicer.Data
{
	public class FrotaContext : DbContext
	{
		public FrotaContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new VeiculoDbConfig());
		}

		public DbSet<Veiculo> VeiculoSet { get; set; }
	}
}
