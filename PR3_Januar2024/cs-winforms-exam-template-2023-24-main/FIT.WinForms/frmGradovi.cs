using FIT.Data;
using FIT.Infrastructure;
using FIT.WinForms.Helpers;
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
    public partial class frmGradovi : Form
    {
        DLWMSDbContext db = new DLWMSDbContext();
        Drzave drzave;
        public frmGradovi(Drzave _drzave)
        {
            InitializeComponent();
            drzave = _drzave;
            dataGridView1.AutoGenerateColumns = false;
        }

        private void frmGradovi_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Ekstenzije.ToImage(drzave.Zastava);
            label1.Text = drzave.Naziv.ToString();
            LoadData();
        }

        private void LoadData()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = db.Gradovi.Include(g => g.Drzave).Where(g => g.DrzaveId == drzave.Id).ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || db.Gradovi.Where(g => g.Naziv == textBox1.Text).Any())
            {
                MessageBox.Show("Postoji isti grad ili niste uneli nista u textbox!!!");
                return;
            }
            var gradovi = new Gradovi();
            gradovi.DrzaveId = drzave.Id;
            gradovi.Naziv = textBox1.Text;
            gradovi.Status = true;
            db.Gradovi.Add(gradovi);
            db.SaveChanges();
            db.Gradovi.Update(gradovi);
            db.SaveChanges();
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (e.ColumnIndex == 2)
            {
                var gr = dataGridView1.Rows[e.RowIndex].DataBoundItem as Gradovi;
                gr.Status = !gr.Status;
                db.Gradovi.Update(gr);
                db.SaveChanges();
                LoadData();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string broj = textBox2.Text;
                if(int.TryParse(broj,out int result))
                {
                    MessageBox.Show("Uneli ste broj!!!");
                }
                else
                {
                    MessageBox.Show("Pogreska");
                    return;
                }
                for (int i = 0; i < result; i++)
                {
                    Task.Delay(300).Wait();
                    richTextBox1.Invoke(new Action(() =>
                    {
                        richTextBox1.AppendText($"{DateTime.Now.ToShortTimeString}-> dodat Grad{i}. za drzavu {drzave.Naziv}\n");
                        richTextBox1.SelectionStart=richTextBox1.Text.Length;
                        richTextBox1.ScrollToCaret();
                    }));
;                   var gr = new Gradovi();
                    gr.Status = checkBox1.Checked;
                    gr.Naziv = $"Grad{i}";
                    gr.DrzaveId = drzave.Id;
                    db.Gradovi.Add(gr);
                }
                db.SaveChanges();
                LoadData();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
