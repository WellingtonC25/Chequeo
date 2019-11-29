namespace ServiceDeskPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregandoDataNota : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Phone", c => c.String(maxLength: 13));
            AlterColumn("dbo.Customers", "Cedula", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Cedula", c => c.String());
            AlterColumn("dbo.Customers", "Phone", c => c.String());
        }
    }
}
