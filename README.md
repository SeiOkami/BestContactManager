# Что умеет?

* Менеджер контактов. Позволяет зарегистрироваться и хранить свои контакты
* Функция импорта и экспорта в файл
* Возможность генерации тестовых контактов
* Веб и десктоп приложение

# Из чего сделано?

* CQRS
* ASP NET Core 6
* EF Core 6 (SQLite)
* MediatR
* Automapper
* Web API
* Swagger
* Отдельный IdentityServer4
* MVC
* WPF

# Как запустить?

* Открыть solution Contacts.Identity\Contacts.Identity.sln 
* Запустить project Contacts.Identity 
* Открыть solution Contacts.Backend\BestContactManager.Backend.sln 
* Запустить project Contacts.WebApi 
* Открыть solution Contacts.Clients\Contacts.Clients.sln 
* Запустить project Contacts.WebClient 
* Запустить project Contacts.DesctopClient 

Все базы самостоятельно создаются. Сервер идентификации при старте имеет пользователей:

alice : Pass123$
bob : Pass123$

Можно регистрироваться и создавать своих.
