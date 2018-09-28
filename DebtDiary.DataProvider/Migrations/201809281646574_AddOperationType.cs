namespace DebtDiary.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOperationType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Operations", "OperationType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Operations", "OperationType");
        }
    }
}
