using System;
using System.Windows.Forms;
using GMS_Kabbo.Data;

namespace GMS_Kabbo.Forms
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            NavigationHelper.Open<Users>(this);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UNameTb.Text) || string.IsNullOrWhiteSpace(PasswordTb.Text))
            {
                MessageBox.Show("Enter Username and Password");
                return;
            }

            try
            {
                LocalDbBootstrap.EnsureInstanceRunning();

                if (!UserRepository.ValidateCredentials(UNameTb.Text, PasswordTb.Text))
                {
                    MessageBox.Show("Invalid Information");
                    return;
                }

                NavigationHelper.Open<Dashboard>(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Login failed: " + ex.Message +
                    "\n\nEnsure LocalDB instance (localdb)\\DCGirlsHostel is running and SQL\\CreateDatabase.sql was executed.");
            }
        }
    }
}
