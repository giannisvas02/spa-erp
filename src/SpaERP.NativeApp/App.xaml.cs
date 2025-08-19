namespace SpaERP.NativeApp;

public partial class App : Application
{

	public App(IServiceProvider serviceProvider)
	{
		InitializeComponent();
		UserAppTheme = AppTheme.Light; // Set the default theme to Light	


		var httpClient = serviceProvider.GetRequiredService<HttpClient>();
    }

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}