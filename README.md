# State Management Uygulaması

Temel bir state yönetimi API uygulamasıdır. **_Task yaratma, State yaratma, Flow yaratma_** ve **task durumu güncelleme** işlemleri yapmaktadır.

## Hikaye

Diyelim ki 4 adet state yaratıyoruz. Bunlar `StateA`, `StateB`, `StateC` ve `StateD` olsun. Ve bir adet `Task` yaratıyoruz. Bunun da ismi `Task1` olsun.

| StateA | StateB | StateC | StateD |
| ------ | ------ | ------ | ------ |
| Task1  |        |        |

Ve sonra da `Task1`in izleyeceği yolu belirlemek için `Flow` yaratıyoruz. Yani yukaridaki tablonun `Flow` açıklaması şu şekilde:
Sırası ile `StateA` <--> `StateB` <--> `StateC` <--> `StateD` yapabilir. Fakat `StateA`'dan `StateC`'ye geçemez. Ama flow'u bu şekilde de düzenleyebilirdik;

| StateA | StateC | StateB | StateD |
| ------ | ------ | ------ | ------ |
| Task1  |        |        |

**Task1** artık `StateA`'dan `StateC`'ye geçebilir. Fakat `StateA`'dan `StateB`ye **geçemez.**

---

| StateA | StateB         | StateC | StateD |
| ------ | -------------- | ------ | ------ |
|        | <--- Task1 --> |        |

**Task1** bulunduğu state'ten **bir önceki** state'e dönebilir ve **bir sonraki** state'e geçebilir.
yani;
**Task1** artık `StateB`'den `StateC`'ye geçebilir ve `StateB`'den `StateA`'ya geçebilir. `StateB`'den `StateD`'ye geçemez.

---

Farklı bir `Flow` yaratıp onun içerisinde de farklı tasklar yürütebilirim.

| StateA | StateC | StateD | StateB |
| ------ | ------ | ------ | ------ |
| Task1  |        |        |        |
| Task2  |        |        |        |

| StateX | StateY | StateZ | StateQ |
| ------ | ------ | ------ | ------ |
| Task3  |        |        |        |
| Task4  |        |        |        |

---

### Teknik Açıklama

Uygulama üzerindeki `Task`, `State`, `Flow` nesneleri `CREATE`, `READ`, `UPDATE`, `DELETE` işlemleri için API çağrıları kullanılacaktır.

[Restful Methods](https://restfulapi.net/http-methods/)

### Teknik gereksinimler

**Teknolojiler**

- Platform: .NET Core 2 ve üstü yada ASP.NET 4 veya üstü.
- IoC Kütüphanesi: Herhangi bir IoC container kullanılabilir
- ORM Kütüphanesi: Herhangi bir kütüphane kullanılabilir.
- API Kütüphanesi: ASP.NET Core Web API ya da ASP.NET Web API.
- Database: Herhangi bir relational database.

**Dependency Injection**

- ApiController sınıfları da dahil olmak üzere tüm sınıflar Dependency Injenction ile sağlanmalıdır.

**Repository Pattern**

- Servis katmanı ile veri erişim katmanı ayrıştırılmalıdır.

### İpucu

- Clean code güzel hazırlanmış bir pazar kahvaltısı gibidir.
- Unit test yazmak hava biraz kapalı olsa bile yanına şemsiyesini alan bir insanın tutumu gibidir.

### Teslim

Bu repository'i fork edip, kendi github hesabınız üzerinden geliştirmeyi yapınız. ve daha sonra geliştirmeyi yaptığınız reponun adresini erdem@proceedlabs.com adresine `Backend Developer - State Management Challange` başlığı ile yollayınız.
