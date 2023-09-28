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

namespace PayrollApplication
{
    public partial class PayrollCalculatorForm : Form
    {
        //------Global variable
        string FullName = string.Empty;

        //Declaring variable Each day of Week
        double Sat1 = 0.00, Sun1 = 0.00, Mon1 = 0.00, Tue1 = 0.00, Wed1 = 0.00, Thu1 = 0.00, Fri1 = 0.00;
        double Sat2 = 0.00, Sun2 = 0.00, Mon2 = 0.00, Tue2 = 0.00, Wed2 = 0.00, Thu2 = 0.00, Fri2 = 0.00;
        double Sat3 = 0.00, Sun3 = 0.00, Mon3 = 0.00, Tue3 = 0.00, Wed3 = 0.00, Thu3 = 0.00, Fri3 = 0.00;
        double Sat4 = 0.00, Sun4 = 0.00, Mon4 = 0.00, Tue4 = 0.00, Wed4 = 0.00, Thu4 = 0.00, Fri4 = 0.00;

        private void btnBack_Click(object sender, EventArgs e)
        {
            EmployeeForm form = new EmployeeForm();
            this.Hide();
            form.ShowDialog();
            this.Visible = true;
        }

        // Declaring ContractualHoursOfWeek, TotalHoursWorked
        double ContractualHoursWk1, ContractualHoursWk2, ContractualHoursWk3, ContractualHoursWk4;

        // Declaring Earnigng
        double ContractualAmountWk1, ContractualAmountWk2, ContractualAmountWk3, ContractualAmountWk4;
        double OvertimeAmountWk1, OvertimeAmountWk2, OvertimeAmountWk3, OvertimeAmountWk4;
        double TotalAmountEarnings;

        // Net Payable
        double NetPay;

        public PayrollCalculatorForm()
        {
            InitializeComponent();
        }

        private void btnComputePay_Click(object sender, EventArgs e)
        {
            if (ValidateControls())
            {
                GetWeeks1Value();
                GetWeeks2Value();
                GetWeeks3Value();
                GetWeeks4Value();
            }
            #region --- First Week Amount Calculation

            ContractualHoursWk1 = 36;
            ContractualAmountWk1 = 40 * ContractualHoursWk1;
            OvertimeAmountWk1 = 30 - ContractualHoursWk1;
            OvertimeAmountWk1 = 20 * 25;
            #endregion

            #region --- Second Week Amount Calculation

            ContractualHoursWk2 = 36;
            ContractualAmountWk2 = 40 * ContractualHoursWk2;
            OvertimeAmountWk2 = 30 - ContractualHoursWk2;
            OvertimeAmountWk2 = 20 * 25;
            #endregion

            #region Third Week Amount Calculation

            ContractualHoursWk3 = 36;
            ContractualAmountWk3 = 40 * ContractualHoursWk3;
            OvertimeAmountWk3 = 30 - ContractualHoursWk3;
            OvertimeAmountWk3 = 20 * 25;
            #endregion

            #region Fourth Week Amount Calculation

            ContractualHoursWk4 = 36;
            ContractualAmountWk4 = 40 * ContractualHoursWk4;
            OvertimeAmountWk4 = 30 - ContractualHoursWk4;
            OvertimeAmountWk4 = 20 * 25;
            #endregion

            txtTotalHoursWorked.Text = "150";
            txtTotalEarnings.Text = "20000";
            txtNetPay.Text = "20000";
        }

        private void btnSavePay_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["PayrollSystemDBConnectionString"].ConnectionString;
            SqlConnection objSqlConnection = new SqlConnection(cs);
            string insertCommand = "INSERT INTO tblPayrecords" +
                "(EmployeeID, FullName,PayDate,PayPeriod,PayMonth,HourlyRate,ContractualHours,TotalHours,ContactualEarnings,OvertimeEarnings,TotalEarnings,NeyPay)" +
                "VAlUES (@EmployeeID, @FullName,@PayDate,@PayPeriod,@PayMonth,@HourlyRate,@ContractualHours,@TotalHours,@ContactualEarnings,@OvertimeEarnings,@TotalEarnings,@NeyPay)";
            SqlCommand objInsertCommand = new SqlCommand(insertCommand, objSqlConnection);
            objInsertCommand.Parameters.AddWithValue("@EmployeeID", txtEmployeeID.Text);
            objInsertCommand.Parameters.AddWithValue("@FullName", lblEmployeeFullName.Text);
            objInsertCommand.Parameters.AddWithValue("@PayDate", dtpPayDate.Value.ToString("MM/dd/yyyy"));
            objInsertCommand.Parameters.AddWithValue("@PayPeriod", listBoxPayPeriod.SelectedItem.ToString());
            objInsertCommand.Parameters.AddWithValue("@PayMonth", cmbCurrentMonth.SelectedItem.ToString());
            objInsertCommand.Parameters.AddWithValue("@HourlyRate", nudHourlyRate.Value.ToString());
            objInsertCommand.Parameters.AddWithValue("@ContractualHours", txtContracturalHours.Text);
            objInsertCommand.Parameters.AddWithValue("@TotalHours", txtTotalHoursWorked.Text);
            objInsertCommand.Parameters.AddWithValue("@ContactualEarnings", txtContractualEarnings.Text);
            objInsertCommand.Parameters.AddWithValue("@OvertimeEarnings", txtOvertimeEarnings.Text);
            objInsertCommand.Parameters.AddWithValue("@TotalEarnings", txtTotalEarnings.Text);
            objInsertCommand.Parameters.AddWithValue("@NeyPay", txtNetPay.Text);
            try
            {
                objSqlConnection.Open();
                objInsertCommand.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objSqlConnection.Close();
            }
            // TODO: This line of code loads data into the 'payrollSystemDBDataSet1.tblPayrecords' table. You can move, or remove it, as needed.
            this.tblPayrecordsTableAdapter.Fill(this.payrollSystemDBDataSet1.tblPayrecords);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reset Pay");
        }

        private void btnPrintPay_Click(object sender, EventArgs e)
        {
            PrintingForm frm = new PrintingForm();
            this.Hide();
            frm.ShowDialog();
            this.Visible= true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["PayrollSystemDBConnectionString"].ConnectionString;
            SqlConnection objSqlConnection = new SqlConnection(cs);
            objSqlConnection.Open();
            string SelectCommand = "SELECT FirstName, LastName FROM tblEmployee WHERE EmployeeID = "+txtEmployeeID.Text+"";
            SqlCommand objSqlCommand = new SqlCommand(SelectCommand,objSqlConnection);
            try
            {
                SqlDataReader DataReader = objSqlCommand.ExecuteReader();
                if (DataReader.Read())
                {
                    txtFirstName.Text = DataReader[0].ToString();
                    txtLastName.Text = DataReader[1].ToString();
                    FullName = txtFirstName.Text + " " + txtLastName.Text;
                    lblEmployeeFullName.Text = FullName;
                }
                else
                {
                    MessageBox.Show("No Records found " + txtEmployeeID.Text ); 
                }
                DataReader.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message, "Error Message");
            }
            finally
            {
                objSqlConnection.Close();
            }
        }

        private void ListOfMonths()
        {
            string[] months = new string[13];
            months[0] = "Select a Month";
            months[1] = "January";
            months[2] = "February";
            months[3] = "March";
            months[4] = "April";
            months[5] = "May";
            months[6] = "June";
            months[7] = "July";
            months[8] = "August";
            months[9] = "September";
            months[10] = "October";
            months[11] = "November";
            months[12] = "December";

            foreach(var  month in months)
            {
                cmbCurrentMonth.Items.Add(month);
                cmbCurrentMonth.SelectedIndex = 0;
            }
        }

        private void PayrollCalculatorForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'payrollSystemDBDataSet1.tblPayrecords' table. You can move, or remove it, as needed.
            this.tblPayrecordsTableAdapter.Fill(this.payrollSystemDBDataSet1.tblPayrecords);
            ListOfMonths();

        }
        private void GetWeeks1Value()
        {
            Sat1 = double.Parse(nudSat1.Value.ToString());
            Sun1 = double.Parse(nudSun1.Value.ToString());
            Mon1 = double.Parse(nudMon1.Value.ToString());
            Tue1 = double.Parse(nudTue1.Value.ToString());
            Wed1 = double.Parse(nudWed1.Value.ToString());
            Thu1 = double.Parse(nudThu1.Value.ToString());
            Fri1 = double.Parse(nudFri1.Value.ToString());
        }
        private void GetWeeks2Value()
        {
            Sat2 = double.Parse(nudSat2.Value.ToString());
            Sun2 = double.Parse(nudSun2.Value.ToString());
            Mon2 = double.Parse(nudMon2.Value.ToString());
            Tue2 = double.Parse(nudTue2.Value.ToString());
            Wed2 = double.Parse(nudWed2.Value.ToString());
            Thu2 = double.Parse(nudThu2.Value.ToString());
            Fri2 = double.Parse(nudFri2.Value.ToString());
        }
        private void GetWeeks3Value()
        {
            Sat3 = double.Parse(nudSat3.Value.ToString());
            Sun3 = double.Parse(nudSun3.Value.ToString());
            Mon3 = double.Parse(nudMon3.Value.ToString());
            Tue3 = double.Parse(nudTue3.Value.ToString());
            Wed3 = double.Parse(nudWed3.Value.ToString());
            Thu3 = double.Parse(nudThu3.Value.ToString());
            Fri3 = double.Parse(nudFri4.Value.ToString());
        }
        private void GetWeeks4Value()
        {
            Sat4 = double.Parse(nudSat4.Value.ToString());
            Sun4 = double.Parse(nudSun4.Value.ToString());
            Mon4 = double.Parse(nudMon4.Value.ToString());
            Tue4 = double.Parse(nudTue4.Value.ToString());
            Wed4 = double.Parse(nudWed4.Value.ToString());
            Thu4 = double.Parse(nudThu4.Value.ToString());
            Fri4 = double.Parse(nudFri4.Value.ToString());
        }

        private bool ValidateControls()
        {
            if(txtEmployeeID.Text == "")
            {
                MessageBox.Show("Please Enter Employee ID");
                txtEmployeeID.Focus();
                return false;
            }
            if(cmbCurrentMonth.SelectedIndex == 0)
            {
                MessageBox.Show("Select a Month");
                txtEmployeeID.Focus();
                return false;
            }
            return true;
        }
    }
}
