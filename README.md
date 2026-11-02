# Key Event Logger

Windows Forms (C#, .NET 8) ile yazılmış basit bir tuş olayı kaydedici. Nesne tabanlı programlama ödevi kapsamında geliştirilmiştir.

Program, tuş basma (KeyDown) ve bırakma (KeyUp) olaylarını zaman damgasıyla birlikte listeler ve isteğe bağlı olarak metin dosyasına dışa aktarır.

> **Kapsam notu:** Uygulama yalnızca kendi penceresi içinde oluşan tuş olaylarını dinler (`KeyPreview = true`). Sistem genelinde klavye kancası (global keyboard hook) kullanılmaz; pencere odakta değilken hiçbir tuş kaydedilmez ve arka planda tuş yakalama yapılmaz.

## Özellikler

- Tuş basma ve bırakma olaylarını zaman, olay türü, tuş adı, tuş kodu ve modifier (Ctrl, Shift, Alt) bilgileriyle listeleme
- Toplam kayıt sayısını durum çubuğunda gösterme
- **Temizle** düğmesiyle tüm kayıtları silme
- **Dışa Aktar** düğmesiyle kayıtları `.txt` dosyasına yazma
- **Yalnızca basılanları kaydet** kutusu ile yalnızca tuşa basma olaylarını kaydetme

## Kullanılan Teknolojiler

- C#
- .NET 8 (`net8.0-windows`)
- Windows Forms
- `KeyDown` / `KeyUp` olayları (Form düzeyinde, `KeyPreview` etkin)

## Sınıf Yapısı

| Dosya | Açıklama |
|---|---|
| `KeyEventRecord.cs` | Tek bir tuş olayının verisi (zaman, olay türü, tuş, modifier) |
| `KeyEventLog.cs` | Kayıtların koleksiyonu; ekleme, temizleme, metne çevirme ve `RecordAdded` olayı |
| `MainForm.cs` | Kullanıcı arayüzü ve olay yönetimi |
| `Program.cs` | Giriş noktası |

## Çalıştırma Adımları

1. [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) kurulumunu yapın (Windows).
2. Depoyu klonlayın:
   ```bash
   git clone https://github.com/neslihan111/key-event-logger-csharp.git
   cd key-event-logger-csharp
   ```
3. Terminalden çalıştırın:
   ```bash
   dotnet run
   ```
   veya Visual Studio'da `KeyEventLogger.csproj` dosyasını açıp **F5** tuşuna basın.
4. Açılan pencerede klavyeden tuşlara basın; olaylar listeye eklenir.
5. Düğmeler ve seçenekler:
   - **Temizle**: Listedeki tüm kayıtları siler.
   - **Dışa Aktar**: Bir kayıt dosyası seçtirir ve listeyi `.txt` olarak kaydeder.
   - **Yalnızca basılanları kaydet**: İşaretliyken yalnızca tuşa basma (Down) olayları kaydedilir, bırakma (Up) olayları atlanır.

## Ekran Görüntüleri

![Ekran 1](docs/screenshots/ekran1.png)

![Ekran 2](docs/screenshots/ekran2.png)

> Not: Ekran görüntüleri (`ekran1.png`, `ekran2.png`) henüz eklenmedi. Uygulama Windows üzerinde çalıştırıldıktan sonra alınıp `docs/screenshots/` klasörüne eklenecektir.
