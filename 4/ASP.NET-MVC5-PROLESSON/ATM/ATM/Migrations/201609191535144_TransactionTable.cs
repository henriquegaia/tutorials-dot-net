namespace ATM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CheckingAccounts", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.CheckingAccounts", new[] { "ApplicationUserId" });
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AmountDecimal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CheckingAccountId = c.Int(nullable: false),
                        TransferDestinationCheckingAccountNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CheckingAccounts", t => t.CheckingAccountId, cascadeDelete: true)
                .Index(t => t.CheckingAccountId);
            
            AddColumn("dbo.AspNetUsers", "Pin", c => c.String());
            AlterColumn("dbo.CheckingAccounts", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.CheckingAccounts", "ApplicationUserId");
            AddForeignKey("dbo.CheckingAccounts", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CheckingAccounts", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "CheckingAccountId", "dbo.CheckingAccounts");
            DropIndex("dbo.Transactions", new[] { "CheckingAccountId" });
            DropIndex("dbo.CheckingAccounts", new[] { "ApplicationUserId" });
            AlterColumn("dbo.CheckingAccounts", "ApplicationUserId", c => c.String(maxLength: 128));
            DropColumn("dbo.AspNetUsers", "Pin");
            DropTable("dbo.Transactions");
            CreateIndex("dbo.CheckingAccounts", "ApplicationUserId");
            AddForeignKey("dbo.CheckingAccounts", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
