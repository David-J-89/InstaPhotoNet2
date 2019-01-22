using AutoMapper;
using InstaPhotoNet.Dtos;
using InstaPhotoNet.Models;
using System.Linq;

namespace InstaPhotoNet.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                    .ForMember(dest => dest.PhotoUrl, opt =>
                    {
                        opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsProfile).Url);
                    });

            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsProfile).Url);
                });


            CreateMap<PhotosForDetailedDto, Photo>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<PhotoForReturnDto, Photo>();
            //CreateMap<User, PhotoForReturnDto>()
            //    .ForMember(dest => dest.UserKnownAs, opt =>
            //   opt.MapFrom(src => src.KnownAs));
            ;
            CreateMap<PhotoForCreationDto, Photo>();
            ;
        }
    }
}
