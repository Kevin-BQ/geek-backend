using AutoMapper;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<Brand, BrandDto>()
                     .ForMember(d => d.Status, m => m.MapFrom(o => o.Status == true ? 1 : 0))
                     .ForMember(d => d.ImageUrl, m => m.Ignore());

            CreateMap<BrandDto, Brand>()
                .ForMember(d => d.ImageUrl, m => m.Ignore());

            CreateMap<Category, CategoryDto>()
                     .ForMember(d => d.Status, m => m.MapFrom(o => o.Status == true ? 1 : 0));

            CreateMap<Subcategory, SubcategoryDto>()
                     .ForMember(d => d.Status, m => m.MapFrom(o => o.Status == true ? 1 : 0))
                     .ForMember(d => d.NameCategory, m => m.MapFrom(o => o.Category.NameCategory));

            CreateMap<Product, ProductDto>()
                    .ForMember(d => d.Status, m => m.MapFrom(o => o.Status == true ? 1 : 0))
                    .ForMember(d => d.NameBrand, m => m.MapFrom(o => o.Brand.NameBrand))
                    .ForMember(d => d.NameCategory, m => m.MapFrom(o => o.Category.NameCategory))
                    .ForMember(d => d.NameSubcategory, m => m.MapFrom(o => o.Subcategory.NameSubcategory))
                    .ForMember(d => d.Images, m => m.Ignore());

            CreateMap<ProductDto, Product>()
                    .ForMember(d => d.Images, m => m.Ignore());

            CreateMap<Image, ImageDto>()
                    .ForMember(d => d.NameProduct, m => m.MapFrom(o => o.Product.NameProduct))
                    .ForMember(d => d.ImageProduct, m => m.MapFrom(o => o.UrlImage))
                    .ForMember(d => d.UrlImage, m => m.Ignore());

            CreateMap<ImageDto, Image>()
                    .ForMember(d => d.UrlImage, m => m.Ignore());

            CreateMap<ShoppingCart, ShoppingCartDto>()
                    .ForMember(d => d.NameUser, m => m.MapFrom(o => o.User.Names));

            CreateMap<ShoppingCartDto, ShoppingCart>();

            CreateMap<ShoppingCartItem, ShoppingCartItemDto>()
                    .ForMember(d => d.NameProduct, m => m.MapFrom(o => o.Product.NameProduct))
                    .ForMember(d => d.Product, m => m.MapFrom(o => o.Product));

            CreateMap<Order, OrderDto>()
                    .ForMember(d => d.NameUser, m => m.MapFrom(o => o.User.Names))
                    .ForMember(d => d.Status, m => m.MapFrom(o => o.Status == true ? 1 : 0))
                    .ForMember(d => d.OrderStatus, m => m.MapFrom(o => o.OrderStatus));

            CreateMap<OrderItem, OrderItemDto>()
                    .ForMember(d => d.NameProduct, m => m.MapFrom(o => o.Product.NameProduct));

            CreateMap<Wishlist, WishlistDto>()
                    .ForMember(d => d.nameProduct, m => m.MapFrom(o => o.Product.NameProduct))
                    .ForMember(d => d.nameUser, m => m.MapFrom(o => o.UserAplication.Names));

        }
    }
}
