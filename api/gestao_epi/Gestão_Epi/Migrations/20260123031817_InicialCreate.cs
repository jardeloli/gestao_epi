using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gestão_Epi.Migrations
{
    /// <inheritdoc />
    public partial class InicialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "colaborador",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    setor = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    matricula = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "epi",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ca = table.Column<int>(type: "int", nullable: true),
                    tamanho = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descricao = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    validade = table.Column<DateOnly>(type: "date", nullable: true),
                    cor = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fabricante = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "perfil",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descricao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "permissao",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    codigo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descricao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "visitante",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    documento = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "estoque",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    epi_id = table.Column<int>(type: "int", nullable: false),
                    quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "estoque_ibfk_1",
                        column: x => x.epi_id,
                        principalTable: "epi",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    senhaHash = table.Column<byte[]>(type: "VARBINARY(64)", nullable: false),
                    senhaSalt = table.Column<byte[]>(type: "VARBINARY(128)", nullable: false),
                    perfil_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_perfil",
                        column: x => x.perfil_id,
                        principalTable: "perfil",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "perfil_permissao",
                columns: table => new
                {
                    perfil_id = table.Column<int>(type: "int", nullable: false),
                    permissao_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.perfil_id, x.permissao_id });
                    table.ForeignKey(
                        name: "perfil_permissao_ibfk_1",
                        column: x => x.perfil_id,
                        principalTable: "perfil",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "perfil_permissao_ibfk_2",
                        column: x => x.permissao_id,
                        principalTable: "permissao",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "retirada_devolucao",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    usuario_id = table.Column<int>(type: "int", nullable: false),
                    colaborador_id = table.Column<int>(type: "int", nullable: true),
                    visitante_id = table.Column<int>(type: "int", nullable: true),
                    epi_id = table.Column<int>(type: "int", nullable: false),
                    quantidade = table.Column<int>(type: "int", nullable: false),
                    data_retirada = table.Column<DateTime>(type: "datetime", nullable: false),
                    data_devolucao = table.Column<DateTime>(type: "datetime", nullable: true),
                    justificativa_retirada = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    justificativa_devolucao = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "retirada_devolucao_ibfk_1",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "retirada_devolucao_ibfk_2",
                        column: x => x.epi_id,
                        principalTable: "epi",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "retirada_devolucao_ibfk_3",
                        column: x => x.colaborador_id,
                        principalTable: "colaborador",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "retirada_devolucao_ibfk_4",
                        column: x => x.visitante_id,
                        principalTable: "visitante",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "notificacao",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    mensagem = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    colaborador_id = table.Column<int>(type: "int", nullable: true),
                    visitante_id = table.Column<int>(type: "int", nullable: true),
                    data_limite = table.Column<DateTime>(type: "datetime", nullable: false),
                    retirada_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, defaultValueSql: "'PENDENTE'", collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data_devolucao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "notificacao_ibfk_1",
                        column: x => x.colaborador_id,
                        principalTable: "colaborador",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "notificacao_ibfk_2",
                        column: x => x.visitante_id,
                        principalTable: "visitante",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "notificacao_ibfk_3",
                        column: x => x.retirada_id,
                        principalTable: "retirada_devolucao",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.InsertData(
                table: "permissao",
                columns: new[] { "id", "codigo", "descricao" },
                values: new object[,]
                {
                    { 1, "EPI_CADASTRAR", "PERMITE REALIZAR CADASTRO DE EPI" },
                    { 2, "EPI_EDITAR", "PERMITE REALIZAR EDIÇÃO DE EPI" },
                    { 3, "EPI_DELETAR", "PERMITE DELETAR EPI" },
                    { 4, "EPI_LISTAR", "PERMITE LISTAR EPIS" },
                    { 5, "USUARIO_CADASTRAR", "PERMITE REALIZAR CADASTRO DE USUÁRIO" },
                    { 6, "USUARIO_EDITAR", "PERMITE REALIZAR EDIÇÃO DE USUÁRIO" },
                    { 7, "USUARIO_DELETAR", "PERMITE DELETAR USUÁRIO" },
                    { 8, "USUARIO_LISTAR", "PERMITE LISTAR USUÁRIO" },
                    { 9, "PERFIL_CADASTRAR", "PERMITE REALIZAR CADASTRO DE PERFIL" },
                    { 10, "PERFIL_EDITAR", "PERMITE REALIZAR EDIÇÃO DE PERFIL" },
                    { 11, "PERFIL_DELETAR", "PERMITE DELETAR PERFIL" },
                    { 12, "PERFIL_LISTAR", "PERMITE LISTAR PERFIS" },
                    { 13, "RETIRADA_REGISTRAR", "PERMITE REGISTRAR SAÍDA EPI" },
                    { 14, "DEVOLUCAO_REGISTRAR", "PERMITE REGISTRAR ENTRADA EPI" },
                    { 15, "ESTOQUE_LISTAR", "PERMITE LISTAR ESTOQUE" },
                    { 16, "ESTOQUE_ENTRADA", "PERMITE AJUSTAR ESTOQUE(ENTRADA)" },
                    { 17, "ESTOQUE_SAIDA", "PERMITE AJUSTAR ESTOQUE(SAÍDA)" },
                    { 18, "VISITANTE_CADASTRAR", "PERMITE REALIZAR CADASTRO DE VISITANTE" },
                    { 19, "VISITANTE_EDITAR", "PERMITE REALIZAR EDIÇÃO VISITANTE" },
                    { 20, "VISITANTE_DELETAR", "PERMITE DELETAR VISITANTE" },
                    { 21, "VISITANTE_LISTAR", "PERMITE LISTAR VISITANTES" },
                    { 22, "COLABORADOR_CADASTRAR", "PERMITE REALIZAR CADASTRO DE COLABORADOR" },
                    { 23, "COLABORADOR_EDITAR", "PERMITE REALIZAR EDIÇÃO DE COLABORADOR" },
                    { 24, "COLABORADOR_DELETAR", "PERMITE DELETAR COLABORADOR" },
                    { 25, "COLABORADOR_LISTAR", "PERMITE LISTAR COLABORADORES" }
                });

            migrationBuilder.CreateIndex(
                name: "epi_id",
                table: "estoque",
                column: "epi_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "colaborador_id",
                table: "notificacao",
                column: "colaborador_id");

            migrationBuilder.CreateIndex(
                name: "retirada_id",
                table: "notificacao",
                column: "retirada_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "visitante_id",
                table: "notificacao",
                column: "visitante_id");

            migrationBuilder.CreateIndex(
                name: "permissao_id",
                table: "perfil_permissao",
                column: "permissao_id");

            migrationBuilder.CreateIndex(
                name: "codigo",
                table: "permissao",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "colaborador_id1",
                table: "retirada_devolucao",
                column: "colaborador_id");

            migrationBuilder.CreateIndex(
                name: "epi_id1",
                table: "retirada_devolucao",
                column: "epi_id");

            migrationBuilder.CreateIndex(
                name: "usuario_id",
                table: "retirada_devolucao",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "visitante_id1",
                table: "retirada_devolucao",
                column: "visitante_id");

            migrationBuilder.CreateIndex(
                name: "fk_perfil",
                table: "usuario",
                column: "perfil_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "estoque");

            migrationBuilder.DropTable(
                name: "notificacao");

            migrationBuilder.DropTable(
                name: "perfil_permissao");

            migrationBuilder.DropTable(
                name: "retirada_devolucao");

            migrationBuilder.DropTable(
                name: "permissao");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "epi");

            migrationBuilder.DropTable(
                name: "colaborador");

            migrationBuilder.DropTable(
                name: "visitante");

            migrationBuilder.DropTable(
                name: "perfil");
        }
    }
}
