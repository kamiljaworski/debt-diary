namespace DebtDiary.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameTransactionToOperationAndAddDescription : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Transactions", newName: "Operations");
            AddColumn("dbo.Operations", "Description", c => c.String(maxLength: 140));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Operations", "Description");
            RenameTable(name: "dbo.Operations", newName: "Transactions");
        }
    }
}
