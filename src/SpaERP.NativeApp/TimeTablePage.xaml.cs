
using Microsoft.Extensions.Logging;
using SpaERP.Models;
using System.Net.Http.Json;
using Microsoft.Maui.Controls;

namespace SpaERP.NativeApp;

public partial class TimeTablePage : ContentPage
{
	HttpClient Client;

    private int DaysPerWeek = 7;
    private int HoursPerDay = 12;

	public TimeTablePage(HttpClient client)
	{
		InitializeComponent();
		Client = client; // Use the injected HttpClient instance

        BuildGrid();

        var resources = Application.Current.Resources;
    }

    private void BuildGrid()
    {
        MainGrid.RowDefinitions.Clear();
        MainGrid.ColumnDefinitions.Clear();
        MainGrid.Children.Clear();

        // Add columns for 7 days
        for (int c = 0; c <= DaysPerWeek; c++)
        {
            GridLength width = c == 0 ? new GridLength(80) : GridLength.Star;
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = width });
        }

        // Add rows for hours + header row
        for (int r = 0; r <= HoursPerDay; r++)
        {
            MainGrid.RowDefinitions.Add(new RowDefinition { Height = 40 });
        }

        // Add day headers
        string[] days = Enum.GetNames(typeof(DayOfWeek));
        for (int c = 1; c <= DaysPerWeek; c++)
        {
            var lbl = new Label
            {
                Text = days[c-1],
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            HeaderGrid.Children.Add(lbl);
            Grid.SetColumn(lbl, c);
            Grid.SetRow(lbl, 0);

        }

        // Add hour headers
        for (int r = 0; r <= HoursPerDay; r++)
        {
            var lbl = new Label
            {
                Text = $"{r + 8}:00", // 8 AM start
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            MainGrid.Children.Add(lbl);
            Grid.SetRow(lbl, r);
            Grid.SetColumn(lbl, 0); 
        }

        // Add events
        //if (Events == null) return;

        //foreach (var ev in Events)
        //{
        //    int col = (int)ev.Day + 1;
        //    int row = ev.StartTime.Hours - 8 + 1; // assuming 8 AM start
        //    int rowSpan = (int)Math.Ceiling(ev.Duration.TotalHours);

        //    var frame = new Frame
        //    {
        //        BackgroundColor = ev.Color,
        //        CornerRadius = 5,
        //        Padding = 2,
        //        Content = new Label
        //        {
        //            Text = ev.Title,
        //            FontSize = 12,
        //            TextColor = Colors.White
        //        }
        //    };

        //    MainGrid.Children.Add(frame, col, row);
        //    Grid.SetRowSpan(frame, rowSpan);
        //}
    }
}
