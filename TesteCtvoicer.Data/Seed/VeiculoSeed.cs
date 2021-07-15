using System.Linq;
using TesteCtvoicer.Entities;
using TesteCtvoicer.Entities.Enums;

namespace TesteCtvoicer.Data.Seed
{
	internal sealed class VeiculoSeed : SeedBase
	{
		public override void Executar(FrotaContext frotaContext)
		{
			Incluir(frotaContext, "6A2XkKA8AACFn5793", TipoVeiculoEnum.Caminhao, 2, "Dourada");
			Incluir(frotaContext, "20TA3n6vuWDAj2419", TipoVeiculoEnum.Onibus, 42, "Verde");
			Incluir(frotaContext, "8MMYTAAHA75Bt9911", TipoVeiculoEnum.Caminhao, 2, "Azul");
			Incluir(frotaContext, "70WB0H2HxhXRX8513", TipoVeiculoEnum.Onibus, 42, "Branca");
			Incluir(frotaContext, "5TX9B4MWAD3Ae5697", TipoVeiculoEnum.Caminhao, 2, "Pratra");
			Incluir(frotaContext, "3Y7DgNgAFV1FA4030", TipoVeiculoEnum.Onibus, 42, "Cinza");
			Incluir(frotaContext, "16Atx82AbSdwa8399", TipoVeiculoEnum.Caminhao, 2, "Vermelho");
			Incluir(frotaContext, "4B6PEAy634GlD9708", TipoVeiculoEnum.Onibus, 42, "Preta");
			Incluir(frotaContext, "7jmV49M37hH3w5322", TipoVeiculoEnum.Caminhao, 2, "Azul");
			Incluir(frotaContext, "3vjh2a4ufw6T75880", TipoVeiculoEnum.Onibus, 42, "Verde");
			Incluir(frotaContext, "46T2vs0DT2jYp5896", TipoVeiculoEnum.Caminhao, 2, "Azul");
			Incluir(frotaContext, "28Ama7f26h65z6196", TipoVeiculoEnum.Onibus, 42, "Laranja");
			Incluir(frotaContext, "2EA3t40N0A1091418", TipoVeiculoEnum.Caminhao, 2, "Amarelo");
			Incluir(frotaContext, "4Ax1vHLxd3vAT7067", TipoVeiculoEnum.Onibus, 42, "Preta");
			Incluir(frotaContext, "8x95ja0gMbSxb3971", TipoVeiculoEnum.Caminhao, 2, "Prata");

			frotaContext.SaveChanges();
		}

		private void Incluir(FrotaContext frotaContext, string chassi, TipoVeiculoEnum tipo, byte numeroPassageiros, string cor)
		{
			if (frotaContext.VeiculoSet.Any(a => a.Chassi == chassi))
				return;

			frotaContext.VeiculoSet.Add(new Veiculo
			{
				Chassi = chassi,
				Tipo = tipo,
				NumeroPassageiros = numeroPassageiros,
				Cor = cor
			});
		}
	}
}
