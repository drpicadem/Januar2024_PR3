using FIT.Data;
using FIT.Infrastructure;
using FIT.WinForms.Helpers;
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
    public partial class frmNovaDrzava : Form
    {

        DLWMSDbContext db = new DLWMSDbContext();
        Drzave drzave;
        public frmNovaDrzava(Drzave _drzave=null)
        {
            InitializeComponent();
            drzave = _drzave;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Helpers.Validator.ProvjeriUnos(textBox1, errorProvider1, Kljucevi.ReqiredValue)
                && (Helpers.Validator.ProvjeriUnos(checkBox1, errorProvider1, Kljucevi.ReqiredValue)
                && (Helpers.Validator.ProvjeriUnos(pictureBox1, errorProvider1, Kljucevi.ReqiredValue))))
            {
                try
                {
                    if (drzave==null)
                    {
                        var drzave = new Drzave();
                        drzave.Naziv = textBox1.Text;
                        drzave.Zastava = Ekstenzije.ToByteArray(pictureBox1.Image);
                        drzave.Status = checkBox1.Checked;
                        db.Drzave.Add(drzave); db.SaveChanges();
                        new frmDrzave().ShowDialog();
                    }
                    else
                    {
                        drzave.Zastava = Helpers.Ekstenzije.ToByteArray(pictureBox1.Image);
                        drzave.Naziv = textBox1.Text;
                        drzave.Status = checkBox1.Checked;
                        db.Drzave.Update(drzave); 
                        db.SaveChanges();
                        this.Close();
                    }

                }
                catch (Exception)
                {

                    MessageBox.Show("Niste lepo uneli podatke!!!");
                }
               
            }
        }

        private void frmNovaDrzava_Load(object sender, EventArgs e)
        {
            if (drzave!=null)
            {
                pictureBox1.Image = Ekstenzije.ToImage(drzave.Zastava);
                textBox1.Text = drzave.Naziv;
                checkBox1.Checked = drzave.Status;
            }
        }
    }
}
