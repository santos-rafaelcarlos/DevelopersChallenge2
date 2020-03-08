using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xayah.Model;
using Xayah.Model.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Xayah.FinancesService.Controllers
{
	[Route("api/[controller]")]
	public class BankTransferController : Controller
	{
		private readonly IRepository<BankTransfer> _repository;

		public BankTransferController(IRepository<BankTransfer> repository)
		{
			_repository = repository;
		}

		// GET: api/<controller>
		[HttpGet]
		public IEnumerable<BankTransfer> Get()
		{			
			return _repository.GetAll();
		}

		// GET api/<controller>/5
		[HttpGet("{id}")]
		public BankTransfer Get(int id)
		{
			return _repository.Get(x => x.Id == id).FirstOrDefault();
		}

		// POST api/<controller>
		[HttpPost]
		public void Post([FromBody]BankTransfer value)
		{
			_repository.Add(value);
		}

		// PUT api/<controller>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]BankTransfer value)
		{
			value.Id = id;
			_repository.Add(value);
		}

		// DELETE api/<controller>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			_repository.Remove(id);
		}
	}
}
