namespace Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHomeTownProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "HomeTown", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "HomeTown");
        }
    }
}
