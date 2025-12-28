using ArceliaHR.Database;
using ArceliaHR.Database.Repositories;
using ArceliaHR.Models;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ArceliaHR
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        
        }
        private readonly IEmployeeRepository _employees =
    new EmployeeRepository();
        private void btnAddEmp_Click(object sender, EventArgs e)
        {
            var EmpForms = new EmpForm();
            EmpForms.ShowDialog();
        }

        private void btnDatabseTest_Click(object sender, EventArgs e)
        {
            _employees.Add(new EmployeeModel
            {
                Name = "Imran"
            });
            LoadEmployees();
        }
        private void LoadEmployees()
        {
            dgList.DataSource = _employees.GetAll().ToList();
        }

    }
}
