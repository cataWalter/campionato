using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using campionatoLite.controller;
using campionatoLite.model;

namespace campionatoLite
{
    public partial class ViewPartita : Form
    {
        List<Partita> _partite;
        private readonly ControllerPartita _controllerPartita = new ControllerPartita();

        public ViewPartita()
        {
            InitializeComponent();
            _partite = _controllerPartita.ReadPartite();
            dataGridView1.DataSource = _partite;

            comboBox1.Items.Add("Qualsiasi data");
            foreach (DateTime x in _partite.Select(obj => obj.Data).Distinct())
            {
                comboBox1.Items.Add(x);
            }

            comboBox1.SelectedItem = "Qualsiasi data";

            comboBox2.Items.Add("Qualsiasi giornata");
            foreach (int x in _partite.Select(obj => obj.Giornata).Distinct())
            {
                comboBox2.Items.Add(x);
            }

            comboBox2.SelectedItem = "Qualsiasi giornata";
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() != "Qualsiasi data")
            {
                dataGridView1.DataSource = _partite.Where(p => comboBox1.SelectedItem.Equals(p.Data))
                    .ToList();
            }
            else
            {
                dataGridView1.DataSource = _partite;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() != "Qualsiasi giornata")
            {
                dataGridView1.DataSource = _partite.Where(p => comboBox2.SelectedItem.Equals(p.Giornata))
                    .ToList();
            }
            else
            {
                dataGridView1.DataSource = _partite;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _controllerPartita.AttivaSimulazione();
            _partite = _controllerPartita.ReadPartite();
            dataGridView1.DataSource = _partite;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _controllerPartita.AzzeraPunteggi();
            _partite = _controllerPartita.ReadPartite();
            dataGridView1.DataSource = _partite;
        }
    }
}