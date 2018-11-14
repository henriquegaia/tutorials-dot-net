namespace ATM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentTable3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Entity = c.Int(nullable: false),
                        Reference = c.Int(nullable: false),
                        AmountDecimal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CheckingAccount_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CheckingAccounts", t => t.CheckingAccount_Id)
                .Index(t => t.CheckingAccount_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "CheckingAccount_Id", "dbo.CheckingAccounts");
            DropIndex("dbo.Payments", new[] { "CheckingAccount_Id" });
            DropTable("dbo.Payments");
        }
    }
}
