using FIT.Data;
using FIT.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIT.WinForms
{
    public partial class frmPretraga : Form
    {
        DLWMSDbContext db = new DLWMSDbContext();
        public frmPretraga()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            LoadData();
        }

        private void frmPretraga_Load(object sender, EventArgs e)
        {
            
            comboBox1.DataSource = null;
            comboBox1.DataSource = db.Drzave.ToList();

            var drzava = comboBox1.SelectedItem as Drzave;

            if (drzava != null)
            {

                comboBox2.DataSource = null;
                comboBox2.DataSource = db.Gradovi.Where(x => x.DrzaveId == drzava.Id).ToList();
                if (dataGridView1.RowCount == 0)
                {
                    MessageBox.Show("NE POSTOJI!!!");
                }
            }
            
        }
        public void LoadData()
        {
            var grad = comboBox2.SelectedItem as Gradovi;

            if (grad != null)
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = db.Studenti.Where(x => x.GradoviID == grad.Id).ToList();
                if (dataGridView1.RowCount == 0)
                {
                    MessageBox.Show("NE POSTOJI!!!");
                }
            }

            var lista = db.Studenti.ToList();
            foreach (var item in lista)
            {
                item.Prosek = (float)(db.PolozeniPredmeti.
                    Where(p => p.StudentId == item.Id).Count() > 0
                    ? db.PolozeniPredmeti.Where(p => p.StudentId == item.Id)
                    .Average(p => p.Ocjena) : 5);
            }

           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drzava = comboBox1.SelectedItem as Drzave;

            if (drzava != null)
            {

                comboBox2.DataSource = null;
                comboBox2.DataSource = db.Gradovi.Where(x => x.DrzaveId == drzava.Id).ToList();
                if (dataGridView1.RowCount == 0)
                {
                    MessageBox.Show("NE POSTOJI!!!");
                }
            }
            


        }
    }
}
