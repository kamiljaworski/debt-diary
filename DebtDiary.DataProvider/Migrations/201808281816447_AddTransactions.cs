namespace DebtDiary.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransactions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Debtor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Debtors", t => t.Debtor_Id)
                .Index(t => t.Debtor_Id);
            
            DropColumn("dbo.Debtors", "Debt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Debtors", "Debt", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.Transactions", "Debtor_Id", "dbo.Debtors");
            DropIndex("dbo.Transactions", new[] { "Debtor_Id" });
            DropTable("dbo.Transactions");
        }
    }
}
