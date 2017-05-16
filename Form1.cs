using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.CustomFormat = "MM-dd-yyyy hh:mm:ss";
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.CustomFormat = "MM-dd-yyyy hh:mm:ss";

            //Populationg ComboBoxe-s with Severities
            foreach (var item in Enum.GetValues(typeof(Log.SeverityType)))
            {
                cmbSeverityFilter.Items.Add(item);
            }
            foreach (var item in Enum.GetValues(typeof(Log.SeverityType)))
            {
                cmbSeverity.Items.Add(item);
            }
        }
        //Add new Log
        private void btnAdd_Click(object sender, EventArgs e)
        {

            Logger logger = new Logger();
            string message = txtMessag.Text;
            Log.SeverityType sevrity = (Log.SeverityType)cmbSeverity.SelectedItem;
            
            logger.Write(new Log(message,sevrity, DateTime.Now));
            MessageBox.Show("Log was added successfully");
           
        }
   
        //Filter Logs and show result in DataGridView
        private void btnFilter_Click(object sender, EventArgs e)
        {
            Logger logger = new Logger();

            Filter filter = new Filter();
            filter.Message = txtMessageFilter.Text;
            filter.StartDate = dtpStart.Value;
            filter.EndDate = dtpEnd.Value;
            filter.Severity = (Log.SeverityType)cmbSeverityFilter.SelectedIndex;
            List<Log> filteredLogs = new List<Log>();
            filteredLogs = logger.Read(filter);

            var bindingList = filteredLogs;
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;


        }

    }
}
