using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SnakeWPF.Common
{
    public class NotificationBase : INotifyPropertyChanged
    {
        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties
        #endregion

        #region Methods
        /// <param name="propertyname"></param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        #endregion
    }
}
