using Microsoft.EntityFrameworkCore;
using TesteCtvoicer.Entities;

namespace TesteCtvoicer.Data.DbConfig
{
	public class VeiculoDbConfig : DbConfigBase<Veiculo>
	{
		private const string TABELA = "veiculos";
		private const string ID_VEICULO = "veiculo_id";

		private const string CHASSI = "chassi";
		private const string TIPO = "tipo";
		private const string NUMERO_PASSAGEIROS = "numero_passageiros";
		private const string COR = "cor";

		public override void ConfigurarChavePrimaria() => _builder.HasKey(h => h.Id);

		public override void ConfigurarPropridades()
		{
			ConfigurarPropriedadeId();
			ConfigurarPropriedadeChassi();
			ConfigurarPropriedadeTipo();
			ConfigurarPropriedadeNumeroPassageiros();
			ConfigurarPropriedadeCor();
		}

		private void ConfigurarPropriedadeId()
		{
			_builder.Property(a => a.Id)
				.HasColumnName(ID_VEICULO)
				.HasColumnType(INT)
				.ValueGeneratedOnAdd();
		}

		private void ConfigurarPropriedadeChassi()
		{
			_builder.Property(a => a.Chassi)
				.HasColumnName(CHASSI)
				.HasColumnType($"{VARCHAR}(17)")
				.IsRequired();
		}

		protected void ConfigurarPropriedadeTipo()
		{
			_builder.Property(a => a.Tipo)
				.HasColumnName(TIPO)
				.HasColumnType(BYTE)
				.IsRequired();
		}

		protected void ConfigurarPropriedadeNumeroPassageiros()
		{
			_builder.Property(a => a.NumeroPassageiros)
				.HasColumnName(NUMERO_PASSAGEIROS)
				.HasColumnType(BYTE)
				.IsRequired();
		}

		private void ConfigurarPropriedadeCor()
		{
			_builder.Property(a => a.Cor)
				.HasColumnName(COR)
				.HasColumnType($"{VARCHAR}(30)")
				.IsRequired();
		}

		public override string NomeTabela => TABELA;
	}
}
