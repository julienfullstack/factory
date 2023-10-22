using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Factory.Models;

namespace Factory.Controllers
{
	public class EngineersController : Controller
	{
		private readonly FactoryContext _db;

		public EngineersController(FactoryContext db)
		{
			_db = db;
		}

		public ActionResult Index()
		{
			List<Engineer> model = _db.Engineers.ToList();
			return View(model);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Engineer Engineer)
		{
			_db.Engineers.Add(Engineer);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

public ActionResult Show(int id)
{
    Engineer thisEngineer = _db.Engineers
                            .Include(Engineer => Engineer.JoinEntities)
														.ThenInclude(join => join.Machine)
                            .FirstOrDefault(Engineer => Engineer.EngineerId == id);
    return View(thisEngineer);
}

public ActionResult AddMachine(int id)
{
	Engineer thisEngineer = _db.Engineers.FirstOrDefault(Engineers => Engineers.EngineerId == id);
	ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
	return View(thisEngineer);
}

[HttpPost] 
public ActionResult AddMachine(Engineer Engineer, int MachineId)
{
    #nullable enable
    EngineerMachine? joinEntity = _db.EngineerMachines.FirstOrDefault(join => (join.MachineId == MachineId && join.EngineerId == Engineer.EngineerId));
    #nullable disable 
    if (joinEntity == null && MachineId != 0)
    {
        _db.EngineerMachines.Add(new EngineerMachine() { MachineId = MachineId, EngineerId = Engineer.EngineerId});
        _db.SaveChanges();
    }
    else
    {
        ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
        return View(Engineer);
    }
    return RedirectToAction("Show", new { id = Engineer.EngineerId });
}


	public ActionResult Edit(int id)
{
    Engineer thisEngineer = _db.Engineers
                            .Include(Engineer => Engineer.Machines)
                            .FirstOrDefault(Engineer => Engineer.EngineerId == id);
    return View(thisEngineer);
}


		[HttpPost]
		public ActionResult Edit(Engineer Engineer)
		{
			_db.Entry(Engineer).State = EntityState.Modified;
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult Delete(int id)
		{
			Engineer thisEngineer = _db.Engineers.FirstOrDefault(Engineer => Engineer.EngineerId == id);
			return View(thisEngineer);
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			Engineer thisEngineer = _db.Engineers.FirstOrDefault(Engineer => Engineer.EngineerId == id);
			_db.Engineers.Remove(thisEngineer);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult DeleteJoin(int joinId)
		{
			EngineerMachine joinEntry = _db.EngineerMachines.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
			_db.EngineerMachines.Remove(joinEntry);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
