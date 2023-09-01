# Echipa_de_fotbal
Aplicația folosește o bază de date PostgreSQL pentru aceasta trebuie instalat PostgreSQL.
Descărcați PostgreSQL:
  Accesați site-ul oficial PostgreSQL (https://www.postgresql.org/download/)
  Alegeți versiunea 15, varianta Windows x86-64
  Rulați installer-ul
  Selectați portul
  Rulați pgAdmin4

Pentru a rula aplicația folosim IDE-ul Visual Studio 2022.
Aceasta poate fi descărcat de pe site-ul: https://visualstudio.microsoft.com/downloads/
Din installer adaugam .Net desktop development
Instalăm pachetele NuGet necesare:
 Click-dreapta pe proiect
 Manage NuGet Packages
 Apoi instalăm următoarele:
    Microsoft.EntityFrameworkCore
    Microsoft.EntityFrameworkCore.Tools
    Microsoft.Extensions.Configuration.Json
    Npgsql.EntityFrameworkCore.PostgreSQL

La rulare ne este deschis un meniu cu tabelele pe care putem opera. La selectia unei tabele ne sunt prezentate operatiile care pot fi utilizate.
