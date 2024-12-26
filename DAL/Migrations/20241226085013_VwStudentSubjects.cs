using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class VwStudentSubjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view VwStudenSubjects
                  as 
                  SELECT dbo.TbSubjects.Id, dbo.TbSubjects.SubjectName, dbo.TbSubjects.CurrentState, dbo.TbSubjects.CreatedBy, dbo.TbSubjects.CreatedDate, dbo.TbSubjects.UpdatedBy, dbo.TbSubjects.UpdatedDate
                  FROM     dbo.TbSubjects INNER JOIN
                  dbo.TbStudentSubjects ON dbo.TbSubjects.Id = dbo.TbStudentSubjects.SubjectId INNER JOIN
                  dbo.TbStudents ON dbo.TbStudentSubjects.StudentId = dbo.TbStudents.Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW VwStudenSubjects;");
        }
    }
}
