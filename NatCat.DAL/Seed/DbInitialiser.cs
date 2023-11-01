using System;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NatCat.DAL.Entity;
using NatCat.Model.StaticData;

namespace NatCat.DAL.Seed
{
    public class DbInitialiser : IDbInitialiser
    {
        private readonly NatCatDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string _defaultPassword;

        public DbInitialiser(NatCatDbContext dbContext,
                             UserManager<ApplicationUser> userManager,
                             RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _defaultPassword = "Chelsea23!";
        }

        public void SeedDatabase()
        {

            if (_dbContext.Database.GetPendingMigrations().Count() > 0)
            {
                _dbContext.Database.Migrate();
            }

            SeedRoles();

            SeedDefaultUsers();

            SeedGenres();

            SeedRhymingPatterns();

            SeedMaskingTypes();

            SeedBookClubs();

            SeedBookClubInvites();

            //SeedStoryTypes();

            SeedStories();

            SeedStoryParts();

        }

        #region users_and_roles
        private void SeedRoles()
        {
            SeedRole(StaticData.ROLE_ADMIN);
            SeedRole(StaticData.ROLE_DEMO_USER);
            SeedRole(StaticData.ROLE_USER);
        }

        private void SeedRole(string roleName)
        {
            if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
            }
        }

        private void SeedDefaultUsers()
        {
            ApplicationUser adminUser = new()
            {
                UserName = "adam23stevens@gmail.com",
                Email = "adam23stevens@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "07854385800",
                ProfileName = "ST2V3NS"
            };

            ApplicationUser normalUser = new()
            {
                UserName = "julia23stevens@gmail.com",
                Email = "julia23stevens@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "07860933229",
                ProfileName = "JFS"
            };

            ApplicationUser demoUser = new()
            {
                UserName = "jamesovenden@gmail.com",
                Email = "jamesovenden@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "0777123456",
                ProfileName = "Darkness James"
            };

            SeedUser(adminUser, StaticData.ROLE_ADMIN);
            SeedUser(normalUser, StaticData.ROLE_USER);
            SeedUser(demoUser, StaticData.ROLE_DEMO_USER);
        }

        private void SeedUser(ApplicationUser user, string role)
        {
            try
            {
                if (_userManager.FindByEmailAsync(user.Email ?? "").GetAwaiter().GetResult() == null)
                {
                    _userManager.CreateAsync(user, _defaultPassword).GetAwaiter().GetResult();

                    _userManager.AddToRoleAsync(user, role).GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Keywords_and_genres

        private void SeedGenres()
        {
            try
            {
                foreach (var genreName in SeedData.GenreNames)
                {
                    if (!_dbContext.Genres.Any(x => (x.Name?? "").ToUpper() == genreName.ToUpper()))
                    {
                        Genre genre = new()
                        {
                            Name = genreName,
                            IsActive = true
                        };

                        _dbContext.Genres.Add(genre);
                        _dbContext.SaveChanges();

                        if (genreName == SeedData.GenreNames.ToList()[0])
                        {
                            foreach (var keyword in SeedData.HorrorKeywords)
                            {
                                KeyWord keyWord = new()
                                {
                                    Difficulty = 1,
                                    Genre = genre,
                                    Word = keyword
                                };
                                _dbContext.KeyWords.Add(keyWord);
                            }
                        }
                        if (genreName == SeedData.GenreNames.ToList()[1])
                        {
                            foreach (var keyword in SeedData.AdventureKeywords)
                            {
                                KeyWord keyWord = new()
                                {
                                    Difficulty = 1,
                                    Genre = genre,
                                    Word = keyword
                                };
                                _dbContext.KeyWords.Add(keyWord);
                            }
                        }
                        if (genreName == SeedData.GenreNames.ToList()[2])
                        {
                            foreach (var keyword in SeedData.FantasyKeywords)
                            {
                                KeyWord keyWord = new()
                                {
                                    Difficulty = 1,
                                    Genre = genre,
                                    Word = keyword
                                };
                                _dbContext.KeyWords.Add(keyWord);
                            }
                        }
                        _dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Stories

        private void SeedMaskingTypes()
        {
            try
            {
                if (!_dbContext.MaskingTypes.Any())
                {
                    for (var cnt = 0; cnt< SeedData.MaskingTypeNames.Count(); cnt++)
                    {
                        var name = SeedData.MaskingTypeNames.ToArray()[cnt];
                        var description = SeedData.MaskingTypeDescriptions.ToArray()[cnt];

                        MaskingType maskingType = new()
                        {
                            Name = name,
                            Description = description
                        };

                        _dbContext.MaskingTypes.Add(maskingType);
                    }

                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void SeedRhymingPatterns()
        {
            try
            {
                if (!_dbContext.RhymingPatterns.Any())
                {
                    for (var cnt = 0; cnt < SeedData.RhymingPatternNames.Count(); cnt++)
                    {
                        var name = SeedData.RhymingPatternNames.ToArray()[cnt];
                        var patternStr = SeedData.RhymingPatterns.ToArray()[cnt];
                        var description = SeedData.RhymingPatternDescriptions.ToArray()[cnt];

                        RhymingPattern pattern = new()
                        {
                            Name = name,
                            PatternStr = patternStr,
                            Description = description
                        };

                        _dbContext.RhymingPatterns.Add(pattern);
                    }

                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void SeedStoryTypes()
        {
            try
            {
                if (!_dbContext.StoryTypes.Any())
                {
                    for (int cnt = 0; cnt < SeedData.StoryTypes.Count(); cnt++)
                    {
                        string? rhymingPattern = null;
                        var isRhymingRequired = false;
                        var typeName = SeedData.StoryTypes.ToArray()[cnt];
                        switch(typeName) {
                            case "Limerick":
                                rhymingPattern = "AABBA";
                                isRhymingRequired = true;
                                break;
                            case "Poem A":
                                rhymingPattern = "AABB";
                                isRhymingRequired = true;
                                break;
                            case "Poem B":
                                rhymingPattern = "ABAB";
                                isRhymingRequired = true;
                                break;
                            default:
                            break;
                        }
                        StoryType storyType = new()
                        {
                            TypeName = SeedData.StoryTypes.ToArray()[cnt],
                            MinCharLengthPerStoryPart = 15,
                            MaxCharLengthPerStoryPart = 150,
                            //MinWordsPerStoryPart = 3,
                            //MaxWordsPerStoryPart = 50,
                            DisplayOrder = cnt,
                            RuleDescription = SeedData.StoryTypeRuleDescriptions.ToArray()[cnt],
                            //IsRhymingRequired = isRhymingRequired,
                            //RhymingPattern = rhymingPattern
                        };

                        _dbContext.StoryTypes.Add(storyType);
                    }

                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void SeedStories()
        {
            try
            {
                if (!_dbContext.Stories.Any())
                {
                    Story horrorStory = new()
                    {
                        Title = SeedData.StoryTitles.ToList()[0],
                        Synopsis = "A scary ghost story",
                        Genre = _dbContext.Genres.First(x => x.Name == "Horror"),
                        DateCreated = DateTime.Now,
                        //StoryType = _dbContext.StoryTypes.First(x => x.TypeName == "Novel"),
                        RhymingPattern = _dbContext.RhymingPatterns.First(x => x.Name == SeedData.RhymingPatternNames.ToArray()[0]),
                        MaskingType = _dbContext.MaskingTypes.First(),
                        AuthorApplicationUser = _userManager.Users.First(),
                        TotalStoryRounds = 3,
                        MaxUsers = 4,
                        MinCharLengthPerStoryPart = 30,
                        MaxCharLengthPerStoryPart = 150,
                        IsBeingWritten = true,
                        IsDeleted = false,
                        IsVisibleOnLibrary = false,
                        BookClub = _dbContext.BookClubs.First()
                    };
                    int cnt = 0;
                    foreach (var user in _dbContext.Users.Where(u => u.Email == "adam23stevens@gmail.com" || u.Email == "julia23stevens@gmail.com" || u.Email == "jamesovenden@gmail.com"))
                    {
                        StoryUser su = new()
                        {
                            Story = horrorStory,
                            ApplicationUser = user,
                            Order = cnt
                        };
                        _dbContext.StoryUsers.Add(su);
                        cnt++;
                    }

                    Story adventureStory = new()
                    {
                        Title = SeedData.StoryTitles.ToList()[1],
                        Synopsis = "Action packed adventure story",
                        Genre = _dbContext.Genres.First(x => x.Name == "Adventure"),
                        DateCreated = DateTime.Now,
                        //StoryType = _dbContext.StoryTypes.First(x => x.TypeName == "Novel"),
                        RhymingPattern = _dbContext.RhymingPatterns.First(x => x.Name == SeedData.RhymingPatternNames.ToArray()[0]),
                        MaskingType = _dbContext.MaskingTypes.First(),
                        AuthorApplicationUser = _userManager.Users.First(),
                        TotalStoryRounds = 3,
                        MinCharLengthPerStoryPart = 30,
                        MaxCharLengthPerStoryPart = 150,
                        MaxUsers = 4,
                        IsBeingWritten = true,
                        IsDeleted = false,
                        IsVisibleOnLibrary = false,
                        BookClub = _dbContext.BookClubs.First()
                    };
                    cnt = 0;
                    foreach (var user in _dbContext.Users.Where(u => u.Email == "adam23stevens@gmail.com" || u.Email == "julia23stevens@gmail.com" || u.Email == "jamesovenden@gmail.com"))
                    {
                        StoryUser su = new()
                        {
                            Story = adventureStory,
                            ApplicationUser = user,
                            Order = cnt
                        };
                        _dbContext.StoryUsers.Add(su);
                        cnt++;
                    }

                    Story fantasyStory = new()
                    {
                        Title = SeedData.StoryTitles.ToList()[2],
                        Synopsis = "A fantasy style novel",
                        Genre = _dbContext.Genres.First(x => x.Name == "Fantasy"),
                        DateCreated = DateTime.Now,
                        //StoryType = _dbContext.StoryTypes.First(x => x.TypeName == "Novel"),
                        RhymingPattern = _dbContext.RhymingPatterns.First(x => x.Name == SeedData.RhymingPatternNames.ToArray()[0]),
                        MaskingType = _dbContext.MaskingTypes.First(),
                        AuthorApplicationUser = _userManager.Users.First(),
                        TotalStoryRounds = 3,
                        MinCharLengthPerStoryPart = 30,
                        MaxCharLengthPerStoryPart = 150,
                        MaxUsers = 4,
                        IsBeingWritten = true,
                        IsDeleted = false,
                        IsVisibleOnLibrary = false,
                        BookClub = _dbContext.BookClubs.First()
                    };
                    cnt = 0;
                    foreach (var user in _dbContext.Users.Where(u => u.Email == "adam23stevens@gmail.com" || u.Email == "julia23stevens@gmail.com" || u.Email == "jamesovenden@gmail.com"))
                    {
                        StoryUser su = new()
                        {
                            Story = fantasyStory,
                            ApplicationUser = user,
                            Order = cnt
                        };
                        _dbContext.StoryUsers.Add(su);
                        cnt++;
                    }

                    _dbContext.Stories.Add(horrorStory);
                    _dbContext.Stories.Add(adventureStory);
                    _dbContext.Stories.Add(fantasyStory);
                }
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string? GetInvisibleText(string? text)
        {
            if (text == null) return null;
            StringBuilder sb = new StringBuilder();
            var wordCnt = text.Split(' ');
            for (int x = 0; x < wordCnt.Length - 2; x++) {
                var letterCnt = wordCnt[x].Length;
                for(int y = 0; y < letterCnt; y++) {
                    sb.Append('x');
                }
                sb.Append(' ');
            }

            var ret = sb.ToString().TrimEnd();
            return ret;
        }

        private string? GetVisibleText(string? text)
        {
            if (text == null) return null;
            StringBuilder sb = new StringBuilder();
            var wordCnt = text.Split(' ');
            for(int x = wordCnt.Length - 2; x < wordCnt.Length; x++) {
                sb.Append(wordCnt[x]);
                sb.Append(" ");
            }

            var ret = sb.ToString().TrimEnd();
            return ret;
        }

        private void SeedStoryParts()
        {
            try
            {
                if (!_dbContext.StoryParts.Any())
                {
                    var horrorParts = new List<StoryPart>();
                    var adventureParts = new List<StoryPart>();
                    var fantasyParts = new List<StoryPart>();

                    var users = _dbContext.ApplicationUsers.ToList();

                    int usrIndex = 0;
                    int cnt = 0;

                    foreach (var horrorText in SeedData.HorrorStoryPartTexts)
                    {
                        var cntMinusOne = cnt -1;
                        string? previousTxt = cntMinusOne >= 0 ? SeedData.HorrorStoryPartTexts.ToArray()[cntMinusOne] : null;

                        StoryPart part = new()
                        {
                            ApplicationUser = users[usrIndex],
                            IsRhymingRequired = false,
                            Order = cnt,
                            Text = horrorText,
                            InvisibleTextFromPrevious = GetInvisibleText(previousTxt),
                            VisibleTextFromPrevious = GetVisibleText(previousTxt),
                            RhymingWords = null,
                            Story = _dbContext.Stories.First(x => x.GenreId == (_dbContext.Genres.First(x => x.Name == "Horror").Id)),
                            IsFinalStoryPart = false
                        };
                        cnt++;
                        usrIndex = usrIndex == 2 ? 0 : usrIndex + 1;
                        horrorParts.Add(part);
                    }

                    var nextHorrorText = new StoryPart
                    {
                        ApplicationUser = users[usrIndex],
                        IsRhymingRequired = false,
                        Order = cnt + 1,
                        Text = "",
                        InvisibleTextFromPrevious = GetInvisibleText(SeedData.HorrorStoryPartTexts.Last()),
                        VisibleTextFromPrevious = GetVisibleText(SeedData.HorrorStoryPartTexts.Last()),
                        RhymingWords = null,
                        Story = _dbContext.Stories.First(x => x.GenreId == (_dbContext.Genres.First(x => x.Name == "Horror").Id)),
                        IsFinalStoryPart = false
                    };
                    horrorParts.Add(nextHorrorText);
                    _dbContext.StoryParts.AddRange(horrorParts);

                    var keyWords = _dbContext.Genres.First(x => x.Name == "Horror").KeyWords.Take(2);
                    foreach(var keyword in keyWords) {
                        var storyPartKeyWord = new StoryPartKeyWord() {
                            KeyWordId = keyword.Id,
                            StoryPartId = nextHorrorText.Id
                        };
                        _dbContext.StoryPartKeyWords.Add(storyPartKeyWord);
                    }

                    usrIndex = 0;
                    cnt = 0;

                    foreach (var adventureText in SeedData.AdventureStoryPartTexts)
                    {
                        var cntMinusOne = cnt -1;
                        string? previousTxt = cntMinusOne >= 0 ? SeedData.AdventureStoryPartTexts.ToArray()[cntMinusOne] : null;
                        StoryPart part = new()
                        {
                            ApplicationUser = users[usrIndex],
                            IsRhymingRequired = false,
                            Order = cnt,
                            RhymingWords = null,
                            Text = adventureText,
                            InvisibleTextFromPrevious = GetInvisibleText(previousTxt),
                            VisibleTextFromPrevious = GetVisibleText(previousTxt),
                            Story = _dbContext.Stories.First(x => x.GenreId == (_dbContext.Genres.First(x => x.Name == "Adventure").Id)),
                            IsFinalStoryPart = false
                        };
                        cnt++;
                        usrIndex = usrIndex == 2 ? 0 : usrIndex + 1;
                        adventureParts.Add(part);
                    }

                    var nextAdventureText = new StoryPart
                    {
                        ApplicationUser = users[usrIndex],
                        IsRhymingRequired = false,
                        Order = cnt + 1,
                        Text = "",
                        InvisibleTextFromPrevious = GetInvisibleText(SeedData.AdventureStoryPartTexts.Last()),
                        VisibleTextFromPrevious = GetVisibleText(SeedData.AdventureStoryPartTexts.Last()),
                        RhymingWords = null,
                        Story = _dbContext.Stories.First(x => x.GenreId == (_dbContext.Genres.First(x => x.Name == "Adventure").Id)),
                        IsFinalStoryPart = false
                    };
                    adventureParts.Add(nextAdventureText);
                    _dbContext.StoryParts.AddRange(adventureParts);

                    var keyWords2 = _dbContext.Genres.First(x => x.Name == "Adventure").KeyWords.Take(2);
                    foreach(var keyword in keyWords2) {
                        var storyPartKeyWord = new StoryPartKeyWord() {
                            KeyWordId = keyword.Id,
                            StoryPartId = nextAdventureText.Id
                        };
                        _dbContext.StoryPartKeyWords.Add(storyPartKeyWord);
                    }

                    usrIndex = 0;
                    cnt = 0;

                    foreach (var fantasyText in SeedData.FantasyStoryPartTexts)
                    {
                        var cntMinusOne = cnt -1;
                        string? previousTxt = cntMinusOne >= 0 ? SeedData.FantasyStoryPartTexts.ToArray()[cntMinusOne] : null;
                        StoryPart part = new()
                        {
                            ApplicationUser = users[usrIndex],
                            IsRhymingRequired = false,
                            RhymingWords = null,
                            Order = cnt,
                            Text = fantasyText,
                            InvisibleTextFromPrevious = GetInvisibleText(previousTxt),
                            VisibleTextFromPrevious = GetVisibleText(previousTxt),
                            Story = _dbContext.Stories.First(x => x.GenreId == (_dbContext.Genres.First(x => x.Name == "Fantasy").Id)),
                            IsFinalStoryPart = false
                        };
                        cnt++;
                        usrIndex = usrIndex == 2 ? 0 : usrIndex + 1;
                        fantasyParts.Add(part);
                    }

                    var nextFantasyPart = new StoryPart
                    {
                        ApplicationUser = users[usrIndex],
                        IsRhymingRequired = false,
                        Order = cnt + 1,
                        Text = "",
                        InvisibleTextFromPrevious = GetInvisibleText(SeedData.FantasyStoryPartTexts.Last()),
                        VisibleTextFromPrevious = GetVisibleText(SeedData.FantasyStoryPartTexts.Last()),
                        RhymingWords = null,
                        Story = _dbContext.Stories.First(x => x.GenreId == (_dbContext.Genres.First(x => x.Name == "Fantasy").Id)),
                        IsFinalStoryPart = false
                    };
                    fantasyParts.Add(nextFantasyPart);
                    _dbContext.StoryParts.AddRange(fantasyParts);

                    var keyWords3 = _dbContext.Genres.First(x => x.Name == "Fantasy").KeyWords.Take(2);
                    foreach(var keyword in keyWords3) {
                        var storyPartKeyWord = new StoryPartKeyWord() {
                            KeyWordId = keyword.Id,
                            StoryPartId = nextFantasyPart.Id
                        };
                        _dbContext.StoryPartKeyWords.Add(storyPartKeyWord);
                    }
                }

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region BookClubs
        private void SeedBookClubs()
        {
            if (!_dbContext.BookClubs.Any())
            {
                var adamUser = _dbContext.Users.First(x => x.Email == "adam23stevens@gmail.com");
                var juliaUser = _dbContext.Users.First(x => x.Email == "julia23stevens@gmail.com");

                var newClub = new BookClub()
                {
                    Name = SeedData.BookClubName,
                    ApplicationUsers = new List<ApplicationUser>() { adamUser, juliaUser }
                };

                _dbContext.BookClubs.Add(newClub);
                _dbContext.SaveChanges();
            }
        }

        private void SeedBookClubInvites()
        {
            if (!_dbContext.BookClubJoinRequests.Any())
            {
                var adamUser = _dbContext.Users.First(x => x.Email == "adam23stevens@gmail.com");
                var jamesUser = _dbContext.Users.First(x => x.Email == "jamesovenden@gmail.com");
                var bookClub = _dbContext.BookClubs.First();

                var bookClubInvite = new BookClubJoinRequest()
                {
                    DateRequested = DateTime.Now,
                    ApplicationUser = jamesUser,
                    BookClub = bookClub,
                    RequestingUserProfileName = adamUser.ProfileName
                };

                _dbContext.BookClubJoinRequests.Add(bookClubInvite);
                _dbContext.SaveChanges();
            }
        }
        #endregion
    }
}

