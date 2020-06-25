using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAS_PROJECT.dbmagazyn;

namespace MAS_PROJECT.DAL
{
    /// <summary>
    /// This class represents DAL (Data Acess Layer).
    /// It is part of EF convention.
    /// We are communicating with a database through this class.
    /// </summary>
    class DbmagazynService
    {

        //represents data storage.
        private dbmagazynContext _context = new dbmagazynContext();


        /// <summary>
        /// Get all Orders form DB.
        /// </summary>
        /// <returns>IEnumerable<Zamowienie> list</returns>
        public IEnumerable<Zamowienie> GetZamowienia()
        {
            return _context.Zamowienia
                  .ToList();
        }

        /// <summary>
        /// Get contetns of order.
        /// </summary>
        /// <param name="zamowienie"></param>
        /// <returns>IEnumerable<Garnitur> list</returns>
        public IEnumerable<Garnitur> GetGarnituryZamowienia(Zamowienie zamowienie)
        {
            return _context.
                  Garnitury.
                  Where(e => e.ZamowienieId == zamowienie.ZamowienieId).ToList();
        }

        /// <summary>
        /// Get all parts of suit.
        /// </summary>
        /// <param name="garnitur"></param>
        /// <returns>IEnumerable<ElementOdziezy> list </returns>
        public IEnumerable<ElementOdziezy> GetElementyOdziezyGarnitura(Garnitur garnitur)
        {
            return _context.ElementyOdzieziezy
                .Where(e => e.Garnitur == garnitur).ToList();
        }

        /// <summary>
        /// Adds suit to DB
        /// </summary>
        /// <param name="garnitur"></param>
        public void addGarniturToDb(Garnitur garnitur)
        {
            _context.Add(garnitur);
        }

        /// <summary>
        /// Changes status of Order
        /// </summary>
        /// <param name="zamowienie"></param>
        /// <param name="status_to_apply"></param>
        /// <returns>true if transaction was succseed, false insted</returns>
        public bool change_order_status(Zamowienie zamowienie, string status_to_apply)
        {
            _context.
                Zamowienia.
                Where(el => el.ZamowienieId == zamowienie.ZamowienieId).
                First()._status = status_to_apply;

            if (_context.SaveChanges() == 0)
            {
                return false;
            }
            else return true;
        }

        /// <summary>
        /// Delets products of Order which don't present in warehouse.
        /// </summary>
        /// <param name="items_to_remove"></param>
        /// <returns>true if transaction was succseed, false insted</returns>
        public bool delete_unavalible_products(ICollection<Towar> items_to_remove)
        {

            foreach (Towar element in items_to_remove) {
                _context.ElementyOdzieziezy.Remove(_context.ElementyOdzieziezy
                     .Where(el => el.TowarId == element.TowarId).First());
                _context.Garnitury.Remove(_context.Garnitury
                    .Where(el => el.GarniturId == element.Garnitur.GarniturId).First());
            }

            if (_context.SaveChanges() == 0)
            {
                return false;
            }
            else return true;
        }


        /// <summary>
        /// Updates overall income from given Client.
        /// </summary>
        /// <param name="garniturs"></param>
        /// <param name="klient"></param>
        public void update_suma_all_towarow(ICollection<Garnitur> garniturs, Osoba klient)
        {
            var suma = 0.0;

            foreach (Garnitur item in garniturs)
            {
                suma += item.koszt;
            }

            _context.Osoby.Where(el => el.OsobaId == klient.OsobaId).First().suma_all_zakupow += suma;

            _context.SaveChanges();
        }

        /// <summary>
        /// Gets all workers in a warehouse.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Osoba> GetAllPracowniki()
        {
            return _context.Osoby.Where(el =>
            el.OsobaTypes == OsobaType.KlientPracownik ||
            el.OsobaTypes == OsobaType.Pracownik);
        }

        /// <summary>
        /// Gets all positions in a warehouse.
        /// </summary>
        /// <returns>IList<Towar> list</returns>
        public IList<Towar> GetAllTowars()
        {
            var res =(IList<Towar>)_context.Akcesoria.ToList();
            
            foreach (ElementOdziezy el in _context.ElementyOdzieziezy.ToList())
            {
                res.Add((Towar)el);
            }

            return res;
        }
        /// <summary>
        /// Changes salary of worker.
        /// </summary>
        /// <param name="pracownik"></param>
        /// <param name="nowa_pensja"></param>
        public void changeSalaryOfPracownik(Osoba pracownik, int nowa_pensja)
        {
            _context.Osoby.Where(el => el.OsobaId == pracownik.OsobaId)
                .First().pensja = nowa_pensja;

            _context.SaveChanges();
        }

        /// <summary>
        ///Gets worker by Id.
        /// </summary>
        /// <param name="osoba"></param>
        /// <returns>Osoba object</returns>
        public Osoba getMe(Osoba osoba)
        {
            return _context.Osoby.Where(el => el.OsobaId == osoba.OsobaId).First();
        }
    }
}
