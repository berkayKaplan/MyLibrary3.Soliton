using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyLibrary.Entities;
using System.Runtime.Remoting.Contexts;

namespace MyLibraryDataAccessLayer.E.F
{
    public class MyInitializer : CreateDatabaseIfNotExists<DataBaseContext>
    {
       protected override void Seed(DataBaseContext context)
        {
            LibraryUser admin1 = new LibraryUser()
            {
                Name = "Berkay",
                Surname = "Kaplan",
                Email = "berkay_kaplan99@hormail.com",
                ActiveteGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "BerkayKaplan",
                Password = "123456",
                CreatedOn = DateTime.Now,
                ModifieOn = DateTime.Now.AddMinutes(5),
                ModifieOnUserName = "berkaykaplan"
            };
            LibraryUser User1 = new LibraryUser()
            {
                Name = "Aleyna",
                Surname = "Kaplan",
                Email = "Aleyna_kaplan99@hormail.com",
                ActiveteGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "AleynaKaplan",
                Password = "123456",
                CreatedOn = DateTime.Now.AddDays(1),
                ModifieOn = DateTime.Now.AddMinutes(65),
                ModifieOnUserName = "berkaykaplan"
               };
            context.LibraryUser.Add(admin1);
            context.LibraryUser.Add(User1);
            for (int p = 0; p < 8; p++)
            {
                LibraryUser User = new LibraryUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ActiveteGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    Username = $"user{p}",
                    Password = "123456",
                    CreatedOn = DateTime.Now.AddDays(1),
                    ModifieOn = DateTime.Now.AddMinutes(65),
                    ModifieOnUserName = $"user{p}",
                };
                context.LibraryUser.Add(User);
            }

            context.SaveChanges();
            List<LibraryUser> userlist = context.LibraryUser.ToList();
            for (int i = 0; i < 10; i++)
            {
                Category cat =new Category()
                {
                    Title = FakeData.PlaceData.GetCountry(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifieOn = DateTime.Now,
                    ModifieOnUserName = "berkaykaplan",
                };
                context.Categories.Add(cat);
                for (int k = 0; k < FakeData.NumberData.GetNumber(5, 10); k++)
                {
                    LibraryUser owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];
                    Examination examination = new Examination()
                    {

                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        Category = cat,
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        Owner = owner,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifieOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),


                    };

                    cat.MyExaminations.Add(examination);


                    for (int j = 0; j < FakeData.NumberData.GetNumber(3, 5); j++)
                    {
                        LibraryUser comment_owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),

                            Owner = comment_owner,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifieOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),

                        };
                        examination.Comments.Add(comment);
                    }

                    for (int m = 0; m < examination.LikeCount; m++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = userlist[m]
                        };
                        examination.Likes.Add(liked);
                    }
                }
          
            }
                context.SaveChanges();
        }
    }
}
