Работает как с локалки, так и с внешней сети.
API запросов только через JSON.

API:
--Вход
Q: {"function":"entrance","email":"starson4588@gmail.com","password":"koolherc"}
A: {"function":"entrance","result":"1"} -- такая связка логин-пароль существует
A: {"function":"entrance","result":"0"} -- такой связки логин-пароль не существует

--Регистрация
Q: {"function":"registration","email":"starson4588@gmail.com","password":"koolherc"}
A: {"function":"registration","result":"0"} -- email уже зарегистрирован
A: {"function":"registration","result":"1"} -- успешная регистрация
A: {"function":"registration","result":"2"} -- некорректный email

--Забыть пароль
Q: {"function":"remind","email":"starson4588@gmail.com"}
A: {"function":"remind","result":"0"} -- пользователя с таким email не найдено
A: {"function":"remind","result":"1"} -- мы успешно выслали вам ваш вароль

--Добавить напоминание
Q: {"function":"add_note","name":"test1","user":"starson4588@gmail.com","x":3,"y":4}
A: {"function":"add_note","result":"1"} -- такой пользователь, есть и мы успешно добавили
A: {"function":"add_note","result":"0"} -- пользователь с таким email не найден

--Получить все напоминания.
Q: {"function":"get_notes","email":"starson4588@gmail.com"}
A: {"notes":[{"id":0,"name":"test1","owner":"starson4588@gmail.com","x":3,"y":4}],"function":"get_notes","result":"1"} - напоминания есть
A: {"notes":"null","function":"get_notes","result":"0"} -- у пользователя нет напоминаний.

