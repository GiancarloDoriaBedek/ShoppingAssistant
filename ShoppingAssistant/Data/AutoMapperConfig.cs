using AutoMapper;
using ShoppingAssistant.DTOs.ScraperDTOs;
using ShoppingAssistant.DTOs.UserDTOs;
using ShoppingAssistant.Models;

namespace ShoppingAssistant.Data
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            MapUser();
            MapScraperObjects();
        }

        private void MapUser()
        {
            CreateMap<User, UserRegistrationRequestDTO>().ReverseMap();
            CreateMap<User, UserLoginRequestDTO>().ReverseMap();
            CreateMap<User, UserPublicResponseDTO>().ReverseMap();
        }

        private void MapScraperObjects()
        {
            CreateMap<ScrapeWebshopRequestDTO, ScrapeCategoriesRequestDTO>().ReverseMap();
            CreateMap<ScrapeCategoriesResponseDTO, ScrapePagesRequestDTO>().ReverseMap();
            CreateMap<ScrapePagesResponseDTO, ScrapeProductsRequestDTO>().ReverseMap();
        }
    }
}
