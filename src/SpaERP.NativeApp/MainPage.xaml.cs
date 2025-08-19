
using System.Net.Http.Json;
using SpaERP.Models;

namespace SpaERP.NativeApp;

public partial class MainPage : ContentPage
{
	int count = 0;
	HttpClient Client;

	public MainPage(HttpClient client)
	{
		InitializeComponent();
		Client = client; // Use the injected HttpClient instance
	}

	private void OnCounterClicked(object? sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	private async void OnGetUsersClicked(object? sender, EventArgs e)
	{

		var users = await Client.GetFromJsonAsync<List<User>>("api/users");
		if (users == null || !users.Any())
		{
			await DisplayAlert("Error", "No users found.", "OK");
			return;
		}
		await DisplayAlert("Users", string.Join(", ", users.Select(u => $"{u.FirstName} {u.LastName}")), "OK");
	}
}
