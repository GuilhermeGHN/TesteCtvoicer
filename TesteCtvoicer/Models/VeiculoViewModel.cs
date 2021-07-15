using System.ComponentModel.DataAnnotations;
using TesteCtvoicer.Entities.Enums;

namespace TesteCtvoicer.Models
{
	public class VeiculoViewModel
	{
		public int Id { get; set; }

		[Display(Name = "Chassi")]
		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		public string Chassi { get; set; }

		[Display(Name = "Tipo")]
		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		public TipoVeiculoEnum? Tipo { get; set; }

		public string TipoDescricao
		{
			get
			{
				return Tipo switch
				{
					TipoVeiculoEnum.Onibus => "Ônibus",
					TipoVeiculoEnum.Caminhao => "Caminhão",
					_ => string.Empty,
				};
			}
		}

		[Display(Name = "Nº de Passageiros")]

		public byte? NumeroPassageiros { get; set; }

		[Display(Name = "Cor")]
		[Required(ErrorMessage = "O campo {0} é obrigatório.")]
		public string Cor { get; set; }
	}
}
