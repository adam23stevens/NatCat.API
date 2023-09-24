using AutoMapper;
using Natcat.Web.Response.Story;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.Story;
using NatCat.Model.Web.Story;

namespace NatCat.Application.Mapping
{
    public class StoryMap : Profile
    {
        public StoryMap()
        {
            CreateMap<Story, StoryDetailDto>();
            CreateMap<Story, StoryListDto>()
                .ForMember(x => x.CurrentUserCount, x => x.MapFrom(m => m.StoryUsers.Count()))
                .ForMember(x => x.StoryTypeName, x => x.MapFrom(m => m.StoryType.TypeName))
                .ForMember(x => x.AssignedUserId, x => x.MapFrom(m => m.StoryParts.OrderBy(p => p.Order).Last().ApplicationUserId))
                .ForMember(x => x.LastUpdated, x => x.MapFrom(m => m.StoryParts.OrderBy(p => p.Order).Last().DateCreated));

            CreateMap<AddStoryReq, Story>();
            CreateMap<Story, ReadStoryResponseDto>()
                .ForMember(m => m.StoryPartDetailDtos, m => m.MapFrom(x => x.StoryParts));
        }
    }

    public class StoryTypeMap : Profile
    {
        public StoryTypeMap()
        {
            CreateMap<StoryType, StoryTypeDetailDto>();
            CreateMap<StoryType, StoryTypeListDto>();
        }
    }

    public class StoryPartMap : Profile {
        public StoryPartMap() {

            CreateMap<StoryPart, LatestStoryPartResponse>()
                .ForMember(m => m.MinWords, m => m.MapFrom(x => x.Story.StoryType.MinWordsPerStoryPart))
                .ForMember(m => m.MaxWords, m => m.MapFrom(x => x.Story.StoryType.MaxWordsPerStoryPart))
                .ForMember(m => m.MinCharLength, m => m.MapFrom(x => x.Story.StoryType.MinCharLengthPerStoryPart))
                .ForMember(m => m.MaxCharLength, m => m.MapFrom(x => x.Story.StoryType.MaxCharLengthPerStoryPart))
                .ForMember(m => m.RequiredKeyWords, m => m.MapFrom(x => x.StoryPartKeyWords.Select(p => p.KeyWord.Word)));

            CreateMap<StoryPart, StoryPartDetailDto>();
            CreateMap<StoryPart, StoryPartListDto>();
        }
    }
}
