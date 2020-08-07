using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using Assistant.Entities;

namespace Assistant.Classes
{
    public class Helper
    {
        private bool IsUrlExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }

        public DataTable TcmbKurGetir(DateTime kurTarihi)
        {
            try
            {
                var dt = new DataTable();
                dt.Columns.Add("Tarih");
                dt.Columns.Add("Doviz");
                dt.Columns.Add("Alis");
                dt.Columns.Add("Satis");
                dt.Columns.Add("EfektifAlis");
                dt.Columns.Add("EfektifSatis");

                if (kurTarihi.DayOfWeek == DayOfWeek.Sunday)//pazar ise iki gün öncesinden
                {
                    kurTarihi = kurTarihi.AddDays(-2);
                }
                else if (kurTarihi.DayOfWeek == DayOfWeek.Saturday)//cumartesi ise bir gün öncesinden veri alalım
                {
                    kurTarihi = kurTarihi.AddDays(-1);
                }

                var xmlDoc = new XmlDocument();
                string link;
                if (kurTarihi >= DateTime.Today)
                {
                    link = "http://www.tcmb.gov.tr/kurlar/today.xml";
                }
                else
                {
                    link = $"http://www.tcmb.gov.tr/kurlar/{kurTarihi.Year}{kurTarihi.ToString("MM")}/{kurTarihi.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')}{kurTarihi.ToString("MM")}{kurTarihi.Year}.xml";

                    int count = 0;
                    while (!IsUrlExists(link))//bayram tatillerinde tcmb güncellenmediği için önceki ilk değeri bulmak için geri git.
                    {
                        count++;
                        if (count > 15)//15 defa denedikten sonra daha da deneme
                        {
                            break;
                        }
                        kurTarihi = kurTarihi.AddDays(-1);
                        link = $"http://www.tcmb.gov.tr/kurlar/{kurTarihi.Year}{kurTarihi.ToString("MM")}/{kurTarihi.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')}{kurTarihi.ToString("MM")}{kurTarihi.Year}.xml";
                    }


                }
                xmlDoc.Load(link);

                // Xml içinden tarihi alma - gerekli olabilir
                var exchangeDate = Convert.ToDateTime(xmlDoc.SelectSingleNode("//Tarih_Date").Attributes["Tarih"].Value);

                var dr = dt.NewRow();
                dr[0] = exchangeDate.ToShortDateString();
                dr[1] = "USD";
                dr[2] = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/ForexBuying").InnerXml;
                dr[3] = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/ForexSelling").InnerXml;
                dr[4] = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerXml;
                dr[5] = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;

                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[0] = exchangeDate.ToShortDateString();
                dr[1] = "EUR";
                dr[2] = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/ForexBuying").InnerXml;
                dr[3] = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/ForexSelling").InnerXml;
                dr[4] = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying").InnerXml;
                dr[5] = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[0] = exchangeDate.ToShortDateString();
                dr[1] = "GBP";
                dr[2] = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='GBP']/ForexBuying").InnerXml;
                dr[3] = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='GBP']/ForexSelling").InnerXml;
                dr[4] = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='GBP']/BanknoteBuying").InnerXml;
                dr[5] = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='GBP']/BanknoteSelling").InnerXml;

                dt.Rows.Add(dr);
                return dt;
            }
            catch (Exception exc)
            {
                MessageBox.Show(@"Merkez bankasına bağlanılamadı. Tekrar Dene Sistem Mesajı : " + exc.Message, @"Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return null;
            }
        }

        public decimal PariteHesapla(int kaynak, int hedef)
        {
            decimal parite = 1;

            using (AssistantEntities dbContext = new AssistantEntities())
            {

                var kaynakSatis = dbContext.DovizKur.OrderByDescending(x => x.Tarih).FirstOrDefault(x => x.DovizCinsId == kaynak);
                var hedefSatis = dbContext.DovizKur.OrderByDescending(x => x.Tarih).FirstOrDefault(x => x.DovizCinsId == hedef);


                //if (kaynakSatis != null && hedefSatis != null) return
                parite =  kaynakSatis.Satis / hedefSatis.Satis;
            }

            return parite;
        }
    }
}
