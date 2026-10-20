using System;
using System.Drawing;
using System.Windows.Forms;

namespace card_info
{
    public partial class Form1 : Form
    {
        private string cardPath = @"C:\Users\user\Desktop\card.png";
        private string chipPath = @"C:\Users\user\Desktop\Desktop\chip.png";
        private string eyeOpenPath = @"C:\Users\user\Desktop\Desktop\view.png";
        private string eyeClosedPath = @"C:\Users\user\Desktop\Desktop\hiden.png";

        private PictureBox picCard = null!;
        private PictureBox picChip = null!;
        private Label lblCardNumberOnCard = null!;
        private Label lblHolderNameOnCard = null!;
        private Label lblExpiryOnCard = null!;
        private Label lblCvcOnCard = null!;
        private Button btnToggleCvc = null!;

        private TextBox txtCardNumber = null!;
        private TextBox txtHolderName = null!;
        private TextBox txtExpiryDate = null!;
        private TextBox txtCvc = null!;
        private Button btnCheck = null!;
        private ProgressBar progressBar = null!;

        private Label lblTitleNum = null!;
        private Label lblTitleName = null!;
        private Label lblTitleExpiry = null!;
        private Label lblTitleCvc = null!;

        private bool isCvcVisible = false;

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(820, 480);
            this.Text = "Card Info";
            this.StartPosition = FormStartPosition.CenterScreen;

            BuildUI();
        }

        private void BuildUI()
        {
            picCard = new PictureBox
            {
                Size = new Size(420, 260),
                Location = new Point(20, 50),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.MediumVioletRed
            };
            if (System.IO.File.Exists(cardPath))
            {
                picCard.Image = Image.FromFile(cardPath);
            }
            this.Controls.Add(picCard);

            picChip = new PictureBox
            {
                Size = new Size(50, 40),
                Location = new Point(240, 30),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Parent = picCard,
                BackColor = Color.Transparent
            };
            if (System.IO.File.Exists(chipPath))
            {
                picChip.Image = Image.FromFile(chipPath);
            }

            lblCardNumberOnCard = new Label
            {
                Text = "Kart serya nömrəsi",
                Font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 100),
                Parent = picCard,
                BackColor = Color.Transparent
            };

            lblHolderNameOnCard = new Label
            {
                Text = "Kart sahibinin ad və soyadı",
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 180),
                Parent = picCard,
                BackColor = Color.Transparent
            };

            lblExpiryOnCard = new Label
            {
                Text = "Bitmə tarixi",
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(200, 210),
                Parent = picCard,
                BackColor = Color.Transparent
            };

            lblCvcOnCard = new Label
            {
                Text = "CVC kod",
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 210),
                Parent = picCard,
                BackColor = Color.Transparent
            };

            btnToggleCvc = new Button
            {
                Size = new Size(30, 25),
                Location = new Point(90, 207),
                Parent = picCard,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent
            };
            btnToggleCvc.FlatAppearance.BorderSize = 0;

            btnToggleCvc.Click += BtnToggleCvc_Click;
            UpdateEyeIcon();

            int rightX = 470;

            lblTitleNum = new Label { Text = "Kart serya nömrəsi", Location = new Point(rightX, 30), AutoSize = true, Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold) };
            txtCardNumber = new TextBox { Location = new Point(rightX, 55), Width = 300, Font = new Font(FontFamily.GenericSansSerif, 10), MaxLength = 16 };
            txtCardNumber.TextChanged += TxtCardNumber_TextChanged;
            txtCardNumber.KeyPress += OnlyNumbers_KeyPress;

            lblTitleName = new Label { Text = "Kart sahibinin ad və soyadı", Location = new Point(rightX, 100), AutoSize = true, Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold) };
            txtHolderName = new TextBox { Location = new Point(rightX, 125), Width = 300, Font = new Font(FontFamily.GenericSansSerif, 10) };
            txtHolderName.TextChanged += TxtHolderName_TextChanged;

            lblTitleExpiry = new Label { Text = "Bitmə tarixi", Location = new Point(rightX, 170), AutoSize = true, Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold) };
            txtExpiryDate = new TextBox { Location = new Point(rightX, 195), Width = 135, Font = new Font(FontFamily.GenericSansSerif, 10), MaxLength = 5 };
            txtExpiryDate.TextChanged += TxtExpiryDate_TextChanged;
            txtExpiryDate.KeyPress += OnlyNumbers_KeyPress;

            lblTitleCvc = new Label { Text = "CVC kod", Location = new Point(rightX + 165, 170), AutoSize = true, Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold) };
            txtCvc = new TextBox { Location = new Point(rightX + 165, 195), Width = 135, Font = new Font(FontFamily.GenericSansSerif, 10), PasswordChar = '*', MaxLength = 3 };
            txtCvc.TextChanged += TxtCvc_TextChanged;
            txtCvc.KeyPress += OnlyNumbers_KeyPress;

            btnCheck = new Button
            {
                Text = "Yoxla",
                Location = new Point(rightX, 250),
                Width = 300,
                Height = 40,
                Font = new Font(FontFamily.GenericSansSerif, 11, FontStyle.Bold),
                BackColor = Color.LightGray
            };
            btnCheck.Click += BtnCheck_Click;

            progressBar = new ProgressBar
            {
                Location = new Point(rightX, 305),
                Width = 300,
                Height = 20,
                Minimum = 0,
                Maximum = 100,
                Value = 0
            };

            this.Controls.Add(lblTitleNum);
            this.Controls.Add(txtCardNumber);
            this.Controls.Add(lblTitleName);
            this.Controls.Add(txtHolderName);
            this.Controls.Add(lblTitleExpiry);
            this.Controls.Add(txtExpiryDate);
            this.Controls.Add(lblTitleCvc);
            this.Controls.Add(txtCvc);
            this.Controls.Add(btnCheck);
            this.Controls.Add(progressBar);
        }

        private void OnlyNumbers_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void UpdateEyeIcon()
        {
            string path = isCvcVisible ? eyeOpenPath : eyeClosedPath;
            if (System.IO.File.Exists(path))
            {
                btnToggleCvc.Image = Image.FromFile(path);
                btnToggleCvc.Text = "";
            }
            else
            {
                btnToggleCvc.Text = isCvcVisible ? "👁" : "🙈";
            }
        }

        private void UpdateProgressBar()
        {
            int filledCount = 0;

            if (!string.IsNullOrWhiteSpace(txtCardNumber.Text)) filledCount++;
            if (!string.IsNullOrWhiteSpace(txtHolderName.Text)) filledCount++;
            if (!string.IsNullOrWhiteSpace(txtExpiryDate.Text)) filledCount++;
            if (!string.IsNullOrWhiteSpace(txtCvc.Text)) filledCount++;

            progressBar.Value = filledCount * 25;
        }

        private void TxtCardNumber_TextChanged(object? sender, EventArgs e)
        {
            lblCardNumberOnCard.Text = string.IsNullOrWhiteSpace(txtCardNumber.Text) ? "Kart serya nömrəsi" : txtCardNumber.Text;
            UpdateProgressBar();
        }

        private void TxtHolderName_TextChanged(object? sender, EventArgs e)
        {
            lblHolderNameOnCard.Text = string.IsNullOrWhiteSpace(txtHolderName.Text) ? "Kart sahibinin ad və soyadı" : txtHolderName.Text;
            UpdateProgressBar();
        }

        private void TxtExpiryDate_TextChanged(object? sender, EventArgs e)
        {
            string text = txtExpiryDate.Text.Replace("/", "");

            if (text.Length >= 2)
            {
                txtExpiryDate.TextChanged -= TxtExpiryDate_TextChanged;
                txtExpiryDate.Text = text.Insert(2, "/");
                txtExpiryDate.SelectionStart = txtExpiryDate.Text.Length;
                txtExpiryDate.TextChanged += TxtExpiryDate_TextChanged;
            }

            lblExpiryOnCard.Text = string.IsNullOrWhiteSpace(txtExpiryDate.Text) ? "Bitmə tarixi" : txtExpiryDate.Text;
            UpdateProgressBar();
        }

        private void TxtCvc_TextChanged(object? sender, EventArgs e)
        {
            UpdateCvcDisplay();
            UpdateProgressBar();
        }

        private void BtnToggleCvc_Click(object? sender, EventArgs e)
        {
            isCvcVisible = !isCvcVisible;
            UpdateEyeIcon();
            UpdateCvcDisplay();
        }

        private void UpdateCvcDisplay()
        {
            if (string.IsNullOrWhiteSpace(txtCvc.Text))
            {
                lblCvcOnCard.Text = "CVC kod";
            }
            else
            {
                lblCvcOnCard.Text = isCvcVisible ? txtCvc.Text : new string('*', txtCvc.Text.Length);
            }
        }

        private void BtnCheck_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCardNumber.Text) ||
                string.IsNullOrWhiteSpace(txtHolderName.Text) ||
                string.IsNullOrWhiteSpace(txtExpiryDate.Text) ||
                string.IsNullOrWhiteSpace(txtCvc.Text))
            {
                MessageBox.Show("Lütfen bütün xanaları doldurun!", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Məlumatlar uğurla yoxlanıldı!", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}