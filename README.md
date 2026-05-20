# 🌿 PlantCatalog — Магазин растений 🌿

Веб-приложение для каталогизации и продажи комнатных растений.

## Описание

Веб-приложение для каталогизации и продажи комнатных растений, горшков и аксессуаров. Реализовано на **.NET 8 + ASP.NET Core Blazor Server** с использованием базы данных SQLite.

## Технологии

- **.NET 8**, ASP.NET Core, Blazor Server (InteractiveServer)
- **Entity Framework Core** (CodeFirst подход, SQLite)
- **Razor Components**, Dependency Injection
- **Docker** (Multi-stage build)
- **GitHub** (исходный код)

## Запуск

Локальный запуск:
1. Откройте терминал в папке с проектом
2. Впишите dotnet restore
3. Затем dotnet run

Запуск через Docker:
1. Откройте терминал в папке с проектом
2. Соберите образ —  docker compose build
3. Зпустите контейнер в фоновом режиме — docker compose up -d

## Структура проекта

Components/ — Razor-страницы, сервисы, модели
Data/ — DbContext, миграции базы данных
wwwroot/ — статические файлы, изображения товаров

## Автор

Kpblc-30, Федяхина Марина Александровна
ББСО-01-24, 2026

## Лицензия

Проект создан в учебных целях