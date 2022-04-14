# Messenger
Мобильное приложение для обмена сообщениями. 

# Команда и роли
- <a href="https://github.com/kickSanchoz">Александр Васильев ИВТ-463 - Мобильное приложение</a>
- <a href="https://github.com/NoTh0ughts">Деревянкин Павел ИВТ-463 - Backend</a>
- <a href="https://github.com/LanLook">Лукьяненко Милана ИВТ-463 - Работа с бд, запросы, документация, тестирование</a>
- <a href="https://github.com/renhen">Шабанова Ирина ИВТ-463 - Backend</a>

---
# Цель проекта
Разработать серверное и мобильное клиентское решение, позволяющее обмениваться сообщениями с другими пользователями.

---

# Технологический стек
## Серверное решение
- ASP .NET Core 6.0 - платформа разработки.
- C# 9.0 - версия языка.
   ### Библиотеки
   - Entity Framework Core 5.0 - ORM
   - Pomelo.EntityFrameworkCore.MySQL 5.0.4 - провайдер данных
   - System.IdentityModel.Jwt 6.16.0 - токенная авторизация
   - EntityFramework 6.4.4 - работа с данными
   - Newtonsoft.JSON 13.0.1 - сериализация/десериализация данных
   - Swashbuckle.AspNetCore.Swagger 6.3.0 - автодокументация
   - MediatR 10.0.1 - преобразование запросов в объекты
   - MediatR.Extensions.Microsoft.DependencyInjection 10.0.1 - функции добавления медиатора в контейнер DI
   - Microsoft.Extensions.DependencyInjection 6.0.0 - инъектирование зависимостей
## Клиентское решение
- ...

---

# Описание обновлений
1. Создано серверное решение, работающее монолитно. Содержит базовые запросы сервера. Автодокументация Swagger. Подключена база данных.
2. Приложение разделенно на микросервисы. Текущие сервисы: Auth, Messages. Данные и все взаимодействие вынесено в отдельную библиотеку Data. Базовые классы, части важной логики вынесены в библиотеку BusinessLogic. Реализованна авторизация и операции с сообщениями. Добавлена поддержка Docker, Docker Compose.
3. Добавленны и настроены сервисы Dialog и User. Написанны некоторые запросы для них.
 
 
