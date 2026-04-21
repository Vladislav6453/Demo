# Инструкция по развёртыванию системы

## 1. Локальный запуск (разработка)

1. Клонировать репозиторий:
   git clone https://github.com/Vladislav6453/Demo.git
   cd Demo

2. Открыть решение Demo.sln в Visual Studio 2022.
3. Восстановить NuGet-пакеты.
4. Убедиться, что база данных RetailStore создана.
5. Запустить проект (F5).

## 2. Установка на компьютер магазина (ClickOnce)

1. В Visual Studio: Project → Properties → Publish.
2. Настроить параметры публикации.
3. Нажать Publish Now.
4. Распространить установщик.

## 3. Создание MSIX-пакета

Использовать Visual Studio или MSIX Packaging Tool для создания современного MSIX-пакета.

## Требования
- Windows 10 / 11
- .NET 8 Desktop Runtime
- Microsoft SQL Server Express