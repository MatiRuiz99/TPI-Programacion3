CREATE TABLE Producto (
		IdProducto int primary key identity(1,1) not null,
		Nombre nvarchar(50),
		Descripcion nvarchar(250),
		Precio decimal,
		Estado nvarchar(50)
)

CREATE TABLE RoleList (
		id int primary key identity(1,1) not null,
		Authority nvarchar(250),

)

CREATE TABLE Users (
		UserId int primary key identity(1,1) not null,
		Email nvarchar(250),
		Pass nvarchar(250),
		Name nvarchar(250),
		RoleId int foreign key references RoleList(id) not null
)

CREATE TABLE SalesHistory (
		Id int primary key identity(1,1) not null,
		ProductId int foreign key references Producto(IdProducto) not null,
		UserId int foreign key references Producto(IdProducto) not null,
		Price decimal,
		SaleDate datetime		
)