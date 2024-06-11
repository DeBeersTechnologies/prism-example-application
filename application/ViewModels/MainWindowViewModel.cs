using Prism.Mvvm;

namespace application.ViewModels;

public sealed class MainWindowViewModel : BindableBase
{
    private string _title = "Prism Application - demonstrating application updates";

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }
}
