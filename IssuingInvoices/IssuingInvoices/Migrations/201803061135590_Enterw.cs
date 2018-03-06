namespace IssuingInvoices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Enterw : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Invoices", "User_Id");
            AddForeignKey("dbo.Invoices", "User_Id", "dbo.ApplicationUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "User_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.Invoices", new[] { "User_Id" });
            DropColumn("dbo.Invoices", "User_Id");
        }
    }
}
