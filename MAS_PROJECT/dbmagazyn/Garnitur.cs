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
    /// This class represents an suit, which consists of 4 parts:
    /// <c>Marynarka</c>
    /// <c>Spodnie</c>
    /// <c>DodatkowyElement</c>
    /// <c>Akcesoria(list)</c>
    /// </summary>
    public class Garnitur
    {
        public int GarniturId { get; set; }
        public string nazwa { get; set; }
        public string opis { get; set; }

        //discount rate
        public static int procent_rabatu = 10;

        public int MarynarkaId { get; set; }
        public virtual Marynarka Marynarka { get; set; }

        public int SpodnieId { get; set; }
        public virtual Spodnie Spodnie { get; set; }

        public int DodatkowyElementId { get; set; }
        public virtual DodatkowyElement DodatkowyElement { get; set; }

        public virtual ICollection<Akcesoria> Akcesorias { get; set; }

        public int ZamowienieId { get; set; }
        public virtual Zamowienie Zamowienie { get; set; }

        public Garnitur(string nazwa,
            string opis,
            Marynarka marynarka,
            Spodnie spodnie,
            DodatkowyElement dodatkowyElement, Zamowienie zamowienie)
        {
            this.nazwa = nazwa;
            this.opis = opis;
            Marynarka = marynarka;
            Spodnie = spodnie;
            DodatkowyElement = dodatkowyElement;
            this.koszt = wylicz_koszt();
            this.Zamowienie = zamowienie;
        }

        public Garnitur() { }

        public double koszt { get; set; }

        /// <summary>
        /// This metod is calculates price of suit with discount rate.
        /// </summary>
        /// <returns>
        /// A price for suit.
        /// </returns>
        public double wylicz_koszt()
        {
            var suma = Marynarka.koszt +
                    Spodnie.koszt +
                    DodatkowyElement.koszt;

            return suma - (suma * (procent_rabatu / 100));
        }
    }
}
