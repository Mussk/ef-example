using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_PROJECT.dbmagazyn
{
   public interface Uzytkownik
   {
        /// <summary>This method shows us all products in warehouse.</summary>
        void pokaz_magazyn();
        /// <summary>This method shows us info about user who calls it.</summary>
        void pokaz_info();
   }
}
