using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;
using System.Xml;
using System.Xml.Linq;

namespace PansiyonKayitUygulamasi
{
    public partial class YeniMusteri : Form
    {
        public YeniMusteri()
        {
            InitializeComponent();
        }
        AccessBaglanti bgl = new AccessBaglanti();
        public void YeniMusteri_Load(object sender, EventArgs e)
        {
            try
            {
                #region HAVA DURUMU
                XDocument hava = XDocument.Load(baglanti);
                txtnem.Text = "%" + hava.Descendants("humidity").ElementAt(0).Attribute("value").Value.ToString();
                txtsicak.Text = hava.Descendants("temperature").ElementAt(0).Attribute("value").Value.ToString() + "°C";

                txtmaxsicak.Text = hava.Descendants("temperature").ElementAt(0).Attribute("max").Value.ToString() + "°C";
                txtminsicak.Text = hava.Descendants("temperature").ElementAt(0).Attribute("min").Value.ToString() + "°C";
                txtbasinc.Text = hava.Descendants("pressure").ElementAt(0).Attribute("value").Value.ToString() + "hPa";
                txtrüzgar.Text = hava.Descendants("city").ElementAt(0).Attribute("name").Value.ToString();
                #endregion
            }
            catch 
            {
                MessageBox.Show("İnternet bağlantınız yok");
                MessageBox.Show("Hava Durumu Gösterilemeyecek");
            } 

            Odadurum();
            listele();
            timer1.Start();

        }


        private const string api = "7543d63ceff6141e38b8f79279d4bf4e";
        private const string baglanti = "http://api.openweathermap.org/data/2.5/weather?q=Kutahya&mode=xml&units=metric&APPID=" + api;
        #region LİSTE GÜNCELLEME
        public void listele()
        {
            listView1.Items.Clear();
            OleDbCommand komut = new OleDbCommand("select * from Musteri where not OdaNo=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", 0);
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = dr["ID"].ToString();
                ekle.SubItems.Add(dr["Ad"].ToString());
                ekle.SubItems.Add(dr["Soyad"].ToString());
                ekle.SubItems.Add(dr["Tel"].ToString());
                ekle.SubItems.Add(dr["Tc"].ToString());
                ekle.SubItems.Add(dr["Mail"].ToString());
                ekle.SubItems.Add(dr["OdaNo"].ToString());
                ekle.SubItems.Add(dr["GirisTarih"].ToString());
                ekle.SubItems.Add(dr["CikisTarih"].ToString());
                ekle.SubItems.Add(dr["Girilen"].ToString());
                listView1.Items.Add(ekle);

            }
            bgl.baglanti().Close();

            listView2.Items.Clear();
            OleDbCommand komut2 = new OleDbCommand("select * from GelirGider ", bgl.baglanti());
            OleDbDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                string gider = dr2[4].ToString();
                string ödendi = dr2[5].ToString();
                ListViewItem ekle = new ListViewItem();
                ekle.Text = dr2["ID"].ToString();
                ekle.SubItems.Add(dr2["ay"].ToString());
                ekle.SubItems.Add(dr2["tanım"].ToString());
                ekle.SubItems.Add(dr2["tutar"].ToString());
                if (gider == "True") { ekle.SubItems.Add("Gider"); } else { ekle.SubItems.Add("Gelir"); }
                if (ödendi == "True") { ekle.SubItems.Add("Ödendi"); } else { ekle.SubItems.Add("Ödenmedi"); }



                listView2.Items.Add(ekle);

            }
            bgl.baglanti().Close();
            string suan = DateTime.Now.ToString("MM");
            OleDbCommand komut3 = new OleDbCommand("select * from GelirGider where ay=@p1 and gider=@p3 and odendi=@p4", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", suan);
            komut3.Parameters.AddWithValue("@p3", false);
            komut3.Parameters.AddWithValue("@p4", false);
            OleDbDataReader dr3 = komut3.ExecuteReader();
            int sayac = 0;
            while (dr3.Read())
            {
                sayac += int.Parse(dr3[3].ToString());
                textBox2.Text = sayac.ToString();
            }
            bgl.baglanti().Close();
            OleDbCommand komut4 = new OleDbCommand("select * from GelirGider where ay=@p1 and gider=@p3 and odendi=@p4", bgl.baglanti());
            komut4.Parameters.AddWithValue("@p1", suan);
            komut4.Parameters.AddWithValue("@p3", true);
            komut4.Parameters.AddWithValue("@p4", false);
            OleDbDataReader dr4 = komut4.ExecuteReader();
            int sayac1 = 0;
            while (dr4.Read())
            {
                sayac1 += int.Parse(dr4[3].ToString());
                textBox3.Text = sayac1.ToString();
            }
            bgl.baglanti().Close();
        }
        #endregion
        public void Odadurum()
        {
            #region combobox ataması
            txtoda.Items.Clear();
            OleDbCommand komut = new OleDbCommand("select * from Odalar ", bgl.baglanti());
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {

                string item = dr[2].ToString();

                if (item == "False")
                {
                    txtoda.Items.Add(dr[1].ToString());

                }

            }
            bgl.baglanti().Close();
            #endregion
            #region tuşboyamna
            btn101.BackColor = Color.Green;
            btn102.BackColor = Color.Green;
            btn103.BackColor = Color.Green;
            btn104.BackColor = Color.Green;
            btn201.BackColor = Color.Green;
            btn202.BackColor = Color.Green;
            btn203.BackColor = Color.Green;
            btn204.BackColor = Color.Green;
            btn301.BackColor = Color.Green;
            btn302.BackColor = Color.Green;
            btn303.BackColor = Color.Green;
            btn304.BackColor = Color.Green;
            btn401.BackColor = Color.Green;
            btn402.BackColor = Color.Green;
            btn403.BackColor = Color.Green;
            btn404.BackColor = Color.Green;

            #endregion
            #region tuş ataması
            OleDbCommand komut1 = new OleDbCommand("select * from Odalar ", bgl.baglanti());
            OleDbDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {

                if (dr1[0].ToString() == "1" && dr1[2].ToString() == "True") { btn101.BackColor = Color.Red; } else { }
                if (dr1[0].ToString() == "2" && dr1[2].ToString() == "True") { btn102.BackColor = Color.Red; } else { }
                if (dr1[0].ToString() == "3" && dr1[2].ToString() == "True") { btn103.BackColor = Color.Red; } else { }
                if (dr1[0].ToString() == "4" && dr1[2].ToString() == "True") { btn104.BackColor = Color.Red; } else { }
                if (dr1[0].ToString() == "5" && dr1[2].ToString() == "True") { btn201.BackColor = Color.Red; } else { }
                if (dr1[0].ToString() == "6" && dr1[2].ToString() == "True") { btn202.BackColor = Color.Red; } else { }
                if (dr1[0].ToString() == "7" && dr1[2].ToString() == "True") { btn203.BackColor = Color.Red; } else { }
                if (dr1[0].ToString() == "8" && dr1[2].ToString() == "True") { btn204.BackColor = Color.Red; } else { }
                if (dr1[0].ToString() == "9" && dr1[2].ToString() == "True") { btn301.BackColor = Color.Red; } else { }
                if (dr1[0].ToString() == "10" && dr1[2].ToString() == "True") { btn302.BackColor = Color.Red; } else { }
                if (dr1[0].ToString() == "11" && dr1[2].ToString() == "True") { btn303.BackColor = Color.Red; } else { }
                if (dr1[0].ToString() == "12" && dr1[2].ToString() == "True") { btn304.BackColor = Color.Red; } else { }
                if (dr1[0].ToString() == "13" && dr1[2].ToString() == "True") { btn401.BackColor = Color.Red; } else { }
                if (dr1[0].ToString() == "14" && dr1[2].ToString() == "True") { btn402.BackColor = Color.Red; } else { }
                if (dr1[0].ToString() == "15" && dr1[2].ToString() == "True") { btn403.BackColor = Color.Red; } else { }
                if (dr1[0].ToString() == "16" && dr1[2].ToString() == "True") { btn404.BackColor = Color.Red; } else { }

            }
            bgl.baglanti().Close();
            #endregion


        }
        string id;
        #region ÇIKIŞ YAP
        public void cikisyap()
        {
            OleDbCommand komut2 = new OleDbCommand("update Musteri set OdaNo=@p3 where ID=@p2", bgl.baglanti());

            komut2.Parameters.AddWithValue("@p3", 0);
            komut2.Parameters.AddWithValue("@p2", id);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();

            OleDbCommand komut = new OleDbCommand("update Odalar set OdaDurum=@p1 where OdaNo=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", false);
            komut.Parameters.AddWithValue("@p2", eskioda.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Çıkış Yapıldı");
        }
        #endregion
        int kalan;
        #region yeni müşteri
        private void Button1_Click(object sender, EventArgs e)
        {

            if (kalan < 0)
            {
                MessageBox.Show("Ücret eksi değer çıktı lütfen manuel değer girişi yapınız");
            }
            else
            {
                #region müsteri tablosu
                OleDbCommand komut = new OleDbCommand("insert into Musteri (Ad,Soyad,Tel,Tc,Mail,OdaNo,GirisTarih,CikisTarih,Girilen,CikisYapti) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", ad.Text);
                komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
                komut.Parameters.AddWithValue("@p3", txttel.Text);
                komut.Parameters.AddWithValue("@p4", txttc.Text);
                komut.Parameters.AddWithValue("@p5", txtmail.Text);
                komut.Parameters.AddWithValue("@p6", txtoda.Text);
                komut.Parameters.AddWithValue("@p7", txtgiris.Text);
                komut.Parameters.AddWithValue("@p8", txtcıkıs.Text);
                komut.Parameters.AddWithValue("@p9", txtgirilen.Text);
                komut.Parameters.AddWithValue("@p10", txtoda.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                #endregion

                #region odalar tablosu
                OleDbCommand komut2 = new OleDbCommand("update Odalar set OdaDurum=@p1 where OdaNo=@p2", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", true);
                komut2.Parameters.AddWithValue("@p2", txtoda.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                #endregion
            }
            listele();
            MessageBox.Show("Kayıt Başarıyla Tamamlandı");

        }
        #endregion


        #region FİYAT HESAPLAMA
        private void Txtcıkıs_ValueChanged(object sender, EventArgs e)
        {
            string girisG = txtgiris.Value.ToString("dd");
            string cikisG = txtcıkıs.Value.ToString("dd");
            string girisA = txtgiris.Value.ToString("MM");
            string cikisA = txtcıkıs.Value.ToString("MM");
            int ay = int.Parse(cikisA) - int.Parse(girisA);
            int gün = int.Parse(cikisG) - int.Parse(girisG);

            kalan = ((ay * 30) + gün) * 100;

            txtgirilen.Text = kalan.ToString();
        }
        #endregion
        #region YENİLE TUŞLARI
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Odadurum();
            listele();
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            listele();
            Odadurum();
        }
        #endregion
        #region DÜZENLEME ÖNCESİ İD ÇEKME
        public void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }
        #endregion
        #region MÜSTERİ DÜZENLEME
        public void düzenle()
        {
            OleDbCommand komut2 = new OleDbCommand("update Musteri set Ad=@p1,Soyad=@p2,Tel=@p3,Tc=@p4,Mail=@p5,OdaNo=@p6,GirisTarih=@p7,CikisTarih=@p8,Girilen=@p9 where ID=@p10", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", ad.Text);
            komut2.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut2.Parameters.AddWithValue("@p3", txttel.Text);
            komut2.Parameters.AddWithValue("@p4", txttc.Text);
            komut2.Parameters.AddWithValue("@p5", txtmail.Text);
            komut2.Parameters.AddWithValue("@p6", txtoda.Text);
            komut2.Parameters.AddWithValue("@p7", txtgiris.Text);
            komut2.Parameters.AddWithValue("@p8", txtcıkıs.Text);
            komut2.Parameters.AddWithValue("@p9", txtgirilen.Text);
            komut2.Parameters.AddWithValue("@p10", id);
            komut2.ExecuteReader();
            bgl.baglanti().Close();
            OleDbCommand komut = new OleDbCommand("update Odalar set OdaDurum=@p1 where OdaNo=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", true);
            komut.Parameters.AddWithValue("@p2", txtoda.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
        }
        #endregion
        private void Button2_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            cikisyap();
            Odadurum();
            listele();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            düzenle();
            listele();
            Odadurum();
        }
        #region ARA
        public void ara()
        {
            listView1.Items.Clear();
            OleDbCommand komut = new OleDbCommand("select * from Musteri WHERE Ad like'%" + textBox1.Text + "%' or Soyad like'%" + textBox1.Text + "%' or Tc like'%" + textBox1.Text + "%' or OdaNo like'%" + textBox1.Text + "%' or CikisTarih like'%" + textBox1.Text + "%' ", bgl.baglanti());
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = dr["ID"].ToString();
                ekle.SubItems.Add(dr["Ad"].ToString());
                ekle.SubItems.Add(dr["Soyad"].ToString());
                ekle.SubItems.Add(dr["Tel"].ToString());
                ekle.SubItems.Add(dr["Tc"].ToString());
                ekle.SubItems.Add(dr["Mail"].ToString());
                ekle.SubItems.Add(dr["OdaNo"].ToString());
                ekle.SubItems.Add(dr["GirisTarih"].ToString());
                ekle.SubItems.Add(dr["CikisTarih"].ToString());
                ekle.SubItems.Add(dr["Girilen"].ToString());
                listView1.Items.Add(ekle);

            }
            bgl.baglanti().Close();
        }
        #endregion
        public void Button5_Click(object sender, EventArgs e)
        {
            ara();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            ara();
        }


        bool kontrol;
        private void RadioButton1_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            kontrol = false;
        }

        private void RadioButton2_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = true;
            kontrol = true;
        }

        private void Button5_Click_1(object sender, EventArgs e)
        {
            string simdikiay = DateTime.Now.ToString("MM");
            OleDbCommand komut = new OleDbCommand("insert into GelirGider (ay,tanım,tutar,gider) values (@p1,@p2,@p3,@p4)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", simdikiay);
            komut.Parameters.AddWithValue("@p2", txttanim.Text);
            komut.Parameters.AddWithValue("@p3", int.Parse(txttutar.Text));
            komut.Parameters.AddWithValue("@p4", kontrol);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }
        int gelirid;
        #region GELİR LİSTESİ ÇEKME
        private void ListView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listView2.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                gelirid = int.Parse(listView2.Items[intselectedindex].Text);

                //do something
                //MessageBox.Show(listView1.Items[intselectedindex].Text); 
            }
            OleDbCommand komut = new OleDbCommand("select * from GelirGider where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", gelirid);
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txttanim.Text = dr[2].ToString();
                txttutar.Text = dr[3].ToString();
                string gidermi = dr[4].ToString();
                if (gidermi == "True")
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                    kontrol = true;
                }
                else
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                    kontrol = false;
                }
                string odendimi = dr[5].ToString();
                if (odendimi == "True")
                {
                    ödendi = true;
                    button7.BackColor = Color.Green;
                    button7.Text = "Ödendi";
                }
                else
                {
                    ödendi = false;
                    ödendi = false;
                    button7.BackColor = Color.Red;
                    button7.Text = "Ödenmedi";
                }


            }
            bgl.baglanti().Close();
            button5.Visible = false;
            button6.Visible = true;
            button8.Visible = true;
        }
        #endregion

        bool ödendi;
        #region GELİRGİDER DÜZENLEME
        public void gelirgiderduzenle()
        {
            OleDbCommand oleDbCommand3 = new OleDbCommand("UPDATE GelirGider set odendi=@p1 where ID=@p2", bgl.baglanti());
            oleDbCommand3.Parameters.AddWithValue("@p1", ödendi);
            oleDbCommand3.Parameters.AddWithValue("@p2", gelirid);
            oleDbCommand3.ExecuteNonQuery();
            bgl.baglanti().Close();
            OleDbCommand oleDbCommand = new OleDbCommand("UPDATE GelirGider set tanım=@p1  where ID=@p2", bgl.baglanti());
            oleDbCommand.Parameters.AddWithValue("@p1", txttanim.Text);
            oleDbCommand.Parameters.AddWithValue("@p2", gelirid);
            oleDbCommand.ExecuteNonQuery();
            bgl.baglanti().Close();
            OleDbCommand oleDbCommand1 = new OleDbCommand("UPDATE GelirGider set tutar=@p1 where ID=@p2", bgl.baglanti());
            oleDbCommand1.Parameters.AddWithValue("@p1", txttutar.Text);
            oleDbCommand1.Parameters.AddWithValue("@p2", gelirid);
            oleDbCommand1.ExecuteNonQuery();
            bgl.baglanti().Close();
            OleDbCommand oleDbCommand2 = new OleDbCommand("UPDATE GelirGider set gider=@p3 where ID=@p2", bgl.baglanti());
            oleDbCommand2.Parameters.AddWithValue("@p3", kontrol);
            oleDbCommand2.Parameters.AddWithValue("@p2", gelirid);
            oleDbCommand2.ExecuteNonQuery();
            bgl.baglanti().Close();

        }
        #endregion
        private void Button6_Click(object sender, EventArgs e)
        {
            gelirgiderduzenle();
            listele();
        }
        int a = 0;
        private void Button7_Click(object sender, EventArgs e)
        {
            if (a % 2 == 0)
            {

                ödendi = false;
                button7.BackColor = Color.Red;
                button7.Text = "Ödenmedi";
            }
            else
            {

                ödendi = true;
                button7.BackColor = Color.Green;
                button7.Text = "Ödendi";
            }
            a++;
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            button5.Visible = true;
            button6.Visible = false;
            button8.Visible = false;
        }
        int timelaps;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            label22.Text = DateTime.Now.ToString();
            timelaps++;
            if (timelaps % 10 == 0)
            {
                listele();
                Odadurum();
            }

        }

        private void YeniMusteri_MaximumSizeChanged(object sender, EventArgs e)
        {
        }

        private void ListView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listView1.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                id = listView1.Items[intselectedindex].Text;

                //do something
                //MessageBox.Show(listView1.Items[intselectedindex].Text); 
            }
            button1.Visible = false;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            OleDbCommand komut = new OleDbCommand("select * from Musteri where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", id);
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                ad.Text = dr[1].ToString();
                txtsoyad.Text = dr[2].ToString();
                txttel.Text = dr[3].ToString();
                txttc.Text = dr[4].ToString();
                txtmail.Text = dr[5].ToString();
                txtoda.Text = dr[6].ToString();
                txtgiris.Text = dr[7].ToString();
                txtcıkıs.Text = dr[8].ToString();
                txtgirilen.Text = dr[9].ToString();
                eskioda.Text = dr[10].ToString();
            }
            bgl.baglanti().Close();
        }
    }
}
