# SD_Fuar - Fuar Yönetim Sistemi

## 📋 Proje Hakkında

**SD_Fuar**, fuar organizasyonları için geliştirilmiş kapsamlı bir yönetim sistemidir. Bu sistem, fuar organizatörlerinin firma yönetimi, stand rezervasyonu, ziyaretçi takibi, muhasebe işlemleri ve raporlama süreçlerini dijital ortamda yönetmelerini sağlar.

## ⚠️ ÖNEMLİ UYARI

**Bu proje şu anda geliştirme aşamasındadır ve henüz production ortamı için hazır değildir.**

### 🔴 Mevcut Durum
- ✅ Temel CRUD işlemleri tamamlanmış
- ✅ Veritabanı yapısı kurulmuş
- ✅ Kullanıcı arayüzü tasarlanmış
- ✅ Validation kuralları eklenmiş
- ⚠️ Kullanıcı kimlik doğrulama sistemi basit seviyede
- ⚠️ Test coverage eksik
- ⚠️ Güvenlik testleri yapılmamış

### 🚨 Bilinen Eksiklikler
- Rol tabanlı yetkilendirme sistemi eksik
- Kapsamlı hata yönetimi geliştirilmeli
- REST API yok (sadece MVC)
- Unit testler yazılmamış
- Performance optimizasyonu tamamlanmamış

## 🛠️ Teknoloji Stack

- **Framework**: ASP.NET Core 9.0
- **Veritabanı**: SQLite (Development)
- **ORM**: Entity Framework Core 9.0.7
- **Frontend**: Bootstrap 5, jQuery
- **Dil**: C# 12.0
- **Platform**: Cross-platform (.NET 9.0)

## 📦 Kurulum

### Gereksinimler
- .NET 9.0 SDK
- Visual Studio 2022 veya VS Code

### Adımlar
1. Projeyi klonlayın:
```bash
git clone https://github.com/SametDulger/SD_Fuar.git
cd SD_FuarProject/SD_Fuar
```

2. Bağımlılıkları yükleyin:
```bash
dotnet restore
```

3. Uygulamayı çalıştırın:
```bash
dotnet run
```

4. Tarayıcıda açın: `https://localhost:7134`

## 🏗️ Sistem Mimarisi

### Ana Modüller

#### 1. **Ana Sayfa** (`HomeController`)
- Dashboard görüntüleme
- İstatistiksel veriler
- Son kayıtlar listesi

#### 2. **Firma Yönetimi** (`FirmaController`)
- Firma kayıt ve düzenleme
- İletişim bilgileri yönetimi
- Sektör kategorilendirme

#### 3. **Fuar Yönetimi** (`FuarController`)
- Fuar oluşturma ve planlama
- Tarih ve lokasyon yönetimi
- Salon organizasyonu

#### 4. **Salon Yönetimi** (`SalonController`)
- Salon oluşturma ve düzenleme
- Fuar-salon ilişkilendirme

#### 5. **Stand Yönetimi** (`StandController`)
- Stand kodu ve boyut yönetimi
- Müsaitlik durumu kontrolü
- Fiyatlandırma sistemi

#### 6. **Sözleşme Yönetimi** (`SozlesmeController`)
- Sözleşme oluşturma
- Ödeme planı yönetimi
- Döviz kuru entegrasyonu

#### 7. **Teklif Yönetimi** (`TeklifController`)
- Teklif oluşturma ve takibi
- Fiyat hesaplamaları

#### 8. **Ziyaretçi Yönetimi** (`ZiyaretciController`)
- Ziyaretçi kayıt sistemi
- Ziyaret takibi

#### 9. **Bilet Yönetimi** (`BiletController`)
- Bilet satışı ve takibi
- Bilet kullanım durumu

#### 10. **Davetiye Yönetimi** (`DavetiyeController`)
- Davetiye oluşturma
- Online davetiye sistemi

#### 11. **Barkod Yönetimi** (`BarkodController`)
- Barkod oluşturma
- Ziyaretçi takibi

#### 12. **Görüşme Yönetimi** (`GorusmeController`)
- Görüşme planlama
- Görüşme takibi

#### 13. **Muhasebe** (`MuhasebeController`)
- Gelir-gider takibi
- Ödeme kayıtları
- Vergi hesaplamaları

#### 14. **İndirim Yönetimi** (`IndirimController`)
- İndirim tanımlama
- İndirim hesaplamaları

#### 15. **Katılım Geçmişi** (`KatilimGecmisiController`)
- Firma katılım geçmişi
- İstatistiksel veriler

#### 16. **Kullanıcı Yönetimi** (`UygulamaKullanicisiController`)
- Kullanıcı kayıt sistemi
- Şifre hashleme (SHA256)

#### 17. **Raporlama** (`RaporController`)
- Satış raporları
- Fuar performans analizi
- Ziyaretçi istatistikleri
- Muhasebe raporları

## 🔧 Konfigürasyon

### Veritabanı Bağlantısı
`appsettings.json` dosyasında SQLite connection string:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=sd_fuar.db"
  }
}
```

**Not**: SQLite veritabanı kullanılmaktadır. Production ortamı için SQL Server veya PostgreSQL'e geçiş önerilir.

## 📊 Veritabanı Şeması

### Ana Tablolar (15 adet)
- `Firmalar` - Firma bilgileri
- `Fuarlar` - Fuar organizasyonları
- `Salonlar` - Fuar salonları
- `Standlar` - Stand bilgileri
- `Sozlesmeler` - Sözleşme kayıtları
- `Teklifler` - Teklif kayıtları
- `Ziyaretciler` - Ziyaretçi bilgileri
- `Biletler` - Bilet kayıtları
- `Davetiyeler` - Davetiye kayıtları
- `Barkodlar` - Barkod kayıtları
- `Gorusmeler` - Görüşme kayıtları
- `MuhasebeKayitlari` - Finansal kayıtlar
- `Indirimler` - İndirim tanımları
- `KatilimGecmisleri` - Katılım geçmişi
- `UygulamaKullanicilari` - Sistem kullanıcıları

### View Models
- `DashboardViewModel` - Ana sayfa dashboard verileri
- `ErrorViewModel` - Hata sayfası verileri

## 🔒 Güvenlik

### Mevcut Güvenlik Önlemleri
- Anti-forgery token koruması (tüm POST işlemlerinde)
- SHA256 şifre hashleme (UygulamaKullanicisi modelinde)
- Input validation (Data Annotations)
- SQL injection koruması (EF Core)
- Global exception handling middleware

### Geliştirilmesi Gereken Alanlar
- JWT token authentication
- Role-based authorization
- API rate limiting
- Data encryption
- Audit logging

## 🚀 Performans

### Mevcut Optimizasyonlar
- Entity Framework Include optimizasyonları
- Async/await pattern kullanımı
- ViewData ile ViewBag optimizasyonu
- Dashboard verileri için sabit sayı (DASHBOARD_ITEM_COUNT = 5)

### İyileştirme Önerileri
- Caching stratejisi
- Database indexing
- Query optimization
- Memory management

## 🧪 Test

### Test Durumu
- ❌ Unit testler yazılmamış
- ❌ Integration testler eksik
- ❌ UI testler yok
- ❌ Performance testler yok

### Test Planı
- [ ] Model validation testleri
- [ ] Controller action testleri
- [ ] Service layer testleri (BarkodService, IndirimService, YedeklemeService)
- [ ] Database integration testleri

## 📝 Controller Endpoints

### Mevcut Controllers (17 adet)
Bu proje **MVC (Model-View-Controller)** yapısında geliştirilmiştir.

- `HomeController` - Ana sayfa ve dashboard
- `FirmaController` - Firma CRUD işlemleri
- `FuarController` - Fuar CRUD işlemleri
- `SalonController` - Salon CRUD işlemleri
- `StandController` - Stand CRUD işlemleri
- `SozlesmeController` - Sözleşme CRUD işlemleri
- `TeklifController` - Teklif CRUD işlemleri
- `ZiyaretciController` - Ziyaretçi CRUD işlemleri
- `BiletController` - Bilet CRUD işlemleri
- `DavetiyeController` - Davetiye CRUD işlemleri
- `BarkodController` - Barkod CRUD işlemleri
- `GorusmeController` - Görüşme CRUD işlemleri
- `MuhasebeController` - Muhasebe CRUD işlemleri
- `IndirimController` - İndirim CRUD işlemleri
- `KatilimGecmisiController` - Katılım geçmişi CRUD işlemleri
- `UygulamaKullanicisiController` - Kullanıcı CRUD işlemleri
- `RaporController` - Rapor görüntüleme

### Tek JSON Endpoint
Sadece 1 AJAX endpoint'i mevcut:
- `IndirimController.HesaplaIndirim(int firmaId, int fuarId, decimal tutar, double metrekare)` - İndirim hesaplama (JSON response)

### Güvenlik
- Tüm POST işlemleri için anti-forgery token gerekli
- Input validation aktif (Data Annotations)
- Error handling mevcut

## 🔄 Deployment

### Production Hazırlığı
- [ ] Veritabanı provider değişikliği (SQLite → SQL Server/PostgreSQL)
- [ ] Environment-specific configuration
- [ ] Logging configuration
- [ ] Error monitoring setup
- [ ] Performance monitoring
- [ ] Security hardening

### Deployment Adımları
1. Production veritabanı kurulumu
2. Environment variables konfigürasyonu
3. SSL sertifikası kurulumu
4. Reverse proxy yapılandırması
5. Monitoring araçları kurulumu

## 🤝 Katkıda Bulunma

### Geliştirme Kuralları
1. Feature branch kullanın
2. Commit mesajları açıklayıcı olsun
3. Code review sürecini takip edin
4. Test coverage'ı koruyun

### Kod Standartları
- C# coding conventions
- SOLID principles
- Clean code practices
- Error handling patterns


### Sorun Bildirimi
GitHub Issues kullanarak:
- Bug raporları
- Feature istekleri
- Dokümantasyon önerileri
- Güvenlik açıkları


## 🔮 Gelecek Planları

### Kısa Vadeli (1-3 ay)
- [ ] Kullanıcı authentication sistemi
- [ ] Role-based authorization
- [ ] Unit test coverage

### Orta Vadeli (3-6 ay)
- [ ] REST API geliştirme
- [ ] API dokümantasyonu
- [ ] Performance optimization
- [ ] Advanced reporting

### Uzun Vadeli (6+ ay)
- [ ] Mobile app development
- [ ] Cloud deployment
- [ ] Multi-tenant architecture
- [ ] AI-powered analytics
- [ ] Integration APIs

---

**⚠️ UYARI**: Bu proje geliştirme aşamasındadır. Production kullanımı için ek güvenlik, test ve optimizasyon çalışmaları gereklidir. 