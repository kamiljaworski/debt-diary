namespace DebtDiary.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveAvatarColorToPerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Debtors", "AvatarColor", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Debtors", "AvatarColor");
        }
    }
}
