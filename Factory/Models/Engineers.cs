using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
    public class Engineer
    {
        public int EngineerId { get; set; }
        
        [Required(ErrorMessage = "This field can't be left empty")]
        public string? Name { get; set; }
        
         public List<Machine> Machines{ get; set; }
        
        public List<EngineerMachine> JoinEntities { get;}
        

    }
}

