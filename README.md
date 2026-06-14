# Portal Kibica Wisły Kraków

Aplikacja webowa stworzona dla kibiców klubu Wisła Kraków. Umożliwia przeglądanie aktualności klubowych, składu zespołu oraz terminarza meczów, a także posiada panel administracyjny do zarządzania treściami.

## Opis projektu

Portal Kibica to system zarządzania treścią (CMS) dedykowany dla klubu piłkarskiego. Aplikacja składa się z dwóch części:

- **Część publiczna** – dostępna dla wszystkich użytkowników, prezentuje aktualności, skład zawodników (z podziałem na pozycje), terminarz najbliższych i rozegranych meczów oraz stronę główną z najważniejszymi informacjami.
- **Panel administracyjny** – dostępny po zalogowaniu, umożliwia pełne zarządzanie (CRUD) aktualnościami, zawodnikami i meczami, w tym upload zdjęć.

## Funkcjonalności

- Logowanie administratora (ASP.NET Core Identity)
- Panel admina z dashboardem (statystyki: liczba newsów, zawodników, meczów)
- Zarządzanie aktualnościami: dodawanie, edycja, usuwanie, upload zdjęć
- Zarządzanie zawodnikami: dodawanie, edycja, usuwanie, upload zdjęć, podział na pozycje
- Zarządzanie meczami: dodawanie, edycja, usuwanie, wyniki
- Publiczna lista aktualności z wyszukiwarką
- Publiczna lista zawodników z filtrowaniem wg pozycji
- Publiczny terminarz (najbliższe i rozegrane mecze)
- Strona główna z najbliższym meczem i najnowszymi newsami
- Responsywny design (Bootstrap 5) w barwach klubowych Wisły Kraków
- Licznik znaków w formularzu dodawania/edycji aktualności (JavaScript)

## Użyte technologie

- ASP.NET Core 8.0 MVC
- Entity Framework Core (Code First, migracje)
- Microsoft SQL Server (LocalDB)
- ASP.NET Core Identity (logowanie administratora)
- Bootstrap 5
- JavaScript (Vanilla JS)
- Razor Views (cshtml)

## Wymagania

- Visual Studio 2022 (lub nowszy) z workloadem ASP.NET i web development
- .NET 8 SDK
- SQL Server LocalDB (instalowany domyślnie z Visual Studio)

## Instrukcja uruchomienia lokalnego

1. **Sklonuj repozytorium:**

2. **Otwórz plik `PortalKibica.sln` w Visual Studio.**

3. **Sprawdź connection string** w pliku `appsettings.json` – domyślnie aplikacja korzysta z LocalDB: Server=(localdb)\mssqllocaldb;Database=PortalKibicaDb;Trusted_Connection=True;MultipleActiveResultSets=true

4. **Zastosuj migracje bazy danych** – otwórz **Tools → NuGet Package Manager → Package Manager Console** i wykonaj: Update-Database
To utworzy bazę danych `PortalKibicaDb` z wszystkimi wymaganymi tabelami.

5. **Uruchom aplikację**

6. Aplikacja uruchomi się w przeglądarce na adresie podobnym do `https://localhost:7166`.

## Logowanie do panelu administracyjnego

Po pierwszym uruchomieniu aplikacja automatycznie tworzy konto administratora:

- **Adres URL:** `/Identity/Account/Login`
- **E-mail:** `admin@wisla.pl`
- **Hasło:** `Admin123!`

Po zalogowaniu w prawym górnym rogu nawigacji pojawi się link **Panel admina**, prowadzący do dashboardu z zarządzaniem aktualnościami, zawodnikami i meczami.

## Struktura bazy danych

- **News** – aktualności (tytuł, treść, data publikacji, zdjęcie)
- **Players** – zawodnicy (imię i nazwisko, pozycja, numer, opis, zdjęcie)
- **Matches** – mecze (rywal, data meczu, stadion, wynik)
- **AspNetUsers** i tabele Identity – konta administratorów



Projekt zrealizowany w ramach przedmiotu Techniki Internetowe - Informatyka i Ekonometria, AGH.
