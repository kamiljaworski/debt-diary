namespace DebtDiary.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDatesToModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Debtors", "AdditionDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Operations", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Operations", "Date");
            DropColumn("dbo.Debtors", "AdditionDate");
        }
    }
}
