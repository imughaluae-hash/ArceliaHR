using ArceliaHR.Database.Repositories;
using ArceliaHR.Models;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ArceliaHR
{
    public partial class EmpForm : Form
    {


        public EmpForm(int employeeId) : this()
        {
            _employeeId = employeeId;
        }
        public EmpForm()
        {
            InitializeComponent();
        }
        private int? _employeeId;
        private EmployeeModel? _employee;
        private BindingSource _bs = new BindingSource();
        public string? fileName;
        private bool _isNewEmployee;


        private void browseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Select Image";
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                using (var img = Image.FromFile(open.FileName))
                {
                    empPicture.Image = new Bitmap(img);
                }
                //empPicture.Image = new Bitmap(open.FileName);
                empPicture.SizeMode = PictureBoxSizeMode.StretchImage;

                fileName = open.FileName.ToString();
            }
        }
        private bool isEm()
        {
            bool rVal = false;
            foreach (Control c in this.Controls)
            {
                if (c is TextBox textBox)
                {
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        MessageBox.Show("Empty");
                        return true;
                    }
                }
            }
            return rVal;
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Reseting All Fields

            //Personal Information
            empId.Text = "";
            empName.Text = "";
            empFather.Text = "";
            empReligion.Text = "";
            empDOB.Value = DateTime.Today;
            empMarital.Text = "";
            empGender.Text = "";
            EmpMobile.Text = "";
            empNationality.Text = "";
            empICE.Text = "";
            empRelation.Text = "";
            empPicture.Image = null;

            //Passport Information
            passportNumber.Text = "";
            passportIssueDate.Value = DateTime.Today;
            passportExpiryDate.Value = DateTime.Today;

            //ID Card Information
            
            IDNumber.Text = "";
            IDExpiryDate.Value = DateTime.Today;

            //Visa Information
            txtWork.Text = "";
            cmbDepart.Text = "";

            //Basic Salary
            udBasicSalary.Value = udBasicSalary.Minimum;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();
            _bs.EndEdit();

            if (_employee == null)
            {
                MessageBox.Show("Employee data is not loaded.");
                return;
            }
            var repo = new EmployeeRepository();
            _employee.Picture = ImageToBytes(empPicture.Image);

            if (_isNewEmployee)
            {
                repo.Add(_employee);
                MessageBox.Show("Employee added");
            }
            else
            {
                repo.Update(_employee);
                MessageBox.Show("Employee updated");
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            if (_employee == null) return;

            _employee.Status = _employee.Status == "Active" ? "DeActive" : "Active";

            txtStatus.Text = _employee.Status;
        }
        private byte[]? ImageToBytes(Image img)
        {
            if (img == null) return null;

            using (var ms = new MemoryStream())
            {
                using (var bmp = new Bitmap(img)) // 🔑 CLONE
                {
                    bmp.Save(ms, ImageFormat.Jpeg);
                }
                return ms.ToArray();
            }
        }
        private void EmpForm_Load(object sender, EventArgs e)
        {
            if (_employeeId.HasValue)
            {
                LoadEmployee(_employeeId.Value);
                _isNewEmployee = false;
                btnSave.Text = "Update";
            }
            else
            {
                _employee = new EmployeeModel { Status = "Active" };
                _isNewEmployee = true;
                _bs.DataSource = _employee;
                BindControls();
            }

        }
        private void LoadEmployee(int employeeId)
        {
            var repo = new EmployeeRepository();

            _employee = repo.GetById(employeeId)
                ?? throw new InvalidOperationException("Employee not found");

            _bs.DataSource = _employee;

            BindControls();
            LoadEmployeePicture();
        }
        private void LoadEmployeePicture()
        {

            if (_employee?.Picture == null || _employee.Picture.Length == 0)
                return;

            using var ms = new MemoryStream(_employee.Picture);
            empPicture.Image = Image.FromStream(ms);
            empPicture.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void BindControls()
        {
            this.DataBindings.Clear();

            foreach (Control c in Controls)
            {
                c.DataBindings.Clear();
            }
            empId.DataBindings.Add("Text", _bs, "Id");
            empName.DataBindings.Add("Text", _bs, "Name");
            empFather.DataBindings.Add("Text", _bs, "FatherName");
            empReligion.DataBindings.Add("Text", _bs, "Religion");
            empMarital.DataBindings.Add("Text", _bs, "MaritalStatus");
            empGender.DataBindings.Add("Text", _bs, "Gender");
            BindDate(empDOB, "DateOfBirth");

            EmpMobile.DataBindings.Add("Text", _bs, "Mobile");
            empICE.DataBindings.Add("Text", _bs, "ICEContact");
            empNationality.DataBindings.Add("Text", _bs, "Nationality");
            empRelation.DataBindings.Add("Text", _bs, "Relation");

            passportNumber.DataBindings.Add("Text", _bs, "PassportNumber");
            BindDate(passportIssueDate, "PassportIssueDate");
            BindDate(passportExpiryDate, "PassportExpiryDate");

            IDNumber.DataBindings.Add("Text", _bs, "IDNumber");
            
            BindDate(IDExpiryDate, "IDExpiryDate");

            txtWork.DataBindings.Add("Text", _bs, "Work");
            cmbDepart.DataBindings.Add("Text", _bs, "Department");

            txtStatus.DataBindings.Add("Text", _bs, "Status");
            udBasicSalary.DataBindings.Add("Value", _bs, "BasicSalary", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        private void BindDate(DateTimePicker picker, string propertyName)
        {
            picker.ValueChanged -= DatePicker_ValueChanged;

            picker.Format = DateTimePickerFormat.Custom;
            picker.CustomFormat = " ";
            picker.ShowCheckBox = true;

            picker.DataBindings.Add(
                "Value",
                _bs,
                propertyName,
                true,
                DataSourceUpdateMode.OnPropertyChanged,
                null
            );

            picker.Tag = propertyName; // store property name safely
            
            // Auto-check if the property has a value - use BeginInvoke to defer until after binding completes
            if (_employee != null)
            {
                var prop = typeof(EmployeeModel).GetProperty(propertyName);
                var value = prop?.GetValue(_employee) as DateTime?;
                if (value.HasValue)
                {
                    // Defer the update until after the data binding is fully established
                    this.BeginInvoke(new Action(() =>
                    {
                        picker.Checked = true;
                        picker.CustomFormat = "dd/MM/yyyy";
                    }));
                }
            }

            picker.ValueChanged += DatePicker_ValueChanged;
        }

        private void DatePicker_ValueChanged(object? sender, EventArgs e)
        {
            if (_employee == null) return;
            if (sender is not DateTimePicker picker) return;
            if (picker.Tag is not string propertyName) return;

            var prop = typeof(EmployeeModel).GetProperty(propertyName);
            if (prop == null) return;

            if (picker.Checked)
            {
                // Use .Date to save only the date part without time
                prop.SetValue(_employee, picker.Value.Date);
                picker.CustomFormat = "dd/MM/yyyy";
            }
            else
            {
                prop.SetValue(_employee, null);
                picker.CustomFormat = " ";
            }
        }


    }
}

