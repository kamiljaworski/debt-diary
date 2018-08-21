namespace DebtDiary.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAvatarColor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AvatarColor", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "AvatarColor");
        }
    }
}
