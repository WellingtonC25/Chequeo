namespace ServiceDeskPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {

            AlterColumn("dbo.Requests", "UsuarioId", c => c.String());

        }
        
        public override void Down()
        {

            AlterColumn("dbo.Requests", "UsuarioId", c => c.Int(nullable: false));

        }
    }
}
