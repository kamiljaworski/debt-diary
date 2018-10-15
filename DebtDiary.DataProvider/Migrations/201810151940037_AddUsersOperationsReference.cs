namespace DebtDiary.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsersOperationsReference : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Operations", "User_Id", c => c.Int());
            CreateIndex("dbo.Operations", "User_Id");
            AddForeignKey("dbo.Operations", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Operations", "User_Id", "dbo.Users");
            DropIndex("dbo.Operations", new[] { "User_Id" });
            DropColumn("dbo.Operations", "User_Id");
        }
    }
}
