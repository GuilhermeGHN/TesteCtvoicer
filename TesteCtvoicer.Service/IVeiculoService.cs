using System.Collections.Generic;
using TesteCtvoicer.Entities;
using TesteCtvoicer.Repositories.Filtros;
using TesteCtvoicer.Services.Infra;

namespace TesteCtvoicer.Services
{
	public interface IVeiculoService
	{
		RetornoOperacao Alterar(Veiculo veiculo);

		RetornoOperacao Excluir(int id);

		List<Veiculo> Listar(VeiculoFiltro veiculoFiltro);

		RetornoOperacao Incluir(Veiculo veiculo);

		Veiculo Obter(int id);
	}
}
