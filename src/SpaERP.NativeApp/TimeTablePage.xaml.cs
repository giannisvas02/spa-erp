
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using SpaERP.Models;
using System.Net.Http.Json;

namespace SpaERP.NativeApp;

public partial class TimeTablePage : ContentPage
{
	HttpClient Client;
    ResourceDictionary AppResources;

    private int DaysPerWeek = 7;
    private int HoursPerDay = 12;

	public TimeTablePage(HttpClient client)
	{
		InitializeComponent();
		Client = client; // Use the injected HttpClient instance

        if (Application.Current is not null)
            AppResources = Application.Current.Resources;

        BuildGrid();
    }

    private void BuildGrid()
    {

        string[] days = Enum.GetNames(typeof(DayOfWeek));

        for (int row = 0; row <= HoursPerDay; row++)
        {
            for (int col = 1; col <= DaysPerWeek; col++)
            {
                var dayName = days[col-1];
                var today = DateTime.Today.DayOfWeek;
                var secondaryColor = AppResources["Secondary"] as Color ?? Colors.LightGray;
                var tertiaryColor = AppResources["Tertiary"] as Color ?? Colors.LightGray;

                var cell = new Border
                {
                    BackgroundColor = Colors.Transparent,
                    StrokeThickness = 1,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill
                };

                if (dayName == today.ToString())
                {
                    cell.BackgroundColor = secondaryColor; // Highlight today's column
                }

                // Handle hover effect
                var pointerGesture = new PointerGestureRecognizer();
                pointerGesture.PointerEntered += (s, e) =>
                {
                    cell.BackgroundColor = tertiaryColor;
                };
                pointerGesture.PointerExited += (s, e) =>
                {
                    cell.BackgroundColor = dayName == today.ToString() ? secondaryColor : Colors.Transparent;
                };

                cell.GestureRecognizers.Add(pointerGesture);


                if (row == 0)
                {
                    var lbl = new Label
                    {
                        Text = dayName,
                        FontSize = 14,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.End,
                        FontAttributes = FontAttributes.Bold,
                        Padding = new Thickness(0, 5)
                    };

                    cell.Content = lbl;
                    cell.GestureRecognizers.Remove(pointerGesture);
                }

                MainGrid.Children.Add(cell);
                Grid.SetColumn(cell, col);
                Grid.SetRow(cell, row);
            }
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
