create table [ShopProducts]
(Id int identity(1,1) not null primary key,
[Name] nvarchar(100) not null,
[Type]  nvarchar(50) not null,
Color  nvarchar(50) not null,
Calories int not null)

insert into ShopProducts ([Name], [Type], Color, Calories)
values 
('Banana', 'Fruit', 'Yellow', 89),
('Apple', 'Fruit', 'Red', 52),
('Tomato', 'Vegetable', 'Red', 18),
('Orange', 'Fruit', 'Orange',  47),
('Cucumber', 'Vegetable', 'Green', 15),
('Carrot', 'Vegetable', 'Orange', 41);