using AutoMapper;
using PRN222.ProductStore.Repository.Entities;
using PRN222.ProductStore.Service.BusinessModels;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Product, ProductDTO>().ReverseMap();
		CreateMap<Category, CategoryDTO>().ReverseMap();
		CreateMap<AccountMember, AccountMemberDTO>().ReverseMap();
	}
}
