# Kurumsal Tedarikçi Kabiliyet Yönetim Sistemi 🏭

Tedarik zinciri operasyonlarını dijitalleştirmek ve merkezileştirmek için tasarlanmış, çok rollü ve kapsamlı bir ASP.NET Core web platformudur. Bu kurumsal sistem; Tedarikçiler, Kalite Güvence (QA) ekipleri, Satın Alma departmanları ve Sistem Yöneticileri için özel portallar sunarak kesintisiz işbirliği ve veriye dayalı karar alma süreçleri sağlar.

## 👥 Role Dayalı Portallar ve Özellikler (RBAC)

### 🏢 1. Tedarikçi Portalı (Self-Servis)
*   **Varlık ve Kabiliyet Yönetimi:** Tedarikçiler kendi makine parkurlarını, üretim proseslerini, İK verilerini ve teknik kabiliyetlerini bağımsız olarak güncelleyebilir.
*   **Sertifika Takibi:** Kalite sertifikalarının sisteme yüklenmesi ve geçerlilik sürelerinin takibi.
*   **Değişiklik Geçmişi (Audit Logging):** Tedarikçi verilerinde yapılan her değişiklik kayıt altına alınarak tam şeffaflık sağlanır.

### 🛡️ 2. Kalite Güvence (QA) Portalı
*   **Uyumluluk İzleme:** Tedarikçi sertifikalarının ve teknik standartlarının takibi, incelenmesi ve doğrulanması.
*   **Denetime Hazırlık:** Belirli kalite yetkinliklerine ve proses onaylarına göre tedarikçilerin filtrelenmesi.

### 💰 3. Satın Alma Portalı
*   **Gelişmiş Arama ve Filtreleme:** Makine parkuru, aktif sertifikalar veya teknik kapasite bazında en uygun tedarikçileri bulmak için güçlü sorgulama araçları.
*   **Otomatik "Tek Sayfa" Profiller:** Sistem, satın alma kararlarını hızlandırmak için ham tedarikçi verilerini otomatik olarak derleyip, okunması kolay, optimize edilmiş "Tek Sayfa Tedarikçi Profili" (One-Pager) oluşturur.

### ⚙️ 4. Yönetici (Admin) Portalı
*   **Sistem ve Kullanıcı Yönetimi:** Rol atamaları, sistem yapılandırmaları ve platform genelindeki denetim loglarının tam kontrolü.

## 🛠️ Teknoloji Yığını ve Mimari

*   **Backend:** ASP.NET Core MVC (.NET), C#
*   **Güvenlik:** Role Dayalı Erişim Kontrolü (RBAC), Claim Tabanlı Yetkilendirme, JWT/Cookie Auth
*   **Frontend:** HTML5, CSS3 (Glassmorphism UI, Flexbox), Vanilla JavaScript, FontAwesome
*   **Mimari:** Çok Katmanlı Mimari (N-Tier Architecture), Veri Transfer Objeleri (DTO), Modülerlik için ViewComponent kullanımı
*   **Veri Bütünlüğü:** Güçlü istemci ve sunucu taraflı doğrulama, güvenli form verisi yenileme (repopulation)

## 🎨 UI/UX Özellikleri
Uygulama, yüksek okunabilirlik ve premium bir hissiyat sunmak için tasarlanmış modern bir **Dark Mode / Glassmorphism (Buzlu Cam)** arayüzüne sahiptir. Duruma bağlı buton yükleme (loading) animasyonları, çift gönderimi (double-submit) engelleme ve dinamik veri bağlama gibi en iyi kullanıcı deneyimi pratiklerini içerir.

## ⚙️ Kurulum ve Çalıştırma

1. Projeyi bilgisayarınıza indirin (Clone):
   ```bash
   git clone [https://github.com/selimpolat/Tedarikci-Kabiliyet-Platformu.git](https://github.com/selimpolat/Tedarikci-Kabiliyet-Platformu.git)
