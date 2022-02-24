using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace diyetDanisanTakipSisFormluOlan
{
    public partial class Form1 : Form
    {
        private Form activeForm;
        public Form1()
        {
            InitializeComponent();
            
        }
      
        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void openChildForm(Form childForm, Object buttonsender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            // ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelContainer.Controls.Add(childForm);
            this.panelContainer.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            //labelTitle.Text = childForm.Text;

        }
        /***************KAYIT OL SAYFASI***************/

        private void button1_Click_1(object sender, EventArgs e)
        { //kayıt ol butonu

            if (string.IsNullOrEmpty(kayıtMailTextBox.Text))
            {
                MessageBox.Show("Lütfen mail adresinizi giriniz.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mailTextBox.Focus();
                return;
            }

            //openChildForm(new Forms.FormAnaSayfa(), sender);
            panelKayit.Visible = false;
            panelLogin.Visible = true;
            Random r = new Random();
            int n = r.Next();
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");

            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Diyetisyen]
           ([Diyetisyen_id]
           ,[diyetisyen_adSoyad]
           ,[password]
           ,[mail]
           ,[Telefon]
           ,[user_name]
           ,[Cinsiyet])
     VALUES
           ('"+n+"','" + kayitadTextBox.Text + "','" + kayıtpasswordTextBox.Text + "','" + kayıtMailTextBox.Text + "','" + diyetisyenTextBox.Text   + "','" + usernameTextBox.Text + "','" + cinsiyetCheckBox.Checked.ToString() + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Giriş başarılı");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            panelLogin.Visible = true;
            panelKayit.Visible = false;
        }

        private void kayitadTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        /***************GİRİŞ YAP SAYFASI***************/

        private void mailTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                passwordTextBox.Focus();
        }

        private void passwordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                buttonLogin.PerformClick();
        }

       
        private void buttonLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(mailTextBox.Text))
            {
                MessageBox.Show("Lütfen mail adresinizi giriniz.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mailTextBox.Focus();
                return;
            }

            try
            {
                using (TestEntities test = new TestEntities())
                {
                    var query = from x in test.Diyetisyens
                                where x.mail == mailTextBox.Text && x.password == passwordTextBox.Text
                                select x;
                    if (query.SingleOrDefault() != null)
                    {
                        MessageBox.Show("Giriş işleminiz Başarılı.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        openChildForm(new Forms.FormAnaSayfa(), sender);
                    }
                    else
                    {
                        MessageBox.Show("Mail adresiniz veya parolanız hatalı!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
