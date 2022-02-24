using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace diyetDanisanTakipSisFormluOlan.Forms
{
    public partial class FormAnaSayfa : Form
    {

        public FormAnaSayfa()
        {
            InitializeComponent();
            customizeDesign();
            fillcombobox();
        
            }

        //Uygulama ilk çalıştığında hanig panallerin görünür olacağı ayarlanıyor
        private void customizeDesign()
    {
            
        panelDnisanEkle.BringToFront();
        panelRandevuEkle.Visible = false;
        panelDiyetListesiEkle.Visible = false;
        panelOlcum.Visible = false;
        panelRandevuListele.Visible = false;
        panelOlcumListele.Visible = false;

        panelDanisanSubMenu.Visible = false;
        panelSubmenuDiyet.Visible = false;
        panelSubmenuRandevu.Visible = false;
        panelSubmenuOlcum.Visible = false;
        }
    private void hideSubmenu() // sol tarafta bulunan panallere ulaşmak için kullanılan butonlardaki (navigation) submenulerin ne zaman aktif olacağı belirleniyor
    {
        if (panelDanisanSubMenu.Visible == true)
        {
            panelDanisanSubMenu.Visible = false;
        }
        if (panelSubmenuDiyet.Visible == true)
        {
            panelSubmenuDiyet.Visible = false;
        }
        if (panelSubmenuRandevu.Visible == true)
        {
            panelSubmenuRandevu.Visible = false;
        }
            if (panelSubmenuOlcum.Visible == true)
            {
                panelSubmenuOlcum.Visible = false;
            }
        }
    private void showSubmenu(Panel submenu) //submenulerin ne zaman görünür olacağı belirleniyor
    {
        if (submenu.Visible == false)
        {
            hideSubmenu();
            submenu.Visible = true;
        }
        else
        {
            submenu.Visible = false;
        }
    }
        /****************NAVIGATION BAR BUTONLAR****************/
        private void Danisan_Click(object sender, EventArgs e)
        {
            showSubmenu(panelDanisanSubMenu);
        }

        private void danisanEkle_Click_1(object sender, EventArgs e)
        {
            panelDnisanEkle.Visible = true;
            panelRandevuEkle.Visible = false;
            panelDiyetListesiEkle.Visible = false;
            panelRandevuListele.Visible = false;
            panelOlcumListele.Visible = false;
            panelOlcum.Visible = false;
            //PanelMain.Visible = false;
            hideSubmenu();
        }

        private void Randevu_Click_1(object sender, EventArgs e)
        {
            showSubmenu(panelSubmenuRandevu);
        }

        private void randevuEkle_Click_1(object sender, EventArgs e)
        {
            panelRandevuEkle.Visible = true;
           panelDnisanEkle.Visible = false;
           // panelRandevuEkle.Visible = false;
            panelDiyetListesiEkle.Visible = false;
            panelRandevuListele.Visible = false;
            panelOlcumListele.Visible = false;
            // panelDiyetListesiEkle.Visible = false;
            panelOlcum.Visible = false;
            //panelDnisanEkle.Visible = false; 

            hideSubmenu();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            panelRandevuListele.Visible = true;
           panelDnisanEkle.Visible = false;
            panelRandevuEkle.Visible = false;
            panelDiyetListesiEkle.Visible = false;
            //panelRandevuListele.Visible = false;
            panelOlcumListele.Visible = false;
            panelOlcum.Visible = false;
            
        }
        private void DiyetListesi_Click_1(object sender, EventArgs e)
        {
            showSubmenu(panelSubmenuDiyet);
        }

        private void diyetListesiEkle_Click_1(object sender, EventArgs e)
        {
           // panelRandevuEkle.Visible = false;
            panelDiyetListesiEkle.Visible = true;
           panelDnisanEkle.Visible = false;
            panelRandevuEkle.Visible = false;
            //panelDiyetListesiEkle.Visible = false;
            panelRandevuListele.Visible = false;
            panelOlcumListele.Visible = false;
            panelOlcum.Visible = false;
            panelOlcumListele.Visible = false;
            // panelDnisanEkle.Visible = false;
            hideSubmenu();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panelOlcumListele.Visible = true;
            panelDnisanEkle.Visible = false;
            panelOlcum.Visible = false;
            panelRandevuEkle.Visible = false;
            panelDiyetListesiEkle.Visible = false;
            panelRandevuListele.Visible = false;
           
            // panelDnisanEkle.Visible = false;
            hideSubmenu();
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            showSubmenu(panelSubmenuOlcum);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        /****************DİYET LİSTESİ KAYDET VE VERİTABANINA EKLE****************/

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e) 
            //diyet listesi ekleme sayfasında, text kısmına yazı yazılınca dosyadan kaydet butonuna basılınca buradan diyetListe tableına veri ekliyor
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");

            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[diyetList]
           ([diyetListId]
           ,[Text])

     VALUES
           ('" + 1+"','" + richTextBox1.Text + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Giriş başarılı");
        }



        /***************SİSTEMDEKİ COMBOBOX'LARIN HEPSİNİ DANIŞAN İSİMLERİYLE DOLDUR***************/

        public void fillcombobox()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");
            string sql = "select * from Danisan";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
                try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    string sname = myreader.GetString(1);
                    comboBox1.Items.Add(sname);
                    comboBox2.Items.Add(sname);
                    comboBox3.Items.Add(sname);
                    comboBox4.Items.Add(sname);
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        /****************YENİ DANIŞAN EKLE****************/
        private void button1_Click_2(object sender, EventArgs e)
        {
            Random r = new Random();
            int danisan_i = r.Next();
            // danışan ekle panelinde danışan bilgilerini girdikten sonra (boy,kilo gibi) danışan ekle butonuna basınca Danışan tablosuna danışan bilgilerini ekliyor
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");

            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Danisan]
           ([danisan_id]
           ,[Ad]
           ,[DogumTarihi]
           ,[Cinsiyet]
           ,[Telefon]
           ,[Boy]
           ,[Kilo])
     VALUES
           ('" +danisan_i + "','" + adTextBox.Text + "','" +dogumTarihiDateTimePicker.Value.ToString("yyyy-MM-dd") + "','" + cinsiyetCheckBox.Checked.ToString() + "','" + telefonTextBox.Text + "','" + boyTextBox.Text + "','" + kiloTextBox.Text + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Giriş başarılı");
           

        }

        /****************RANDEVU EKLE****************/

        private void button3_Click(object sender, EventArgs e)
        {
            //randevu ver butonu ile seçili kullanıcıya seçili tarihteki randevu atanıyor
            Random r = new Random();
            int randevu_i = r.Next();
            SqlConnection constr = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");
            string comboBoxItem = comboBox1.SelectedItem.ToString();
            string sql = "select danisan_id from Danisan where Ad = '" + comboBoxItem + "' ";

            SqlCommand cmdstr = new SqlCommand(sql, constr);

            int danisan_id;

            constr.Open();
            danisan_id = (Int32)cmdstr.ExecuteScalar();
            constr.Close();

            //richTextBox2
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[RandevuTable]
           ([randevu_id]
           ,[randevu_danisanId]
           ,[randevu_tarih]
           ,[diyetisyenId]
           ,[randevu_saat]
           ,[not_text])
     VALUES
           ('" + randevu_i + "','" + danisan_id + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + 10 + "','" + "01:50 AM" + "','" + richTextBox2.Text + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Giriş başarılı");
        }




        private void button4_Click(object sender, EventArgs e)
        {
            showSubmenu(panelSubmenuOlcum);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panelOlcumListele.Visible = false;
            panelDnisanEkle.Visible = false;
            panelOlcum.Visible = true; 
            panelRandevuEkle.Visible = false;
            panelDiyetListesiEkle.Visible = false;
            panelRandevuListele.Visible = false;
            panelOlcumListele.Visible = false;

            hideSubmenu();
        }

        /****************ÖLÇÜM EKLE****************/

        private void button6_Click(object sender, EventArgs e)
        {
            //ölçüm ekle butonu ile seçili kullanıcıya seçili tarihteki ölçüm bilgileri ekleniyor
            Random r = new Random();
            int olcum_i = r.Next();
            SqlConnection constr = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");
            string comboBoxItem = comboBox2.SelectedItem.ToString();
            string sql = "select danisan_id from Danisan where Ad = '" + comboBoxItem + "' ";

            SqlCommand cmdstr = new SqlCommand(sql, constr);

            int danisan_id;

            constr.Open();
            danisan_id = (Int32)cmdstr.ExecuteScalar();
            constr.Close();

            //richTextBox2
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(@"INSERT INTO[dbo].[Olcum]
            ([olcum_id]
          ,[boy]
          ,[kilo]
          ,[bel_cevresi]
          ,[olcum_tarihi]
          ,[danisan_id])
     VALUES
           ('" + olcum_i + "','" + textBoxBoy.Text  + "','" + textBox3.Text /*kilo*/ + "','" + textBox1.Text + "','" + dateTimePicker3.Value.ToString("yyyy-MM-dd") + "','" + danisan_id  + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Giriş başarılı");

        }


        /***************RANDEVULARI GÖRÜNTÜLE***************/
        public void fillListBox()
        {
            
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");
            string comboBoxItem = comboBox3.SelectedItem.ToString();
            string sql = "select randevu_tarih from Danisan,RandevuTable where Ad = '" + comboBoxItem + "' AND danisan_id = randevu_danisanId";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    string sname = Convert.ToDateTime(myreader["randevu_tarih"]).ToString("yyyy-MM-dd");//myreader.GetString(0);
                    listBox1.Items.Add(sname);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void button7_Click_1(object sender, EventArgs e)
        { //Listele butonuna basıldığında randevu listelemek için sql querysi çalışacak 
           
            listBox1.DataSource = null;
            listBox1.Items.Clear();

            fillListBox();
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");
            string comboBoxItem = " ";
            if (listBox1.SelectedItem != null)
            {
                 comboBoxItem = listBox1.SelectedItem.ToString();
            }
            string sql = "select randevu_saat,not_text from Danisan,RandevuTable where randevu_tarih = '" + comboBoxItem + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    string randevu_saat = Convert.ToDateTime(myreader["randevu_saat"]).ToString("dd/MM/yyyy");//myreader.GetString(0);
                    textBox2.Text = randevu_saat;
                    string randevu_not = myreader.GetString(1);
                    textBox4.Text = randevu_not;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");
            string comboBoxItem = comboBox3.SelectedItem.ToString();
            string sql = "select randevu_saat,not_text from Danisan,RandevuTable where Ad = '" + comboBoxItem + "' AND danisan_id = randevu_danisanId";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    string randevu_saat = Convert.ToDateTime(myreader["randevu_saat"]).ToString("dd/MM/yyyy");//myreader.GetString(0);
                    textBox2.Text = randevu_saat;
                    string randevu_not = myreader.GetString(1);
                    textBox4.Text = randevu_not;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /***************ÖLÇÜLERİ GÖRÜNTÜLE***************/



        private void button10_Click(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();

            fillListBox();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");
            string comboBoxItem = " ";
            if (listBox2.SelectedItem != null)
            {
                comboBoxItem = listBox2.SelectedItem.ToString();
            }

            //DateTime enteredDate = DateTime.ParseExact(comboBoxItem, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            string sql = "select boy,kilo, bel_cevresi,olcum_tarihi from Danisan,Olcum where Ad = '" + comboBoxItem + "' AND danisan_id = Olcum.danisan_id";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    string boy = myreader.GetString(0);
                    string kilo = myreader.GetString(1);
                    string bel_cevresi = myreader.GetString(2);
                    // string olcum_tarihi = Convert.ToDateTime(myreader["olcum_tarihi"]).ToString("dd/MM/yyyy");//myreader.GetString(0);
                    textBoxBoyGoruntule.Text = boy;
                    textBoxKilo.Text = kilo;
                    textBoxBel.Text = bel_cevresi;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");
            string comboBoxItem = comboBox4.SelectedItem.ToString();
            string sql = "select boy,kilo, bel_cevresi,olcum_tarihi from Danisan,Olcum where Ad = '" + comboBoxItem + "' AND danisan_id = Olcum.danisan_id";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    string boy = myreader.GetString(0);
                    string kilo = myreader.GetString(1);
                    string bel_cevresi = myreader.GetString(2);
                    
                    textBoxBoyGoruntule.Text = boy;
                    textBoxKilo.Text = kilo;
                                     
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /*************** END***************/







        private void button3_Click_1(object sender, EventArgs e)
        {
            //randevu ver butonu ile seçili kullanıcıya seçili tarihteki randevu atanıyor
            Random r = new Random();
            int randevu_i = r.Next();
            SqlConnection constr = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");
            string comboBoxItem = comboBox1.SelectedItem.ToString();
            string sql = "select danisan_id from Danisan where Ad = '" + comboBoxItem + "' ";

            SqlCommand cmdstr = new SqlCommand(sql, constr);

            int danisan_id;

            constr.Open();
            danisan_id = (Int32)cmdstr.ExecuteScalar();
            constr.Close();

            //richTextBox2
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[RandevuTable]
           ([randevu_id]
           ,[randevu_danisanId]
           ,[randevu_tarih]
           ,[diyetisyenId]
           ,[randevu_saat]
           ,[not_text])
     VALUES
           ('" + randevu_i + "','" + danisan_id + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + 10 + "','" + "01:50 AM" + "','" + richTextBox2.Text + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Giriş başarılı");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();

            fillListBox();
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");
            string comboBoxItem = " ";
            if (listBox1.SelectedItem != null)
            {
                comboBoxItem = listBox1.SelectedItem.ToString();
            }

            //DateTime enteredDate = DateTime.ParseExact(comboBoxItem, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            string sql = "select randevu_saat,not_text from Danisan,RandevuTable where randevu_tarih = '" + comboBoxItem + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    string randevu_saat = Convert.ToDateTime(myreader["randevu_saat"]).ToString("dd/MM/yyyy");//myreader.GetString(0);
                    textBox2.Text = randevu_saat;
                    string randevu_not = myreader.GetString(1);
                    textBox4.Text = randevu_not;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");
            string comboBoxItem = comboBox3.SelectedItem.ToString();
            string sql = "select randevu_saat,not_text from Danisan,RandevuTable where Ad = '" + comboBoxItem + "' AND danisan_id = randevu_danisanId";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    string randevu_saat = Convert.ToDateTime(myreader["randevu_saat"]).ToString("dd/MM/yyyy");//myreader.GetString(0);
                    textBox2.Text = randevu_saat;
                    string randevu_not = myreader.GetString(1);
                    textBox4.Text = randevu_not;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click_2(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();

            fillListBox();
        }

        private void button7_Click_3(object sender, EventArgs e)
        {
                        listBox1.DataSource = null;
            listBox1.Items.Clear();

            fillListBox();

        }

        private void listBox1_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");
            string comboBoxItem = " ";
            if (listBox1.SelectedItem != null)
            {
                comboBoxItem = listBox1.SelectedItem.ToString();
            }

            //DateTime enteredDate = DateTime.ParseExact(comboBoxItem, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            string sql = "select randevu_saat,not_text from Danisan,RandevuTable where randevu_tarih = '" + comboBoxItem + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    string randevu_saat = Convert.ToDateTime(myreader["randevu_saat"]).ToString("dd/MM/yyyy");//myreader.GetString(0);
                    textBox2.Text = randevu_saat;
                    string randevu_not = myreader.GetString(1);
                    textBox4.Text = randevu_not;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void comboBox3_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");
            string comboBoxItem = comboBox3.SelectedItem.ToString();
            string sql = "select randevu_saat,not_text from Danisan,RandevuTable where Ad = '" + comboBoxItem + "' AND danisan_id = randevu_danisanId";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    string randevu_saat = Convert.ToDateTime(myreader["randevu_saat"]).ToString("dd/MM/yyyy");//myreader.GetString(0);
                    textBox2.Text = randevu_saat;
                    string randevu_not = myreader.GetString(1);
                    textBox4.Text = randevu_not;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            //randevu ver butonu ile seçili kullanıcıya seçili tarihteki randevu atanıyor
            Random r = new Random();
            int randevu_i = r.Next();
            SqlConnection constr = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");
            string comboBoxItem = comboBox1.SelectedItem.ToString();
            string sql = "select danisan_id from Danisan where Ad = '" + comboBoxItem + "' ";

            SqlCommand cmdstr = new SqlCommand(sql, constr);

            int danisan_id;

            constr.Open();
            danisan_id = (Int32)cmdstr.ExecuteScalar();
            constr.Close();

            //richTextBox2
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[RandevuTable]
           ([randevu_id]
           ,[randevu_danisanId]
           ,[randevu_tarih]
           ,[diyetisyenId]
           ,[randevu_saat]
           ,[not_text])
     VALUES
           ('" + randevu_i + "','" + danisan_id + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + 10 + "','" + "01:50 AM" + "','" + richTextBox2.Text + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Randevu Atanmıştır");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int danisan_i = r.Next();
            // danışan ekle panelinde danışan bilgilerini girdikten sonra (boy,kilo gibi) danışan ekle butonuna basınca Danışan tablosuna danışan bilgilerini ekliyor
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-I2VDD13;Initial Catalog=diyettakipsis;Integrated Security=True");

            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Danisan]
           ([danisan_id]
           ,[Ad]
           ,[DogumTarihi]
           ,[Cinsiyet]
           ,[Telefon]
           ,[Boy]
           ,[Kilo])
     VALUES
           ('" + danisan_i + "','" + adTextBox.Text + "','" + dogumTarihiDateTimePicker.Value.ToString("yyyy-MM-dd") + "','" + cinsiyetCheckBox.Checked.ToString() + "','" + telefonTextBox.Text + "','" + boyTextBox.Text + "','" + kiloTextBox.Text + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Danışan Eklendi");
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            panelOlcumListele.Visible = false;
            panelDnisanEkle.Visible = false;
            panelOlcum.Visible = true;
            panelRandevuEkle.Visible = false;
            panelDiyetListesiEkle.Visible = false;
            panelRandevuListele.Visible = false;
            panelOlcumListele.Visible = false;

            hideSubmenu();
        }
    }
}
