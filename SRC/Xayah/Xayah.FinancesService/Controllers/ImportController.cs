using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xayah.Model;
using Xayah.Model.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Xayah.FinancesService.Controllers
{
	[Route("api/[controller]")]
	public class ImportController : Controller
	{
		private readonly IConciliateService _conciliateService;
		private readonly IOfxReader _reader;
		private readonly IRepository<BankTransfer> _repository;

		public ImportController(IConciliateService conciliateService, IOfxReader reader, IRepository<BankTransfer> repository)
		{
			_conciliateService = conciliateService;
			_reader = reader;
			_repository = repository;
		}

		[HttpGet]
		public string Get()
		{
			return "Tá funcionando";
		}

		[HttpPost]
		public void Post(List<IFormFile> files)
		{
			var ofxFiles = LoadFiles(files);
			var bankStatement = _conciliateService.Reconcile(ofxFiles.ToArray());
			_repository.Add(bankStatement.OrderByDescending(x => x.Date).ToArray());
		}

		private IEnumerable<OfxFile> LoadFiles(IEnumerable<IFormFile> files)
		{
			foreach (var f in files)
				yield return _reader.ReadFile(f.FileName, f.OpenReadStream());
		}
	}
}
