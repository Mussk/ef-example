using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_PROJECT.dbmagazyn
{
    /// <summary>
    /// This class represents Manufacturer.
    /// </summary>
    public class Producent
    {
       public int ProducentId { get; set; }
       public string nazwa { get; set; }
       public virtual ICollection<ElementOdziezy> ElementOdziezies { get; set; }

    }
}
