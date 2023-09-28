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
using System.IO;

namespace PayrollApplication
{
    public partial class EmployeeForm : Form
    {
        public EmployeeForm()
        {
            InitializeComponent();
        }
        bool IsNumberOrBackspace;
        private void txtEmployeeID_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumberOrBackspace = false;
            if(char.IsNumber(e.KeyChar) || e.KeyChar ==8)
            {
                IsNumberOrBackspace = true;
            }
            else
            {
                e.Handled = true;
            }

        }
        // Checkd Items (gender, Marital status, Member)
        string gender;
        string maritalStatus;
        bool isMember;
        private void CheckedItems()
        {
            // checked Gender
            if (rdbMale.Checked)
            {
                gender = "Male";
            }
            else
            {
                gender = "Female";
            }
            // Checked Marital status
            if (rdbMarried.Checked)
            {
                maritalStatus = "Married";
            }
            else
            {
                maritalStatus = "Single";
            } // Checked Union Member
            if (chkBoxIsMember.Checked)
            {
                isMember = true;
            }
            else
            {
                isMember = false;

            }
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["PayrollSystemDBConnectionString"].ConnectionString;
            SqlConnection objSqlConnection = new SqlConnection(cs);
            try
            {
                CheckedItems();
                objSqlConnection.Open();
                string InsertCommand = "INSERT INTO tblEmployee VALUES("+Convert.ToInt32(txtEmployeeID.Text)+", '"+txtFirstName.Text+"','"+txtLastName.Text+"', '"
                    +gender+"','"+ Path.GetFileName(txtImagePath.Text) + "','"+dateTimePicker1.Value.ToString("MM/dd/yyyy")+"','"+maritalStatus+"', '"+isMember+"', '"
                    +txtAddress.Text+"', '"+txtCity.Text+"' ,'"+cmbCourntry.SelectedItem.ToString()+"')";
                SqlCommand objSqlCommand = new SqlCommand(InsertCommand, objSqlConnection);
                objSqlCommand.ExecuteNonQuery();

                // TODO: This line of code loads data into the 'payrollSystemDBDataSet.tblEmployee' table. You can move, or remove it, as needed.
                this.tblEmployeeTableAdapter.Fill(this.payrollSystemDBDataSet.tblEmployee);
                MessageBox.Show("Employee ID " + txtEmployeeID.Text + "" + "Successfully added");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error occured", ex.Message);
            }
            finally
            {
                objSqlConnection.Close();
            }

            // image path
            File.Copy(txtImagePath.Text, Path.Combine(@"C:\Users\Admin\Desktop\Payroll\PayrollApplicationSolution\PayrollApplication\Images\", Path.GetFileName(txtImagePath.Text)), true);
        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {

        }

        // Delete Function
        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            DialogResult objDialogResult = MessageBox.Show("Are Sure Want to Delete!?", "Confirm Deletion",MessageBoxButtons.YesNo,MessageBoxIcon.Warning );
            if(objDialogResult == DialogResult.Yes)
            {
                string cs = ConfigurationManager.ConnectionStrings["PayrollSystemDBConnectionString"].ConnectionString;
                SqlConnection objSqlConnection = new SqlConnection(cs);
                try
                {
                    objSqlConnection.Open();
                    string DeleteCommand = "DELETE FROM tblEmployee WHERE EmployeeID = "+txtEmployeeID.Text+"";
                    SqlCommand objSqlCommand = new SqlCommand(DeleteCommand, objSqlConnection);
                    objSqlCommand.ExecuteNonQuery();

                    // TODO: This line of code loads data into the 'payrollSystemDBDataSet.tblEmployee' table. You can move, or remove it, as needed.
                    this.tblEmployeeTableAdapter.Fill(this.payrollSystemDBDataSet.tblEmployee);
                    MessageBox.Show("Employee ID " + txtEmployeeID.Text + "" + "Successfully Deleted");
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured", ex.Message);
                }
                finally
                {
                    objSqlConnection.Close();
                }
            }
        }
        // Clear Form
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtEmployeeID.Clear();
            txtFirstName.Clear();
            txtLastName.Text= "";
            rdbMale.Checked = false;
            rdbFemale.Checked = false;
            dateTimePicker1.Value = new DateTime(1990, 12,30);
            chkBoxIsMember.Checked = false;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'payrollSystemDBDataSet.tblEmployee' table. You can move, or remove it, as needed.
            this.tblEmployeeTableAdapter.Fill(this.payrollSystemDBDataSet.tblEmployee);

        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                string imagePath = open.FileName;
                pictureBox1.Image = new Bitmap(imagePath);
                txtImagePath.Text = imagePath;
            }
        }

        private void btnSalaryCalculator_Click(object sender, EventArgs e)
        {
            PayrollCalculatorForm form = new PayrollCalculatorForm();
            this.Hide();
            form.ShowDialog();
            this.Visible = true;
        }
    }
}
