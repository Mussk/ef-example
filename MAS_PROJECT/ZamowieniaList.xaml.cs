using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MAS_PROJECT.DAL;
using MAS_PROJECT.dbmagazyn;

namespace MAS_PROJECT
{
    /// <summary>
    /// Interaction logic for ZamowieniaList.xaml
    /// </summary>
    public partial class ZamowieniaList : Window
    {
        //Database instance
        DbmagazynService db;

        ObservableCollection<Zamowienie> zamowienias;


        public ZamowieniaList()
        {
            InitializeComponent();

            db = new DbmagazynService();

            zamowienias = new ObservableCollection<Zamowienie>(db.GetZamowienia());

            Zamowienia_DataGrid.ItemsSource = zamowienias;
        }

        /// <summary>
        /// Event handler for DoubleClick event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Zamowienia_DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new ZamowienieInfo((Zamowienie)Zamowienia_DataGrid.SelectedItem).Show();

            this.Close();
        }


        /// <summary>
        /// Event handler for Wybrac_button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        private void Wybrac_button_Click(object sender, RoutedEventArgs e)
        {
           
            new ZamowienieInfo((Zamowienie)Zamowienia_DataGrid.SelectedItem).Show();

            this.Close();
        }
    }
}
