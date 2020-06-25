using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_PROJECT.dbmagazyn
{
    /// <summary>
    /// This class represents <c>Akcesoria</c> class.
    /// </summary>
    public class Akcesoria : Towar
    {
        
        public string nazwa { get; set; }
        public string rodzaj;
        public string _rodzaj {
            get => rodzaj;
            set { if (!rodzaje.Contains(value))
                    throw new Exception("Niepoprawny rodzaj!");
                else
                    rodzaj = value;
            }
        }


      public static List<string> rodzaje = new List<string>()
        {"z tkaniny", "z metali"};

    }
}
