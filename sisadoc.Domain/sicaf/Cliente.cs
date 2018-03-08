using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace sisadoc.Domain.sicaf
{
   public  class Cliente:Entity
    {

       public virtual string NombreCompleto { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Cedula { get; set; }



    }
}
