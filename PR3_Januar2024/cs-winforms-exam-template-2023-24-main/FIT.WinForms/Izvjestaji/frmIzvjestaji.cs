using FIT.Data;
using FIT.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.WinForms;

namespace FIT.WinForms.Izvjestaji
{
    public partial class frmIzvjestaji : Form
    {
        DLWMSDbContext db = new DLWMSDbContext();
        public frmIzvjestaji()
        {
            InitializeComponent();
        }

        private void frmIzvjestaji_Load(object sender, EventArgs e)
        {
            var gradovi = db.Gradovi.Include(g => g.Drzave).ToList();
            var brojGradova = db.Gradovi.Count().ToString();
            var rpc = new ReportParameterCollection();
            rpc.Add(new ReportParameter("pBrojGradova",brojGradova));
            reportViewer1.LocalReport.SetParameters(rpc);

            var rdc = new ReportDataSource();
            var tabela = new DataSet1.DataTable1DataTable();
            for (int i = 0; i <gradovi.Count() ; i++)
            {
                var red = tabela.NewDataTable1Row();
                red.Rb = $"{i}".ToString();
                red.Grad = gradovi[i].Naziv.ToString();
                red.Drzava = gradovi[i].Drzave.Naziv.ToString();
                red.Status = gradovi[i].Status ? "Da" : "Ne";
                tabela.AddDataTable1Row(red);
            }
            var rds=new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = tabela;

            reportViewer1.LocalReport.DataSources.Add(rds); 

            reportViewer1.RefreshReport();
        }
    }
}
