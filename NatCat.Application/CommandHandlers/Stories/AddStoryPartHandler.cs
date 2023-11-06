using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using NatCat.Application.Commands.Stories;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.DAL.Entity.Extensions;
using NatCat.Model.Dto.RhymingPattern;
using NatCat.Model.Dto.Story;
using NatCat.Model.Web.Response.RhymingWord;

namespace NatCat.Application.CommandHandlers.Stories
{
    public class AddStoryPartHandler : IRequestHandler<AddStoryPart>
    {
        private IRepository<Story, StoryDetailDto, StoryListDto> _storyRepository;
        private IRepository<StoryPart, StoryPartDetailDto, StoryPartListDto> _storyPartRepository;
        private IRepository<RhymingPattern, RhymingPatternDto, RhymingPatternDto> _rhymingRepository;
        //private IRepository<StoryType, StoryTypeDetailDto, StoryTypeListDto> _storyTypeRepository;
        public AddStoryPartHandler(
            IRepository<Story, StoryDetailDto, StoryListDto> storyRepository,
            IRepository<StoryPart, StoryPartDetailDto, StoryPartListDto> storyPartRepository,
            IRepository<StoryType, StoryTypeDetailDto, StoryTypeListDto> storyTypeRepository,
            IRepository<RhymingPattern, RhymingPatternDto, RhymingPatternDto> rhymingRepository)
        {
            _storyRepository = storyRepository;
            _storyPartRepository = storyPartRepository;
            //_storyTypeRepository = storyTypeRepository;
            _rhymingRepository = rhymingRepository;
        }

        public async Task<Unit> Handle(AddStoryPart request, CancellationToken cancellationToken)
        {
            try
            {
                //This method does all the work. 
                //It needs to validate the story part submitted is correct based on previous story part
                //Then it needs to create the next one
                //When creating the next one, it needs to set everything up so it's ready for the next person
                //to retrieve it. At this point it could send email / notifications to this user. 
                //var storyPart = await _storyPartRepository.GetEntityAsync(req.StoryPartId);

                // var story = await _storyRepository.GetEntityAsync(request.AddStoryPartReq.StoryId,
                // p => p.StoryParts,
                // p => p.StoryUsers,
                // p => p.Genre);
                var story = await _storyRepository.GetEntityAsync(request.AddStoryPartReq.StoryId);

                //var storyType = await _storyTypeRepository.GetEntityAsync(story.StoryTypeId);

                var storyRhymingPattern = await _rhymingRepository.GetAsync(story.RhymingPatternId);
                var orderStoryParts = story.StoryParts.OrderByDescending(x => x.Order);
                var storyPartId = orderStoryParts.First().Id;
                var storyPart = await _storyPartRepository.GetEntityAsync(storyPartId,
                p => p.StoryPartKeyWords);


                var exceptionMessages = new List<string>();

                if (storyPart.StoryPartKeyWords.Any())
                {
                    var requiredKeywords = storyPart.StoryPartKeyWords.Select(x => x.KeyWord.Word);

                    foreach (var keyword in requiredKeywords)
                    {
                        if (!request.AddStoryPartReq.Text.ToUpper().Contains(keyword.ToUpper()))
                        {
                            throw new Exception($"Required keyword missing: {keyword}");
                        }
                    }
                }

                if (request.AddStoryPartReq.Text.IsNullOrEmpty())
                {
                    exceptionMessages.Add("No text found");
                }

                if (storyPart.ApplicationUserId != request.ApplicationUserId)
                {
                    exceptionMessages.Add("It is not this user's turn to add to this story");
                }

                var textToValidate = request.AddStoryPartReq.Text.TrimStart().TrimEnd();

                if (request.AddStoryPartReq.Text.Length < story.MinCharLengthPerStoryPart
                 || request.AddStoryPartReq.Text.Length > story.MaxCharLengthPerStoryPart)
                {
                    exceptionMessages.Add("Invalid number of characters submitted");
                }

                //var wordCount = request.AddStoryPartReq.Text.Split(' ').Count();
                //if (wordCount < storyType.MinWordsPerStoryPart
                // || wordCount > storyType.MaxWordsPerStoryPart)
                //{
                //    exceptionMessages.Add("Invalid number of words submitted");
                //}

                if (storyPart.IsRhymingRequired && !string.IsNullOrEmpty(storyPart.RhymingWords))
                {
                    //var previousPart = story.StoryParts.FirstOrDefault(x => x.Order == storyPart.Order - 1);
                    //if (previousPart == null)
                    //{
                    //    exceptionMessages.Add("Unable to find rhyming word for story part");
                    //}
                    //else
                    //{
                    //    var lastWord = previousPart.LastWord();
                    //    if (!storyPart.RhymingWords.Split(',').Any(x => x.ToUpper() == lastWord.ToUpper()))
                    //    {
                    //        exceptionMessages.Add($"This story part does not rhyme with {lastWord}");
                    //    }
                    //}
                    storyPart.Text = request.AddStoryPartReq.Text;
                    var lastWord = storyPart.Text.LastWord();
                    if (!storyPart.RhymingWords.Split(',').Any(x => x.ToUpper() == lastWord.ToUpper()))
                    {
                        exceptionMessages.Add($"This story part does not rhyme with {lastWord}");
                    }
                }

                Random rnd = new Random();
                var numberKeywords = rnd.Next(1, 4);
                var allKeywords = story.Genre.KeyWords;
                var thisPartKeywords = new List<KeyWord>();
                for (int x = 0; x < numberKeywords; x++)
                {
                    var newKeyword = allKeywords
                        .Where(x => !thisPartKeywords.Any(p => p.Word == x.Word))
                        .OrderBy(x => Guid.NewGuid())
                        .First();
                    thisPartKeywords.Add(newKeyword);
                }

                storyPart.Text = request.AddStoryPartReq.Text;
                storyPart.DateCreated = DateTime.Now;

                story.CurrentStoryRound = storyPart.Order + 1;

                if (storyPart.IsFinalStoryPart)
                {
                    story.DateCompleted = DateTime.Now;
                    story.IsBeingWritten = false;
                    story.DatePublished = DateTime.Now;
                    
                    foreach (var oldP in story.StoryParts)
                    {
                        oldP.RhymingWords = null;

                        await _storyPartRepository.UpdateAsync(oldP.Id, oldP);
                    }

                    await _storyRepository.UpdateAsync(story.Id, story);
                }
                else
                {
                    var thisUserOrder = story.StoryUsers.First(x => x.ApplicationUserId == request.ApplicationUserId).Order;
                    var nextUserOrder = thisUserOrder + 1;
                    var maxUserOrder = story.StoryUsers.Max(x => x.Order);
                    if (thisUserOrder >= maxUserOrder)
                    {
                        nextUserOrder = 0;
                    }

                    var nextRhymingRequired = false;
                    string? wordToRhymeWith = null;
                    string? nextRhymingRequiredWords = null;
                    var rhymingPatternStr = storyRhymingPattern.PatternStr;

                    if (rhymingPatternStr != string.Empty)
                    {
                        var thisLastWord = storyPart.Text.LastWord();
                        var allRhymingWords = GetAllRhymingWords(thisLastWord);
                        if (allRhymingWords.Count() < 3)
                        {
                            exceptionMessages.Add($"Not enough words rhyme with {thisLastWord}");
                        }


                        while (rhymingPatternStr.Length <= storyPart.Order + 1)
                        {
                            rhymingPatternStr += rhymingPatternStr;
                        }

                        var nextPattern = rhymingPatternStr[storyPart.Order + 1];
                        var lastMatchingPatternIndex = storyPart.Order;
                        var checks = 0;

                        while (checks < 3 && lastMatchingPatternIndex >= 0 && rhymingPatternStr[lastMatchingPatternIndex] != nextPattern)
                        {
                            lastMatchingPatternIndex--;
                            checks++;
                        }

                        if (lastMatchingPatternIndex >= 0 && checks < 3)
                        {
                            var rhymingPart = story.StoryParts.First(x => x.Order == lastMatchingPatternIndex);
                            if (rhymingPart == null)
                            {
                                exceptionMessages.Add("Error finding rhyming pattern word");
                            }
                            else
                            {
                                var lastWord = rhymingPart?.Text?.LastWord();
                                nextRhymingRequiredWords = GetAllRhymingWords(lastWord);
                                if (nextRhymingRequiredWords.Split(',').Count() <= 2)
                                {
                                    exceptionMessages.Add($"Not enough words rhyme with {lastWord}");
                                }
                                wordToRhymeWith = lastWord;
                                nextRhymingRequired = true;
                            }

                            foreach (var oldP in story.StoryParts)
                            {
                                oldP.RhymingWords = null;

                                await _storyPartRepository.UpdateAsync(oldP.Id, oldP);
                            }
                        }
                    }

                    if (exceptionMessages.Any())
                    {
                        throw new Exception($"Error occured: {exceptionMessages.Select(x => x + " ")}");
                    }

                    var nextStoryPart = new StoryPart()
                    {
                        Story = story,
                        Order = storyPart.Order + 1,
                        ApplicationUser = story.StoryUsers.First(x => x.Order == nextUserOrder).ApplicationUser,
                        VisibleTextFromPrevious = GetVisibleText(story.PreviousTextVisibilityPercentage, storyPart.Text),
                        InvisibleTextFromPrevious = GetInvisibleText(story.PreviousTextVisibilityPercentage, story.MaskingType.Name, storyPart.Text),
                        DateSubmitted = null,
                        IsDeleted = false,
                        DeadlineTime = null,
                        RhymingWords = nextRhymingRequiredWords,
                        IsRhymingRequired = nextRhymingRequired,
                        WordToRhymeWith = wordToRhymeWith,
                        Text = "",
                        IsFinalStoryPart = storyPart.Order + 1 >= story.TotalStoryRounds - 1
                    };

                    var newStoryPartKeyWords = thisPartKeywords.Select(x => new StoryPartKeyWord
                    {
                        KeyWord = x,
                        StoryPart = nextStoryPart
                    }).ToList();

                    nextStoryPart.StoryPartKeyWords = newStoryPartKeyWords;
                    await _storyPartRepository.AddAsync(nextStoryPart);
                }
                await _storyPartRepository.UpdateAsync(storyPart.Id, storyPart);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string GetInvisibleText(int percentage, string maskingType, string text)
        {
            StringBuilder sb = new StringBuilder();
            var wordCnt = text.Split(' ');
            var percStart = MathF.Round((float)wordCnt.Length * ((float)percentage / 100), MidpointRounding.AwayFromZero);

            if (maskingType.Trim().ToUpper() == "INVISIBLE MASKING")
            {
                sb.Append("...");
            }
            else if (maskingType.Trim().ToUpper() == "X MASKING")
            {
                for (int x = 0; x < percStart; x++)
                {
                    var letterCnt = wordCnt[x].Length;
                    for (int y = 0; y < letterCnt; y++)
                    {
                        sb.Append('x');
                    }
                    sb.Append(' ');
                }
            }
            else if (maskingType.Trim().ToUpper() == "LETTER JUMBLE")
            {
                for (int x = 0; x < percStart; x++)
                {
                    var letterCnt = wordCnt[x].Length;
                    var indexStore = new List<int>();
                    for (int y = 0; y < letterCnt; y++)
                    {
                        Random rnd = new Random();
                        var letterIndex = rnd.Next(0, letterCnt);
                        do
                        {
                            if (!indexStore.Contains(letterIndex))
                            {
                                indexStore.Add(letterIndex);
                            }
                            else
                            {
                                letterIndex = rnd.Next(0, letterCnt);
                            }
                        }
                        while (!indexStore.Contains(letterIndex));

                        sb.Append(wordCnt[x][letterIndex]);
                    }
                    sb.Append(' ');
                }
            }
            else if (maskingType.Trim().ToUpper() == "WORD JUMBLE")
            {
                
                    var indexStore = new List<int>();
                    for (int y = 0; y < wordCnt.Length; y++)
                    {
                        Random rnd = new Random();
                        var wordIndex = rnd.Next(0, (int)percStart);

                        do
                        {
                            if (!indexStore.Contains(wordIndex))
                            {
                                indexStore.Add(wordIndex);
                            }
                            else
                            {
                                wordIndex = rnd.Next(0, (int)percStart);
                            }
                        }
                        while (!indexStore.Contains(wordIndex));
                    }
                    for(int y = 0; y < indexStore.Count(); y++)
                    {
                        var index = indexStore[y];
                        sb.Append(wordCnt[index]);

                        sb.Append(' ');
                    }
                
            }
            

            var ret = sb.ToString().TrimEnd();
            return ret;
        }

        private string GetVisibleText(int percentage, string text)
        {
            StringBuilder sb = new StringBuilder();
            var wordCnt = text.Split(' ');
            var percStart = MathF.Round((float)wordCnt.Length * ((float)percentage / 100), MidpointRounding.AwayFromZero);

            for (int x = int.Parse(percStart.ToString()); x < wordCnt.Length; x++)
            {
                sb.Append(wordCnt[x]);
                sb.Append(" ");
            }

            var ret = sb.ToString().TrimEnd();
            return ret;
        }
        private string GetAllRhymingWords(string wordToRhyme)
        {
            var ret = "";
            const string URL = "https://api.datamuse.com/words";
            string urlParameters = $"?rel_rhy={wordToRhyme}";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var responseItem = response.Content.ReadAsAsync<IEnumerable<RhymingWordResponseDto>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                ret = string.Join(',', responseItem.OrderBy(x => x.Score).Select(x => x.Word));
            }

            client.Dispose();

            return ret;
        }
    }
}