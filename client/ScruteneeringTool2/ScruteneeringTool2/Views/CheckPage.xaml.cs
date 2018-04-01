using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ScruteneeringTool2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScruteneeringTool2.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CheckPage : ContentPage
	{
	    private CheckViewModel viewModel;

        public CheckPage ()
		{
			InitializeComponent ();

		    BindingContext = viewModel = new CheckViewModel();
        }

	    void TakePhoto_Clicked(object sender, EventArgs e)
	    {
	        viewModel.TakePictureCommand.Execute(null);
	    }

	    void PickPhoto_Clicked(object sender, EventArgs e)
	    {
            viewModel.PickFromGalleryCommand.Execute(null);
	    }

	    void CheckLabel_Clicked(object sender, EventArgs e)
	    {
            viewModel.DoOCR.Execute(null);
	    }

        protected override void OnAppearing()
	    {
	        base.OnAppearing();

	    }
    }
}