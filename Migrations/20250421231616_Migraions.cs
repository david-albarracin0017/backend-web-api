using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroService_NaceTuIdea.Migrations
{
    /// <inheritdoc />
    public partial class Migraions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaLocals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaLocals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ComodidadLocals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComodidadLocals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AddServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LocalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisponibilidadLocals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    horainicio = table.Column<TimeSpan>(type: "time", nullable: true),
                    horafin = table.Column<TimeSpan>(type: "time", nullable: true),
                    disponible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisponibilidadLocals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LocalCategorias",
                columns: table => new
                {
                    Categoriasid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Localesid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalCategorias", x => new { x.Categoriasid, x.Localesid });
                    table.ForeignKey(
                        name: "FK_LocalCategorias_CategoriaLocals_Categoriasid",
                        column: x => x.Categoriasid,
                        principalTable: "CategoriaLocals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalComodidades",
                columns: table => new
                {
                    Comodidadesid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Localesid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalComodidades", x => new { x.Comodidadesid, x.Localesid });
                    table.ForeignKey(
                        name: "FK_LocalComodidades_ComodidadLocals_Comodidadesid",
                        column: x => x.Comodidadesid,
                        principalTable: "ComodidadLocals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "locals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    propietarioid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    precioxhora = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    capacidad = table.Column<int>(type: "int", nullable: false),
                    FotosUrls = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideosUrls = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoriasIds = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ReglaLocals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReglaLocals", x => x.id);
                    table.ForeignKey(
                        name: "FK_ReglaLocals_locals_LocalId",
                        column: x => x.LocalId,
                        principalTable: "locals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    registro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    propietario = table.Column<bool>(type: "bit", nullable: false),
                    Localid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_locals_Localid",
                        column: x => x.Localid,
                        principalTable: "locals",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Notificacions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    usuarioid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    vista = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Notificacions_Users_usuarioid",
                        column: x => x.usuarioid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reseñas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    localid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    calificacion = table.Column<int>(type: "int", nullable: false),
                    comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    respuestaid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reseñas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reseñas_Users_userid",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reseñas_locals_localid",
                        column: x => x.localid,
                        principalTable: "locals",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    usuarioid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    localid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    asistentes = table.Column<int>(type: "int", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    reserva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reservas_Users_usuarioid",
                        column: x => x.usuarioid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_locals_localid",
                        column: x => x.localid,
                        principalTable: "locals",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "RespuestaRs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    reseñaid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    propietarioid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestaRs", x => x.id);
                    table.ForeignKey(
                        name: "FK_RespuestaRs_Reseñas_reseñaid",
                        column: x => x.reseñaid,
                        principalTable: "Reseñas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RespuestaRs_Users_propietarioid",
                        column: x => x.propietarioid,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddServices_LocalId",
                table: "AddServices",
                column: "LocalId");

            migrationBuilder.CreateIndex(
                name: "IX_DisponibilidadLocals_LocalId",
                table: "DisponibilidadLocals",
                column: "LocalId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalCategorias_Localesid",
                table: "LocalCategorias",
                column: "Localesid");

            migrationBuilder.CreateIndex(
                name: "IX_LocalComodidades_Localesid",
                table: "LocalComodidades",
                column: "Localesid");

            migrationBuilder.CreateIndex(
                name: "IX_locals_propietarioid",
                table: "locals",
                column: "propietarioid");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacions_usuarioid",
                table: "Notificacions",
                column: "usuarioid");

            migrationBuilder.CreateIndex(
                name: "IX_ReglaLocals_LocalId",
                table: "ReglaLocals",
                column: "LocalId");

            migrationBuilder.CreateIndex(
                name: "IX_Reseñas_localid",
                table: "Reseñas",
                column: "localid");

            migrationBuilder.CreateIndex(
                name: "IX_Reseñas_userid",
                table: "Reseñas",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_localid",
                table: "Reservas",
                column: "localid");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_usuarioid",
                table: "Reservas",
                column: "usuarioid");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestaRs_propietarioid",
                table: "RespuestaRs",
                column: "propietarioid");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestaRs_reseñaid",
                table: "RespuestaRs",
                column: "reseñaid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Localid",
                table: "Users",
                column: "Localid");

            migrationBuilder.AddForeignKey(
                name: "FK_AddServices_locals_LocalId",
                table: "AddServices",
                column: "LocalId",
                principalTable: "locals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisponibilidadLocals_locals_LocalId",
                table: "DisponibilidadLocals",
                column: "LocalId",
                principalTable: "locals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalCategorias_locals_Localesid",
                table: "LocalCategorias",
                column: "Localesid",
                principalTable: "locals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalComodidades_locals_Localesid",
                table: "LocalComodidades",
                column: "Localesid",
                principalTable: "locals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_locals_Users_propietarioid",
                table: "locals",
                column: "propietarioid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_locals_Localid",
                table: "Users");

            migrationBuilder.DropTable(
                name: "AddServices");

            migrationBuilder.DropTable(
                name: "DisponibilidadLocals");

            migrationBuilder.DropTable(
                name: "LocalCategorias");

            migrationBuilder.DropTable(
                name: "LocalComodidades");

            migrationBuilder.DropTable(
                name: "Notificacions");

            migrationBuilder.DropTable(
                name: "ReglaLocals");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "RespuestaRs");

            migrationBuilder.DropTable(
                name: "CategoriaLocals");

            migrationBuilder.DropTable(
                name: "ComodidadLocals");

            migrationBuilder.DropTable(
                name: "Reseñas");

            migrationBuilder.DropTable(
                name: "locals");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
