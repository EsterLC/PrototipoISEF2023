use cinevision;

CREATE TABLE IF NOT EXISTS `cinevision`.`factura_encabezado` (
  `idFactura` INT NOT NULL,
  `fecha_factura` DATETIME NOT NULL,
  `Estado_factura` VARCHAR(45) NOT NULL,
  `idClientes` INT NOT NULL,
  PRIMARY KEY (`idFactura`),
  CONSTRAINT `idcliente_v`
    FOREIGN KEY (`idClientes`)
    REFERENCES `cinevision`.`clientes` (`idClientes`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `cinevision`.`factura_detalle` (
  `PK_detalle_factura` INT NOT NULL,
  `idFactura` INT NOT NULL,
  `idReservaciones` INT NOT NULL,
  `metodoPago` varchar(45),
  `Total` double NOT NULL,
  PRIMARY KEY (`PK_detalle_factura`),
  CONSTRAINT `venta`
    FOREIGN KEY (`idFactura`)
    REFERENCES `cinevision`.`factura_encabezado` (`idFactura`),
    CONSTRAINT `reservacion`
    FOREIGN KEY (`idReservaciones`)
    REFERENCES `cinevision`.`reservaciones` (`idReservaciones`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

insert into cinevision.salas values (1,1,"SALA A","3D",10,35.25);
insert into cinevision.salas values (2,1,"SALA B","2D",10,25.16);
insert into cinevision.salas values (3,1,"SALA B","4D",10,45.85);

insert into cinevision.clientes values (1,"Ester","19314");
insert into cinevision.clientes values (2,"Daniela","1896");
insert into cinevision.clientes values (3,"José","3656");

insert into cinevision.reservaciones values (1,1,24.50);
insert into cinevision.reservaciones values (2,2,50.23);
insert into cinevision.reservaciones values (3,3,45.33);

insert into cinevision.cines values (1,"Cinepolis","El Naranjo");

insert into cinevision.asientos values (1,1,5,5);
insert into cinevision.asientos values (2,2,3,1);
insert into cinevision.asientos values (3,2,6,10);

SELECT Nombre FROM cines, salas WHERE fkCine = idCines AND idSalas = 2;