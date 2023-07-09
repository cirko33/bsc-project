using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class Changed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "Sold",
                table: "Keys",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Birthday", "Password" },
                values: new object[] { new DateTime(1998, 7, 10, 0, 55, 34, 296, DateTimeKind.Local).AddTicks(1736), "$2a$11$Sq0VBtf/1MQLoNLfUWuT1uR.VHqH6dapJdGSfF6bVxEizvYtCoDzy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Birthday", "Password" },
                values: new object[] { new DateTime(1998, 7, 10, 0, 55, 34, 464, DateTimeKind.Local).AddTicks(8681), "$2a$11$8DWzW22AWvShlwTq.JaDd.mmtH69USn7pIq7Vgb4K.PxkdbVcaSSe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Birthday", "Password" },
                values: new object[] { new DateTime(1998, 7, 10, 0, 55, 34, 619, DateTimeKind.Local).AddTicks(1615), "$2a$11$U2ohIZ2lpBz3CE2Czjg.2eQGaGZFe3EXNzXMj8gKgjTxS.uCMzmQe" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Orders");

            migrationBuilder.AlterColumn<bool>(
                name: "Sold",
                table: "Keys",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Birthday", "Password" },
                values: new object[] { new DateTime(1998, 6, 27, 19, 35, 58, 415, DateTimeKind.Local).AddTicks(2518), "$2a$11$bIS/LQcUBWSdQOlFUw0.yOlTwrPhyVNey8fmSQUyLQfFGn8BoetV." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Birthday", "Password" },
                values: new object[] { new DateTime(1998, 6, 27, 19, 35, 58, 601, DateTimeKind.Local).AddTicks(7072), "$2a$11$HzkxRiY4Coxc.HeA7s5/3ONKb6IYFwWs0FdpQ0KJd2kR/Lf6kvXlK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Birthday", "Password" },
                values: new object[] { new DateTime(1998, 6, 27, 19, 35, 58, 774, DateTimeKind.Local).AddTicks(3095), "$2a$11$Xf06iF76XGK6RNIlpmK15O62v8T71DTFyBiqn0QPJifP2XdpOZ1Dy" });
        }
    }
}
