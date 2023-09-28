using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;

namespace PayrollApplication
{
    public partial class PrintingForm : Form
    {
        public PrintingForm()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            ReportDocument reportDocument = new ReportDocument();
            string cs = ConfigurationManager.ConnectionStrings["PayrollSystemDBConnectionString"].ConnectionString;
            SqlConnection objSqlConnection = new SqlConnection(cs);
            string sqlString = "SELECT * FROM tblPayrecords";
            SqlDataAdapter da = new SqlDataAdapter(sqlString, objSqlConnection);
            DataSet dataReport = new DataSet();
            da.Fill(dataReport, "tblPayrecords");

            CrystalReport2 myDataReport = new CrystalReport2();
            myDataReport.SetDataSource(dataReport);
            crystalReportViewer1.ReportSource = myDataReport;

 

        }
    }
}
