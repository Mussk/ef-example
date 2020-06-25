using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using MAS_PROJECT.dbmagazyn;

namespace MAS_PROJECT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Dodaj_Click(object sender, RoutedEventArgs e)
        {

            using (var _context = new dbmagazynContext())
            {
               
                _context.Database.EnsureCreated();

                _context.Add(new Producent { nazwa = "Nike" });
                _context.SaveChanges();

                _context.Add(new Marynarka {
                    nazwa = "aaa",
                    rozmiar = Rozmiar.S,
                    Producent = _context.Producents.Where(el => el.nazwa.Equals("Nike")).First(),
                    Argb = Color.FromName("Black").ToArgb(),
                    koszt = 2300.5,
                    opis = "NORM",
                    ilosc_guzikow = 5
                });

                _context.Add(new Marynarka
                {
                    nazwa = "bbb",
                    rozmiar = Rozmiar.S,
                    Producent = _context.Producents.Where(el => el.nazwa.Equals("Nike")).First(),
                    Argb = Color.FromName("Black").ToArgb(),
                    koszt = 2300.5,
                    opis = "NORM",
                    ilosc_guzikow = 5
                });

                _context.Add(new Spodnie {
                    nazwa = "spodnie",
                    rozmiar = Rozmiar.S,
                    Producent = _context.Producents.Where(el => el.nazwa.Equals("Nike")).First(),
                    Argb = Color.FromName("Black").ToArgb(),
                    koszt = 2300.5,
                    opis = "NORM",
                    is_pas = true
                });

                _context.Add(new Spodnie
                {
                    nazwa = "spodnie2",
                    rozmiar = Rozmiar.S,
                    Producent = _context.Producents.Where(el => el.nazwa.Equals("Nike")).First(),
                    Argb = Color.FromName("Black").ToArgb(),
                    koszt = 2300.5,
                    opis = "NORM",
                    is_pas = true
                });

                _context.Add(new Kamizelka {
                    nazwa = "kamizelka",
                    rozmiar = Rozmiar.S,
                    Producent = _context.Producents.Where(el => el.nazwa.Equals("Nike")).First(),
                    Argb = Color.FromName("Black").ToArgb(),
                    koszt = 2300.5,
                    opis = "NORM",
                    typ_podkladu = "futro"
                });

                _context.Add(new Kamizelka
                {
                    nazwa = "kamizelka2",
                    rozmiar = Rozmiar.S,
                    Producent = _context.Producents.Where(el => el.nazwa.Equals("Nike")).First(),
                    Argb = Color.FromName("Black").ToArgb(),
                    koszt = 2300.5,
                    opis = "NORM",
                    typ_podkladu = "futro"
                });

                _context.SaveChanges();

                _context.Osoby.Add(new Osoba
                {
                    imie = "Alex",
                    nazwisko = "KK",
                    adresa = "dasdsa",
                    nr_telefonu = "12321",
                    OsobaTypes = OsobaType.Klient,
                   
                });
                _context.Osoby.Add(new Osoba
                {
                    imie = "Alex",
                    nazwisko = "Kowalski",
                    adresa = "dasdsa",
                    nr_telefonu = "12321",
                    OsobaTypes = OsobaType.Klient,

                });

                _context.SaveChanges();


                var zamowienie_to_add = new Zamowienie
                {
                    status = Zamowienie.statusy[0].ToString(),
                    opis = "aaa",
                    _OsobaKlient = _context.Osoby
                   .Where(el => el.nazwisko.Equals("KK"))
                   .First(),
                    _OsobaPracownik = null
                    
                };

                var zamowienie_to_add2 = new Zamowienie
                {
                    status = Zamowienie.statusy[0].ToString(),
                    opis = "aaa",
                    _OsobaKlient = _context.Osoby
                   .Where(el => el.nazwisko.Equals("Kowalski"))
                   .First(),
                    _OsobaPracownik = null

                };

                _context.Zamowienia.Add(zamowienie_to_add);
                _context.Zamowienia.Add(zamowienie_to_add2);
                _context.SaveChanges();


                var garnitur_to_add =
                    new Garnitur("Gar1",
                    "sdad",                 
                    (Marynarka)_context.
                    ElementyOdzieziezy.
                    Where(el => el.nazwa.Equals("aaa")).
                    First(), (Spodnie)_context.
                    ElementyOdzieziezy.
                    Where(el => el.nazwa.Equals("spodnie")).
                    First(),
                    (DodatkowyElement)_context.ElementyOdzieziezy
                    .Where(el => el.nazwa.Equals("kamizelka"))
                    .First(),
                     _context.Zamowienia.Where(el => el.ZamowienieId == 1).First()
                     );

                var gar2= 
                new Garnitur("Gar2",
                    "sdad",
                    (Marynarka)_context.
                    ElementyOdzieziezy.
                    Where(el => el.nazwa.Equals("bbb")).
                    First(), (Spodnie)_context.
                    ElementyOdzieziezy.
                    Where(el => el.nazwa.Equals("spodnie2")).
                    First(),
                    (DodatkowyElement)_context.ElementyOdzieziezy
                    .Where(el => el.nazwa.Equals("kamizelka2"))
                    .First(),
                     _context.Zamowienia.Where(el => el.ZamowienieId == 2).First()
                     );



                /*   garnitur_to_add.Akcesorias.Add(
                       _context.
                       Akcesoria.
                       Where(el => el.nazwa.
                       Equals("akcesoria"))
                       .First()); */

                _context.Add(garnitur_to_add);
                _context.Add(gar2);
                _context.SaveChanges();

                _context.Add(new Akcesoria
                {
                    koszt = 200.0,
                    nazwa = "akcesoria",
                    _rodzaj = Akcesoria.rodzaje[0].ToString(),
                    Garnitur = _context.Garnitury.
                    Where(el => el.nazwa.Equals("Gar1")).First()

                });

                _context.Add(new Akcesoria
                {
                    koszt = 200.0,
                    nazwa = "akcesoria2",
                    _rodzaj = Akcesoria.rodzaje[0].ToString(),
                    Garnitur = _context.Garnitury.
                   Where(el => el.nazwa.Equals("Gar1")).First()

                });
                _context.SaveChanges();


               

               

               /* zamowienie_to_add.
                    Garniturs.
                    Add(_context.Garnitury
                    .Where(el => el.nazwa.Equals("Gar1"))
                    .First()); */

                
          
               

          /*      var obj_to_add = new dbmagazyn.Producent { nazwa = "Nike" };

                _context.Add(obj_to_add);
                _context.SaveChanges();

                _context.Add(new dbmagazyn.Marynarka
                {
                    nazwa = "aaa",
                    rozmiar = dbmagazyn.Rozmiar.S,
                    ProducentId1 = _context.Producents.Where(el => el.nazwa.Equals("Nike")).First(),
                    Argb = Color.FromName("Black").ToArgb(),
                    koszt = 2300.5,
                    opis = "NORM",
                    ilosc_guzikow = 5
                });
                _context.SaveChanges();
           */
            } 


        }
    }
}
