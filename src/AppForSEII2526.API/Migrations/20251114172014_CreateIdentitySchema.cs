using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppForSEII2526.API.Migrations
{
    /// <inheritdoc />
    public partial class CreateIdentitySchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apellido_1Cliente = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Apellido_2Cliente = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    NombreCliente = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrecioTotal = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false),
                    NumBocadillos = table.Column<int>(type: "int", nullable: false),
                    MetodoPago = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Compra_Producto",
                columns: table => new
                {
                    Compraid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DireccionEnvio = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Metodo_Pago = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PrecioFinal = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Apellido_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra_Producto", x => x.Compraid);
                });

            migrationBuilder.CreateTable(
                name: "TipoBocadillo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoBocadillo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoPan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoProducto",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoProducto", x => x.ProductoId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BonoBocadillo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoBocadilloId = table.Column<int>(type: "int", nullable: false),
                    CantidadDisponible = table.Column<int>(type: "int", nullable: false),
                    NumeroBocadillos = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecioPorBono = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonoBocadillo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonoBocadillo_TipoBocadillo_TipoBocadilloId",
                        column: x => x.TipoBocadilloId,
                        principalTable: "TipoBocadillo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bocadillo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PVP = table.Column<double>(type: "float", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Tamano = table.Column<int>(type: "int", nullable: false),
                    TipoPanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bocadillo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bocadillo_TipoPan_TipoPanId",
                        column: x => x.TipoPanId,
                        principalTable: "TipoPan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PVP = table.Column<double>(type: "float", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    TipoProductoProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.ProductoId);
                    table.ForeignKey(
                        name: "FK_Producto_TipoProducto_TipoProductoProductoId",
                        column: x => x.TipoProductoProductoId,
                        principalTable: "TipoProducto",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompraBono",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MetodoPago = table.Column<int>(type: "int", nullable: false),
                    NBonos = table.Column<int>(type: "int", nullable: false),
                    PrecioTotalBono = table.Column<double>(type: "float", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraBono", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompraBono_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Resenya",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Valoracion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resenya", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resenya_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompraBocadillo",
                columns: table => new
                {
                    BocadilloId = table.Column<int>(type: "int", nullable: false),
                    CompraId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false),
                    NombreBocadillo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoPan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraBocadillo", x => new { x.BocadilloId, x.CompraId });
                    table.ForeignKey(
                        name: "FK_CompraBocadillo_Bocadillo_BocadilloId",
                        column: x => x.BocadilloId,
                        principalTable: "Bocadillo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompraBocadillo_Compra_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BonosComprados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    IdCompra = table.Column<int>(type: "int", nullable: false),
                    PrecioBono = table.Column<double>(type: "float", nullable: false),
                    BonoBocadilloId = table.Column<int>(type: "int", nullable: false),
                    CompraBonoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonosComprados", x => x.Id);
                    table.UniqueConstraint("AK_BonosComprados_Id_IdCompra", x => new { x.Id, x.IdCompra });
                    table.ForeignKey(
                        name: "FK_BonosComprados_BonoBocadillo_BonoBocadilloId",
                        column: x => x.BonoBocadilloId,
                        principalTable: "BonoBocadillo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BonosComprados_CompraBono_CompraBonoId",
                        column: x => x.CompraBonoId,
                        principalTable: "CompraBono",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Producto_Compra",
                columns: table => new
                {
                    CompraId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PVP = table.Column<double>(type: "float", nullable: false),
                    CompraId1 = table.Column<int>(type: "int", nullable: false),
                    ResenyaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto_Compra", x => new { x.ProductoId, x.CompraId });
                    table.ForeignKey(
                        name: "FK_Producto_Compra_Compra_Producto_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compra_Producto",
                        principalColumn: "Compraid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Producto_Compra_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Producto_Compra_Resenya_ResenyaId",
                        column: x => x.ResenyaId,
                        principalTable: "Resenya",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResenyaBocadillo",
                columns: table => new
                {
                    BocadilloId = table.Column<int>(type: "int", nullable: false),
                    ResenyaId = table.Column<int>(type: "int", nullable: false),
                    Puntuacion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResenyaBocadillo", x => new { x.BocadilloId, x.ResenyaId });
                    table.UniqueConstraint("AK_ResenyaBocadillo_ResenyaId_BocadilloId", x => new { x.ResenyaId, x.BocadilloId });
                    table.ForeignKey(
                        name: "FK_ResenyaBocadillo_Bocadillo_BocadilloId",
                        column: x => x.BocadilloId,
                        principalTable: "Bocadillo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResenyaBocadillo_Resenya_ResenyaId",
                        column: x => x.ResenyaId,
                        principalTable: "Resenya",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bocadillo_TipoPanId",
                table: "Bocadillo",
                column: "TipoPanId");

            migrationBuilder.CreateIndex(
                name: "IX_BonoBocadillo_TipoBocadilloId",
                table: "BonoBocadillo",
                column: "TipoBocadilloId");

            migrationBuilder.CreateIndex(
                name: "IX_BonosComprados_BonoBocadilloId",
                table: "BonosComprados",
                column: "BonoBocadilloId");

            migrationBuilder.CreateIndex(
                name: "IX_BonosComprados_CompraBonoId",
                table: "BonosComprados",
                column: "CompraBonoId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraBocadillo_CompraId",
                table: "CompraBocadillo",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraBono_ApplicationUserId",
                table: "CompraBono",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_TipoProductoProductoId",
                table: "Producto",
                column: "TipoProductoProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Compra_CompraId",
                table: "Producto_Compra",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Compra_ResenyaId",
                table: "Producto_Compra",
                column: "ResenyaId");

            migrationBuilder.CreateIndex(
                name: "IX_Resenya_ApplicationUserId",
                table: "Resenya",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonosComprados");

            migrationBuilder.DropTable(
                name: "CompraBocadillo");

            migrationBuilder.DropTable(
                name: "Producto_Compra");

            migrationBuilder.DropTable(
                name: "ResenyaBocadillo");

            migrationBuilder.DropTable(
                name: "BonoBocadillo");

            migrationBuilder.DropTable(
                name: "CompraBono");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Compra_Producto");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Bocadillo");

            migrationBuilder.DropTable(
                name: "Resenya");

            migrationBuilder.DropTable(
                name: "TipoBocadillo");

            migrationBuilder.DropTable(
                name: "TipoProducto");

            migrationBuilder.DropTable(
                name: "TipoPan");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
