namespace ServiceDeskPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminadoAsignadaDeLaTablaRequest : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Requests", "Asigned");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "Asigned", c => c.Boolean(nullable: false));
        }
    }
}
