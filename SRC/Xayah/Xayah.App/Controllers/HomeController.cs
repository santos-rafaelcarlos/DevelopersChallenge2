using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Xayah.App.Models;
using Xayah.Model;

namespace Xayah.App.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		private byte[] ReadFromFile(IFormFile file)
		{
			byte[] data;
			using (var br = new BinaryReader(file.OpenReadStream()))
				data = br.ReadBytes((int)file.OpenReadStream().Length);

			return data;
		}

		[HttpPost()]
		public async Task<IActionResult> Index(List<IFormFile> files)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:51251/api/import");

				MultipartFormDataContent multiContent = new MultipartFormDataContent();

				foreach (var file in files)
				{
					var data = ReadFromFile(file);
					ByteArrayContent bytes = new ByteArrayContent(data);
					multiContent.Add(bytes, "files", file.FileName);
				}

				var result = client.PostAsync("", multiContent).Result;

				if (result.IsSuccessStatusCode)
				{
					return RedirectToAction("ShowBankStatement");
				}
			}

			ModelState.AddModelError(string.Empty, "Server Error.");

			return RedirectToAction("Index");
		}


		public IActionResult ShowBankStatement()
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:51251/api/");

				var postTask = client.GetStringAsync("BankTransfer");
				postTask.Wait();

				var result = postTask.Result;
				if (postTask.IsCompletedSuccessfully)
				{
					var items = JsonConvert.DeserializeObject<IEnumerable<BankTransfer>>(result);

					return View(items);
				}
			}

			ModelState.AddModelError(string.Empty, "Server Error.");

			return RedirectToAction("Index");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
