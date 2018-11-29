using EasyLife.Domain.Models;
using EasyLife.Domain.ViewModels;

namespace EasyLife.Web.Client.Profile
{
	public class EasyLifeProfile : AutoMapper.Profile
	{
		public EasyLifeProfile()
		{
			CreateMap<Advertisement, AdvertisementViewModel>();
			CreateMap<Service, ServiceViewModel>();
		}
	}
}
