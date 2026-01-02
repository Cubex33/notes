using Notes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public partial class NotesEditPage : ContentPage
    {
        private bool _isEdit;
        private Nots _note;
        public NotesEditPage(Nots nots = null)
        {
            InitializeComponent();

            if (nots != null)
            {
                _isEdit = true;
                _note = nots;

                TitleEditor.Text = nots.Name;
                BodyEditor.Text = nots.Description;
            }
            else
            {
                _isEdit = false;
                _note = new Nots
                {
                    CreatedAt = DateTime.Now.ToString("dd.MM.yyyy HH:mm")
                };
            }

        }

        private async void OnExitToNoteEditor(object sender, EventArgs e)
        {
            if (_isEdit &&
                string.IsNullOrWhiteSpace(TitleEditor.Text) &&
                string.IsNullOrWhiteSpace(BodyEditor.Text))
            {
                DatabaseProvider.Connection.Delete(_note);
                await Navigation.PopModalAsync();
                return;
            }

            if (!_isEdit &&
                string.IsNullOrWhiteSpace(TitleEditor.Text) &&
                string.IsNullOrWhiteSpace(BodyEditor.Text))
            {
                await Navigation.PopModalAsync();
                return;
            }

            _note.Name = TitleEditor.Text;
            _note.Description = BodyEditor.Text;

            if (_isEdit)
            {
                DatabaseProvider.Connection.Update(_note);
            }
            else
            {
                DatabaseProvider.Connection.Insert(_note);
            }

            await Navigation.PopModalAsync();
        }


    }
}
