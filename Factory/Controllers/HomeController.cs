using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Factory.Controllers
{
	public class HomeController : Controller
	{
		private readonly FactoryContext _db;

		public HomeController(FactoryContext db)
		{
			_db = db;
		}

		[HttpGet("/")]
		public ActionResult Index()
		{
			Engineer[] Engineers = _db.Engineers.ToArray();
			Machine[] Machines = _db.Machines.ToArray();
			Dictionary<string,object[]> model = new Dictionary<string, object[]>();
			model.Add("Engineers", Engineers);
			model.Add("Machines", Machines);
			return View(model);
		}
	}
}