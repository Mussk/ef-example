using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAS_PROJECT.dbmagazyn
{
    /// <summary>
    /// This abstract class is needed for combining all parts of <c>Garnitur</c> class together.
    /// Implements <c>INotifyPropertyChanged</c> interface.
    /// </summary>
    public abstract class Towar : INotifyPropertyChanged
    {
        
        public int TowarId { get; set; }
        public virtual Garnitur Garnitur { get; set; }
        private bool _czyZarezerwowany;
        public bool czyZarezerwowany
        {
            get => _czyZarezerwowany;
            set
            {
                if (value == _czyZarezerwowany) return;
                _czyZarezerwowany = value;
                OnPropertyChanged();
            }
        }

        public double koszt { get; set; }

        public Towar()
        {
            getClassName();
        }

        //This attribute contains class name (DataGrid binding)
        public string className { get; set; }

        /// <summary>
        /// This metod set a value to <c>className</c> attribute.
        /// Because of using Lazy Loading Proxies 
        /// (see <see cref="MAS_PROJECT.dbmagazyn.dbmagazynContext
        /// .OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)"/> 
        /// method) it wraps classes in <ClassName>Proxy classes. 
        /// </summary>
        public void getClassName()
        {
            className = this.GetType()
                .Name.Split('P')[0];
        }

        //Interface implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        

    }
}
