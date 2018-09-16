namespace DebtDiary.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameDateToAdditionDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Operations", "AdditionDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Operations", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Operations", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Operations", "AdditionDate");
        }
    }
}
