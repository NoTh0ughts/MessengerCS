<div id="MainTitle">

# Серверное решение для мессенджера

</div>

<div id="SubTitle"> 

### Микросервисное приложение для мобильного или десктопного мессенджера

</div>

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
### Серверное решение
<div id="TechStack">

* ASP.NET Core
* EF 
* MediatR

</div>

### Клиентское решение
- Отсутствует

---

# Описание обновлений
1. Создано серверное решение, работающее монолитно. Содержит базовые запросы сервера. Автодокументация Swagger. Подключена база данных.
2. Приложение разделенно на микросервисы. Текущие сервисы: Auth, Messages. Данные и все взаимодействие вынесено в отдельную библиотеку Data. Базовые классы, части важной логики вынесены в библиотеку BusinessLogic. Реализованна авторизация и операции с сообщениями. Добавлена поддержка Docker, Docker Compose.
3. Добавленны и настроены сервисы Dialog и User. Написанны некоторые запросы для них.
4. Добавлен сервер nginx, с конфигом default.conf. Перенаправляет все роуты в соответствующий сервер. Добавлен сервер Nginx в docker-compose и настроены зависимости от микросервисов
 
 # Архитектура микросервисов
<img src="Снимок%20экрана%202022-05-26%20021029.png">
