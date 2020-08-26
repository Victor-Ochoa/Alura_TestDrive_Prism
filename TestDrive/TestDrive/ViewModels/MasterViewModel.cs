using Prism.Navigation;
using TestDrive.Core;

namespace TestDrive.ViewModels
{
    public class MasterViewModel : ViewModelBase
    {
        public MasterViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
    }
}
