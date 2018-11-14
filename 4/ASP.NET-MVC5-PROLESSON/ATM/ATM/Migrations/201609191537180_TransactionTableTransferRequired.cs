namespace ATM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionTableTransferRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transactions", "TransferDestinationCheckingAccountNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transactions", "TransferDestinationCheckingAccountNumber", c => c.String());
        }
    }
}
