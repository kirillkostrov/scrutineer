using Xamarin.Forms;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;

using ScruteneeringTool2.Views;

namespace ScruteneeringTool2
{
    public partial class App : Application
	{

		public App ()
		{
			InitializeComponent();


            MainPage = new MainPage();
        }

		protected override void OnStart ()
		{
            AppCenter.Start("uwp=be836021-5a4f-40c3-9e34-f8df193f0c26;" +
                              "android={Your Android App secret here}" +
                              "ios={Your iOS App secret here}",
                              typeof(Analytics));
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
