use RepairService

-- Создание таблицы Заказчик
create table Заказчик(
[ID Заказчика] int primary key not null,
Телефон nchar(11) not null,
ФИО nchar(50) not null,
Логин nchar(30) not null,
Пароль nchar(10) not null,
Роль nchar(20) not null
)

-- Создание таблицы Менеджер
create table Менеджер(
[ID Менеджера] int primary key not null,
Телефон nchar(11) not null,
ФИО nchar(50) not null,
Логин nchar(30) not null,
Пароль nchar(15) not null,
Роль nchar(20) not null
)


-- Создание таблицы Мастер
create table Мастер(
[ID Мастера] int primary key not null,
Телефон nchar(11) not null,
ФИО nchar(50) not null,
Логин nchar(30) not null,
Пароль nchar(15) not null,
Роль nchar(20) not null
)

-- Создание таблицы Оператор
create table Оператор(
[ID Оператора] int primary key not null,
Телефон nchar(11) not null,
ФИО nchar(50) not null,
Логин nchar(30) not null,
Пароль nchar(15) not null,
Роль nchar(20) not null
)


create table Заявка(
[ID Заявки] int primary key not null,
[Дата добавления] date not null,
[Тип техники] nchar(30) not null,
[Модель техники] nchar(100),
Описание nchar(300),
Статус nchar(20) not null,
[Дата окончания] date,
Запчасти nchar(200),

[ID Мастера] int,
-- Вторичный ключ на ID мастера
FOREIGN KEY ([ID Мастера]) REFERENCES Мастер ([ID Мастера]),

[ID Заказчика] int not null,
-- Вторичный ключ на ID заказчика
FOREIGN KEY ([ID Заказчика]) REFERENCES Заказчик ([ID Заказчика]),
)

-- Хранит в себе комментарии к заявкам
create table Комментарий(
[ID Комментария] int primary key not null,
Сообщение nchar(300) not null,
[ID Мастера] int not null,
FOREIGN KEY ([ID Мастера]) REFERENCES Мастер ([ID Мастера]),
[ID Заявки] int not null,
FOREIGN KEY ([ID Заявки]) REFERENCES Заявка ([ID Заявки])
)


