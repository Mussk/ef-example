using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAS_PROJECT.dbmagazyn
{
    /// <summary>
    /// This enum represents sizes of clothes in European system.
    /// </summary>
    public enum Rozmiar
    {
        XS,
        S,
        M,
        L,
        XL,
        XXL
    }

    /// <summary>
    /// This abstract class represents an element of clothes.
    /// Inherits <c>Towar</c> class.
    /// </summary>
    public abstract class ElementOdziezy : Towar
    {
       
        public string nazwa { get; set; }
        public Rozmiar rozmiar { get; set; }
 
        //storing color as ARGB value in database
        public Int32 Argb
        {
            get
            {
                return color.ToArgb();
            }
            set
            {
                color = Color.FromArgb(value);
            }
        }
        [NotMapped]
        public Color color { get; set; }       
        public string opis { get; set; }
        

        
        public int ProducentId { get; set; }
        public virtual Producent Producent { get; set; }
  
    }
}
