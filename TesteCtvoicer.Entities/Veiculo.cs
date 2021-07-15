using TesteCtvoicer.Entities.Enums;

namespace TesteCtvoicer.Entities
{
	public class Veiculo
	{
		public int Id { get; set; }

		public string Chassi { get; set; }

		public TipoVeiculoEnum Tipo { get; set; }

		public byte NumeroPassageiros { get; set; }

		public string Cor { get; set; }
	}
}
