using System.Collections.Generic;
using TesteCtvoicer.Entities;
using TesteCtvoicer.Repositories.Filtros;

namespace TesteCtvoicer.Repositories
{
	public interface IVeiculoRepository
	{
		void Alterar(Veiculo veiculo);

		void Excluir(Veiculo veiculo);

		bool ExisteChassi(string chassi);

		Veiculo Incluir(Veiculo veiculo);

		List<Veiculo> Listar(VeiculoFiltro veiculoFiltro);

		Veiculo Obter(int id);
	}
}
