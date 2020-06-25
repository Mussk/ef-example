using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAS_PROJECT.DAL;

namespace MAS_PROJECT.dbmagazyn
{
    /// <summary>
    /// This class represents a Director-User. Impements Uzytkownik
    /// </summary>
    /// See <see cref="Uzytkownik)"/>.  
    class Dyrektor : Uzytkownik
    {
        DbmagazynService db = new DbmagazynService();

        //Interface implementation
        /// <summary>
        /// This method shows all workers.
        /// </summary>
        public void pokaz_info()
        {
            db.GetAllPracowniki();
        }

        /// <summary>
        /// This method shows all positions in warehouse.
        /// </summary>
        public void pokaz_magazyn()
        {
            db.GetAllTowars();
        }

        /// <summary>
        /// This method changes salary of worker
        /// </summary>
        public void change_salary(Osoba worker, int nowa_pensja)
        {
            db.changeSalaryOfPracownik(worker, nowa_pensja);
        }
    }
}
