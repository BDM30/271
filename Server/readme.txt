Сервер поддерживаем регистрацию, вход, и восстановление.
Работает как с локалки, так и с внешней сети.
К нему прикреплена отправлялка запросов на с#. (она ток для теста)
Синнтаксис запросов не изменился.

API:
--Вход
Q: func=entrace;email=example@vlad.ru;password=1488;
A: func=entrace;result=1; -- такая связка логин-пароль существует
A: func=entrace;result=0; -- такой связки логин-пароль не существует

--Регистрация
Q: func=registration;email=example@vlad.ru;password=1488;
A: func=registration;result=0; -- email уже зарегистрирован
A: func=registration;result=1; -- успешная регистрация
A: func=registration;result=1; -- некорректный email

--Забыть пароль
Q: func=remind;email=example@vlad.ru;
A: func=remind;result=0; -- пользователя с таким email не найдено
A: func=remind;result=1; -- мы успешно выслали вам ваш вароль

