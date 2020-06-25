using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAS_PROJECT.dbmagazyn
{
    /// <summary>
    /// This class represents a ViewModel of <c>Towar</c> class.
    /// Needed for XAML Binding.
    /// Implements <c>INotifyPropertyChanged</c> interface.
    /// DOESN'T WORK PROPRLY FOR NOW!
    /// </summary>
    public class TowarViewModel : INotifyPropertyChanged
    {
        private bool _allSelectedChanging;
        private bool? _allSelected;
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Towar> _towary;
        public ObservableCollection<Towar> towary 
        {
            get => _towary;
            set
            {
                if (Equals(value, _towary)) return;
                _towary = value;
                OnPropertyChanged();
            }
        }

        public bool? AllSelected
        {
            get => _allSelected;
            set
            {
                if (value == _allSelected) return;
                _allSelected = value;

               
                AllSelectedChanged();
                OnPropertyChanged();
            }
        }

        public TowarViewModel(ObservableCollection<Towar> towary)
        {
            this.towary = towary;

            foreach (Towar el in towary)
            {
                el.PropertyChanged += TowarOnPropertyChanged;
            }
        }

        private void TowarOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            
            if (args.PropertyName == nameof(Towar.czyZarezerwowany))
                RecheckAllSelected();
        }

        /// <summary>
        /// This method rechecks all checkboxes in column.
        /// </summary>
        private void AllSelectedChanged()
        {
            
            if (_allSelectedChanging) return;

            try
            {
                _allSelectedChanging = true;

               
                if (AllSelected == true)
                {
                    foreach (Towar element in towary)
                        element.czyZarezerwowany = true;
                }
                else if (AllSelected == false)
                {
                    foreach (Towar element in towary)
                        element.czyZarezerwowany = false;
                }
            }
            finally
            {
                _allSelectedChanging = false;
            }
        }

        /// <summary>
        /// This method checks if all checkboxes are in same state.
        /// </summary>
        private void RecheckAllSelected()
        {
           
            if (_allSelectedChanging) return;

            try
            {
                _allSelectedChanging = true;

                if (towary.All(e => e.czyZarezerwowany))
                    AllSelected = true;
                else if (towary.All(e => !e.czyZarezerwowany))
                    AllSelected = false;
                else
                    AllSelected = null;
            }
            finally
            {
                _allSelectedChanging = false;
            }
        }

        //interface realization
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
