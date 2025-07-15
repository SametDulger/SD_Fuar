using System.IO;
using System.IO.Compression;

namespace SD_Fuar.Services
{
    public class YedeklemeService
    {
        private readonly string _yedeklemeKlasoru;

        public YedeklemeService()
        {
            _yedeklemeKlasoru = Path.Combine(Directory.GetCurrentDirectory(), "Yedekler");
            if (!Directory.Exists(_yedeklemeKlasoru))
            {
                Directory.CreateDirectory(_yedeklemeKlasoru);
            }
        }

        public string VeritabaniYedekle(string veritabaniDosyasi)
        {
            var tarih = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var yedekDosyaAdi = $"SD_Fuar_Yedek_{tarih}.db";
            var yedekDosyaYolu = Path.Combine(_yedeklemeKlasoru, yedekDosyaAdi);

            if (File.Exists(veritabaniDosyasi))
            {
                File.Copy(veritabaniDosyasi, yedekDosyaYolu);
                return yedekDosyaYolu;
            }

            throw new FileNotFoundException("Veritabanı dosyası bulunamadı.");
        }

        public string SıkıstırılmışYedekOlustur(string veritabaniDosyasi)
        {
            var tarih = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var zipDosyaAdi = $"SD_Fuar_Yedek_{tarih}.zip";
            var zipDosyaYolu = Path.Combine(_yedeklemeKlasoru, zipDosyaAdi);

            using (var zip = ZipFile.Open(zipDosyaYolu, ZipArchiveMode.Create))
            {
                zip.CreateEntryFromFile(veritabaniDosyasi, Path.GetFileName(veritabaniDosyasi));
            }

            return zipDosyaYolu;
        }

        public List<string> YedekleriListele()
        {
            var yedekler = new List<string>();
            var dosyalar = Directory.GetFiles(_yedeklemeKlasoru, "*.db");
            var zipDosyalar = Directory.GetFiles(_yedeklemeKlasoru, "*.zip");

            yedekler.AddRange(dosyalar);
            yedekler.AddRange(zipDosyalar);

            return yedekler.OrderByDescending(f => File.GetCreationTime(f)).ToList();
        }

        public void EskiYedekleriTemizle(int gunSayisi = 30)
        {
            var eskiTarih = DateTime.Now.AddDays(-gunSayisi);
            var dosyalar = Directory.GetFiles(_yedeklemeKlasoru);

            foreach (var dosya in dosyalar)
            {
                if (File.GetCreationTime(dosya) < eskiTarih)
                {
                    File.Delete(dosya);
                }
            }
        }

        public bool YedektenGeriYukle(string yedekDosyaYolu, string hedefDosyaYolu)
        {
            try
            {
                if (File.Exists(yedekDosyaYolu))
                {
                    if (File.Exists(hedefDosyaYolu))
                    {
                        File.Delete(hedefDosyaYolu);
                    }
                    File.Copy(yedekDosyaYolu, hedefDosyaYolu);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
} 