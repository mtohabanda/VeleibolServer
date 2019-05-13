create table equipo(
  equipoid int identity(1,1) primary key,
  nombre varchar(50)
)


create table puntuacion(
   equipoid int not null FOREIGN KEY REFERENCES equipo(equipoid),
   puntaje int
)
