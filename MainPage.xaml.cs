using Notes.Models;
using System;
using System.Collections.Generic;

namespace Notes
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CreateNotePanel();
        }

        public List<Nots> GetAllNotes()
        {
            return DatabaseProvider.Connection.Table<Nots>().ToList();
        }

        public void CreateNotePanel()
        {
            NotesStack.Children.Clear();

            var notes = GetAllNotes();

            bool isDarkMode = Application.Current.RequestedTheme == AppTheme.Dark;

            foreach (var note in notes)
            {
                var grid = new Grid
                {
                    ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto }
            }
                };

                var frame = new Frame
                {
                    HeightRequest = 60,
                    Padding = 10,
                    CornerRadius = 10,
                    BackgroundColor = isDarkMode ? Colors.Black : Colors.White, 
                    BorderColor = isDarkMode ? Colors.Gray : Colors.LightGray
                };

                var tap = new TapGestureRecognizer();
                tap.Tapped += async (s, e) =>
                {
                    await Navigation.PushModalAsync(new NotesEditPage(note));
                };

                frame.GestureRecognizers.Add(tap);

                var innerGrid = new Grid
                {
                    ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto }
            }
                };

                var titleLabel = new Label
                {
                    Text = note.Name,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = isDarkMode ? Colors.White : Colors.Black 
                };

                var dateLabel = new Label
                {
                    Text = note.CreatedAt,
                    FontSize = 12,
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = isDarkMode ? Colors.LightGray : Colors.Gray
                };

                innerGrid.Add(titleLabel, 0, 0);
                innerGrid.Add(dateLabel, 1, 0);

                frame.Content = innerGrid;
                grid.Add(frame);

                NotesStack.Children.Add(grid);
            }
        }


        private async void OnAddNoteClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NotesEditPage());
        }
    }
}
