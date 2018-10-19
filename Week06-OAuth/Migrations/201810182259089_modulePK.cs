namespace Week06_OAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modulePK : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Modules");
            AddColumn("dbo.Modules", "ModuleId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Modules", "MacAddress", c => c.String());
            AddPrimaryKey("dbo.Modules", "ModuleId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Modules");
            AlterColumn("dbo.Modules", "MacAddress", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Modules", "ModuleId");
            AddPrimaryKey("dbo.Modules", "MacAddress");
        }
    }
}
