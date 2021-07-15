using System.Collections.Generic;
using TesteCtvoicer.Entities;
using TesteCtvoicer.Entities.Enums;
using TesteCtvoicer.Repositories;
using TesteCtvoicer.Repositories.Filtros;
using TesteCtvoicer.Services.Infra;

namespace TesteCtvoicer.Services
{
	public class VeiculoService : IVeiculoService
	{
		private readonly IVeiculoRepository _veiculoRepository;

		private const int NUMERO_PASSAGEIROS_ONIBUS = 42;
		private const int NUMERO_PASSAGEIROS_CAMINHOES = 2;

		public VeiculoService(IVeiculoRepository veiculoRepository)
		{
			_veiculoRepository = veiculoRepository;
		}

		public RetornoOperacao Alterar(Veiculo veiculo)
		{
			var veiculoBase = _veiculoRepository.Obter(veiculo.Id);

			if (veiculoBase == null)
				return new RetornoOperacao { Mensagem = "O veículo não encontrado." };

			veiculoBase.Cor = veiculo.Cor;
			_veiculoRepository.Alterar(veiculoBase);

			return new RetornoOperacao { Sucesso = true };
		}

		public RetornoOperacao Excluir(int id)
		{
			var veiculo = _veiculoRepository.Obter(id);

			if (veiculo == null)
				return new RetornoOperacao { Mensagem = "O veículo não encontrado." };

			_veiculoRepository.Excluir(veiculo);
			return new RetornoOperacao { Sucesso = true };
		}

		public RetornoOperacao Incluir(Veiculo veiculo)
		{
			veiculo.NumeroPassageiros = ObterNumeroPassageirosPorTipo(veiculo.Tipo);

			if (_veiculoRepository.ExisteChassi(veiculo.Chassi))
				return new RetornoOperacao { Mensagem = "O Chassi informado já existe, o veículo não foi criado." };

			_veiculoRepository.Incluir(veiculo);

			return new RetornoOperacao { Sucesso = true };
		}

		public List<Veiculo> Listar(VeiculoFiltro veiculoFiltro)
		{
			return _veiculoRepository.Listar(veiculoFiltro);
		}

		public Veiculo Obter(int id)
		{
			return _veiculoRepository.Obter(id);
		}

		private byte ObterNumeroPassageirosPorTipo(TipoVeiculoEnum tipoVeiculoEnum)
		{
			return tipoVeiculoEnum switch
			{
				TipoVeiculoEnum.Onibus => NUMERO_PASSAGEIROS_ONIBUS,
				TipoVeiculoEnum.Caminhao => NUMERO_PASSAGEIROS_CAMINHOES,
				_ => 0,
			};
		}
	}
}
