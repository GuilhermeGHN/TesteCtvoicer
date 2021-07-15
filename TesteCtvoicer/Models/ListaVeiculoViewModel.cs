using System.Collections.Generic;

namespace TesteCtvoicer.Models
{
	public class ListaVeiculoViewModel
	{
		public string Chassi { get; set; }

		public List<VeiculoViewModel> VeiculoSet { get; set; } = new List<VeiculoViewModel>();
	}
}
