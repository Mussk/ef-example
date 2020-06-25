using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAS_PROJECT.DAL;

namespace MAS_PROJECT.dbmagazyn
{
    /// <summary>
    /// This enum is needed for oveplapping inheritance implementation.
    /// </summary>
    [Flags]
    public enum OsobaType : int
    {
        Klient = 1,
        Pracownik = 2,
        KlientPracownik = Klient | Pracownik

    }

    /// <summary>
    /// This class represents Person. Implements <c>Uzytkownik</c> interface.
    /// </summary>
    public partial class Osoba : Uzytkownik
    {
        public int OsobaId { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string nr_telefonu { get; set; }
        public string adresa { get; set; }
        public OsobaType OsobaTypes { get; set; }

        //Orders list for klient
        [ForeignKey("_OsobaKlientOsobaId")]
        public virtual ICollection<Zamowienie> ZamowieniesKlient { get; set; }

        //Orders list for worker
        [ForeignKey("_OsobaPracownikOsobaId")]
        public virtual ICollection<Zamowienie> ZamowieniesPracownik { get; set; }

        DbmagazynService db = new DbmagazynService();

        //Klient part
        public int procent_rabatu;
        public int _procent_rabatu
        {
            get { return this.procent_rabatu; }
            set
            {
                if(this.OsobaTypes.HasFlag(OsobaType.Klient))
                    wylicz_procent_rabatu();
            }
        }
        public double suma_all_zakupow { get; set; }

        public Osoba()
        {
            if (this.OsobaTypes.HasFlag(OsobaType.Klient))
                wylicz_procent_rabatu();

            if (this.OsobaTypes.HasFlag(OsobaType.Pracownik))
                this.staz = wylicz_staz();
        }

        public void wylicz_procent_rabatu()
        {

            if (suma_all_zakupow <= 5000.0
                && suma_all_zakupow > 2500.0)
                procent_rabatu = 5;
            else if (suma_all_zakupow <= 2500.0 &&
                suma_all_zakupow > 500)
                procent_rabatu = 2;
            else if (suma_all_zakupow <= 500)
                procent_rabatu = 0;
            else if (suma_all_zakupow > 5000.0)
                procent_rabatu = 10;
        }
    
    //Worker part
    public DateTime data_zatrudnienia { get; set; }
    public int pensja { get; set; }
    //w ciagu roku
    public int il_zamowien { get; set; }
    public int staz;
    public int _staz
    {
        get { return this.staz; }
        set
            {
                if (this.OsobaTypes.HasFlag(OsobaType.Pracownik))
                    staz = wylicz_staz();
            }
    }


        /// <summary>
        /// This method calculates worker experience in full years.
        /// </summary>
        /// <returns>Count of full worked years.</returns>
        public int wylicz_staz()
    {
        return DateTime.UtcNow.Year - data_zatrudnienia.Year;
    }

        //Interface implementation
        public void pokaz_magazyn()
        {
            db.GetAllTowars();
        }

        public void pokaz_info()
        {
            db.getMe(this);
        }
    }
}

