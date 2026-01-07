# TextGenApp - Personalized AI Language Learning Assistant

TextGenApp, dil öğrenimindeki **bağlam eksikliğini** gidermek amacıyla tasarlanmış; kullanıcıların kişisel kelime listelerine dayalı, yapay zeka destekli dinamik hikayeler ve interaktif quizler üreten SaaS tabanlı bir backend sistemidir. Geleneksel yöntemlerin aksine, kullanıcının seviyesine ve öğrenmek istediği spesifik kelimelere odaklanarak öğrenme sürecini tamamen kişiselleştirir.

## 🚀 Öne Çıkan Özellikler (Öğretici Yaklaşım)

* **Dinamik Metin Üretimi:** Google Gemini LLM entegrasyonu ile kullanıcının belirlediği konu ve CEFR seviyesine uygun, hedef kelimeleri içeren özgün okuma metinleri oluşturulur.
* **Kelime Listesi Yönetimi:** Kullanıcılar kendi kelime listelerini oluşturabilir ve bu listelerdeki kelimelerin metin içerisinde kullanılmasını isteyebilir.
* **İnteraktif Quiz Sistemi:** Üretilen her metin için LLM tarafından otomatik olarak oluşturulan kavrama soruları ile okuma-anlama becerileri test edilir.
* **Kelime Mastery Takibi:** Kullanıcıların öğrendikleri kelimeleri listelere ekleyebildiği ve bu kelimelerin pekiştirilmesi için LLM aracılığıyla metinlere dahil edildiği bir yapı sunar.

## 🏗️ Sistem Mimarisi

Proje, kurumsal standartlarda **Mikroservis mimarisi** ile kurgulanmıştır:


**TextGen Service:** LLM entegrasyonu, içerik üretimi ve quiz mantığının yönetildiği ana servistir.

**Vocabulary Service:** Kullanıcı kelime listeleri ve kelime statülerinin yönetildiği servistir.


* **Ocelot API Gateway:** Tüm mikroservis isteklerinin tek bir noktadan güvenli ve performanslı şekilde yönlendirilmesi sağlanır.
* **Servisler Arası İletişim:** Mevcut sürümde servisler arası iletişim **HTTP** üzerinden sağlanmaktadır; production için **gRPC** geçişi planlanmaktadır.

## 🛠️ Teknolojiler

* **Framework:** .NET 10 (C#)
* **Mimari Yaklaşım:** Mikroservis Mimarisi, CQRS (MediatR), Clean Architecture
* **Veri Yönetimi:** PostgreSQL & Entity Framework Core
* **AI/LLM:** Google Gemini AI API
* **Doğrulama:** FluentValidation
* **API Gateway:** Ocelot Gateway
* **Background Jobs :** Hangfire
* **DevOps & Altyapı:**
* **Docker - Docker Compose:** Containerization ve yerel orkestrasyon.
* **Nginx:** Reverse Proxy ve sunucu yapılandırması.
* **AWS (EC2):** Bulut ortamında canlı yayım.



## 💻 Kurulum ve Çalıştırma

Projeyi yerel ortamınızda Docker kullanarak hızlıca ayağa kaldırabilirsiniz:

```bash
# Depoyu klonlayın
git clone https://github.com/emirhansesigur/textGenApp.git

# Docker Compose ile tüm servisleri başlatın
docker-compose up -d

```

## 🌐 Canlı Yayın

Projenin backend altyapısı **AWS EC2** üzerinde **Nginx** ile yapılandırılmış şekilde aktif olarak çalışmaktadır. Mobil uygulama entegrasyonu süreçleri devam etmektedir.