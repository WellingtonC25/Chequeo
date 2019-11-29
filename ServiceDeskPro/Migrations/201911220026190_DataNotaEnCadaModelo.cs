namespace ServiceDeskPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataNotaEnCadaModelo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Services", "Description", c => c.String(maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "Cedula", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Cedula", c => c.String());
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AlterColumn("dbo.Services", "Description", c => c.String());
        }
    }
}
