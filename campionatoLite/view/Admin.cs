using System;
using System.Windows.Forms;

namespace campionatoLite
{
    public partial class Admin : Form
    {
        private Form _activeChildForm;

        public Admin()
        {
            InitializeComponent();
        }

        private void OpenChildForm(Form childForm)
        {
            if (_activeChildForm != null)
            {
                _activeChildForm.Close();
            }

            _activeChildForm = childForm;
            _activeChildForm.TopLevel = false;
            _activeChildForm.FormBorderStyle = FormBorderStyle.None;
            _activeChildForm.Dock = DockStyle.Fill;
            panel2.Controls.Add(_activeChildForm);
            panel2.Tag = _activeChildForm;
            _activeChildForm.BringToFront();
            _activeChildForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ViewSquadra());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ViewGiocatore());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ViewPartita());
        }
    }
}