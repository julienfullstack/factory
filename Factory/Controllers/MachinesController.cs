using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models; 
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Factory.Controllers
{
  public class MachinesController : Controller
  {
    private readonly FactoryContext _db;

    public MachinesController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Machine> model = _db.Machines
                              // .Include(Machine => Machine.Engineer)
                              .ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
      return View();
    }

   [HttpPost]
public ActionResult Create(Machine Machine, int EngineerId)
{
    if (ModelState.IsValid)
    {
        _db.Machines.Add(Machine);
        _db.SaveChanges();

        if (EngineerId > 0)
        {
          //  _db.Machines
          //                     .Include(Machine => Machine.JoinEntities)
          //                     .ThenInclude(join => join.Engineer)
          //                     .FirstOrDefault(Machine => Machine.MachineId == id);
        }

        return RedirectToAction("Index");
    }
    else
    {
        ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
        return View(Machine);
    }
}


    public ActionResult Show(int id)
    {
      Machine thisMachine = _db.Machines
                              .Include(Machine => Machine.JoinEntities)
                              .ThenInclude(join => join.Engineer)
                              .FirstOrDefault(Machine => Machine.MachineId == id);
      return View(thisMachine);
    }

    public ActionResult AddEngineer(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(Machines => Machines.MachineId == id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult AddEngineer(Machine Machine, int EngineerId)
    {
      #nullable enable
      EngineerMachine? joinEntity = _db.EngineerMachines.FirstOrDefault(join => (join.MachineId == Machine.MachineId && join.EngineerId == EngineerId));
      #nullable disable
      if (joinEntity == null && EngineerId != 0)
      {
        _db.EngineerMachines.Add(new EngineerMachine() { EngineerId = EngineerId, MachineId = Machine.MachineId });
        _db.SaveChanges();
      }
      else
      {
        ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
        return View(Machine);
      }
      return RedirectToAction("Show", new { id = Machine.MachineId });
    }

    public ActionResult Edit(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(Machine => Machine.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult Edit(Machine Machine)
    {
      _db.Entry(Machine).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(Machine => Machine.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(Machine => Machine.MachineId == id);
      _db.Machines.Remove(thisMachine);
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
