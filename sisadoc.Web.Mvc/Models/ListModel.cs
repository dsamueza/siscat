using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sisadoc.Web.Mvc.Models
{
    public class ListModel
    {
        public ListModel()
          {
        ActionsList = new List<SelectListItem>();
          }
      public IEnumerable<SelectListItem> ActionsList { get; set; }
      public string Text { get; set; }
      public string Id { get; set; }
  
    }
}