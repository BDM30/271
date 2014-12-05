Работает как с локалки, так и с внешней сети.
API запросов только через JSON. Порядок полей в JSON ответе и запросе и не гарантируется!

API:
--Вход
Q: {"function":"entrance","email":"starson4588@gmail.com","password":"koolherc"}
A: {"Function":"entrance","Result":"1"} -- такая связка логин-пароль существует
A: {"Function":"entrance","Result":"0"} -- такой связки логин-пароль не существует

--Регистрация
Q: {"function":"registration","email":"starson4588@gmail.com","password":"koolherc"}
A: {"Function":"registration","Result":"0"} -- email уже зарегистрирован
A: {"Function":"registration","Result":"1"} -- успешная регистрация
A: {"Function":"registration","Result":"2"} -- некорректный email

--Забыть пароль
Q: {"function":"remind","email":"starson4588@gmail.com"}
A: {"Runction":"remind","Result":"0"} -- пользователя с таким email не найдено
A: {"Runction":"remind","Result":"1"} -- мы успешно выслали вам ваш вароль

--Добавить напоминание
Q: {"function":"add_note","name":"test1","user":"starson4588@gmail.com","x":3,"y":4}
A: {"Function":"add_note","Result":"1"} -- такой пользователь, есть и мы успешно добавили
A: {"Function":"add_note","Result":"0"} -- пользователь с таким email не найден

--Получить все напоминания.
Q: {"function":"get_notes","email":"starson4588@gmail.com"}
A: {"Notes":[{"Id":0,"Name":"test1","Owner":"starson4588@gmail.com","X":3,"Y":4}],"Function":"get_notes","Result":"1"} - напоминания есть
A: {"Notes":"null","Function":"get_notes","Result":"0"} -- у пользователя нет напоминаний.

