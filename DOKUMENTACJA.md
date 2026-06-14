# Dokumentacja techniczna – Portal Kibica Wisły Kraków

## 1. Architektura rozwiązania

Aplikacja została zbudowana w oparciu o wzorzec **MVC (Model-View-Controller)** w technologii **ASP.NET Core 8.0**.

### Podział na obszary (Areas)

Aplikacja wykorzystuje mechanizm **Areas** do rozdzielenia:
- części publicznej (dostępnej dla każdego użytkownika, bez logowania),
- panelu administracyjnego (`Areas/Admin`, dostępny tylko po zalogowaniu, oznaczony atrybutem `[Authorize]`),
- modułu logowania (`Areas/Identity`, generowany przez scaffolding ASP.NET Core Identity).

### Uwierzytelnianie i autoryzacja

Logowanie administratora realizowane jest przez **ASP.NET Core Identity**. Konto administratora jest tworzone automatycznie przy pierwszym uruchomieniu aplikacji (seed w `Program.cs`). Wszystkie kontrolery w `Areas/Admin` są zabezpieczone atrybutem `[Authorize]`, co oznacza, że dostęp do nich wymaga zalogowania.

## 2. Struktura bazy danych

Baza danych: **SQL Server (LocalDB)**, nazwa: `PortalKibicaDb`. Dostęp do bazy realizowany jest przez **Entity Framework Core** (Code First, migracje).

### Tabele Identity
Standardowe tabele generowane przez ASP.NET Core Identity (`AspNetUsers`, `AspNetRoles`, `AspNetUserClaims`, `AspNetUserLogins`, `AspNetUserRoles`, `AspNetUserTokens`) – przechowują dane kont administratorów.

## 3. Najważniejsze funkcjonalności

### Część publiczna

- **Strona główna** – wyświetla najbliższy zaplanowany mecz oraz trzy najnowsze aktualności.
- **Aktualności (`/News`)** – lista wszystkich newsów z możliwością wyszukiwania po tytule i treści, oraz strona szczegółów pojedynczego newsa.
- **Zawodnicy (`/Players`)** – lista zawodników pogrupowana wg pozycji, z możliwością filtrowania po wybranej pozycji, oraz strona profilu zawodnika.
- **Terminarz (`/Matches`)** – podział na mecze nadchodzące (karty) i rozegrane (tabela z wynikami).

### Panel administracyjny (`/Admin`)

- **Dashboard** – statystyki: liczba aktualności, zawodników i meczów w bazie, szybkie linki do zarządzania i dodawania nowych wpisów.
- **Zarządzanie aktualnościami** – pełny CRUD (Create, Read, Update, Delete), upload i podmiana zdjęcia, licznik znaków w polu treści (JavaScript).
- **Zarządzanie zawodnikami** – pełny CRUD, upload i podmiana zdjęcia.
- **Zarządzanie meczami** – pełny CRUD, możliwość wpisania wyniku po rozegraniu meczu.

## 4. Technologie

- ASP.NET Core 8.0 MVC
- Entity Framework Core (Code First + migracje)
- Microsoft SQL Server (LocalDB)
- ASP.NET Core Identity
- Bootstrap 5
- JavaScript (Vanilla JS)