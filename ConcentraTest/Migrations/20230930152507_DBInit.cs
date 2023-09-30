using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcentraTest.Migrations
{
    /// <inheritdoc />
    public partial class DBInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    WDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandID);
                    table.ForeignKey(
                        name: "FK_Brands_Status",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_Brands_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "PersonTypes",
                columns: table => new
                {
                    PersonTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    StatusID = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    WDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonTypes", x => x.PersonTypeID);
                    table.ForeignKey(
                        name: "FK_PersonTypes_Status",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_PersonTypes_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    VehicleTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PlateType = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    PlatePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    WDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.VehicleTypeID);
                    table.ForeignKey(
                        name: "FK_VehicleTypes_Status",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_VehicleTypes_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    ModelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    BrandID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    WDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.ModelID);
                    table.ForeignKey(
                        name: "FK_Models_Brands",
                        column: x => x.BrandID,
                        principalTable: "Brands",
                        principalColumn: "BrandID");
                    table.ForeignKey(
                        name: "FK_Models_Status",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_Models_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientID = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    PersonTypeID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Birthdate = table.Column<DateTime>(type: "date", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    WDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientID);
                    table.ForeignKey(
                        name: "FK_Clients_PersonTypes",
                        column: x => x.PersonTypeID,
                        principalTable: "PersonTypes",
                        principalColumn: "PersonTypeID");
                    table.ForeignKey(
                        name: "FK_Clients_Status",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_Clients_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleTypeID = table.Column<int>(type: "int", nullable: false),
                    BrandID = table.Column<int>(type: "int", nullable: false),
                    ModelID = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    ClientID = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    WDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleID);
                    table.ForeignKey(
                        name: "FK_Vehicles_Brands",
                        column: x => x.BrandID,
                        principalTable: "Brands",
                        principalColumn: "BrandID");
                    table.ForeignKey(
                        name: "FK_Vehicles_Clients",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID");
                    table.ForeignKey(
                        name: "FK_Vehicles_Models",
                        column: x => x.ModelID,
                        principalTable: "Models",
                        principalColumn: "ModelID");
                    table.ForeignKey(
                        name: "FK_Vehicles_Status",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_Vehicles_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleTypes",
                        column: x => x.VehicleTypeID,
                        principalTable: "VehicleTypes",
                        principalColumn: "VehicleTypeID");
                });

            migrationBuilder.CreateTable(
                name: "Plates",
                columns: table => new
                {
                    PlateID = table.Column<string>(type: "char(6)", unicode: false, fixedLength: true, maxLength: 6, nullable: false),
                    VehicleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plates_1", x => x.PlateID);
                    table.ForeignKey(
                        name: "FK_Plates_Vehicles",
                        column: x => x.VehicleID,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleID");
                });

            migrationBuilder.CreateTable(
                name: "PlateRecords",
                columns: table => new
                {
                    PlateRecordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlateID = table.Column<string>(type: "char(6)", unicode: false, fixedLength: true, maxLength: 6, nullable: false),
                    PlateValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    WDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlateRecords", x => x.PlateRecordID);
                    table.ForeignKey(
                        name: "FK_PlateRecords_Plates",
                        column: x => x.PlateID,
                        principalTable: "Plates",
                        principalColumn: "PlateID");
                    table.ForeignKey(
                        name: "FK_PlateRecords_Status",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_PlateRecords_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brands_StatusID",
                table: "Brands",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_UserID",
                table: "Brands",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PersonTypeID",
                table: "Clients",
                column: "PersonTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_StatusID",
                table: "Clients",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserID",
                table: "Clients",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Models_BrandID",
                table: "Models",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_Models_StatusID",
                table: "Models",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Models_UserID",
                table: "Models",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonTypes_StatusID",
                table: "PersonTypes",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonTypes_UserID",
                table: "PersonTypes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PlateRecords_PlateID",
                table: "PlateRecords",
                column: "PlateID");

            migrationBuilder.CreateIndex(
                name: "IX_PlateRecords_StatusID",
                table: "PlateRecords",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PlateRecords_UserID",
                table: "PlateRecords",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Plates_VehicleID",
                table: "Plates",
                column: "VehicleID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_BrandID",
                table: "Vehicles",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ClientID",
                table: "Vehicles",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ModelID",
                table: "Vehicles",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_StatusID",
                table: "Vehicles",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_UserID",
                table: "Vehicles",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleTypeID",
                table: "Vehicles",
                column: "VehicleTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypes_StatusID",
                table: "VehicleTypes",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypes_UserID",
                table: "VehicleTypes",
                column: "UserID");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[USP_GenerateUniquePlate](    
                    @VehicleID INT,  
                    @UserID INT  
                )    
                AS    
                BEGIN    
 
                BEGIN TRAN
                BEGIN TRY  
                    DECLARE @newPlate CHAR(6)    
        
                    -- Generar un nuevo valor aleatorio    
                    SELECT @newPlate = RIGHT('000000' + CAST(CAST(RAND() * 1000000 AS INT) AS VARCHAR(6)), 6)    
        
                    -- Verificar si el valor ya existe en la tabla    
                    WHILE EXISTS (SELECT 1 FROM Plates WHERE PlateID = @newPlate)    
                    BEGIN    
                        -- Si el valor ya existe, genera otro valor    
                        SELECT @newPlate = RIGHT('000000' + CAST(CAST(RAND() * 1000000 AS INT) AS VARCHAR(6)), 6)    
                    END    
        
                    -- Insertar el valor único en la tabla  
	                SELECT   
		                @newPlate AS PlateID, 
		                b.PlateType,
		                b.PlatePrice AS Price,  
		                @UserID AS UserID,  
		                1 AS StatusID,  
		                GETDATE() AS WDate
		                INTO #tmp_vehicleData
                    FROM   
                    Vehicles a  
                    INNER JOIN VehicleTypes b ON (a.VehicleTypeID = b.vehicleTypeID)  
                    WHERE   
                    VehicleID = @VehicleID;  


                    INSERT INTO Plates (PlateID, VehicleID) VALUES (@newPlate, @VehicleID)  ;  
                    INSERT INTO PlateRecords(PlateID, PlateValue, UserID, StatusID, WDate)   
                    SELECT  
		                PlateID,  
		                Price,  
		                UserID,  
		                StatusID,  
		                WDate
                    FROM   
		                #tmp_vehicleData;
  
  
	                SELECT   
		                PlateType + PlateID AS PlateID,  
		                Price
                    FROM   
		                #tmp_vehicleData
  

	                DROP TABLE #tmp_vehicleData
                    COMMIT  
                END TRY  
                BEGIN CATCH  
                    ROLLBACK  
                END CATCH  
                END  

                GO

                CREATE PROCEDURE [dbo].[USP_getPlates](
	                @ClientID VARCHAR(11)
                )
                AS
                /*
                DECLARE @ClientID VARCHAR(11)
                SET @ClientID = '00117378570'
                */
                ;WITH myClients as (
                SELECT
	                a.ClientID,
	                b.PersonType,
	                a.[Name],
	                a.LastName,
	                a.Birthdate
                FROM
	                Clients a
	                INNER JOIN PersonTypes b ON (a.PersonTypeID = b.PersonTypeID)
                WHERE
	                ClientID = @ClientID
                ),
                myVehicleData as (
                SELECT
	                b.PlateType,
	                a.VehicleID,
	                c.*
                FROM
	                Vehicles a
	                INNER JOIN VehicleTypes b ON (a.VehicleTypeID = b.VehicleTypeID)
	                INNER JOIN myClients c ON (a.ClientID = c.ClientID)
                ),
                myPlates as (
                SELECT
	                b.*,
	                a.PlateID as Plate,
	                c.wDate,
	                c.plateValue
                FROM
	                Plates a
	                INNER JOIN myVehicleData b ON (a.VehicleID = b.VehicleID)
	                INNER JOIN PlateRecords c ON (a.PlateID = c.PlateID)
                )

                SELECT 
	                PlateType,
	                Plate,
	                ClientID,
	                PersonType,
	                [Name],
	                LastName,
	                Birthdate,
	                wDate,
	                plateValue
                FROM
	                myPlates

               GO
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlateRecords");

            migrationBuilder.DropTable(
                name: "Plates");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "VehicleTypes");

            migrationBuilder.DropTable(
                name: "PersonTypes");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.Sql(@"
                DROP PROCEDURE USP_GenerateUniquePlate;
                DROP PROCEDURE USP_getPlates;
            ");
        }
    }
}
