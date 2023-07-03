using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.ViewModels
{
    public class ItemVM
    {
        public Item Item { get; set; }

        [ValidateNever] // Using this annotation will make sure it donot validate this property
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
