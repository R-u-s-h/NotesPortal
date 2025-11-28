# NotesPortal Solution

Solution для проекта системы управления заметками.

## Проекты в решении

### NotesApp
Основное веб-приложение для управления заметками.

**Технологии:**
- ASP.NET Core MVC
- Entity Framework Core
- PostgreSQL
- SignalR (для real-time обновлений)
- Локализация (multilanguage support)

**Запуск:**
```bash
cd NotesApp
dotnet run
```

### NotesPortalTest.E2E
End-to-End тесты для веб-приложения NotesApp с использованием Selenium.

**Технологии:**
- NUnit
- Selenium WebDriver
- ChromeDriver

**Конфигурация:**
Для запуска тестов необходимо настроить User Secrets с учетными данными администратора:

```bash
cd NotesPortalTest.E2E
dotnet user-secrets set "TestAdmin:Login" "your-admin-login"
dotnet user-secrets set "TestAdmin:Password" "your-admin-password"
```

**Запуск тестов:**
```bash
cd NotesPortalTest.E2E
dotnet test
```

## Структура тестового проекта

```
NotesPortalTest.E2E/
├── Common/               # Общие конфигурации и константы
├── Helper/              # Вспомогательные классы (например, LoginHelper)
├── Selectors/
│   └── Notes/           # CSS/XPath селекторы для страниц
└── Tests/               # Тестовые классы
```

## Требования

- .NET 8.0 SDK
- PostgreSQL
- Chrome/Chromium (для E2E тестов)

