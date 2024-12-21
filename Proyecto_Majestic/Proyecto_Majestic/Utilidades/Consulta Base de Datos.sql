USE MASTER
GO

IF EXISTS(SELECT * from sys.databases WHERE name='BDMAJESTIC')  
BEGIN 
alter database BDMAJESTIC set single_user 
with rollback immediate
END 
go

-- buscamos si existe la base de datos
IF EXISTS(SELECT * from sys.databases WHERE name='BDMAJESTIC')  
BEGIN 
    -- seleccionamos el master 
    use master
    --eliminamos la base de datos 
    drop DATABASE BDMAJESTIC
END 
go

-- creacion de la base de datos
create database BDMAJESTIC
go

-- seleccionamos la base de datos
use BDMAJESTIC
go

-- TABLA ROL
CREATE TABLE Rol(
cod_rol INT PRIMARY KEY NOT NULL IDENTITY(1,1),
nom_rol VARCHAR(50),
est_rol BIT NOT NULL
)

--TABLA EMPLEADO
CREATE TABLE Empleado( 
cod_emp INT PRIMARY KEY NOT NULL IDENTITY(1,1),
nom_emp VARCHAR(80) NOT NULL,
ape_emp VARCHAR(80) NOT NULL,
dni_emp CHAR(8) NOT NULL,
fecha_in_emp DATE NOT NULL,
telf_emp CHAR(9) NOT NULL,
sexo_emp CHAR(1) NULL,
num_emp INT NOT NULL,
est_emp BIT NOT NULL
)

-- TABLA USUARIO
CREATE TABLE Usuario(
cod_usu INT PRIMARY KEY IDENTITY(1,1),
nom_usu VARCHAR(100) NOT NULL,
ape_usu VARCHAR(100) NOT NULL,
ema_usu VARCHAR(100) NOT NULL,
con_usu VARCHAR(100) NOT NULL,
fec_cre DATETIME,
cod_emp INT NULL FOREIGN KEY REFERENCES empleado,
cod_rol INT NULL FOREIGN KEY REFERENCES rol(cod_rol),
est_usu BIT NOT NULL
)

--TABLA DISTRITO
CREATE TABLE Distrito(
cod_dis INT PRIMARY KEY NOT NULL IDENTITY(1,1),
nom_dis VARCHAR(40) NOT NULL,
est_dis BIT NOT NULL
)

-- TABLA CATEGORIA
CREATE TABLE Categoria(
cod_cat INT PRIMARY KEY NOT NULL IDENTITY(1,1),
nom_cat VARCHAR(40) NOT NULL,
est_cat BIT NOT NULL
)

-- TABLA MARCA
CREATE TABLE Marca(
cod_mar INT PRIMARY KEY NOT NULL IDENTITY(1,1),
nom_mar VARCHAR(40) NOT NULL,
est_mar BIT NOT NULL
)

--TABLA PRODUCTO
CREATE TABLE Producto (
cod_prod INT PRIMARY KEY NOT NULL IDENTITY(1,1),
nom_prod VARCHAR (25) NOT NULL,
stock_max_prod INT NULL,
stock_min_prod INT NULL,
prec_list_prod MONEY NOT NULL,
cap_bat VARCHAR(15) NULL,
cod_cat INT NOT NULL FOREIGN KEY REFERENCES categoria(cod_cat),
cod_mar INT NOT NULL FOREIGN KEY REFERENCES marca(cod_mar),
est_prod BIT NOT NULL
)

-- TABLA CLIENTE
CREATE TABLE Cliente (
cod_cli INT PRIMARY KEY NOT NULL IDENTITY(1,1),
ruc_cli CHAR(11) NOT NULL,
nom_cli VARCHAR(80) NOT NULL,
telf_cli CHAR(7) NULL,
email_cli VARCHAR(60) NULL,
dir_cli VARCHAR(80) NOT NULL,
cod_dis INT NOT NULL FOREIGN KEY REFERENCES distrito,
est_cli BIT NOT NULL
)

--TABLA PEDIDO
CREATE TABLE Pedido(
cod_ped INT PRIMARY KEY NOT NULL IDENTITY(1,1),
cod_cli INT NOT NULL FOREIGN KEY REFERENCES cliente,
cod_emp INT NOT NULL FOREIGN KEY REFERENCES empleado,
fec_ped DATE NOT NULL,
act_ped VARCHAR(50) NOT NULL,
est_ped BIT NOT NULL
)

--TABLA PEDIDO-PRODUCTO
CREATE TABLE Pedido_Producto(
cod_ped INT NOT NULL FOREIGN KEY REFERENCES pedido,
cod_prod INT NOT NULL FOREIGN KEY REFERENCES producto,
pre_prod MONEY NOT NULL,
can_prod INT NOT NULL,
est_pp BIT NOT NULL
PRIMARY KEY(cod_ped, cod_prod)
)

--VERIFICACIÓN

--EL ESTADO DEL PEDIDO ADMITA 'EN PROCESO', 'PAGADO', 'CANCELADO', 'DEVUELTO', 'ENVIADO'
ALTER TABLE pedido
ADD CONSTRAINT CHK_ACT_PED
CHECK (ACT_PED IN('Entregado', 'En Proceso', 'Pagado', 'Cancelado', 'Devuelto', 'Enviado'))

-- LA FECHA DE INGRESO DE PEDIDO SEA GETDATE()
ALTER TABLE PEDIDO
	ADD DEFAULT GETDATE() FOR FEC_PED

ALTER TABLE USUARIO
	ADD DEFAULT GETDATE() FOR FEC_CRE

-- REGISTRO PARA TABLA ROL
INSERT INTO ROL(nom_rol, est_rol) VALUES
('ADM', 1),
('EDT', 0),
('DEV', 1)

-- REGISTRO PARA TABLA DISTRITO
INSERT INTO DISTRITO (NOM_DIS, EST_DIS) VALUES
('CERCADO DE LIMA', 1),
('BARRANCO', 1),
('BREÑA', 1),
('CHORILLOS', 1),
('EL AGUSTINO', 1),
('INDEPENDENCIA', 1),
('JESUS MARIA', 1),
('LA VICTORIA', 1),
('LINCE', 1),
('LOS OLIVOS', 1),
('MAGDALENA DEL MAR', 1),
('PUEBLO LIBRE', 1),
('MIRAFLORES', 1),
('RIMAC', 1),
('SAN BORJA', 1),
('SAN ISIDRO', 1),
('SAN LUIS', 1),
('SAN MARTIN DE PORRES', 1),
('SAN MIGUEL', 1),
('SANTA ANITA', 1),
('SANTIAGO DE SURCO', 1),
('SURQUILLO', 1),
('ANCON', 0),
('CARABAYLLO', 0),
('CHACLACAYO', 0),
('CIENEGUILLA', 0),
('PACHACAMAC', 0),
('ATE', 0),
('COMAS', 0),
('LA MOLINA', 1),
('SAN JUAN DE LURIGANCHO', 1),
('SAN JUAN DE MIRAFLORES', 1),
('VILLA EL SALVADOR', 1),
('VILLA MARIA DEL TRIUNFO', 1),
('CALLAO', 1),
('BELLAVISTA', 1),
('CARMEN DE LA LEGUA', 0),
('LA PERLA', 0)

--REGISTRO PARA LA TABLA EMPLEADO
INSERT INTO EMPLEADO(nom_emp, ape_emp, dni_emp, fecha_in_emp, telf_emp, sexo_emp, num_emp, est_emp) VALUES
('Maria Isabel', 'Gomez Perez', '70445677', '2015/05/10', '951665245', 'F', 20, 1),
('Vanessa Alejandra', 'Menacho Lopez', '09454617', '2018/12/05', '961565599', 'M', 120, 0),
('Alex Antonio', 'Torres Gonzalez', '01045788', '2018/09/20', '955565679', 'M',34, 1),
('Joaquin Liam', 'Garcia Lopez', '61435657', '2005/03/11', '975575598', 'M', 30, 0),
('Andres Jorge', 'Reyes Martinez', '50943655', '2014/08/15', '956765578', 'M', 23, 1),
('Bruno Edgar', 'Lopez Fernandez', '40343687', '2020/04/13', '924565567', 'M', 56, 1),
('Alexia Alejandra', 'Godinez Cruz', '40245529', '2016/11/16', '932557889', 'F', 34, 0),
('Gabriel Andres', 'Meneses Ramirez', '80335907', '2015/02/11', '952355512', 'M',12, 1),
('Isabel Gabriela', 'Hernandez Ortiz', '70245660', '2008/09/13', '945156598', 'F',32, 0),
('Samuel Esteban', 'Cordova Jimenez', '55845676', '2006/06/12', '942565573', 'M',23, 1),
('Elena Sofia', 'Guerrero Rodriguez', '50475610', '2009/11/16', '967665879', 'F',21, 1),
('Alexander Jose', 'Mejia Vasquez', '90678898', '2019/07/17', '951386599', 'M',33, 0),
('Jaime Alejandro', 'Cisneros Ortega', '23324545', '2007/12/12', '940565599', 'F',23, 1),
('Diana Carolina', 'Lopez Torres', '80475689', '2011/09/15', '920565599', 'F',24, 0),
('Mariano Luis', 'Santos Silva', '24455667', '2008/06/19', '930565599', 'M',34, 1),
('Daniel Enrique', 'Ramirez Gutierrez', '09465687', '2003/02/13', '951475599', 'M',23, 0),
('Camila Isabel', 'Rodriguez Mendoza', '90446788', '2005/11/16', '951563399', 'F',23, 1),
('Nicolas Antonio', 'Gonsalez Ramos', '08543637', '2011/04/17', '928565599', 'M',45, 0),
('Valentina Elena', 'Martinez Guzman', '90545687', '2007/10/14', '951455599', 'F',45, 1),
('Martina Fernanda', 'Garcia Hernandez', '70557627', '2019/09/10', '961573399', 'F',67, 0)

INSERT INTO EMPLEADO(nom_emp, ape_emp, dni_emp, fecha_in_emp, telf_emp, sexo_emp, num_emp, est_emp) VALUES
('Luis Alberto', 'Rojas Garcia', '10987654', '2027/08/18', '955665577', 'M',34, 1),
('Carolina Isabel', 'Vargas Mendoza', '30546789', '2012/01/16', '932345678', 'F',34, 0),
('Eduardo Jose', 'Perez Gonzalez', '70894567', '2004/03/11', '945678912', 'M',34, 1),
('Fernanda Sofia', 'Diaz Torres', '50987654', '2009/12/12', '923456789', 'F',23, 0),
('Hugo Alejandro', 'Sanchez Ramirez', '60456789', '2008/02/17', '934567890', 'M',23, 1),
('Natalia Alejandra', 'Fernandez Silva', '40345678', '2001/11/12', '967890123', 'F',23, 0),
('Ricardo Antonio', 'Silva Ortiz', '20987654', '2006/06/18', '978901234', 'M',45, 1),
('Alicia Gabriela', 'Ortiz Perez', '60894567', '2005/10/17', '989012345', 'F',43, 0),
('Martin Eduardo', 'Jimenez Guzman', '30785678', '2023/05/14', '998765432', 'M',32, 1),
('Sofia Valentina', 'Luna Fernandez', '50986789', '2015/03/17', '987654321', 'F',23, 0),
('Diego Alejandro', 'Herrera Mendoza', '40785678', '2002/07/14', '989543210', 'M',23, 1),
('Laura Valeria', 'Castro Ramos', '20986543', '2009/09/17', '965432109', 'F',32, 0),
('Pedro Luis', 'Roca Guzman', '60987654', '2016/03/16', '954321098', 'M',23, 1),
('Ana Gabriela', 'Gomez Torres', '30895467', '2005/01/13', '943210987', 'F',23, 0),
('Carlos Eduardo', 'Molina Ortega', '10986543', '2018/10/16', '932109876', 'M',23, 1),
('Monica Isabel', 'Lara Vasquez', '40987654', '2004/08/11', '921098765', 'F',23, 0),
('Juan Carlos', 'Suarez Mendoza', '20985678', '2013/05/14', '910987654', 'M',23, 1),
('Valeria Fernanda', 'Paz Ramos', '50895467', '2002/10/15', '988876543', 'F',33, 0),
('Francisco Javier', 'Mendez Ortega', '30986543', '2004/04/11', '988765432', 'M',23, 1),
('Margarita Elena', 'Hidalgo Guzman', '10987654', '2008/06/17', '965654321', 'F',23, 0),
('Raul Antonio', 'Chavez Mendoza', '70896543', '2011/03/18', '976543210', 'M',56, 1),
('Elena Isabel', 'Molina Torres', '50986543', '2015/05/20', '988432109', 'F',78, 0),
('Mario Alberto', 'Herrera Guzman', '20987654', '2009/08/11', '944321098', 'M',56, 1),
('Sara Valentina', 'Torres Ramos', '40896543', '2016/04/15', '933210987', 'F',56, 0),
('Alejandro Andres', 'Gutierrez Silva', '20986543', '2020/12/19', '922109876', 'M', 66, 1)

INSERT INTO USUARIO VALUES
('Fabiana', 'Canales', 'fcanales@gmail.com', '123', GETDATE(), NULL, 3, 1),
('Josue', 'Correa', 'jcorrea@gmail.com', '567', GETDATE(), NULL, 3, 1),
('Alex', 'Torres','atorres@gmail.com', '012', GETDATE(), 3, 1, 1),
('Maria', 'Gomez', 'mgomez@gmail.com', '567', GETDATE(), 1, 2, 1)

--REGISTROS PARA TABLA CLIENTE
INSERT INTO CLIENTE (ruc_cli, nom_cli, telf_cli, email_cli, dir_cli, cod_dis, est_cli) VALUES
('23456789012', 'TecnoSoluciones Innovadoras S.A.', '5123456', 'techno_sol@innova.com', 'Jr. 28 De Julio 148', 1, 0),
('78901234567', 'QuantumSpark Ventures', '4789123', 'q_spark@ventures.com', 'Av. Tacna 246', 2, 1),
('34567890123', 'NovaMatrix Solutions', '5634567', 'contact_NovaMatrix@novagroup.com', 'Av. Garcilaso de La Vega 235', 3, 0),
('01234567890', 'Nexus Dynamics Group', '8412345', 'g_dynamics@nexus.com', 'Jr. Ucayali 388', 4, 1),
('56789012345', 'Apex Innovate Partners', '6943210', 'apex_innova@apexpartners.com', 'Av. Jose Galvez 345', 5, 0),
( '89012345678', 'Pinnacle Synergy Systems', '7890123', 'help_systems@pinnacle.com', 'Av. Sucre 124', 6, 1),
( '25234567890', 'Horizon Nexus Enterprises', '6523489', 'nexus_horizon@nexusentreprises.com', 'Jr. Quilca 1204', 7, 1),
('45678901234', 'TimeSync Innovations', '6123456', 'time_innova@sync.com', 'Av. Proceres 367', 8, 1),
('90123456789', 'TechChrono Dynamics', '6789123', 'techchromo_dyna@techgroup.com', 'Av. Canevaro 1890', 9, 0),
('11223344556', 'ShopMi Inc', '8154321', 'contactgroup@shopmi.com', 'Jr. Militar 1856', 10, 1),
('98765432109', 'InfinityTech Solutions SA', '3187654', 'infinityTech@contacttech.com', 'Av. Petit Thouars 1749', 11, 0),
('87654321028', 'QuantumLeap Innovations Inc', '7643210', 'innovationssupport@quantumleap.com', 'Av. Universitaria 3490', 12, 1),
('76543210957', 'CyberPulse Dynamics SRL', '5134567', 'cyberpulse_dynamics@cpd.com', 'Av. Peru 1567', 13, 1),
('65432109856', 'CodeSphere Tech Ventures', '4125678', 'venture_codesphere@tech_venture.com', 'Ca. Las Begonias 1235', 14, 0),
('54321098755', 'SynthWave Innovators Ltda', '4789012', 'synthwave@innovacontact.com', 'Av. Paseo de la Republica 3454', 15, 1),
('43210987674', 'QuantumByte Systems Corp', '6981234', 'newsource_byte@quantumsys.com', 'Jr. De la Raya 212', 16, 1),
('32109876543', 'InnovateSphere Solutions', '5890123', 'innovasphere@techsolutions.com', 'Jr. El Conde 978', 17, 0),
('21098765432', 'DataWave Dynamics SA', '3678901', 'wavedynamics@data_dyna.com', 'Jr. Ancash 375', 18, 1),
('10987654321', 'TechVortex Innovations Inc', '3176540', 'tech_vortex@vortexinnova.com', 'Ca. Zela 785', 19, 0),
('87654321098', 'FutureSync Tech Group SRL', '5123409', 'futuresync@futuretechgroup.com', 'Jr. Huallaga 1575', 20, 1)

INSERT INTO CLIENTE (ruc_cli, nom_cli, telf_cli, email_cli, dir_cli, cod_dis, est_cli) VALUES
('10010010012', 'Garcia & Asociados S.A.', '7123456', 'vane_garcia12@hotmail.com', 'Av. Bausate y Mesa 150', 1, 1),
('10010010013', 'Caminos del Inca S.A.C.', '7123457', 'carlos_albertoxx@hotmail.com', 'Av. Caminos del Inca 343', 20, 0),
('10010010014', 'Las Begonias Ltda.', '7123458', 'sara_lopez1232@gmail.com', 'Av. Las Begonias 1343', 18, 0),
('10010010015', 'Brasil Innovaciones S.A.', '7123459', 'm_martin@gmail.com', 'Av. Brasil 2332', 15, 0),
('10010010016', 'Naranjal Tech Solutions S.A.', '7123460', 'lau_ram5880@hotmail.com', 'Av. Naranjal 3245', 11, 1),
('10010010017', 'Agosto Servicios S.A.C.', '7123461', 'hhernandez@hotmail.com', 'Jr 6 De Agosto 148', 12, 0),
('10010010018', 'Amancaes Group SRL', '7123462', 'rosi_cab@hotmail.com', 'Jr De Amancaes 323', 8, 1),
('10010010019', 'Julio Velarde Corp.', '7123463', 'ga_go2332@hotmail.com', 'Av. Julio Velarde 148', 5, 1),
('10010010020', 'Militar Solutions S.A.', '7123464', 'alex_torres@gmail.com', 'Ca. Militar 148', 6, 0),
('10010010021', 'Los Canarios Ltda.', '7123465', 'jaimediaz_23@gmail.com', 'Av. Los Canarios 540', 11, 0),
('10010010022', 'Bausate y Mesa E.I.R.L.', '7123466', 'luci_guti43@hotmail.com', 'Av. Bausate y Mesa 230', 2, 1),
('10010010023', 'Las Lechuzas Technologies S.A.', '7123467', 'nicolas_vega@hotmail.com', 'Av. Las Lechuzas 343', 3, 1),
('10010010024', 'Canada Solutions TB', '7123468', 'valenrojas@gmail.com', 'Av. Canada 1343', 4, 0),
('10010010025', 'Alipio Ponce Enterprises S.A.', '7123469', 'fernansuarez@gmail.com', 'Jr Alipio Ponce 1221', 5, 1),
('10010010026', 'Argentina Innovaciones S.A.', '7123470', 'isa_perezxx@gmail.com', 'Av. Argentina 232', 8, 1),
('10010010027', 'Juan de Arona S.A.C.', '7123471', 'andres_moliruiz@hotmail.com', 'Av. Juan de Arona 2112', 7, 0),
('10010010028', 'Mercaderes Group TB', '7123472', 's_cruz1323@hotmail.com', 'Jr. Mercaderes 458', 5, 1),
('10010010029', 'Los Alisos Technologies TD', '7123473', 'jsebass_cast@gmail.com', 'Ca. Los Alisos 987', 4, 1),
('10010010030', 'Los Frutales Ltda.', '7123474', 'lauvic_fernan@gmail.com', 'Av. Los Frutales 544', 1, 1),
('10010010031', 'Benavides Tech S.A.', '7123475', 'andressanchez_2343@gmail.com', 'Av. Benavides 1326', 2, 1),
('10010010032', 'Pardo Innovations TC', '7123476', 'ale_sanchez1232@hotmail.com', 'Av. Pardo 316', 3, 1),
('10010010033', 'Arequipa Solutions TC', '7123477', 'camivalen_estrada@gmail.com', 'Av. Arequipa 1232', 4, 0),
('10010010034', 'Larco Tech TC', '7123478', 'leximonica@gmail.com', 'Av. Larco 445', 8, 1),
('10010010035', 'Montero Rosas Enterprises TC', '7123479', 'j_morenogiraldo@hotmail.com', 'Ca. Montero Rosas 156', 3, 1),
('10010010036', 'Alcanfores Solutions TC', '7123480', 'nico_andrea2778@gmail.com', 'Ca. Alcanfores 272', 10, 0)

-- REGISTROS PARA TABLA CATEGORIA
INSERT INTO CATEGORIA VALUES
('Reloj inteligente', 1),
('Audífonos inalámbricos', 1),
('Consola de videojuegos', 1),
('Smartphone', 0),
('Batería externa', 1),
('Cámara', 0),
('Micrófono externo', 1),
('Dron', 0)

INSERT INTO MARCA VALUES
('Apple', 0),
('Samsung', 1),
('Garmin', 1),
('Sony', 0),
('JBL', 1),
('Nintendo', 0),
('Xiaomi', 1),
('Anker', 1),
('Nikon', 1),
('Rode', 1),
('DJI', 1),
('Parrot', 1)

--REGISTROS PARA TABLA PRODUCTO
INSERT INTO PRODUCTO(nom_prod, stock_max_prod, stock_min_prod, prec_list_prod, cap_bat, cod_cat, cod_mar, est_prod) VALUES
('F9 5C V5.3', 250, 80, 39.0, '2600mAh', 1, 2, 0),
('F9 MINI', 250, 80, 35.0, '2600mAh', 2, 2, 1),
('M28', 250, 80, 45.0, '3500mAh', 8, 12, 0),
('M7', 250, 80, 59.0, '4000mAh', 8, 12, 1),
('A6S', 250, 80, 39.0, '350mAh', 4, 1, 1),
('P30 GAMER', 250, 80, 55.0,'400mAh', 2, 8, 1),
('PRO 60 GAMER', 250, 80, 45.0, '500mAh', 2, 9, 0),
('R190 BUDS PRO', 250, 80, 59.0,'400mAh', 4, 10, 1),
('X7 GAMER', 250, 80, 59.0,'150mAh', 4, 6, 1),
('Y80', 250, 80, 49.0,'200mAh', 4, 6, 0),
('INPODS PRO', 250, 80, 49.0, '200mAh', 7, 4, 1),
('B35 KRYSTAL', 250, 80, 59.0, '300mAh', 1, 8, 1),
('G11 GAMER', 250, 80, 39.0, '350mAh', 8, 12, 0),
('M25', 250, 80, 59.0, '2500mAh', 3, 3, 1),
('PRO 6', 250, 80, 39.0, '200mAh', 3, 5, 1),
('M6', 250, 80, 69.0, '4000mAh', 2, 6, 1),
('F9 PRO', 250, 80, 59.0, '2600mAh', 3, 7, 1),
('G10', 155, 70, 69.0, '200mAh', 4, 8, 0),
('G90', 200, 40, 79.0, '200mAh', 7, 11, 0),
('P9 PLUS', 430, 80, 49.0, '200mAh', 8, 1, 1),
('Y08', 320, 60, 69.0, '200mAh', 4, 7, 1),
('Q19 KIDS', 560, 30, 79.0,'400mAh', 4, 5, 1),
('M7 BAND', 340, 50, 35.0, '55mAh', 5, 2, 0),
('D20', 250, 80, 35.0, '150mAh', 5, 3, 0),
('D20 ULTRA', 250, 80, 35.0, '150mAh', 3, 6, 1),
('T900 PRO MAX L', 250, 80, 55.0, '280mAh', 6, 2, 0),
('T900 PRO MAX S', 250, 80, 59.0, '280mAh', 5, 9, 0),
('I8 PRO MAX', 250, 80, 65.0, '200mAh', 4, 5, 1),
('IWO 7 GAMA ALTA', 250, 80, 99.0, '230mAh', 7, 10, 1),
('DT100 PRO MAX', 250, 80, 89.0, '200mAh', 6, 9, 1),
('W27 MAX', 250, 80, 109.0, '270mAh', 6, 9, 0),
('HD7 PREMIUM', 250, 80, 99.0, '230mAh', 5, 2, 0),
('HW37 PLUS', 250, 80, 89.0, '230mAh', 8, 10, 1),
('DT4 PLUS', 250, 80, 169.0, '380mAh', 2, 12, 1),
('HK5 HERO', 250, 80, 169.0, '380mAh', 8, 7, 1)

--REGISTRO PARA TABLA PEDIDO
INSERT INTO PEDIDO (cod_cli, cod_emp, fec_ped, act_ped, est_ped) VALUES
(1, 12, '2024-12-10', 'Entregado', 1), 
(3, 2, '2024-12-10', 'Entregado', 1), 
(2, 3, '2024-12-20', 'En Proceso', 0), 
(3, 4, '2024-12-20', 'Entregado', 1), 
(10, 19, '2024-12-20', 'Pagado', 1), 
(5, 7, '2024-12-20','Pagado', 1), 
(6, 16, '2024-12-16', 'Entregado', 0), 
(7, 3, '2024-12-15', 'Cancelado', 1), 
(8, 9, '2024-12-14', 'Entregado', 1), 
(9, 14, '2024-12-18', 'Pagado', 1),
(11, 11, '2024-12-10', 'Pagado', 0), 
(20, 2, '2024-12-10', 'Devuelto', 1),
(12, 8, '2024-12-20', 'Entregado', 1), 
(13, 6, '2024-12-10', 'Pagado', 1), 
(14, 20, '2024-12-15', 'Pagado', 1), 
(15, 10, '2024-12-18', 'Pagado', 0), 
(16, 4, '2024-12-20', 'Pagado', 1), 
(17, 17, '2024-12-20', 'Pagado', 1), 
(18, 13, '2024-12-19', 'Devuelto', 1),
(19, 18, '2024-12-10', 'Enviado', 1),
(20, 15, '2024-12-12', 'Enviado', 1),
(18, 18, '2024-12-08', 'Entregado', 1),
(1, 13, '2024-12-20', 'En Proceso', 0), 
(3, 35, '2024-12-19','Entregado', 1),
(12, 18, '2024-12-19', 'Pagado', 0), 
(5, 14, '2024-12-02', 'Pagado', 0),
(19, 7, '2024-12-17', 'Entregado', 0), 
(2, 15, '2024-12-20','Cancelado', 1), 
(8, 20, '2024-12-15', 'Entregado', 1), 
(17, 3, '2024-12-12', 'Pagado', 1), 
(10, 6, '2024-12-15', 'Pagado', 1),
(1, 19, '2024-12-16', 'Devuelto', 1), 
(15, 11, '2024-12-20', 'Entregado', 0), 
(6, 9, '2024-12-20', 'Pagado', 0), 
(20, 5, '2024-12-03', 'Pagado', 0), 
(3, 13, '2024-12-10', 'Pagado', 1), 
(11, 8, '2024-12-12', 'Pagado', 1),
(7, 2, '2024-12-19', 'Pagado', 1), 
(4, 12, '2024-12-17', 'Cancelado', 1), 
(9, 4, '2024-12-12', 'Enviado', 0),
(14, 16, '2024-12-18', 'Enviado', 0),
(13, 2, '2024-12-19', 'Pagado', 1),
(18, 10, '2024-12-04', 'Pagado', 0), 
(16, 17, '2024-12-05', 'Pagado', 1), 
(20, 14, '2024-12-08', 'Enviado', 1)

INSERT INTO PEDIDO_PRODUCTO (cod_ped, cod_prod, pre_prod, can_prod, est_pp) VALUES
(1, 1, 42.5, 34, 1),
(1, 3, 32.4, 12, 1),
(2, 11, 52.5, 15, 0),
(3, 19, 82.5,  5, 1),
(3, 18, 72.6, 31, 0),
(4, 7, 48.6, 2, 0),
(4, 1, 42.5, 17, 1),
(4, 24, 35.5, 32, 0),
(5, 13, 40.5, 5, 1),
(5, 14, 62.6, 12, 1),
(6, 15, 40.6, 6, 0),
(6, 16, 72.5, 24, 1),
(6, 8, 38.4, 18, 1),
(6, 9, 62.4, 12, 0),
(8, 10, 50.2, 12, 1),
(9, 11, 50.2, 4, 0),
(9, 12, 60.5, 6, 1),
(10, 13, 38.5, 4, 0),
(11, 17,58.5, 30, 1),
(11, 1, 49.5, 14, 0)