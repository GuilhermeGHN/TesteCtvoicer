using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TesteCtvoicer.Data.DbConfig
{
	public abstract class DbConfigBase<TEntity> :
		IEntityTypeConfiguration<TEntity>
		where TEntity : class
	{
		protected EntityTypeBuilder<TEntity> _builder;
		private int _ordem;

		protected const string BIGINT = "bigint";
		protected const string BIT = "bit";
		protected const string BYTE = "byte";
		protected const string CHAR = "char";
		protected const string DATE = "date";
		protected const string DATETIME = "datetime";
		protected const string DECIMAL = "decimal";
		protected const string IMAGE = "image";
		protected const string INT = "int";
		protected const string NUMERIC = "numeric";
		protected const string NVARCHAR = "nvarchar";
		protected const string NVARCHAR_MAX = "nvarchar(max)";
		protected const string SMALLINT = "smallint";
		protected const string TEXT = "text";
		protected const string TIME = "time";
		protected const string TINYINT = "tinyint";
		protected const string UNIQUEIDENTIFIER = "uniqueidentifier";
		protected const string VARBINARY = "varbinary";
		protected const string VARBINARY_MAX = "varbinary(max)";
		protected const string VARCHAR = "varchar";
		protected const string VARCHAR_MAX = "varchar(max)";

		public virtual void ConfigurarChavePrimaria()
		{
		}

		public abstract void ConfigurarPropridades();

		public virtual void ConfigurarRelacionamentos()
		{
		}

		public void Configure(EntityTypeBuilder<TEntity> builder)
		{
			_builder = builder;
			_builder.ToTable(NomeTabela);

			ConfigurarChavePrimaria();
			ConfigurarPropridades();
			ConfigurarRelacionamentos();
		}

		public int ObterOrdem() => _ordem++;

		public abstract string NomeTabela { get; }
	}
}