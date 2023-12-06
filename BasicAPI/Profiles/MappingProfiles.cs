using AutoMapper;
using BasicAPI.Model.Entities;
using BasicAPI.Model.Request;
using BasicAPI.Model.Response;

namespace BasicAPI.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Product
            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<CreateProductRequest, Product>().ReverseMap();
        }
    }
}
