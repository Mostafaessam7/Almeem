using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
  public class OrderSpecParams : PagingParams
  {
    public string? Status { get; set; }
  }
}
