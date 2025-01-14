namespace GymLifts.ViewModel;

/// <summary>
/// Base view model for other view models to inherit from.
/// Contains basic functionality shared by all pages
/// </summary>
public partial class BaseViewModel : ObservableObject
{

    public BaseViewModel() 
    {
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    [ObservableProperty]
    string title;

    public bool IsNotBusy => !IsBusy;

}
