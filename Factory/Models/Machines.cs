using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
    public class Machine
    {
        public int MachineId { get; set; }

        [Required(ErrorMessage = "This field can't be left empty")]
        public string? Name { get; set; }
        public int EngineerId { get; set; }
        public virtual Engineer Engineer { get; set; }
        public List<EngineerMachine> JoinEntities { get;}

    }
}