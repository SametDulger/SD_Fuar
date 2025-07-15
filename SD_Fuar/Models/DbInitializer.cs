using Microsoft.EntityFrameworkCore;

namespace SD_Fuar.Models
{
    public static class DbInitializer
    {
        public static void Initialize(SD_FuarContext context)
        {
            context.Database.EnsureCreated();

            if (context.Firmalar.Any())
            {
                return;
            }

            var firmalar = new Firma[]
            {
                new Firma
                {
                    Ad = "ABC Teknoloji A.Ş.",
                    Yetkili = "Ahmet Yılmaz",
                    Unvan = "Genel Müdür",
                    Tel = "0212 555 0101",
                    Dahili = "101",
                    DirektTel = "0212 555 0102",
                    Eposta = "ahmet.yilmaz@abcteknoloji.com",
                    Faks = "0212 555 0103",
                    Adres = "Maslak Mah. Büyükdere Cad. No:123",
                    Sehir = "İstanbul",
                    Ulke = "Türkiye",
                    Sektor = "Teknoloji"
                },
                new Firma
                {
                    Ad = "XYZ Mobilya Ltd. Şti.",
                    Yetkili = "Fatma Demir",
                    Unvan = "Satış Müdürü",
                    Tel = "0312 444 0201",
                    Dahili = "201",
                    DirektTel = "0312 444 0202",
                    Eposta = "fatma.demir@xyzmobilya.com",
                    Faks = "0312 444 0203",
                    Adres = "Çankaya Mah. Atatürk Bulvarı No:456",
                    Sehir = "Ankara",
                    Ulke = "Türkiye",
                    Sektor = "Mobilya"
                },
                new Firma
                {
                    Ad = "Delta Otomotiv A.Ş.",
                    Yetkili = "Mehmet Kaya",
                    Unvan = "Pazarlama Direktörü",
                    Tel = "0232 333 0301",
                    Dahili = "301",
                    DirektTel = "0232 333 0302",
                    Eposta = "mehmet.kaya@deltaotomotiv.com",
                    Faks = "0232 333 0303",
                    Adres = "Bornova Mah. Ege Üniversitesi Cad. No:789",
                    Sehir = "İzmir",
                    Ulke = "Türkiye",
                    Sektor = "Otomotiv"
                }
            };

            context.Firmalar.AddRange(firmalar);
            context.SaveChanges();

            var fuarlar = new Fuar[]
            {
                new Fuar
                {
                    Ad = "İstanbul Teknoloji Fuarı 2024",
                    Ulke = "Türkiye",
                    Sehir = "İstanbul",
                    BaslangicTarihi = new DateTime(2024, 10, 15),
                    BitisTarihi = new DateTime(2024, 10, 20)
                },
                new Fuar
                {
                    Ad = "Ankara Mobilya Fuarı 2024",
                    Ulke = "Türkiye",
                    Sehir = "Ankara",
                    BaslangicTarihi = new DateTime(2024, 11, 5),
                    BitisTarihi = new DateTime(2024, 11, 10)
                },
                new Fuar
                {
                    Ad = "İzmir Otomotiv Fuarı 2024",
                    Ulke = "Türkiye",
                    Sehir = "İzmir",
                    BaslangicTarihi = new DateTime(2024, 12, 1),
                    BitisTarihi = new DateTime(2024, 12, 6)
                }
            };

            context.Fuarlar.AddRange(fuarlar);
            context.SaveChanges();

            var salonlar = new Salon[]
            {
                new Salon { Ad = "A Salonu", FuarId = fuarlar[0].Id },
                new Salon { Ad = "B Salonu", FuarId = fuarlar[0].Id },
                new Salon { Ad = "C Salonu", FuarId = fuarlar[0].Id },
                new Salon { Ad = "Ana Salon", FuarId = fuarlar[1].Id },
                new Salon { Ad = "Yan Salon", FuarId = fuarlar[1].Id },
                new Salon { Ad = "Merkez Salon", FuarId = fuarlar[2].Id }
            };

            context.Salonlar.AddRange(salonlar);
            context.SaveChanges();

            var standlar = new Stand[]
            {
                new Stand { Kod = "A01", En = 3.0, Boy = 4.0, Metrekare = 12.0, SalonId = salonlar[0].Id },
                new Stand { Kod = "A02", En = 4.0, Boy = 3.0, Metrekare = 12.0, SalonId = salonlar[0].Id },
                new Stand { Kod = "A03", En = 5.0, Boy = 5.0, Metrekare = 25.0, SalonId = salonlar[0].Id },
                new Stand { Kod = "B01", En = 6.0, Boy = 4.0, Metrekare = 24.0, SalonId = salonlar[1].Id },
                new Stand { Kod = "B02", En = 4.0, Boy = 6.0, Metrekare = 24.0, SalonId = salonlar[1].Id },
                new Stand { Kod = "C01", En = 8.0, Boy = 6.0, Metrekare = 48.0, SalonId = salonlar[2].Id }
            };

            context.Standlar.AddRange(standlar);
            context.SaveChanges();

            var ziyaretciler = new Ziyaretci[]
            {
                new Ziyaretci
                {
                    Ad = "Ali",
                    Soyad = "Veli",
                    FirmaAdi = "ABC Teknoloji A.Ş.",
                    Adres = "Maslak Mah. Büyükdere Cad. No:123, İstanbul",
                    Tel = "0555 111 1111",
                    Eposta = "ali.veli@abcteknoloji.com",
                    Faks = "0212 111 1111",
                    Meslek = "Mühendis"
                },
                new Ziyaretci
                {
                    Ad = "Ayşe",
                    Soyad = "Fatma",
                    FirmaAdi = "XYZ Mobilya Ltd. Şti.",
                    Adres = "Çankaya Mah. Atatürk Bulvarı No:456, Ankara",
                    Tel = "0555 222 2222",
                    Eposta = "ayse.fatma@xyzmobilya.com",
                    Faks = "0212 222 2222",
                    Meslek = "Satış Temsilcisi"
                }
            };

            context.Ziyaretciler.AddRange(ziyaretciler);
            context.SaveChanges();

            var sozlesmeler = new Sozlesme[]
            {
                new Sozlesme
                {
                    FirmaId = firmalar[0].Id,
                    FuarId = fuarlar[0].Id,
                    SalonId = salonlar[0].Id,
                    StandId = standlar[0].Id,
                    Tarih = DateTime.Today.AddDays(-30),
                    SozlesmeTipi = "Katılımcı Firma",
                    StandTipi = "Hazır Konstrüksiyonlu",
                    Metrekare = 12.0,
                    BirimFiyat = 150.00m,
                    Doviz = "USD",
                    DovizKuru = 30.50m,
                    IndirimOrani = 0.0,
                    VadeSayisi = 3,
                    OdemePlani = "30-30-40",
                    SonGecerlilikTarihi = DateTime.Today.AddDays(30),
                    ImzaDurumu = "Alındı",
                    UcretsizVerilenMetrekare = 0.0
                }
            };

            context.Sozlesmeler.AddRange(sozlesmeler);
            context.SaveChanges();
        }
    }
} 