namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDataInsert6 : DbMigration
    {
        public override void Up()
        {
            AnatoliDbContext context = new AnatoliDbContext();
            context.BaseValues.AddOrUpdate(item => item.Id,
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("A591658A-E46B-440D-9ADB-E3E5B01B7489"), BaseValueName = "درخواست", BaseTypeId = Guid.Parse("80EAB271-D2BD-4119-92EF-A6F2DD867A6B"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("A0418ABD-941C-4826-8063-E82B4A5D48FE"), BaseValueName = "در صف ارسال", BaseTypeId = Guid.Parse("80EAB271-D2BD-4119-92EF-A6F2DD867A6B"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("5C0D43FC-6822-4D39-AB40-363B885BE464"), BaseValueName = "صدور پیش فاکتور", BaseTypeId = Guid.Parse("80EAB271-D2BD-4119-92EF-A6F2DD867A6B"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("EA5961AB-792A-4D20-8A52-5501F01F034A"), BaseValueName = "ارسال شده", BaseTypeId = Guid.Parse("80EAB271-D2BD-4119-92EF-A6F2DD867A6B"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("3AA22CED-A45E-4E58-B992-A4B1F838B19B"), BaseValueName = "تحویل شده ", BaseTypeId = Guid.Parse("80EAB271-D2BD-4119-92EF-A6F2DD867A6B"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("D12DEEE1-DA5B-44F6-937A-7B282789908F"), BaseValueName = "تسویه شده", BaseTypeId = Guid.Parse("80EAB271-D2BD-4119-92EF-A6F2DD867A6B"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.SaveChanges();
        }
        
        public override void Down()
        {
        }
    }
}
