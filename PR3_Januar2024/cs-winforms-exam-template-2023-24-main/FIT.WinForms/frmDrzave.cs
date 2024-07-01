using FIT.Data;
using FIT.Infrastructure;
using FIT.WinForms.Izvjestaji;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
    public partial class frmDrzave : Form
    {
        DLWMSDbContext db = new DLWMSDbContext();
        Drzave drzave;
        public frmDrzave()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new frmNovaDrzava().ShowDialog();
        }

        private void frmDrzave_Load(object sender, EventArgs e)
        {
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
            timer1.Start();
            LoadData();
        }

        private void LoadData()
        {
            var drzava = db.Drzave.ToList();
            foreach (var dr in drzava)
            {
                dr.BrojGradova = db.Gradovi.Include(p => p.Drzave).Where(p => p.DrzaveId == dr.Id).Count();
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = drzava;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {


            label1.Text = "Trenutno vreme->" + DateTime.Now.ToString("HH:mm:ss");

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (e.ColumnIndex == 4)
            {
                var drzava = dataGridView1.Rows[e.RowIndex].DataBoundItem as Drzave;
                new frmGradovi(drzava).ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new frmIzvjestaji().ShowDialog();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            var drzava = dataGridView1.Rows[e.RowIndex].DataBoundItem as Drzave;
            new frmNovaDrzava(drzava).ShowDialog();
            LoadData();
        }
    }
}
