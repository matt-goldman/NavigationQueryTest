using NavigationQueryTest.ViewModels;

namespace NavigationQueryTest.Pages;

public partial class ParamPage : ContentPage
{
    private readonly ParamViewModel _viewModel;

    public ParamPage(ParamViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.Appearing();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _viewModel.Navigated();
    }
}