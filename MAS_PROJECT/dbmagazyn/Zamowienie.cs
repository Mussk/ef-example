using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_PROJECT.dbmagazyn
{
    /// <summary>
    /// This class represents Order.
    /// </summary>
   public partial class Zamowienie
    {
        public int ZamowienieId { get; set; }
        public string opis { get; set; }
        public static IList statusy = new ArrayList()
        { "akceptowane", "realizowane", "anulowane" };
        public virtual ICollection<Garnitur> Garniturs { get; set; }
        public string status;
        public string _status
        {
            get { return this.status; }
            set
            {
                if (!statusy.Contains(value))
                    throw new Exception("Nieprawidlowy status!");
                else this.status = value;
            }
        }

        //Foreign key for Client
        [ForeignKey("OsobaKlient")]
        public int? _OsobaKlientOsobaId { get; set; }       
        public Osoba _OsobaKlient;      
        public virtual Osoba OsobaKlient
        {
            get
            {
                return this._OsobaKlient;
            }
            set
            {
                if (value == null)
                {
                    this._OsobaKlient = value;
                }
                else
                {
                    if (value.OsobaTypes == OsobaType.Klient ||
                        value.OsobaTypes == OsobaType.KlientPracownik)
                    {
                        this._OsobaKlient = value;
                    }
                    else
                    {
                        throw new Exception("Osoba nie jest klientem!");
                    }
                }
            }
        }
        //Foreign key for Worker
        [ForeignKey("OsobaPracownik")]
        public int? _OsobaPracownikOsobaId { get; set; }       
        public Osoba _OsobaPracownik;      
        public virtual Osoba OsobaPracownik
        {
            get
            {
                return this._OsobaPracownik;
            }
            set
            {
                if (value == null)
                {
                    this._OsobaPracownik = value;
                }
                else { 
                    if (value.OsobaTypes == OsobaType.Pracownik ||
                        value.OsobaTypes == OsobaType.KlientPracownik)
                    {
                        this._OsobaPracownik = value;
                    }
                    else
                    {
                        throw new Exception("Osoba nie jest pracownikiem!");
                    }
                }
            }
        }


    }
}
