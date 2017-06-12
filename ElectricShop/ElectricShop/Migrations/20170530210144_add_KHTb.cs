using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ElectricShop.Migrations
{
    public partial class add_KHTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "maBaoHanh",
                table: "SanPhamBan",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DiaChi = table.Column<string>(nullable: true),
                    HoTenKhachHang = table.Column<string>(nullable: true),
                    SoDienThoai = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.AlterColumn<string>(
                name: "maBaoHanh",
                table: "SanPhamBan",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
