Работает как с локалки, так и с внешней сети.
API запросов только через JSON.

API:
--Вход
Q: {"function":"entrance","email":"starson4588@gmail.com","password":"koolherc"}
A: func=entrance;result=1; -- такая связка логин-пароль существует
A: func=entrance;result=0; -- такой связки логин-пароль не существует

--Регистрация
Q: {"function":"registration","email":"starson4588@gmail.com","password":"koolherc"}
A: func=registration;result=0; -- email уже зарегистрирован
A: func=registration;result=1; -- успешная регистрация
A: func=registration;result=2; -- некорректный email

--Забыть пароль
Q: {"function":"remind","email":"starson4588@gmail.com"}
A: func=remind;result=0; -- пользователя с таким email не найдено
A: func=remind;result=1; -- мы успешно выслали вам ваш вароль

--Добавить напоминание
Q: {"function":"add_note","name":"test1","user":"starson4588@gmail.com","x":3,"y":4}
A: func=add_notification;result=1; -- такой пользователь, есть и мы успешно добавили
A: func=add_notification;result=0; -- пользователь с таким email не найден

--Получить все напоминания. Если успешно, то разделение напоминаний ";;;"
Q: {"function":"get_notes","email":"starson4588@gmail.com"}
A: func=get_notification;result=1;;;{"id":1,"name":"test1","owner":"starson4587@gmail.com","x":3,"y":4};;
-- разделение в виде ;;
A: func=get_notification;result=0; -- у пользователя нет напоминаний.

