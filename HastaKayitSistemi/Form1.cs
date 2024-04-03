using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HastaKayitSistemi
{
    public partial class Form1 : Form
    {
        public Hasta hasta;
        public List<Hasta> hastaListesi;
        public DoktorListesi doktorListesi = new DoktorListesi();
        // HastahaneListesi'ni oluşturun
        public HastahaneListesi hastahaneListesi = new HastahaneListesi();
        public List<Randevu> randevuListesi = new List<Randevu>();
        public int sec = 0;

        //private object privite;


        public Form1()
        {
            InitializeComponent();

            hastaListesi = new List<Hasta>(); // hastaListesi'ni başlat
                                              // Hazır kayıtlı hastaları ekleyin


            Hasta hasta1 = new Hasta
            {
                hastaAdiSoyadi = "Ahmet Yılmaz",
                hastaCinsiyet = "Erkek",
                hastaAdres = "Karabağlar/İzmir",
                hastaMeslek = "İşçi",
                hastaYas = 33,
                hastaTelefon = "555 111 1098",
                HastaEmail = "ahmtylmz@gmail.com",
            };

            Hasta hasta2 = new Hasta
            {
                hastaAdiSoyadi = "Ayşe Kaya",
                hastaCinsiyet = "Kadın",
                hastaAdres = "Bornova/İzmir",
                hastaMeslek = "Çalışmıyor",
                hastaYas = 42,
                hastaTelefon = "555 333 1097",
                HastaEmail = "akaya@gmail.com",
            };

            Hasta hasta3 = new Hasta
            {
                hastaAdiSoyadi = "Mehmet Acar",
                hastaCinsiyet = "Erkek",
                hastaAdres = "Buca/İzmir",
                hastaMeslek = "Emekli",
                hastaYas = 62,
                hastaTelefon = "542 771 9800",
                HastaEmail = "acar35@gmail.com",
            };

            // Hasta listesine ekleyin
            hastaListesi.Add(hasta1);
            hastaListesi.Add(hasta2);
            hastaListesi.Add(hasta3);
            GuncelleHastaListBox();

            
            Hastahane hastahane1 = new Hastahane
            {
                HastaAdi = "İzmir Devlet Hastanesi",
                HastahaneAdresi = "Konak/İzmir"
            };

            Hastahane hastahane2 = new Hastahane
            {
                HastaAdi = "Özel Sağlık Hastanesi",
                HastahaneAdresi = "Bornova/İzmir"
            };

            hastahaneListesi.HastahaneEkle(hastahane1);
            hastahaneListesi.HastahaneEkle(hastahane2);
            GuncelleHastahaneListBox();

            
            Doktor doktor1 = new Doktor
            {
                DoktorAdiSoyadi = "Dr. Mehmet Yılmaz",
                DoktorCinsiyet = "Erkek",
                DoktorHastaneAdi = "İzmir Devlet Hastanesi",
                DoktorBolum = "Genel Cerrahi",
                DoktorTelefon = "555 111 2222"
            };

            Doktor doktor2 = new Doktor
            {
                DoktorAdiSoyadi = "Dr. Ayşe Kaya",
                DoktorCinsiyet = "Kadın",
                DoktorHastaneAdi = "Özel Sağlık Hastanesi",
                DoktorBolum = "Nöroloji",
                DoktorTelefon = "555 333 4444"
            };

            doktorListesi.DoktorEkle(doktor1);
            doktorListesi.DoktorEkle(doktor2);
            GuncelleDoktorListBox();

            Randevu randevu1 = new Randevu
            {
                Hastahane = "İzmir Devlet Hastanesi",
                Bolum = "Genel Cerrahi",
                Doktor = "Dr. Mehmet Yılmaz",
                Teshis = "Kontrol",
                Tarih = DateTime.Now.AddDays(7),
                HastaTc = "12345678901"
            };

            Randevu randevu2 = new Randevu
            {
                Hastahane = "Özel Sağlık Hastanesi",
                Bolum = "Nöroloji",
                Doktor = "Dr. Ayşe Kaya",
                Teshis = "Muayene",
                Tarih = DateTime.Now.AddDays(14),
                HastaTc = "98765432101"
            };

            randevuListesi.Add(randevu1);
            randevuListesi.Add(randevu2);
            GuncelleRandevuListBox();

            txtAdiSoyadi.KeyPress += TxtAdiSoyadi_KeyPress; //Sadece harf ve boşluk girilmesi sağlandı
            txtBoxHastaAd.KeyPress += TxtAdiSoyadi_KeyPress;

            txtTc.MaxLength = 11; // TC kimlik numarası 11 haneli olmalı
            txtTc.KeyPress += TxtTc_KeyPress; // KeyPress olayını bağla

            // Klavye olaylarını dinlemek için olayları bağla
            this.KeyDown += AnaForm_KeyDown;
            this.KeyUp += AnaForm_KeyUp;
            
            tabControl1.Dock = DockStyle.Fill;
            btnKapat.Click += btnKapat_Click;


        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                hasta = new Hasta(); 
                hasta.hastaAdiSoyadi = txtBoxHastaAd.Text;
                hasta.hastaAdres = txtHastaAdres.Text;
                hasta.HastaEmail = txtHastaMail.Text;
                hasta.hastaMeslek = comboBoxHMeslek.Text;
                hasta.hastaTelefon = mTxtHastaTel.Text;
                
                if (rbErkek.Checked)
                {
                    hasta.hastaCinsiyet = "Erkek";
                }
                if (rbKadin.Checked)
                {
                    hasta.hastaCinsiyet = "Kadın";
                }

                hasta.hastaYas = (int)numericUpDownYas.Value;

                hasta.hastaFotograf = pictureBoxHastaFoto.Image;
                
                if (string.IsNullOrEmpty(txtBoxHastaAd.Text) ||
                    string.IsNullOrEmpty(txtHastaAdres.Text) ||
                    string.IsNullOrEmpty(txtHastaMail.Text) ||
                    string.IsNullOrEmpty(comboBoxHMeslek.Text) ||
                    string.IsNullOrEmpty(mTxtHastaTel.Text) ||
                    (!rbErkek.Checked && !rbKadin.Checked) ||
                    numericUpDownYas.Value == 0 ||
                    pictureBoxHastaFoto.Image == null)
                {
                    MessageBox.Show("Lütfen tüm bilgileri doldurun ve bir fotoğraf seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Bilgiler eksik olduğunda kayıt işlemi yapılmaz
                }

                hastaListesi.Add(hasta); 
                GuncelleHastaListBox();
                Temizle();
                txtAdiSoyadi.Focus();

            }

            if (tabControl1.SelectedTab == tabPage2)
            {
                // Form üzerindeki kontrollerden bilgileri al
                string adiSoyadi = txtAdiSoyadi.Text;
                string hastahane = comboDrKayitHstnList.Text;
                string bolum = cbDrBolum.Text;
                string cinsiyet = rbDrE.Checked ? "Erkek" : "Kadın";
                string telefon = mtbTelefon.Text;
                // Kontrol
                if (string.IsNullOrEmpty(txtAdiSoyadi.Text) ||
                    string.IsNullOrEmpty(comboDrKayitHstnList.Text) ||
                    string.IsNullOrEmpty(cbDrBolum.Text) ||
                    string.IsNullOrEmpty(mtbTelefon.Text) ||
                    (!rbDrE.Checked && !rbDrK.Checked)
                    )
                {
                    MessageBox.Show("Lütfen tüm bilgileri doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Bilgiler eksik olduğunda kayıt işlemi yapılmaz
                }
                // Bilgileri Doktor nesnesine kaydet
                Doktor yeniDoktor = new Doktor
                {
                    DoktorAdiSoyadi = adiSoyadi,
                    DoktorHastaneAdi = hastahane,
                    DoktorBolum = bolum,
                    DoktorCinsiyet = cinsiyet,
                    DoktorTelefon = telefon
                };

                // Doktoru listeye ekle
                doktorListesi.DoktorEkle(yeniDoktor);

                // ListBox'ı güncelle
                GuncelleDoktorListBox();
                Temizle();
            }
            if (tabControl1.SelectedTab == tabPage4)
            {

                if (checkedListBoxHastalar.CheckedItems.Count != 1 || checkedListBoxDoktorlar.CheckedItems.Count != 1)
                {
                    MessageBox.Show("Lütfen bir hasta ve bir doktor seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtBoxTeshis.Text == "")
                {
                    MessageBox.Show("Lütfen teşhis bilgisi yazınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Seçilen hastanın adını al
                string secilenHastaAdi = checkedListBoxHastalar.CheckedItems[0].ToString();

                // Seçilen doktorun adını al
                string secilenDoktorAdi = checkedListBoxDoktorlar.CheckedItems[0].ToString();

                // Teşhis bilgisini al
                string teşhis = txtBoxTeshis.Text;

                // Teşhis bilgisini ListBox'a ekle
                listBoxTeshisler.Items.Add($"{secilenHastaAdi} - {secilenDoktorAdi}: {teşhis}");

                temizleTeshis();

            }

            if (tabControl1.SelectedTab == tabPage6)
            {
                // TextBox'lardan hastahane adı ve adresini al
                string hastahaneAdi = txtHastahaneAdi.Text;
                string hastahaneAdresi = txtHastahaneAdresi.Text;

                // Kontrol: Her iki bilgi de girilmiş mi?
                if (string.IsNullOrEmpty(hastahaneAdi) || string.IsNullOrEmpty(hastahaneAdresi))
                {
                    MessageBox.Show("Hastahane adı ve adresi boş bırakılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Hastahane bilgilerini listeye ekle
                Hastahane yeniHastahane = new Hastahane
                {
                    HastaAdi = hastahaneAdi,
                    HastahaneAdresi = hastahaneAdresi
                };

                // Hastahane bilgilerini listbox'a ekle
                hastahaneListesi.Hastahaneler.Add(yeniHastahane);

                // ListBox'ı güncelle
                GuncelleHastahaneListBox();

                // Bilgileri temizle
                Temizle();
            }

            if (tabControl1.SelectedTab == tabPage5)
            {

                if (CLboxdoktor.CheckedItems.Count != 1 || CLboxHasta.CheckedItems.Count != 1)
                {
                    MessageBox.Show("Lütfen bir hasta ve bir doktor seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                // Seçilen hastanın adını al
                string secilenHastaAdi2 = CLboxHasta.CheckedItems[0].ToString();

                // Seçilen doktorun adını al
                string secilenDoktorAdi2 = CLboxdoktor.CheckedItems[0].ToString();
                string secilenDurum = "";

                if (checkBoxTahlil.Checked)
                {
                    secilenDurum = "Tahlil";
                }
                else if (checkBoxTest.Checked)
                {
                    secilenDurum = "Test";
                }
                else 
                {
                    MessageBox.Show("Test / Tahlil seçimi yapınız!!");
                    return;
                }
                // Teşhis bilgisini ListBox'a ekle
                listBoxTetkik.Items.Add($"{secilenHastaAdi2} - {secilenDoktorAdi2}: {secilenDurum}");

                TemizleTahlilTest();
            }

        }

        public void btnSec_Click(object sender, EventArgs e)
        {

            if (tabControl1.SelectedTab == tabPage1)
            {
                if (listBoxHastalar.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen öncelikle seçim yapın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Seçilen hastanın bilgilerini göstermek için bir metot çağırın
                GosterSecilenHastaBilgileri();
                MessageBox.Show("Seçtiğiniz hasta bilgileri getirildi.");
                sec = 1;
            }

            if (tabControl1.SelectedTab == tabPage2)
            {
                if (listBoxDoktorlar.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen öncelikle seçim yapın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Seçilen doktorun bilgilerini göstermek için bir metot çağırın
                GosterSecilenDoktorBilgileri();
                MessageBox.Show("Seçtiğiniz doktor bilgileri getirildi.");
                sec = 1;
            }
            if (tabControl1.SelectedTab == tabPage3)
            {
                if (listBoxRandevular.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen öncelikle seçim yapın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Seçilen doktorun bilgilerini göstermek için bir metot çağırın
                GosterSecilenRandevuBilgileri();
                MessageBox.Show("Seçtiğiniz randevu bilgileri getirildi.");
                sec = 1;
            }

            if (tabControl1.SelectedTab == tabPage6)
            {
                if (listBoxHastahaneler.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen öncelikle seçim yapın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Seçilen doktorun bilgilerini göstermek için bir metot çağırın
                GosterSecilenHastahaneBilgileri();
                MessageBox.Show("Seçtiğiniz hastahane bilgileri getirildi.");
                sec = 1;
            }


        }
        private void BtnGuncelle_Click(object sender, EventArgs e)
        { 
            if (tabControl1.SelectedTab == tabPage1)
            {
                if (sec == 0)
                {
                    MessageBox.Show("Lütfen güncellenmesi gereken bir hasta seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Güncelleme işlemleri için bir metot çağırın
                GuncelleSecilenHasta();
                sec = 0;
            }

            if (tabControl1.SelectedTab == tabPage2)
            {
                if (sec == 0)
                {
                    MessageBox.Show("Lütfen güncellenecek doktor seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Güncelleme işlemleri için bir metot çağırın
                GuncelleSecilenDoktor();
                sec = 0;
            }

            if (tabControl1.SelectedTab == tabPage3)
            {
                if (sec == 0)
                {
                    MessageBox.Show("Lütfen güncellenecek randevu seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Güncelleme işlemleri için bir metot çağırın
                GuncelleSecilenRandevu();
                sec = 0;
                TemizleRandevuBilgileri();
            }
            if (tabControl1.SelectedTab == tabPage6)
            {
                if (sec == 0)
                {
                    MessageBox.Show("Lütfen güncellenecek hastahaneyi seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                GuncelleSecilenHastahane();
                TemizleHastahane();
                sec = 0;
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage4 || tabControl1.SelectedTab== tabPage5)
            {
                sec = 1;
            }

            if(sec == 0 )
            {
                MessageBox.Show("Lütfen silme işlemi için önce seçim yapın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tabControl1.SelectedTab == tabPage1)
            {


                // Silme işlemi için bir metot çağırın
                SilSecilenHasta();
                sec = 0;
            }

            if (tabControl1.SelectedTab == tabPage2)
            {

                // Silme işlemi için bir metot çağırın
                SilSecilenDoktor();
                Temizle();
                sec = 0;
            }

            if (tabControl1.SelectedTab == tabPage3)
            {
                // Silme işlemi için bir metot çağırın
                SilSecilenRandevu();
                sec = 0;
                TemizleRandevuBilgileri();
            }
            if (tabControl1.SelectedTab == tabPage4)
            {
                SilSecilenTeshis();
                sec = 0;
            }
            if (tabControl1.SelectedTab == tabPage5)
            {
                SilSecilenTetkik();
                sec = 0;
            }
            if (tabControl1.SelectedTab == tabPage6)
            {
                SilSecilenHastahane();
                TemizleHastahane();
                sec = 0;
            }
        }
        public void GuncelleHastaListBox()
        {
            // ListBox'ı temizle
            listBoxHastalar.Items.Clear();

            // Hastaları ListBox'a ekle
            foreach (Hasta hasta in hastaListesi)
            {
                listBoxHastalar.Items.Add($"{hasta.hastaAdiSoyadi} - {hasta.hastaCinsiyet} - {hasta.hastaYas}  - {hasta.hastaMeslek}");
            }
        }

        private void GuncelleDoktorListBox()
        {
            // ListBox'ı temizle
            listBoxDoktorlar.Items.Clear();

            // Listeye eklenen doktorları ListBox'a ekleyin
            foreach (Doktor doktor in doktorListesi.Doktorlar)
            {
                listBoxDoktorlar.Items.Add($"{doktor.DoktorAdiSoyadi} - {doktor.DoktorHastaneAdi} - {doktor.DoktorBolum}");
            }
        }

        private void GuncelleHastahaneListBox()
        {
            // ListBox'ı temizle
            listBoxHastahaneler.Items.Clear();

            // Listeye eklenen hastahaneleri ListBox'a ekleyin
            foreach (Hastahane hastahane in hastahaneListesi.Hastahaneler)
            {
                listBoxHastahaneler.Items.Add($"{hastahane.HastaAdi} - {hastahane.HastahaneAdresi}");
            }
            listBoxHastahaneler.SelectedIndex = -1;
        }
        public void Temizle()
        {
            // Text box'ları temizle
            txtBoxHastaAd.Text = "";
            txtHastaAdres.Text = "";
            txtHastaMail.Text = "";
            mTxtHastaTel.Text = "";
            numericUpDownYas.Value = 0;
            comboBoxHMeslek.SelectedIndex = -1;

            // Cinsiyet RadioButton'ları temizle
            rbErkek.Checked = false;
            rbKadin.Checked = false;

            txtAdiSoyadi.Text = "";
            cbDrBolum.Text = "";
            comboDrKayitHstnList.Text = "";
            mtbTelefon.Text = "";
            rbDrE.Checked = false;
            rbDrK.Checked = false;

            // TextBox'ları temizle
            txtHastahaneAdi.Clear();
            txtHastahaneAdresi.Clear();

        }

        private void btnFsec_Click(object sender, EventArgs e)
        {
            // OpenFileDialog ile resim seçme penceresini açın
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.png;*.bmp|Tüm Dosyalar|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Seçilen resmi PictureBox'a yerleştirin
                pictureBoxHastaFoto.Image = Image.FromFile(openFileDialog.FileName);
            } 

        }

        private void listBoxHastalar_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSec.Enabled = true;
            //MessageBox.Show("Bu Hastayı seçmek istiyorsanız Seç'e basınız !");

        }
        private void listBoxDoktorlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSec.Enabled = true;
            //MessageBox.Show("Bu Hastayı seçmek istiyorsanız Seç'e basınız !");
        }
        private void GuncelleSecilenHasta()
        {
            if (listBoxHastalar.SelectedIndex != -1)
            {
                int secilenIndex = listBoxHastalar.SelectedIndex;

                Hasta secilenHasta = hastaListesi[secilenIndex];

                secilenHasta.hastaAdiSoyadi = txtBoxHastaAd.Text;
                secilenHasta.hastaAdres = txtHastaAdres.Text;
                secilenHasta.hastaMeslek = comboBoxHMeslek.Text;
                secilenHasta.hastaTelefon = mTxtHastaTel.Text;
                secilenHasta.HastaEmail = txtHastaMail.Text;
                
                secilenHasta.hastaCinsiyet = rbErkek.Checked ? "Erkek" : "Kadın";

                
                secilenHasta.hastaYas = (int)numericUpDownYas.Value;
       
                GuncelleHastaListBox();
                Temizle();
               
            }
        }

        private void GuncelleSecilenDoktor()
        {
            // ListBox'ta seçilen bir doktor var mı kontrol et
            if (listBoxDoktorlar.SelectedIndex != -1)
            {
                // Seçilen doktorun indeksini al
                int secilenIndex = listBoxDoktorlar.SelectedIndex;

                // Seçilen doktoru doktorListesi'nden al
                Doktor secilenDoktor = doktorListesi.Doktorlar[secilenIndex];

                // Yeni bilgileri al
                string yeniAdiSoyadi = txtAdiSoyadi.Text;
                string yeniHastahane = comboDrKayitHstnList.Text;
                string yeniBolum = cbDrBolum.Text;
                string yeniCinsiyet = rbErkek.Checked ? "Erkek" : "Kadın";
                string yeniTelefon = mtbTelefon.Text;

                // Seçilen doktorun bilgilerini güncelle
                secilenDoktor.DoktorAdiSoyadi = yeniAdiSoyadi;
                secilenDoktor.DoktorHastaneAdi = yeniHastahane;
                secilenDoktor.DoktorBolum = yeniBolum;
                secilenDoktor.DoktorCinsiyet = yeniCinsiyet;
                secilenDoktor.DoktorTelefon = yeniTelefon;

                // ListBox'ı güncelle
                GuncelleDoktorListBox();
                Temizle();
                btnSec.Enabled = false;

                MessageBox.Show("Doktor bilgileri güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lütfen önce bir doktor seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void GuncelleSecilenHastahane()
        {
            // ListBox'ta seçilen bir hastahane var mı kontrol et
            if (listBoxHastahaneler.SelectedIndex != -1)
            {
                // Seçilen hastahanenin indeksini al
                int secilenIndex = listBoxHastahaneler.SelectedIndex;

                // Seçilen hastahaneyi hastahaneListesi'nden al
                Hastahane secilenHastahane = hastahaneListesi.Hastahaneler[secilenIndex];

                // Yeni bilgileri al
                string yeniHastahaneAdi = txtHastahaneAdi.Text;
                string yeniHastahaneAdresi = txtHastahaneAdresi.Text;

                // Yeni bilgilerle hastahane bilgilerini güncelle
                secilenHastahane.HastaAdi = yeniHastahaneAdi;
                secilenHastahane.HastahaneAdresi = yeniHastahaneAdresi;

                // ListBox'ı güncelle
                GuncelleHastahaneListBox();

                MessageBox.Show("Hastahane bilgileri güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lütfen önce bir hastahane seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void GosterSecilenHastaBilgileri()
        {
            // Listbox'ta seçilen bir öğe var mı kontrol et
            if (listBoxHastalar.SelectedIndex != -1)
            {
                // Seçilen öğenin indeksini al
                int secilenIndex = listBoxHastalar.SelectedIndex;

                // Seçilen hastanın bilgilerini al
                Hasta secilenHasta = hastaListesi[secilenIndex];

                // Bilgileri TextBox'lara ve diğer kontrollere yerleştir
                txtBoxHastaAd.Text = secilenHasta.hastaAdiSoyadi;
                txtHastaAdres.Text = secilenHasta.hastaAdres;
                comboBoxHMeslek.Text = secilenHasta.hastaMeslek;
                mTxtHastaTel.Text = secilenHasta.hastaTelefon;
                // Diğer kontrol değerlerini güncelle
                txtHastaMail.Text = secilenHasta.HastaEmail;


                // Örneğin cinsiyet RadioButton'larını güncelleyin
                if (secilenHasta.hastaCinsiyet == "Erkek")
                {
                    rbErkek.Checked = true;
                    rbKadin.Checked = false;
                }
                else if (secilenHasta.hastaCinsiyet == "Kadın")
                {
                    rbErkek.Checked = false;
                    rbKadin.Checked = true;
                }

                // Örneğin yaş değerini güncelleyin
                numericUpDownYas.Value = secilenHasta.hastaYas;

                // Resmi göster
                pictureBoxHastaFoto.Image = secilenHasta.hastaFotograf;
            }
        }

        private void GosterSecilenDoktorBilgileri()
        {
            // ListBox'ta seçilen bir doktor var mı kontrol et
            if (listBoxDoktorlar.SelectedIndex != -1)
            {
                // Seçilen doktorun indeksini al
                int secilenIndex = listBoxDoktorlar.SelectedIndex;

                // Seçilen doktoru doktorListesi'nden al
                Doktor secilenDoktor = doktorListesi.Doktorlar[secilenIndex];

                // Doktorun bilgilerini göstermek için uygun kontrollere atama yap
                txtAdiSoyadi.Text = secilenDoktor.DoktorAdiSoyadi;
                comboDrKayitHstnList.Text = secilenDoktor.DoktorHastaneAdi;
                cbDrBolum.Text = secilenDoktor.DoktorBolum;

                // Cinsiyet RadioButton'ları
                if (secilenDoktor.DoktorCinsiyet == "Erkek")
                {
                    rbDrE.Checked = true;
                    rbDrK.Checked = false;
                }
                else
                {
                    rbDrE.Checked = false;
                    rbDrK.Checked = true;
                }

                // Telefon MaskedTextBox
                mtbTelefon.Text = secilenDoktor.DoktorTelefon;
            }
            else
            {
                MessageBox.Show("Lütfen önce bir doktor seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void GosterSecilenHastahaneBilgileri()
        {
            // ListBox'ta seçilen bir hastahane var mı kontrol et
            if (listBoxHastahaneler.SelectedIndex != -1)
            {
                // Seçilen hastahanenin indeksini al
                int secilenIndex = listBoxHastahaneler.SelectedIndex;

                // Seçilen hastahaneyi hastahaneListesi'nden al
                Hastahane secilenHastahane = hastahaneListesi.Hastahaneler[secilenIndex];

                // Hastahane bilgilerini göstermek için uygun kontrollere atama yap
                txtHastahaneAdi.Text = secilenHastahane.HastaAdi;
                txtHastahaneAdresi.Text = secilenHastahane.HastahaneAdresi;
            }
            else
            {
                MessageBox.Show("Lütfen önce bir hastahane seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void GuncelleSecilenRandevu()
        {
            // ListBox'ta seçilen bir randevu var mı kontrol et
            if (listBoxRandevular.SelectedIndex != -1)
            {
                // Seçilen randevunun indeksini al
                int secilenIndex = listBoxRandevular.SelectedIndex;

                // Seçilen randevuyu randevuListesi'nden al
                Randevu secilenRandevu = randevuListesi[secilenIndex];

                // Yeni bilgileri al
                string yeniHastahane = comboHastane.SelectedItem as string;
                string yeniBolum = comboBolum.SelectedItem as string;
                string yeniDoktor = comboDoktor.SelectedItem as string;
                DateTime yeniTarih = dateTimePickerTarih.Value;
                string yeniTC = txtTc.Text;

                // Yeni bilgilerle randevu bilgilerini güncelle
                secilenRandevu.Hastahane = yeniHastahane;
                secilenRandevu.Bolum = yeniBolum;
                secilenRandevu.Doktor = yeniDoktor;
                secilenRandevu.Tarih = yeniTarih;
                secilenRandevu.HastaTc = yeniTC;

                // ListBox'ı güncelle
                GuncelleRandevuListBox();

                MessageBox.Show("Randevu bilgileri güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lütfen önce bir randevu seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SilSecilenHasta()
        {
            // Listbox'ta seçilen bir öğe var mı kontrol et
            if (listBoxHastalar.SelectedIndex != -1)
            {
                // Seçilen öğenin indeksini al
                int secilenIndex = listBoxHastalar.SelectedIndex;

                // Seçilen hastayı ListBox'tan ve hastaListesi'nden kaldır
                listBoxHastalar.Items.RemoveAt(secilenIndex);
                hastaListesi.RemoveAt(secilenIndex);

                // Textboxları temizle
                Temizle();

                // PictureBox'ı temizle
                pictureBoxHastaFoto.Image = null;

                MessageBox.Show("Hasta başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SilSecilenDoktor()
        {
            // ListBox'ta seçilen bir doktor var mı kontrol et
            if (listBoxDoktorlar.SelectedIndex != -1)
            {
                // Seçilen doktorun indeksini al
                int secilenIndex = listBoxDoktorlar.SelectedIndex;

                // Seçilen doktoru doktorListesi'nden al
                Doktor secilenDoktor = doktorListesi.Doktorlar[secilenIndex];

                // Silme işlemi için bir onay mesajı göster
                DialogResult result = MessageBox.Show($"'{secilenDoktor.DoktorAdiSoyadi}' adlı doktoru silmek istediğinizden emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Seçilen doktoru listeden sil
                    doktorListesi.Doktorlar.RemoveAt(secilenIndex);

                    // ListBox'ı güncelle
                    GuncelleDoktorListBox();

                    MessageBox.Show("Doktor başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Lütfen önce bir doktor seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SilSecilenHastahane()
        {
            // ListBox'ta seçilen bir hastahane var mı kontrol et
            if (listBoxHastahaneler.SelectedIndex != -1)
            {
                // Seçilen hastahanenin indeksini al
                int secilenIndex = listBoxHastahaneler.SelectedIndex;

                // Seçilen hastahaneyi hastahaneListesi'nden al
                Hastahane secilenHastahane = hastahaneListesi.Hastahaneler[secilenIndex];

                // Silme işlemi için bir onay mesajı göster
                DialogResult result = MessageBox.Show($"'{secilenHastahane.HastaAdi}' adlı hastahaneyi silmek istediğinizden emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Seçilen hastahaneyi listeden sil
                    hastahaneListesi.Hastahaneler.RemoveAt(secilenIndex);

                    // ListBox'ı güncelle
                    GuncelleHastahaneListBox();

                    MessageBox.Show("Hastahane başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Lütfen önce bir hastahane seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabControl1.SelectedTab == tabPage1)
            {
                btnSec.Enabled = true;
                BtnGuncelle.Enabled = true;
            }
            if (tabControl1.SelectedTab == tabPage3)
            {
                // Tab 3 (Randevu Oluşturma Tab'ı) seçildiğinde yapılacak işlemleri burada gerçekleştirin.

                // Hastaneleri ComboBox'a ekle
                comboHastane.DataSource = hastahaneListesi.Hastahaneler.Select(h => h.HastaAdi).ToList();

                // Doktorları ComboBox'a ekle
                comboDoktor.DataSource = doktorListesi.Doktorlar.Select(d => d.DoktorAdiSoyadi).ToList();
                btnSec.Enabled = true;
                BtnGuncelle.Enabled = true;
            }

            if (tabControl1.SelectedTab == tabPage2)
            {

                comboDrKayitHstnList.DataSource = hastahaneListesi.Hastahaneler.Select(h => h.HastaAdi).ToList();
                btnSec.Enabled = true;
                BtnGuncelle.Enabled = true;
            }
            if (tabControl1.SelectedTab == tabPage4)
            {
                GuncelleHastaCheckedListBox();
                GuncelleDoktorCheckedListBox();
                //checkedListBoxHastalar.DataSource = hastaListesi.H.Select(h => h.HastaAdi).ToList();
                btnSec.Enabled = false;
                BtnGuncelle.Enabled = false;
            }
            if (tabControl1.SelectedTab == tabPage5)
            {
                // Hastaları checkedListBoxHastalar'a ekle
                CLboxHasta.DataSource = hastaListesi.Select(h => h.hastaAdiSoyadi).ToList();

                // Doktorları checkedListBoxDoktorlar'a ekle
                CLboxdoktor.DataSource = doktorListesi.Doktorlar.Select(d => d.DoktorAdiSoyadi).ToList();
                btnSec.Enabled = false;
                BtnGuncelle.Enabled = false;
            }
            if (tabControl1.SelectedTab == tabPage6)
            {
                txtHastahaneAdi.Focus();
                btnSec.Enabled = true;
                BtnGuncelle.Enabled = true;
            }
        }
        private void RandevuOlustur()
        {
            // ComboBox'lardan seçilen değerleri al
            string hastahane = comboHastane.SelectedItem as string;
            string bolum = comboBolum.SelectedItem as string;
            string doktor = comboDoktor.SelectedItem as string;
            DateTime tarih = dateTimePickerTarih.Value;

            // Randevu oluştur
            Randevu yeniRandevu = new Randevu
            {
                Hastahane = hastahane,
                Bolum = bolum,
                Doktor = doktor,
                Tarih = tarih
            };

            // Oluşturulan randevuyu listeye ekle
            randevuListesi.Add(yeniRandevu);

            // ListBox'ı güncelle
            GuncelleRandevuListBox();

            // Bilgileri temizle
            TemizleRandevuBilgileri();
        }

        private void GuncelleRandevuListBox()
        {
            // ListBox'ı temizle
            listBoxRandevular.Items.Clear();

            // Listeye eklenen randevuları ListBox'a ekleyin
            foreach (Randevu randevu in randevuListesi)
            {
                listBoxRandevular.Items.Add(randevu.ToString());
            }
        }

        private void TemizleRandevuBilgileri()
        {
            // ComboBox'ları ve TextBox'ı temizle
            comboHastane.SelectedIndex = -1;
            comboBolum.SelectedIndex = -1;
            comboDoktor.SelectedIndex = -1;
            dateTimePickerTarih.Value = DateTime.Now;
            txtTc.Text = "";
        }

        private void GosterSecilenRandevuBilgileri()
        {
            // ListBox'ta seçilen bir randevu var mı kontrol et
            if (listBoxRandevular.SelectedIndex != -1)
            {
                // Seçilen randevunun indeksini al
                int secilenIndex = listBoxRandevular.SelectedIndex;

                // Seçilen randevuyu randevuListesi'nden al
                Randevu secilenRandevu = randevuListesi[secilenIndex];

                // ComboBox'lara seçilen bilgileri atayın
                comboHastane.SelectedItem = secilenRandevu.Hastahane;
                comboBolum.SelectedItem = secilenRandevu.Bolum;
                comboDoktor.SelectedItem = secilenRandevu.Doktor;
                dateTimePickerTarih.Value = secilenRandevu.Tarih;
                txtTc.Text = secilenRandevu.HastaTc;
            }
            else
            {
                MessageBox.Show("Lütfen önce bir randevu seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void SilSecilenRandevu()
        {
            // ListBox'ta seçilen bir randevu var mı kontrol et
            if (listBoxRandevular.SelectedIndex != -1)
            {
                // Seçilen randevunun indeksini al
                int secilenIndex = listBoxRandevular.SelectedIndex;

                // Seçilen randevuyu randevuListesi'nden al
                Randevu secilenRandevu = randevuListesi[secilenIndex];

                // Kullanıcıdan onay al
                DialogResult result = MessageBox.Show("Seçili randevuyu silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Seçilen randevuyu randevuListesi'nden sil
                    randevuListesi.Remove(secilenRandevu);

                    // ListBox'ı güncelle
                    GuncelleRandevuListBox();

                    MessageBox.Show("Randevu başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Lütfen önce bir randevu seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboHastane.Text) || string.IsNullOrEmpty(comboBolum.Text) ||
          string.IsNullOrEmpty(comboDoktor.Text) || string.IsNullOrEmpty(txtTc.Text))
            {
                MessageBox.Show("Lütfen tüm bilgileri doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                RandevuOlustur();
                // Randevu oluşturulacak işlemleri gerçekleştir
                MessageBox.Show("Randevu başarıyla oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           

        }

        // Teşhis Kayıt tabında Checked ListBox'ları güncelle
        private void GuncelleHastaCheckedListBox()
        {
            // Hasta listesini Checked ListBox'a ekle
            checkedListBoxHastalar.Items.Clear();
            foreach (Hasta hasta in hastaListesi)
            {
                checkedListBoxHastalar.Items.Add(hasta.hastaAdiSoyadi);
            }
        }

        // Teşhis Kayıt tabında Checked ListBox'ları güncelle
        public void GuncelleDoktorCheckedListBox()
        {
            // Doktor listesini Checked ListBox'a ekle
            // Örnek olarak doktorListesi isminde bir liste kullanıldığını varsayalım
            checkedListBoxDoktorlar.Items.Clear();
            foreach (var doktor in doktorListesi.Doktorlar)
            {
                checkedListBoxDoktorlar.Items.Add(doktor.DoktorAdiSoyadi);
            }


        }
        private void temizleTeshis()
        {
            // checkedListBoxDoktor'daki tüm işaretleri kaldır
            for (int i = 0; i < checkedListBoxDoktorlar.Items.Count; i++)
            {
                checkedListBoxDoktorlar.SetItemChecked(i, false);
            }

            // checkedListBoxHasta'daki tüm işaretleri kaldır
            for (int i = 0; i < checkedListBoxHastalar.Items.Count; i++)
            {
                checkedListBoxHastalar.SetItemChecked(i, false);
            }

            // textBoxTeshisBilgisi'ni temizle
            txtBoxTeshis.Clear();
        }
        private void SilSecilenTeshis()
        {
            // Seçilen teshisin indeksini al
            int secilenIndex = listBoxTeshisler.SelectedIndex;

            // Eğer hiç teshis seçilmemişse, fonksiyonu bitir
            if (secilenIndex == -1)
            {
                MessageBox.Show("Lütfen silinecek bir teshis seçin.");
                return;
            }

            // Seçilen teshisi kaldır
            listBoxTeshisler.Items.RemoveAt(secilenIndex);

        }
        private void SilSecilenTetkik()
        {
            // Seçilen tetkik öğesini al
            int secilenIndex = listBoxTetkik.SelectedIndex;

            // Eğer hiç tetkik seçilmemişse, fonksiyonu bitir
            if (secilenIndex == -1)
            {
                MessageBox.Show("Lütfen silinecek bir tetkik seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Seçilen tetkik öğesini kaldır
            listBoxTetkik.Items.RemoveAt(secilenIndex);

        }

        private void TemizleTahlilTest()
        {
            // checkedListBoxHasta'daki tüm işaretleri kaldır
            for (int i = 0; i < CLboxHasta.Items.Count; i++)
            {
                CLboxHasta.SetItemChecked(i, false);
            }

            // checkedListBoxDoktor'daki tüm işaretleri kaldır
            for (int i = 0; i < CLboxdoktor.Items.Count; i++)
            {
                CLboxdoktor.SetItemChecked(i, false);
            }

            // checkBoxTahlil'i temizle
            checkBoxTahlil.Checked = false;

            // checkBoxTest'i temizle
            checkBoxTest.Checked = false;
        }

        private void txtBoxHastaAd_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBoxHastaAd.Text))
            {
                MessageBox.Show("Hasta adı boş bırakılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxHastaAd.Focus();
            }
        }

        private void txtHastaAdres_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHastaAdres.Text))
            {
                MessageBox.Show("Hasta adresi boş bırakılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHastaAdres.Focus();
            }
        }

        private void txtHastaMail_Leave(object sender, EventArgs e)
        {

            string eposta = txtHastaMail.Text;
            if (!IsValidEmail(eposta))
            {
                // Geçersiz e-posta, kullanıcıyı uyar ve odaklanmayı sağla
                MessageBox.Show("Geçerli bir e-posta adresi giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHastaMail.Focus();

            }
        }

        private void mTxtHastaTel_Leave(object sender, EventArgs e)
        {
            if (!mTxtHastaTel.MaskFull)
            {
                MessageBox.Show("Hasta telefon numarası eksik veya hatalı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mTxtHastaTel.Focus();
            }
        }

        private void comboBoxHMeslek_Leave(object sender, EventArgs e)
        {
            MessageBox.Show("Hasta mesleği boş bırakılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
        public bool IsValidEmail(string email)
        {
            try
            {
                // E-posta doğrulama için düzenli ifade (regex) kullanma
                string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
                Regex regex = new Regex(pattern);
                return regex.IsMatch(email);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private void btnHsT_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

       

        public void txtHastahaneAdresi_Leave(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage6)
            {
                if (string.IsNullOrEmpty(txtHastahaneAdresi.Text))
                {
                    MessageBox.Show("Hastahane Adresi boş bırakılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHastahaneAdresi.Focus();
                }
            }
        }

        private void BtnHstnT_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage6;
        }

        private void btnDrT_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void btnRandevu_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void btnTeshis_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
        }

        private void btnTahlilTest_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage5;
        }

        private void linkLblH_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Hasta linkine gitmek için bir URL belirtin
            string hastaLinki = "https://egehastane.ege.edu.tr/";
            // URL'yi varsayılan tarayıcıda aç
            System.Diagnostics.Process.Start(hastaLinki);
        }

        public void txtBoxTeshis_MouseClick(object sender, MouseEventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                // Kullanıcıya yazı tipi seçme penceresini göster
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    // Kullanıcının seçtiği yazı tipini ve özellikleri al
                    Font secilenFont = fontDialog.Font;

                    // Burada seçilen font ile yapılacak işlemleri gerçekleştir
                    // Örneğin, bir Label'ın fontunu değiştir:
                    txtBoxTeshis.Font = secilenFont;
                }
            }

            using (ColorDialog colorDialog = new ColorDialog())
            {
                // Kullanıcıya renk seçme penceresini göster
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    // Kullanıcının seçtiği rengi al
                    Color secilenRenk = colorDialog.Color;

                    // Burada seçilen rengi ile yapılacak işlemleri gerçekleştir
                    // Örneğin, bir Label'ın arka plan rengini değiştir:
                    txtBoxTeshis.BackColor = secilenRenk;
                }
            }
        }

        private void temizleTestTahlil()
        {
            // Hasta listesini temizle
            CLboxHasta.Items.Clear();

            // Doktor listesini temizle
            CLboxdoktor.Items.Clear();

            // Tahlil ve Test checkbox'larını temizle
            checkBoxTahlil.Checked = false;
            checkBoxTest.Checked = false;
        }

        private void checkBoxTahlil_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Tetkik seçimi yaptınız.");
        }

        private void checkBoxTest_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Tetkik seçimi yaptınız.");
        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            string metin = txtTc.Text;

            // Girilen metnin uzunluğunu kontrol et
            if (metin.Length >= 11)
            {
                // 11 karaktere ulaşıldığında daha fazla yazmaya izin verme
                txtTc.Enabled = false;
            }
            else
            {
                // 11 karaktere ulaşılmadığında yazmaya devam etme
                txtTc.Enabled = true;
            }
        }

        private void pictureBoxHastaFoto_Click(object sender, EventArgs e)
        {
            // PictureBox'ta bir resim varsa
            if (pictureBoxHastaFoto.Image != null)
            {
                // SaveFileDialog oluşturun
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JPEG Dosyası|*.jpg|PNG Dosyası|*.png|BMP Dosyası|*.bmp|Tüm Dosyalar|*.*";

                // İsteğe bağlı olarak başlangıç dizinini ayarlayın
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                // Kullanıcının kaydedilecek dosya yolu ve adını seçmesini bekleyin
                DialogResult result = saveFileDialog.ShowDialog();

                // Kullanıcı OK düğmesine tıkladıysa
                if (result == DialogResult.OK)
                {
                    // Seçilen dosya yolu ve adını alın
                    string dosyaYolu = saveFileDialog.FileName;

                    // PictureBox'ta görüntüyü kaydetme
                    pictureBoxHastaFoto.Image.Save(dosyaYolu);

                    MessageBox.Show("Resim başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Kaydedilecek resim bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            toolTip1.SetToolTip(linkLblH, "Hastane websitesini görmek için tıklayın.");
            toolTip1.SetToolTip(BtnGuncelle, "Güncelle işlemi yapmadan seçim yapmalısınız.");
            toolTip1.SetToolTip(pictureBoxHastaFoto, "Fotoğrafi indirmek için tıklayın");

        }

        
        private void TxtAdiSoyadi_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece harfleri ve boşluk karakterini kabul etmek için KeyPress olayını kullanabilirsiniz
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Bu karakteri engelle
            }
        }

        private void TxtTc_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece rakamları ve boşluk karakterini kabul etmek için KeyPress olayını kullanabilirsiniz
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Bu karakteri engelle
            }
        }

        private void AnaForm_KeyDown(object sender, KeyEventArgs e)
        {
            // TC Kimlik Numarasının belirli bir formatta olup olmadığını kontrol et
            if (e.KeyCode == Keys.Enter)
            {
                KontrolEt();
            }
        }

        private void AnaForm_KeyUp(object sender, KeyEventArgs e)
        {
            // TC Kimlik Numarasının belirli bir formatta olup olmadığını kontrol et
            if (e.KeyCode == Keys.Enter)
            {
                KontrolEt();
            }
        }

        private void KontrolEt()
        {
            string tcKimlik = txtTc.Text;

            // TC kimlik numarasının uygunluğunu kontrol et
            if (tcKimlik.Length != 11)
            {
                MessageBox.Show("Geçersiz TC Kimlik Numarası!");
            }
            else
            {
                MessageBox.Show("TC Kimlik Numarası Geçerli!");
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab.Visible = false;
        }

        public void TemizleHastahane()
        {
            txtHastahaneAdi.Text = string.Empty;
            txtHastahaneAdresi.Text = string.Empty;
        }
    }
}
