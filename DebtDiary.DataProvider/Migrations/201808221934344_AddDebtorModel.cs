namespace DebtDiary.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDebtorModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Debtors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Debt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FirstName = c.String(maxLength: 80),
                        LastName = c.String(maxLength: 80),
                        Gender = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Debtors", "User_Id", "dbo.Users");
            DropIndex("dbo.Debtors", new[] { "User_Id" });
            DropTable("dbo.Debtors");
        }
    }
}
