using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Factory.Models
{
  public class EngineerMachine
  {
    public int EngineerMachineId { get; set; }
    public int MachineId { get; set; }
    public Machine Machine { get; set; }
    public int EngineerId { get; set; }
    public Engineer Engineer { get; set; }
  }
}