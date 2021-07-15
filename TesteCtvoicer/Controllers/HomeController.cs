using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TesteCtvoicer.Entities;
using TesteCtvoicer.Models;
using TesteCtvoicer.Repositories.Filtros;
using TesteCtvoicer.Services;

namespace TesteCtvoicer.Controllers
{
	public class HomeController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IVeiculoService _veiculoService;

		public HomeController(IMapper mapper, IVeiculoService veiculoService)
		{
			_mapper = mapper;
			_veiculoService = veiculoService;
		}

		public IActionResult Index(string chassi = null)
		{
			var veiculoFiltro = new VeiculoFiltro { Chassi = chassi };

			var veiculoSet = _veiculoService.Listar(veiculoFiltro);
			var veiculoModelSet = veiculoSet.ConvertAll(veiculo => _mapper.Map<VeiculoViewModel>(veiculo));

			return View(new ListaVeiculoViewModel
			{
				Chassi = chassi,
				VeiculoSet = veiculoModelSet
			});
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(VeiculoViewModel veiculoViewModel)
		{
			if (!ModelState.IsValid)
				return View(veiculoViewModel);

			var veiculo = _mapper.Map<Veiculo>(veiculoViewModel);
			var retornoOperacao = _veiculoService.Incluir(veiculo);

			if (!retornoOperacao.Sucesso)
			{
				ModelState.AddModelError("", retornoOperacao.Mensagem);
				return View(veiculoViewModel);
			}

			ViewBag.Message = "Veículo inserido com sucesso!";
			return View();
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var veiculo = _veiculoService.Obter(id);
			var veiculoViewModel = _mapper.Map<VeiculoViewModel>(veiculo);

			return View(veiculoViewModel);
		}

		[HttpPost]
		public ActionResult Edit(VeiculoViewModel veiculoViewModel)
		{
			if (!ModelState.IsValid)
				return View(veiculoViewModel);

			var veiculo = _mapper.Map<Veiculo>(veiculoViewModel);
			var retornoOperacao = _veiculoService.Alterar(veiculo);

			if (!retornoOperacao.Sucesso)
			{
				ModelState.AddModelError("", retornoOperacao.Mensagem);
				return View(veiculoViewModel);
			}

			ViewBag.Message = "Veículo alterado com sucesso!";
			return View();
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var veiculo = _veiculoService.Obter(id);
			var veiculoViewModel = _mapper.Map<VeiculoViewModel>(veiculo);

			return View(veiculoViewModel);
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(VeiculoViewModel veiculoViewModel)
		{
			var retornoOperacao = _veiculoService.Excluir(veiculoViewModel.Id);

			if (!retornoOperacao.Sucesso)
			{
				ModelState.AddModelError("", retornoOperacao.Mensagem);
				return View(veiculoViewModel);
			}

			ViewBag.Message = "Veículo excluído com sucesso!";
			return View(veiculoViewModel);
		}
	}
}
