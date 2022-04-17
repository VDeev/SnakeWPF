using SnakeWPF.ViewModel;
using System.Windows;

namespace SnakeWPF.View
{
    public partial class SnakeView : Window
    {
        #region Fields
        #endregion

        #region Constructors

        public SnakeView()
        {
            InitializeComponent();

            SnakeViewModel viewModel = new SnakeViewModel();
            DataContext = viewModel;
        }

        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Methods
        #endregion
    }
}
