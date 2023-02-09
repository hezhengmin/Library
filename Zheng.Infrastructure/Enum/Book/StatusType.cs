using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zheng.Infra.Data.Models
{
    public partial class Book
    {
        public enum StatusType
        {
            [Display(Name = "無庫存")]
            None = 0,
            [Display(Name = "有庫存")]
            Stock = 1
        }
    }
       
}
