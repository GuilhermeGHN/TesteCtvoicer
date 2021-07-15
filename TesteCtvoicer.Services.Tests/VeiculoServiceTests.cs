using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TesteCtvoicer.Entities;
using TesteCtvoicer.Entities.Enums;
using TesteCtvoicer.Repositories;
using TesteCtvoicer.Repositories.Filtros;

namespace TesteCtvoicer.Services.Tests
{
	[TestFixture]
	public class VeiculoServiceTests
	{
		private VeiculoService _veiculoService;

		private Mock<IVeiculoRepository> _veiculoRepositoryMock;

		[SetUp]
		public void SetUp()
		{
			_veiculoRepositoryMock = new Mock<IVeiculoRepository>();

			_veiculoService = new VeiculoService(_veiculoRepositoryMock.Object);
		}

		[TestCase]
		public void Alterar_VeiculoNaoEncontrado_MensagemErroEsperada()
		{
			//Arrange
			var veiculo = new Veiculo();

			_veiculoRepositoryMock
				.Setup(s => s.Obter(It.IsAny<int>()))
				.Returns<Veiculo>(null);

			//Act
			var retornoOperacao = _veiculoService.Alterar(veiculo);

			//Assert
			Assert.AreEqual(retornoOperacao.Mensagem, "O veículo não encontrado.");
		}

		[TestCase]
		public void Alterar_Veiculo_Sucesso()
		{
			//Arrange
			var veiculo = new Veiculo();

			_veiculoRepositoryMock
				.Setup(s => s.Obter(It.IsAny<int>()))
				.Returns(new Veiculo());

			_veiculoRepositoryMock.Setup(s => s.Alterar(It.IsAny<Veiculo>()));

			//Act
			var retornoOperacao = _veiculoService.Alterar(veiculo);

			//Assert
			Assert.IsTrue(retornoOperacao.Sucesso);
		}

		[TestCase]
		public void Alterar_ApenasCorVeiculo_Sucesso()
		{
			//Arrange
			var veiculo = new Veiculo
			{
				Chassi = "7jmV49M37hH3w5322",
				Cor = "Amarelo",
				Id = 999,
				NumeroPassageiros = 2,
				Tipo = TipoVeiculoEnum.Caminhao
			};

			var veiculoBase = new Veiculo
			{
				Chassi = "3vjh2a4ufw6T75880",
				Cor = "Verde",
				Id = 999,
				NumeroPassageiros = 42,
				Tipo = TipoVeiculoEnum.Onibus
			};

			Veiculo veiculoAlterado = null;

			_veiculoRepositoryMock
				.Setup(s => s.Obter(It.IsAny<int>()))
				.Returns(veiculoBase);

			_veiculoRepositoryMock
				.Setup(s => s.Alterar(It.IsAny<Veiculo>()))
				.Callback<Veiculo>(c => veiculoAlterado = c);

			//Act
			var retornoOperacao = _veiculoService.Alterar(veiculo);

			//Assert
			Assert.AreNotEqual(veiculo.Chassi, veiculoAlterado.Chassi);
			Assert.AreNotEqual(veiculo.NumeroPassageiros, veiculoAlterado.NumeroPassageiros);
			Assert.AreNotEqual(veiculo.Tipo, veiculoAlterado.Tipo);
			Assert.AreEqual(veiculo.Id, veiculoAlterado.Id);
			Assert.AreEqual(veiculo.Cor, veiculoAlterado.Cor);
		}

		[TestCase]
		public void Excluir_VeiculoNaoEncontrado_MensagemErroEsperada()
		{
			//Arrange
			const int veiculoId = 999;

			_veiculoRepositoryMock
				.Setup(s => s.Obter(It.IsAny<int>()))
				.Returns<Veiculo>(null);

			//Act
			var retornoOperacao = _veiculoService.Excluir(veiculoId);

			//Assert
			Assert.AreEqual(retornoOperacao.Mensagem, "O veículo não encontrado.");
		}

		[TestCase]
		public void Excluir_Veiculo_Sucesso()
		{
			//Arrange
			const int veiculoId = 999;

			_veiculoRepositoryMock
				.Setup(s => s.Obter(It.IsAny<int>()))
				.Returns(new Veiculo());

			_veiculoRepositoryMock.Setup(s => s.Excluir(It.IsAny<Veiculo>()));

			//Act
			var retornoOperacao = _veiculoService.Excluir(veiculoId);

			//Assert
			Assert.IsTrue(retornoOperacao.Sucesso);
		}

		[TestCase]
		public void Incluir_VeiculoExistente_MensagemErroEsperada()
		{
			//Arrange
			var veiculo = new Veiculo();

			_veiculoRepositoryMock
				.Setup(s => s.ExisteChassi(It.IsAny<string>()))
				.Returns(true);

			//Act
			var retornoOperacao = _veiculoService.Incluir(veiculo);

			//Assert
			Assert.AreEqual(retornoOperacao.Mensagem, "O Chassi informado já existe, o veículo não foi criado.");
		}

		[TestCase]
		public void Incluir_Veiculo_Sucesso()
		{
			//Arrange
			var veiculo = new Veiculo();

			_veiculoRepositoryMock
				.Setup(s => s.ExisteChassi(It.IsAny<string>()))
				.Returns(false);

			_veiculoRepositoryMock
				.Setup(s => s.Incluir(It.IsAny<Veiculo>()));

			//Act
			var retornoOperacao = _veiculoService.Incluir(veiculo);

			//Assert
			Assert.IsTrue(retornoOperacao.Sucesso);
		}

		[TestCase(TipoVeiculoEnum.Onibus, 42)]
		[TestCase(TipoVeiculoEnum.Caminhao, 2)]
		[TestCase((TipoVeiculoEnum)9999, 0)]
		public void Incluir_VeiculoPorTipo_NumeroPassageirosEsperado(TipoVeiculoEnum tipoVeiculoEnum, int numeroPassageirosEsperado)
		{
			//Arrange
			var veiculo = new Veiculo { Tipo = tipoVeiculoEnum };
			Veiculo veiculoInserido = null;

			_veiculoRepositoryMock
				.Setup(s => s.ExisteChassi(It.IsAny<string>()))
				.Returns(false);

			_veiculoRepositoryMock
				.Setup(s => s.Incluir(It.IsAny<Veiculo>()))
				.Callback<Veiculo>(c => veiculoInserido = c);

			//Act
			_veiculoService.Incluir(veiculo);

			//Assert
			Assert.AreEqual(veiculoInserido.NumeroPassageiros, numeroPassageirosEsperado);
		}

		[TestCase]
		public void Listar_Veiculos_Sucesso()
		{
			//Arrange
			var veiculoEsperadoSet = new List<Veiculo>
			{
				new Veiculo{ Id = 90 },
				new Veiculo{ Id = 80 }
			};

			var veiculoFiltro = new VeiculoFiltro();

			_veiculoRepositoryMock
				.Setup(s => s.Listar(It.IsAny<VeiculoFiltro>()))
				.Returns(veiculoEsperadoSet);

			//Act
			var veiculoSet = _veiculoService.Listar(veiculoFiltro);

			//Assert
			Assert.AreSame(veiculoEsperadoSet, veiculoSet);
		}

		[TestCase]
		public void Obter_Veiculo_Sucesso()
		{
			//Arrange
			var veiculoEsperado = new Veiculo { Id = 100 };

			_veiculoRepositoryMock
				.Setup(s => s.Obter(It.IsAny<int>()))
				.Returns(veiculoEsperado);

			//Act
			var veiculo = _veiculoService.Obter(veiculoEsperado.Id);

			//Assert
			Assert.AreSame(veiculoEsperado, veiculo);
		}
	}
}
