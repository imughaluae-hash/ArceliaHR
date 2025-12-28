using ArceliaHR.Database;
using ArceliaHR.Database.Repositories;
using ArceliaHR.Models;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ArceliaHR
{
    public partial class EmpForm : Form
    {
        public EmpForm()
        {
            InitializeComponent();
        }
        private EmployeeModel _employee;
        private BindingSource _bs = new BindingSource();
        public string fileName;
        private void button1_Click(object sender, EventArgs e)
        {
            //Personal Information
            string empID = empId.Text;
            string empl = empName.Text;
            string empFatherX = empFather.Text;
            string empReligionX = empReligion.Text;
            DateTime empDOBx = empDOB.Value;
            string EmpShadix = empMarital.Text;
            string empSexX = empGender.Text;
            string EmpMobileX = EmpMobile.Text;
            string empCountryX = empNationality.Text;
            string empICEX = empICE.Text;
            string empHomeNumberX = empRelation.Text;
            //string empPicture.Image;

            //Passport Information
            string passportNumberX = passportNumber.Text;
            DateTime passportIssueDateX = passportIssueDate.Value;
            DateTime passportExpiryDateX = passportExpiryDate.Value;

            //ID Card Information
            string IDCardNumberX = IDCardNumber.Text;
            string IDNumberX = IDNumber.Text;
            DateTime IDExpiryDateX = IDExpiryDate.Value;

            //Visa Information
            string work = txtWork.Text;
            string Department = cmbDepart.Text;

        }

        private void browseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Select Image";
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box

                empPicture.Image = new Bitmap(open.FileName);
                empPicture.SizeMode = PictureBoxSizeMode.StretchImage;

                fileName = open.FileName.ToString();
            }
        }
        private bool isEm()
        {
            bool rVal = false;
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    using TextBox textBox = (TextBox)c;
                    if (textBox.Text == string.Empty)
                    {
                        rVal = true;
                        MessageBox.Show("Empty");
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
            IDCardNumber.Text = "";
            IDNumber.Text = "";
            IDExpiryDate.Value = DateTime.Today;

            //Visa Information
            txtWork.Text = "";
            cmbDepart.Text = "";

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var repo = new EmployeeRepository();
            repo.Add(_employee);
            MessageBox.Show("Employee saved");
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            txtStatus.Text = txtStatus.Text == "Active" ? "DeActive" : "Active";
        }

        private void EmpForm_Load(object sender, EventArgs e)
        {
            _employee = new EmployeeModel { Status = "Active" };
            _bs.DataSource = _employee;
            BindControls();
        }
        private void BindControls()
        {
            empId.DataBindings.Add("Text", _bs, "Id");
            empName.DataBindings.Add("Text", _bs, "Name");
            empFather.DataBindings.Add("Text", _bs, "FatherName");
            empReligion.DataBindings.Add("Text", _bs, "Religion");
            empMarital.DataBindings.Add("Text", _bs, "MaritalStatus");
            empGender.DataBindings.Add("Text", _bs, "Gender");
            empDOB.DataBindings.Add("Value", _bs, "DateOfBirth");

            EmpMobile.DataBindings.Add("Text", _bs, "Mobile");
            empICE.DataBindings.Add("Text", _bs, "ICEContact");
            empNationality.DataBindings.Add("Text", _bs, "Nationality");
            empRelation.DataBindings.Add("Text", _bs, "Relation");

            passportNumber.DataBindings.Add("Text", _bs, "PassportNumber");
            passportIssueDate.DataBindings.Add("Value", _bs, "PassportIssueDate");
            passportExpiryDate.DataBindings.Add("Value", _bs, "PassportExpiryDate");

            IDNumber.DataBindings.Add("Text", _bs, "IDNumber");
            IDCardNumber.DataBindings.Add("Text", _bs, "IDCardNumber");
            IDExpiryDate.DataBindings.Add("Value", _bs, "IDExpiryDate");

            txtWork.DataBindings.Add("Text", _bs, "Work");
            cmbDepart.DataBindings.Add("Text", _bs, "Department");

            txtStatus.DataBindings.Add("Text", _bs, "Status");
        }

    }
}

