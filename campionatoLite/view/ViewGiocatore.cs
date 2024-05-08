using System.Windows.Forms;
using campionatoLite.controller;

namespace campionatoLite
{
    public partial class ViewGiocatore : Form
    {
        public ViewGiocatore()
        {
            InitializeComponent();
            dataGridView1.DataSource = (new ControllerGiocatore()).ReadGiocatori();
        }

    }
}