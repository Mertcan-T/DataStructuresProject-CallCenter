
# 📞 Call Center Simulation

Proje github link : https://github.com/Mertcan-T/DataStructuresProject-CallCenter

Bu proje, ASP.NET Core MVC mimarisi ve SignalR kütüphanesi kullanılarak geliştirilmiş gerçek zamanlı bir müşteri temsilcisi çağrı merkezi simülasyonudur. Gerçek hayattaki çağrı merkezlerinin işleyişini temel alarak tasarlanmış bu sistemde, müşteriler ad ve taleplerini girerek sisteme başvuru yapar. Başvurular, özel olarak geliştirilen bağlı liste tabanlı Kuyruk<T> veri yapısı ile FIFO (First-In First-Out) prensibine göre sıraya alınır.

Temsilci ekranı, gelen talepleri anlık olarak takip eder ve sıradaki müşterinin isteğine özel yanıtlar verebilir. Temsilcinin gönderdiği yanıt, müşterinin ekranına gerçek zamanlı olarak ulaşır. Ayrıca, işlem tamamlandıktan sonra müşteriye bir memnuniyet anketi sunularak kullanıcıdan geri bildirim alınır.

Bu projede klasik veritabanı sistemleri yerine tamamen in-memory (bellek içi) özel veri yapıları kullanılmıştır. Her veri yapısının kullanımı, kendi zaman ve alan avantajlarına göre seçilmiş ve sistemin uygun alanlarına entegre edilmiştir.

---

## 📌 Özellikler

- Müşteri talebi oluşturma
- FIFO mantığı ile kuyrukta sıralama (`Kuyruk<T>`)
- Gerçek zamanlı kuyruk güncellemeleri (SignalR)
- Temsilciden müşteriye anlık cevap gönderimi
- Cevaplanan müşteriler için geri bildirim anketi (`Stack`)
- Aktif müşteri takibi (`Dictionary`)
- İşlem geçmişi kayıtları (`LinkedList`)

---

## 🧠 Veri Yapıları ve Nedenleri

| Veri Yapısı | Kullanım Amacı | Zaman Karmaşıklığı | Neden Bu Yapı? |
|-------------|----------------|---------------------|----------------|
| `Kuyruk<T>` | Müşteri taleplerini FIFO sırasıyla yönetmek | Ekleme: O(1), Silme: O(1) | FIFO sırasına en uygun yapı olduğu için tercih edildi. Alternatif sabit dizi yerine `linked list` tabanlı yapı ile dinamik boyut sağlandı. |
| `Dictionary<int, Customer>` | Aktif müşterileri benzersiz şekilde takip etmek | Arama: O(1), Ekleme: O(1), Silme: O(1) | Aynı isimle tekrar başvuru yapılmasını engellemek için ideal. Benzersizliği ve hızlı erişimi garanti eder. |
| `Stack<CustomerFeedback>` | Anketleri son giren ilk çıkar (LIFO) sırasıyla tutmak | Ekleme: O(1), Silme: O(1) | Geri bildirimleri en son yapılan işlem üzerinden hızlıca almak için uygundur. |
| `LinkedList<string>` | Temsilcilerin işlem geçmişini zaman sırasına göre kaydetmek | Ekleme: O(1) | Zaman sıralı log tutmak için idealdir. Diziye göre daha az yeniden boyutlandırma gerektirir. |

---
## 🧠 Nasıl Çalışır?

1. **Müşteri** adını ve talebini girerek kuyruğa girer.
2. **Temsilci**, sıradaki müşteriyi çağırır ve cevap yazar.
3. **SignalR**, tüm istemcilere kuyruk güncellemesini gerçek zamanlı gönderir.
4. **Müşteri**, temsilci cevabını görür ve ardından anketi doldurur.
5. Anketler bir `Stack` yapısında saklanır, önceki geri bildirimler istenirse görülebilir.
---

## 🛠 Kullanılan Teknolojiler

- ASP.NET Core MVC (.NET 6/7/8)
- SignalR (real-time iletişim)
- HTML & CSS
- C# (backend)
- In-memory veri yapıları (veritabanı yok)

---

## 💻 Kurulum ve Çalıştırma

### 🔧 Gereksinimler

- [.NET 6 SDK veya üzeri](https://dotnet.microsoft.com/download)
- Visual Studio 2022 veya üzeri (veya Visual Studio Code)
- Git (isteğe bağlı)

### 🔄 Adımlar

1. Bu repoyu bilgisayarına klonla:

```bash
git clone https://github.com/kullaniciAdi/CallCenterSimulation.git
```

2. Projeyi Visual Studio ile aç:
   - `CallCenterSimulation.sln` dosyasını çift tıkla.
   - Ya da terminalde şunu yaz:  
     ```bash
     cd CallCenterSimulation
     code .  # (VS Code için)
     ```

3. Gerekli NuGet paketleri yüklenecektir. Yüklenmezse:
   - Sağ tıklayıp “Restore NuGet Packages” seçeneğini kullan.
   - Ya da terminalde:
     ```bash
     dotnet restore
     ```

4. Projeyi başlat:

```bash
dotnet run
```

veya Visual Studio'da `F5` tuşuna bas.

5. Tarayıcıda şu adresi aç:

```
https://localhost:5001
```

---

## 👥 Proje Katkıcıları

| Öğrenci No | İsim |
|------------|------|
| 032290088  | ERDOĞAN TOPÇU |
| 032290096  | SARAH AHMAD ALAYI |
| 032290098  | SAHAR ZAR |
| 032290109  | MUSTAFA ÖZTÜRK |
| 032290113  | MERTCAN TAŞKIRAN |

---

## ✨ Geliştirici Notları

Bu simülasyon, veritabanı kullanmadan sadece C# veri yapılarıyla oluşturulmuştur. Amaç, hem gerçek zamanlı sistem mantığını hem de veri yapılarının etkili kullanımını öğretmektir.


