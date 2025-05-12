# 📞 Call Center Simulation

Proje github link : https://github.com/Mertcan-T/DataStructuresProject-CallCenter

Bu proje, ASP.NET Core MVC ve SignalR kullanılarak geliştirilmiş bir **müşteri temsilcisi çağrı merkezi simülasyonudur**. Müşteriler sistem üzerinden taleplerini iletir, temsilciler bu talepleri sırayla cevaplar. Sistem, özelleştirilmiş veri yapıları ile çalışır ve tüm işlemler gerçek zamanlı olarak güncellenir.

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


## 🧠 Nasıl Çalışır?

1. **Müşteri** adını ve talebini girerek kuyruğa girer.
2. **Temsilci**, sıradaki müşteriyi çağırır ve cevap yazar.
3. **SignalR**, tüm istemcilere kuyruk güncellemesini gerçek zamanlı gönderir.
4. **Müşteri**, temsilci cevabını görür ve ardından anketi doldurur.
5. Anketler bir `Stack` yapısında saklanır, önceki geri bildirimler istenirse görülebilir.

---

## 🧪 Veri Yapıları

| Veri Yapısı | Kullanım Amacı |
|-------------|----------------|
| `Kuyruk<T>` | Müşteri taleplerini FIFO sırasıyla yönetmek |
| `Stack`     | Anket sonuçlarını geri dönüş sırasıyla saklamak |
| `Dictionary`| Aktif müşterileri hızlı erişimle tutmak |
| `LinkedList`| Temsilci işlem geçmişini zaman sırasına göre tutmak |


---

## ✨ Geliştirici Notları

Bu simülasyon, veritabanı kullanmadan sadece C# veri yapılarıyla oluşturulmuştur. Amaç, hem gerçek zamanlı sistem mantığını hem de veri yapılarının etkili kullanımını öğretmektir.

---

