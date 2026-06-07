using AutoMapper;
using FinTrack.Application.Common.Mappings;
using FinTrack.Domain.Entities.Auth.AuthViews;
using FinTrack.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthView.Models
{
    public class MenuItemVM : IMapFrom<Menu>
    {
        public MenuItemVM()
        {
            Childs = new List<MenuItemVM>();
        }
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public bool IsMenuItem { get; set; }
        public int DisplayOrder { get; set; }
        public string IconClass { get; set; }
        public ViewTypeEnum Type { get; set; }
        public bool Active { get; set; }
        public List<MenuItemVM> Childs { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Menu, MenuItemVM>()
                .ForMember(x => x.Childs, opt => opt.Ignore());
        }
    }
}
