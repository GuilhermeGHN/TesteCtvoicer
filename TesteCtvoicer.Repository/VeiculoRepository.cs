using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TesteCtvoicer.Data;
using TesteCtvoicer.Entities;
using TesteCtvoicer.Repositories.Filtros;

namespace TesteCtvoicer.Repositories
{
	public class VeiculoRepository : IVeiculoRepository
	{
		private readonly FrotaContext _frotaContext;

		public VeiculoRepository(FrotaContext frotaContext)
		{
			_frotaContext = frotaContext;
		}

		public void Alterar(Veiculo veiculo)
		{
			_frotaContext.VeiculoSet.Update(veiculo);
			_frotaContext.SaveChanges();
		}

		public void Excluir(Veiculo veiculo)
		{
			_frotaContext.VeiculoSet.Remove(veiculo);
			_frotaContext.SaveChanges();
		}

		public bool ExisteChassi(string chassi)
		{
			return _frotaContext.VeiculoSet.Any(w => string.Equals(w.Chassi, chassi, StringComparison.InvariantCultureIgnoreCase));
		}

		public Veiculo Incluir(Veiculo veiculo)
		{
			_frotaContext.VeiculoSet.Add(veiculo);
			_frotaContext.SaveChanges();

			return veiculo;
		}

		public List<Veiculo> Listar(VeiculoFiltro veiculoFiltro)
		{
			var predicado = ConverterFiltroParaPredicado(veiculoFiltro);

			return _frotaContext.VeiculoSet
				.Where(predicado)
				.ToList();
		}

		public Veiculo Obter(int id)
		{
			return _frotaContext.VeiculoSet.Find(id);
		}

		private Expression<Func<Veiculo, bool>> ConverterFiltroParaPredicado(VeiculoFiltro veiculoFiltro)
		{
			Expression<Func<Veiculo, bool>> predicado = _ => true;

			if (!string.IsNullOrWhiteSpace(veiculoFiltro.Chassi))
				predicado = predicado.And(a => a.Chassi.Contains(veiculoFiltro.Chassi));

			return predicado.Expand();
		}
	}
}
