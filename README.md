# NotesPortal

Система управления заметками с поддержкой категорий, тегов, аутентификации и real-time уведомлений.

## 📋 Описание проектов

### NotesApp
Основное веб-приложение на ASP.NET Core MVC для управления заметками с поддержкой:
- ✅ CRUD операций с заметками
- ✅ Категоризация и тегирование
- ✅ Аутентификация и авторизация (роли: Administrator, Moderator, User)
- ✅ Real-time уведомления через SignalR
- ✅ Многоязычность (EN/RU)
- ✅ Система избранного
- ✅ Права доступа на основе ролей

### NotesApi
RESTful API для управления баннерами:
- ✅ Версионирование API (v1)
- ✅ Swagger/OpenAPI документация
- ✅ CRUD endpoints для баннеров
- ✅ Entity Framework Core + PostgreSQL

### NotesPortalTest.E2E
End-to-End тесты с использованием Selenium WebDriver и NUnit.

## 🛠️ Технологический стек

- .NET 8.0
- ASP.NET Core MVC / Web API
- Entity Framework Core 9.0
- PostgreSQL
- SignalR
- Selenium WebDriver (тестирование)
- NUnit

## 🚀 Быстрый старт

### Вариант 1: Docker (Рекомендуется) 🐳

**Самый быстрый способ запустить проект:**

```bash
git clone https://github.com/R-u-s-h/NotesPortal.git
cd NotesPortal
docker-compose up -d
```

**Готово!** Приложение доступно:
- **NotesApp**: http://localhost:5000
- **NotesApi**: http://localhost:5001/swagger
- **PostgreSQL**: localhost:5432

Остановка: `docker-compose down`

---

### Вариант 2: Локальная установка

#### Предварительные требования

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/) 12+
- (Опционально) [Rider](https://www.jetbrains.com/rider/) или [Visual Studio](https://visualstudio.microsoft.com/)
- (Для E2E тестов) Chrome/Chromium браузер

### 1. Клонирование репозитория

```bash
git clone https://github.com/R-u-s-h/NotesPortal.git
cd NotesPortal
```

### 2. Настройка базы данных

Создайте PostgreSQL базу данных:

```sql
CREATE DATABASE your_db;
CREATE USER your_username WITH PASSWORD 'your_password';
GRANT ALL PRIVILEGES ON DATABASE your_db TO your_username;
```

### 3. Конфигурация подключений

#### Вариант A: Использование appsettings.Development.json (рекомендуется для разработки)

Создайте файл `NotesApp/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultDbConnection": "Host=localhost;Port=5432;Database=your_db;Username=your_username;Password=your_password"
  }
}
```

Создайте файл `NotesApi/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultDbConnection": "Host=localhost;Port=5432;Database=your_db;Username=your_username;Password=your_password"
  }
}
```

#### Вариант B: Использование User Secrets (рекомендуется для production)

```bash
# Для NotesApp
cd NotesApp
dotnet user-secrets set "ConnectionStrings:DefaultDbConnection" "Host=localhost;Port=5432;Database=your_db;Username=your_username;Password=your_password"

# Для NotesApi
cd ../NotesApi
dotnet user-secrets set "ConnectionStrings:DefaultDbConnection" "Host=localhost;Port=5432;Database=your_db;Username=your_username;Password=your_password"

# Для E2E тестов (учетные данные администратора)
cd ../NotesPortalTest.E2E
dotnet user-secrets set "TestAdmin:Login" "your_admin_login"
dotnet user-secrets set "TestAdmin:Password" "your_admin_password"
```

### 4. Применение миграций

```bash
# Для NotesApp
cd NotesApp
dotnet ef database update

# Для NotesApi
cd ../NotesApi
dotnet ef database update
```

### 5. Запуск приложений

#### Запуск NotesApp (MVC приложение)

```bash
cd NotesApp
dotnet run
```

Приложение будет доступно по адресу: `https://localhost:5001` или `http://localhost:5000`

#### Запуск NotesApi (REST API)

```bash
cd NotesApi
dotnet run
```

API будет доступен по адресу: `https://localhost:7001` или `http://localhost:5001`

Swagger UI: `https://localhost:7001/swagger`

### 6. Запуск тестов

```bash
cd NotesPortalTest.E2E
dotnet test
```

## 📁 Структура проекта

```
NotesPortal/
├── NotesApp/                      # Основное MVC приложение
│   ├── Controllers/               # Контроллеры
│   ├── DbStuff/                   # База данных и репозитории
│   │   ├── Models/Notes/         # Модели данных
│   │   └── Repositories/         # Репозитории
│   ├── Services/                  # Бизнес-логика
│   ├── Views/                     # Razor представления
│   ├── Hubs/                      # SignalR хабы
│   ├── Localizations/            # Ресурсы локализации
│   └── wwwroot/                   # Статические файлы
│
├── NotesApi/                      # REST API для баннеров
│   ├── Controllers/               # API контроллеры
│   ├── DTOs/                      # Data Transfer Objects
│   ├── Services/                  # Сервисы
│   └── DbStuff/                   # База данных
│
└── NotesPortalTest.E2E/          # E2E тесты
    ├── Tests/                     # Тестовые классы
    ├── Selectors/                # Page Object селекторы
    └── Helper/                    # Вспомогательные классы
```

## 🔐 Безопасность

⚠️ **ВАЖНО**: Никогда не коммитьте в Git следующие файлы с реальными данными:

- `appsettings.Development.json`
- `appsettings.Production.json`
- `appsettings.Local.json`
- Любые файлы с паролями и строками подключения

Эти файлы уже добавлены в `.gitignore`.

### Рекомендации:

1. **Используйте User Secrets** для локальной разработки
2. **Используйте Environment Variables** для production
3. **Используйте Azure Key Vault / AWS Secrets Manager** для облачных развертываний
4. **Никогда не храните пароли в коде**

## 📚 API Документация

### NotesApi Endpoints

#### Banners

```
GET    /api/v1/Banners          # Получить все баннеры
GET    /api/v1/Banners/{id}     # Получить баннер по ID
POST   /api/v1/Banners          # Создать новый баннер
PUT    /api/v1/Banners/{id}     # Обновить баннер
DELETE /api/v1/Banners/{id}     # Удалить баннер
```

Полная документация доступна через Swagger UI после запуска API.

## 👥 Роли пользователей (NotesApp)

- **Administrator**: Полный доступ ко всем функциям
- **Moderator**: Создание и редактирование заметок
- **User**: Просмотр заметок и управление профилем

## 🌍 Локализация

Приложение поддерживает следующие языки:
- Английский (EN)
- Русский (RU)

Язык можно выбрать в профиле пользователя.

## 🧪 Тестирование

### E2E тесты

```bash
cd NotesPortalTest.E2E
dotnet test
```

Перед запуском тестов:
1. Убедитесь, что NotesApp запущен
2. Настройте User Secrets с учетными данными администратора
3. Убедитесь, что Chrome/Chromium установлен

## 🎯 Особенности проекта

### NotesApp
- **Архитектура**: Repository Pattern, MVC
- **Аутентификация**: Cookie-based authentication
- **Real-time**: SignalR для мгновенных уведомлений
- **Локализация**: ResourceManager с поддержкой 2 языков (EN/RU)
- **Валидация**: Custom validation attributes
- **Responsive**: Адаптивный дизайн для всех устройств

### NotesApi
- **REST API**: RESTful принципы
- **API Versioning**: Поддержка версионирования через URL
- **Swagger**: Автогенерация документации
- **DTO Pattern**: Разделение моделей данных и передачи
- **CORS**: Настроенная политика CORS

---

## 🔧 Дополнительные команды

### Создание новой миграции

```bash
# NotesApp
cd NotesApp
dotnet ef migrations add YourMigrationName

# NotesApi
cd NotesApi
dotnet ef migrations add YourMigrationName
```

### Откат миграции

```bash
dotnet ef database update PreviousMigrationName
```

### Пересоздание базы данных

```bash
dotnet ef database drop
dotnet ef database update
```

### Просмотр User Secrets

```bash
cd NotesApp  # или NotesApi
dotnet user-secrets list
```

### Удаление User Secret

```bash
dotnet user-secrets remove "ConnectionStrings:DefaultDbConnection"
```

---

## 🚢 CI/CD и Деплой

Проект настроен для автоматического деплоя на AWS EC2 через GitHub Actions.

### ⚡ Быстрый деплой

1. **Настройте GitHub Secrets** (Settings → Secrets):
   - `DOCKER_USERNAME`, `DOCKER_PASSWORD`
   - `AWS_HOST`, `AWS_USERNAME`, `AWS_SSH_KEY`, `AWS_SSH_PORT`
   - `POSTGRES_USER`, `POSTGRES_PASSWORD`, `POSTGRES_DB`
   - `ADMIN_USERNAME`, `ADMIN_PASSWORD` (учётные данные администратора NotesApp)

2. **Push в main ветку:**
   ```bash
   git push origin main
   ```

3. **GitHub Actions автоматически:**
   - ✅ Соберет и протестирует проект
   - ✅ Создаст Docker образы
   - ✅ Задеплоит на AWS EC2


### 🛠️ Инфраструктура

- **Docker Hub**: Хранение образов
- **GitHub Actions**: CI/CD pipeline
- **AWS EC2**: Хостинг (t2.micro/t3.micro Free Tier)
- **PostgreSQL 16**: База данных с persistent volume

### 🔄 Workflow

```
Push → Build & Test → Docker Build → Push to Hub → Deploy to AWS
```

