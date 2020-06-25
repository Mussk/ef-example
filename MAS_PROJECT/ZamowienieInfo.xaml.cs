using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MAS_PROJECT.DAL;
using MAS_PROJECT.dbmagazyn;

namespace MAS_PROJECT
{
    /// <summary>
    /// Interaction logic for ZamowienieInfo.xaml
    /// </summary>
    public partial class ZamowienieInfo : Window
    {
        //Database instance
        DbmagazynService db;

        TowarViewModel TowarViewModel;

        Zamowienie zamowienie;

        

        public ZamowienieInfo(Zamowienie zamowienie)
        {
            InitializeComponent();

            this.zamowienie = zamowienie;

            change_zamowienie_id_label(this.zamowienie);

            db = new DbmagazynService();

            TowarViewModel = new TowarViewModel(getAllTowarZamowienia(zamowienie));


            Produkty_DataGrid.ItemsSource = TowarViewModel.towary;

        }


        /// <summary>
        ///  Event handler for Dalej_button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dalej_button_Click(object sender, RoutedEventArgs e)
        {

            if (TowarViewModel.towary.Count == 0)
            {
                MessageBox.Show("Zamowienie jest anulowane", "Informacja", MessageBoxButton.OK);

                new ZamowieniaList().Show();

                this.Close();
            }
            
            if(zamowienie.status.Equals(Zamowienie.statusy[1].ToString()))
            {
                MessageBox.Show("Zamowienie juz zrealizowane", "Informacja", MessageBoxButton.OK);

                new ZamowieniaList().Show();

                this.Close();
            }

            if (TowarViewModel.towary.Any(el => el.czyZarezerwowany == false))
            {
                MessageBoxResult res = MessageBox.Show("Nie wzsyscy towary są obecne do zebrania zamówienia." +
                " Rozpocząc anulowanie?", "Podtwierdzenie", MessageBoxButton.OKCancel);

                if (res == MessageBoxResult.OK)
                {
                    //removing items
                    if (!db.delete_unavalible_products(
                        TowarViewModel.towary.Where(el => el.czyZarezerwowany == false)
                        .ToList()))
                    {
                        MessageBox.Show("Nie zmodyfikowalem zawartość magazynu. Sprobuj ponownie.", "Błąd", MessageBoxButton.OK);
                    }

                    if (db.change_order_status(zamowienie, Zamowienie.statusy[2].ToString()))
                    {
    
                            MessageBox.Show("Status zamówienia zmieniony na \"anulowane\"", "Podtwierdzenie", MessageBoxButton.OK);

                        new ZamowieniaList().Show();

                        this.Close();
                    }
                    else
                    {
                           MessageBox.Show("Nie zmodyfikowałem status zamowienia." +
                           " Wszystkie zmieny zostaly odwrócone.", "Błąd", MessageBoxButton.OK);
                    }
                }
                else if (res == MessageBoxResult.Cancel)
                {

                    new ZamowieniaList().Show();

                    this.Close();
                }

            }
            else
            {
                
                if (db.change_order_status(zamowienie, Zamowienie.statusy[1].ToString()))
                {
                    db.update_suma_all_towarow(zamowienie.Garniturs,zamowienie.OsobaKlient);
                    MessageBoxResult res =
                        MessageBox.Show("Status zamówienia zmieniony na \"zrealizowane\"", "Podtwierdzenie", MessageBoxButton.OK);

                    new ZamowieniaList().Show();
                    
                    this.Close();
                }
                else
                {
                    MessageBoxResult res =
                       MessageBox.Show("Nie zmodyfikowałem status zamowienia." +
                       " Wszystkie zmieny zostaly odwrócone.", "Podtwierdzenie", MessageBoxButton.OK);
                }
            }


        }

        /// <summary>
        /// Applies correct id of Order on label.
        /// </summary>
        /// <param name="zamowienie"></param>
        private void change_zamowienie_id_label(Zamowienie zamowienie)
        {
            ZamowienieId_label.Content = "Zamówienie#" + zamowienie.ZamowienieId;
        }

        /// <summary>
        /// Gets all products of Order
        /// </summary>
        /// <param name="zamowienie"></param>
        /// <returns> ObservableCollection<Towar> list</returns>
        private ObservableCollection<Towar> 
            getAllTowarZamowienia(Zamowienie zamowienie)
        {
            var res = new ObservableCollection<Towar>();

            foreach(Garnitur g in zamowienie.Garniturs)
            {
                res.Add(g.Marynarka);
                res.Add(g.Spodnie);
                res.Add(g.DodatkowyElement);
                foreach (Akcesoria a in g.Akcesorias)
                    res.Add(a);
             
            }

            return res;
        }

      
    }
}
