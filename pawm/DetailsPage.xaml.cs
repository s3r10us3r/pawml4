using pawm.ViewModels;

namespace pawm;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(DetailsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		Title = viewModel.Title;
	}
}