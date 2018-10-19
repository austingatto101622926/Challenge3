namespace Week06_OAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModuleHasStudent : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Modules", name: "AspNetUser_Id", newName: "Student_Id");
            RenameIndex(table: "dbo.Modules", name: "IX_AspNetUser_Id", newName: "IX_Student_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Modules", name: "IX_Student_Id", newName: "IX_AspNetUser_Id");
            RenameColumn(table: "dbo.Modules", name: "Student_Id", newName: "AspNetUser_Id");
        }
    }
}
