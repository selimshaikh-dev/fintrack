using FinTrack.Application.Common.Models;
using FinTrack.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthView.Commands
{
    public class CreateMenuCommand : IRequest<Result>
    {
        public long Id { get; set; }
        public Nullable<long> ParentId { get; set; } = default(Nullable<long>);
        public string Title { get; set; }
        public string URL { get; set; }
        public ViewTypeEnum Type { get; set; }
        public bool IsMenuItem { get; set; }
        public bool Active { get; set; }
        public int DisplayOrder { get; set; }
        public string IconClass { get; set; }
        public CreateMenuCommand(long id, long? parentId, string title, string url, ViewTypeEnum type,
                                 bool isMenuItem, bool active, int displayOrder, string iconClass)
        {
            Id = id;
            ParentId = parentId;
            Title = title;
            URL = url;
            Type = type;
            IsMenuItem = isMenuItem;
            Active = active;
            DisplayOrder = displayOrder;
            IconClass = iconClass;    
        }
    }
}
