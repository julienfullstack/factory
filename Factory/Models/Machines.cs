using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable enable
namespace Factory.Models
{
    public class Machine
    {
        public int MachineId { get; set; }

        [Required(ErrorMessage = "This field can't be left empty")]
        public string Name { get; set; } = "";

        public List<EngineerMachine> JoinEntities { get; } = new List<EngineerMachine>();
    }
#nullable disable
}