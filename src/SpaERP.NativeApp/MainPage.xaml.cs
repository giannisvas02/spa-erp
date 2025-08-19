
using System.Net.Http.Json;
using SpaERP.Models;

namespace SpaERP.NativeApp;

public partial class MainPage : ContentPage
{
	HttpClient Client;

	public MainPage(HttpClient client)
	{
		InitializeComponent();
		Client = client; // Use the injected HttpClient instance
	}

	private async void OnGoToTimeTableClicked(object? sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("///TimeTablePage");
    }
}
