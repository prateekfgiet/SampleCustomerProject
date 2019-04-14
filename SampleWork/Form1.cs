using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MiddleLayer;
using FactoryCustomer;
using System.Globalization;
using DataAccessLayer;

namespace SampleWork
{
    public partial class Form1 : Form
    {
        CustomerDal Fac = new CustomerDal();
        CustomerBase cust = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                cust = Factory.Create(cmbCustomerType.Text);
                cust.CustomerName = txtCustomerName.Text;
                cust.Address = txtAddress.Text;
                cust.PhoneNumber = txtPhoneNumber.Text;
                if (cust.GetType() != typeof(LeadCustomer))
                {
                    cust.BillDate = Convert.ToDateTime(txtBillingDate.Text);
                    cust.BillAmount = Convert.ToDecimal(txtBillingAmount.Text);
                }
                cust.CustomerType = cmbCustomerType.Text;
                string returnMessage = Fac.AddCustomer(cust);
                if (returnMessage == "SUCCESS")
                {
                    MessageBox.Show("Customer saved successfully");
                }
                else if (returnMessage == "FAILED")
                {
                    MessageBox.Show("Unable to save custome right now ,pleas try later");
                }
                else
                {
                    MessageBox.Show(returnMessage);
                }
                Clear();
                LoadGridView();
            }
            catch (Exception ex)
            {

            }
        }
        private void Clear()
        {
            txtAddress.Text = "";
            txtBillingAmount.Text = "";
            txtBillingDate.Text = "";
            txtCustomerName.Text = "";
            txtPhoneNumber.Text = "";
            cust.CustomerType = "";
            cmbCustomerType.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadGridView();
        }

        private void LoadGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Fac.GetAllCustomers();

        }

        private void cmbCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cust = Factory.Create(cmbCustomerType.Text);
            txtBillingDate.Text = DateTime.Now.ToString();
            txtBillingAmount.Text = "0";
            Clear();
        }

        private void comboRepository_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Fac = new CustomerUiFacade(comboRepository.Text);
            dataGridView1.DataSource = Fac.GetAllCustomers();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtCustomerName.Text = "";
            txtPhoneNumber.Text = "";
            txtBillingDate.Text = DateTime.Now.ToString();
            txtBillingAmount.Text = "0";
            txtAddress.Text = "";
            LoadGridView();
        }

        private void LoadData()
        {
            txtAddress.Text = cust.Address;
            txtBillingAmount.Text = cust.BillAmount.ToString();
            txtBillingDate.Text = cust.BillDate.ToString();
            txtCustomerName.Text = cust.CustomerName;
            txtPhoneNumber.Text = cust.PhoneNumber;
            cmbCustomerType.Text = cust.CustomerType;
        }


        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            cust = Fac.GetCustomerByIndex(e.RowIndex);
            StringBuilder sb = new StringBuilder();
            sb.Append("Customer Type : " + cust.CustomerType + Environment.NewLine);
            sb.Append("Customer Name : " + cust.CustomerName + Environment.NewLine);
            sb.Append("Customer PhoneNo : " + cust.PhoneNumber + Environment.NewLine);
            sb.Append("Customer BillingAmount : " + cust.BillAmount + Environment.NewLine);
            sb.Append("Customer BillingDate : " + cust.BillDate + Environment.NewLine);
            sb.Append("Customer Address : " + cust.Address + Environment.NewLine);
            
            MessageBox.Show(sb.ToString());
            //LoadData();
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cust != null)
                {
                    cust.CustomerName = txtCustomerName.Text;
                    cust.PhoneNumber = txtPhoneNumber.Text;
                    string billingAmount = txtBillingAmount.Text;
                    cust.BillAmount = string.IsNullOrEmpty(billingAmount) ? 0 : Convert.ToDecimal(billingAmount);
                    if (validateDate(txtBillingDate.Text))
                    {
                        cust.BillDate = string.IsNullOrEmpty(txtBillingDate.Text) ? DateTime.Now : Convert.ToDateTime(txtBillingDate.Text);
                    }
                    else
                    {
                        MessageBox.Show("Please enter valid date");
                    }
                    cust.Address = txtAddress.Text;
                    cust.Validate();
                }
                else
                {
                    MessageBox.Show("Please select customer type first");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtBillingAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
         (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private bool validateDate(string value)
        {
            bool IsDate = false;
            DateTime dt;
            if (DateTime.TryParseExact(value.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                IsDate = true;
            }
            return IsDate;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                cust = Fac.GetCustomer(txtCustomerName.Text);
                if (cust != null)
                {
                    string returnMessage = Fac.DeleteCustomer(cust);
                    if (returnMessage == "SUCCESS")
                    {
                        MessageBox.Show("Customer renoved successfully");
                    }
                    else if (returnMessage == "FAILED")
                    {
                        MessageBox.Show("Unable to remove custome right now ,pleas try later");
                    }
                    else
                    {
                        MessageBox.Show(returnMessage);
                    }
                }
                ClearForm();
            }
            catch (Exception ex)
            {

            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {

            try
            {
                cust = Factory.Create(cmbCustomerType.Text);
                cust.CustomerName = txtCustomerName.Text;
                cust.Address = txtAddress.Text;
                cust.PhoneNumber = txtPhoneNumber.Text;
                if (cust.GetType() == typeof(LeadCustomer))
                {
                    cust.BillDate = Convert.ToDateTime(txtBillingDate.Text);
                    cust.BillAmount = Convert.ToDecimal(txtBillingAmount.Text);
                }
                cust.CustomerType = cmbCustomerType.Text;
                string returnMessage = Fac.UpdateCustomer(cust);
                if (returnMessage == "SUCCESS")
                {
                    MessageBox.Show("Customer updated successfully");
                }
                else if (returnMessage == "FAILED")
                {
                    MessageBox.Show("Unable to update customer right now ,pleas try later");
                }
                else
                {
                    MessageBox.Show(returnMessage);
                }
                Clear();
                LoadGridView();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
