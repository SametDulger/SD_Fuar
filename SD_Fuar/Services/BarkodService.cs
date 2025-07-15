using System.Security.Cryptography;
using System.Text;

namespace SD_Fuar.Services
{
    public class BarkodService
    {
        private const string DAVETIYE_PREFIX = "DAV";
        private const string BILET_PREFIX = "BIL";
        private const string DATE_FORMAT = "yyyyMMdd";
        private const int BARKOD_LENGTH = 12;
        private const char REPLACE_CHAR_1 = '/';
        private const char REPLACE_CHAR_2 = '+';

        public string BarkodOlustur(string ziyaretciAdi, string firmaAdi, DateTime tarih)
        {
            if (string.IsNullOrWhiteSpace(ziyaretciAdi) || string.IsNullOrWhiteSpace(firmaAdi))
            {
                throw new ArgumentException("Ziyaretçi adı ve firma adı boş olamaz.");
            }

            var barkodVerisi = $"{ziyaretciAdi}_{firmaAdi}_{tarih.ToString(DATE_FORMAT)}";
            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(barkodVerisi));
            var barkod = Convert.ToBase64String(hash)
                .Substring(0, BARKOD_LENGTH)
                .Replace(REPLACE_CHAR_1, string.Empty)
                .Replace(REPLACE_CHAR_2, string.Empty);
            return barkod;
        }

        public string DavetiyeBarkodOlustur(int ziyaretciId, DateTime tarih)
        {
            if (ziyaretciId <= 0)
            {
                throw new ArgumentException("Geçerli bir ziyaretçi ID'si gerekli.");
            }

            var barkodVerisi = $"{DAVETIYE_PREFIX}_{ziyaretciId}_{tarih.ToString(DATE_FORMAT)}";
            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(barkodVerisi));
            var barkod = Convert.ToBase64String(hash)
                .Substring(0, BARKOD_LENGTH)
                .Replace(REPLACE_CHAR_1, string.Empty)
                .Replace(REPLACE_CHAR_2, string.Empty);
            return barkod;
        }

        public string BiletBarkodOlustur(int biletId, DateTime tarih, string tip)
        {
            if (biletId <= 0)
            {
                throw new ArgumentException("Geçerli bir bilet ID'si gerekli.");
            }

            if (string.IsNullOrWhiteSpace(tip))
            {
                throw new ArgumentException("Bilet tipi boş olamaz.");
            }

            var barkodVerisi = $"{BILET_PREFIX}_{biletId}_{tip}_{tarih.ToString(DATE_FORMAT)}";
            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(barkodVerisi));
            var barkod = Convert.ToBase64String(hash)
                .Substring(0, BARKOD_LENGTH)
                .Replace(REPLACE_CHAR_1, string.Empty)
                .Replace(REPLACE_CHAR_2, string.Empty);
            return barkod;
        }

        public bool BarkodDogrula(string barkod, string beklenenVeri)
        {
            if (string.IsNullOrWhiteSpace(barkod) || string.IsNullOrWhiteSpace(beklenenVeri))
            {
                return false;
            }

            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(beklenenVeri));
            var beklenenBarkod = Convert.ToBase64String(hash)
                .Substring(0, BARKOD_LENGTH)
                .Replace(REPLACE_CHAR_1, string.Empty)
                .Replace(REPLACE_CHAR_2, string.Empty);
            return barkod == beklenenBarkod;
        }
    }
} 