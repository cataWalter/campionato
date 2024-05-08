using System.Windows.Forms;
using campionatoLite.controller;

namespace campionatoLite
{
    public partial class ViewSquadra : Form
    {
        public ViewSquadra()
        {
            InitializeComponent();
            dataGridView1.DataSource = (new ControllerSquadra()).ReadSquadre();
        }


        
        
    }
}