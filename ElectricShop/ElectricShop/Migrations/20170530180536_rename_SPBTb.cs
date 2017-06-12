using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ElectricShop.Migrations
{
    public partial class rename_SPBTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SanPhamDaBan");

            migrationBuilder.CreateTable(
                name: "SanPhamBan",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    khachHangID = table.Column<int>(nullable: false),
                    maBaoHanh = table.Column<string>(nullable: true),
                    ngayBan = table.Column<DateTime>(nullable: false),
                    sanPhamID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhamBan", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SanPhamBan");

            migrationBuilder.CreateTable(
                name: "SanPhamDaBan",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    khachHangID = table.Column<int>(nullable: false),
                    maBaoHanh = table.Column<string>(nullable: true),
                    ngayBan = table.Column<DateTime>(nullable: false),
                    sanPhamID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhamDaBan", x => x.ID);
                });
        }
    }
}
