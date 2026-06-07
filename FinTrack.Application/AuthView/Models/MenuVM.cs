using AutoMapper;
using FinTrack.Application.AuthView.Commands;
using FinTrack.Application.Common.Mappings;
using FinTrack.Application.Common.Models;
using FinTrack.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthView.Models
{
    public class MenuVM : IMapFrom<CreateMenuCommand>
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public ViewTypeEnum Type { get; set; }
        public bool IsMenuItem { get; set; }
        public bool Active { get; set; }
        public int DisplayOrder { get; set; }
        public string IconClass { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateMenuCommand, MenuVM>()
                  .ForMember(x => x.URL, d => d.MapFrom(m => m.URL != null ? m.URL : "#"))
                  .ForMember(x => x.IconClass, d => d.MapFrom(m => m.IconClass != null ? m.IconClass : "fa fa-th-large"));
        }
    }
}
