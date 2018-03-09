namespace IssuingInvoices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Final : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Netto", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "NettoPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "NettoPrice");
            DropColumn("dbo.Invoices", "Netto");
        }
    }
}
