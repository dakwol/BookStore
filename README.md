# Запуск и настройка сервера

Скачиваем и устанавливаем PgAdmin и экспортируем дамп базы из корня проекта PmSoftDevDump.sql

Скачиваем dotnet
В проекте переходим в папку backend и выполняем команды
dotnet ef migrations add --project DataAccess Initial
dotnet ef database update --project DataAccess --startup-project PMSoftAPI
dotnet run
Сервер запущен!

## Запуск front-end

Установка зависимостей npm install или npm install (--force)
npm start
Готово!

Аккаунт админа для входа на сайт
admin@mail.com
123

Аккаунт готового обычного пользователя
user@mail.com
123
