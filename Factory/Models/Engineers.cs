using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable enable
namespace Factory.Models
{
    public class Engineer
    {
        public int EngineerId { get; set; }

        [Required(ErrorMessage = "This field can't be left empty")]
        public string Name { get; set; } = "";

        // public int MachineId { get; set; }
        // public virtual Machine Machines { get; set; } = null!;
        public List<EngineerMachine> JoinEntities { get; } = new List<EngineerMachine>();
    }
#nullable disable
}

