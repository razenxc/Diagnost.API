using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Diagnost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Sessions_SessionId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "Results");

            migrationBuilder.RenameColumn(
                name: "SuccessfullClicks",
                table: "Results",
                newName: "UFP_MinExposure_ms");

            migrationBuilder.RenameColumn(
                name: "StandardDeviation",
                table: "Results",
                newName: "UFP_TotalTime_s");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Results",
                newName: "AccessCodeId");

            migrationBuilder.RenameColumn(
                name: "Errors",
                table: "Results",
                newName: "UFP_ErrorsWrongButton");

            migrationBuilder.RenameColumn(
                name: "AverageLatency",
                table: "Results",
                newName: "UFP_TimeTillMinExp_s");

            migrationBuilder.RenameIndex(
                name: "IX_Results_SessionId",
                table: "Results",
                newName: "IX_Results_AccessCodeId");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Results",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Results",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "Results",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "PV2_3Smth1",
                table: "Results",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "PV2_ErrorsFalseAlarm",
                table: "Results",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PV2_ErrorsMissed",
                table: "Results",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PV2_ErrorsWrongButton",
                table: "Results",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "PV2_StdDev_ms",
                table: "Results",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PZMRChtoToTam1",
                table: "Results",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PZMRSmth2",
                table: "Results",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "PZMR_ErrorsTotal",
                table: "Results",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PZMR_SuccessfulClicks",
                table: "Results",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SportQualification",
                table: "Results",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SportType",
                table: "Results",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentFullName",
                table: "Results",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "UFPSmth1",
                table: "Results",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "UFP_ErrorsFalseAlarm",
                table: "Results",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UFP_ErrorsMissed",
                table: "Results",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "UFP_StdDev_ms",
                table: "Results",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Sessions_AccessCodeId",
                table: "Results",
                column: "AccessCodeId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Sessions_AccessCodeId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "Group",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "PV2_3Smth1",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "PV2_ErrorsFalseAlarm",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "PV2_ErrorsMissed",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "PV2_ErrorsWrongButton",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "PV2_StdDev_ms",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "PZMRChtoToTam1",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "PZMRSmth2",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "PZMR_ErrorsTotal",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "PZMR_SuccessfulClicks",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "SportQualification",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "SportType",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "StudentFullName",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "UFPSmth1",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "UFP_ErrorsFalseAlarm",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "UFP_ErrorsMissed",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "UFP_StdDev_ms",
                table: "Results");

            migrationBuilder.RenameColumn(
                name: "UFP_TotalTime_s",
                table: "Results",
                newName: "StandardDeviation");

            migrationBuilder.RenameColumn(
                name: "UFP_TimeTillMinExp_s",
                table: "Results",
                newName: "AverageLatency");

            migrationBuilder.RenameColumn(
                name: "UFP_MinExposure_ms",
                table: "Results",
                newName: "SuccessfullClicks");

            migrationBuilder.RenameColumn(
                name: "UFP_ErrorsWrongButton",
                table: "Results",
                newName: "Errors");

            migrationBuilder.RenameColumn(
                name: "AccessCodeId",
                table: "Results",
                newName: "SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Results_AccessCodeId",
                table: "Results",
                newName: "IX_Results_SessionId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Results",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "Results",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Sessions_SessionId",
                table: "Results",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
