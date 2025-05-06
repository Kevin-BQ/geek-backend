using AutoMapper;
using Models.DTOs;
using Models.Entities;

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
                    .ForMember(d => d.OrderStatus, m => m.MapFrom(o => o.OrderStatus))
                    .ForMember(d => d.Address, m => m.MapFrom(o => o.ShippingAddress.Address))
                    .ForMember(d => d.City, m => m.MapFrom(o => o.ShippingAddress.City))
                    .ForMember(d => d.ShippingMethod, m => m.MapFrom(o => o.Shipment.ShipmentMethod))
                    .ForMember(d => d.ShippingDate, m => m.MapFrom(o => o.Shipment.ShipmentDate)); ;


            CreateMap<OrderItem, OrderItemDto>()
                    .ForMember(d => d.NameProduct, m => m.MapFrom(o => o.Product.NameProduct));

            CreateMap<Wishlist, WishlistDto>()
                    .ForMember(d => d.nameProduct, m => m.MapFrom(o => o.Product.NameProduct))
                    .ForMember(d => d.nameUser, m => m.MapFrom(o => o.UserAplication.Names));

            CreateMap<Review, ReviewDto>()
                    .ForMember(d => d.NameProduct, m => m.MapFrom(o => o.Product.NameProduct))
                    .ForMember(d => d.NameUser, m => m.MapFrom(o => o.User.Names));

            CreateMap<ReviewDto, Review>();
            
            CreateMap<Comment, CommentDto>()
                    .ForMember(d => d.NameProduct, m => m.MapFrom(o => o.Product.NameProduct))
                    .ForMember(d => d.Status, m => m.MapFrom(o => o.Status == true ? 1 : 0))
                    .ForMember(d => d.NameUser, m => m.MapFrom(o => o.User.Names));

            CreateMap<CommentDto, Comment>();

            CreateMap<Product, ProductListDto>()
                    .ForMember(d => d.Status, m => m.MapFrom(o => o.Status ? 1 : 0))
                    .ForMember(d => d.NameBrand, m => m.MapFrom(o => o.Brand.NameBrand))
                    .ForMember(d => d.NameCategory, m => m.MapFrom(o => o.Category.NameCategory))
                    .ForMember(d => d.NameSubCategory, m => m.MapFrom(o => o.Subcategory.NameSubcategory))
                    .ForMember(d => d.Review, m => m.MapFrom(o =>
                        o.Reviews != null && o.Reviews.Any()
                            ? (decimal?)o.Reviews.Average(r => r.Rating)
                            : null))
                    .ForMember(d => d.Image, m => m.MapFrom(o =>
                        o.Images != null && o.Images.Any()
                            ? o.Images.First().UrlImage
                            : null));

            CreateMap<Product, ProductDetailsDto>()
                    .ForMember(d => d.Status, m => m.MapFrom(o => o.Status ? 1 : 0))
                    .ForMember(d => d.NameBrand, m => m.MapFrom(o => o.Brand.NameBrand))
                    .ForMember(d => d.NameCategory, m => m.MapFrom(o => o.Category.NameCategory))
                    .ForMember(d => d.NameSubCategory, m => m.MapFrom(o => o.Subcategory.NameSubcategory))
                    .ForMember(d => d.Review, m => m.MapFrom(o =>
                        o.Reviews != null && o.Reviews.Any()
                            ? (decimal?)o.Reviews.Average(r => r.Rating)
                            : null))
                    .ForMember(d => d.Images, m => m.MapFrom(o =>
                        o.Images != null && o.Images.Any()
                            ? o.Images.Select(i => i.UrlImage).ToList()
                            : new List<string>()))
                    .ForMember(d => d.Comments, m => m.MapFrom(o =>
                        o.Comments != null
                            ? o.Comments.Where(c => c.ParentCommentId == null && c.Status)
                            : new List<Comment>()));

            CreateMap<Comment, CommentResponseDto>()
                    .ForMember(dest => dest.NameUser, opt =>
                        opt.MapFrom(src => src.User.Names))
                    .ForMember(dest => dest.CommentsChild, opt =>
                        opt.MapFrom(src => src.CommentsChild.Where(c => c.Status)));

            CreateMap<Shipment, ShipmentDto>();

            CreateMap<ShipmentDto, Shipment>();

            CreateMap<ShippingAddress, ShippingAddressDto>()
                    .ForMember(d => d.State, m => m.MapFrom(o => o.State ? 1 : 0))
                    .ForMember(d => d.UserName, m => m.MapFrom(o => o.User.Names));
        }
    }
}
