using AutoMapper;
using System;
using Contacts.Application.Common.Mappings;
using Contacts.Application.Contacts.Commands.UpdateContact;
using Contacts.Application.Contacts.Commands.ImportContacts;

namespace Contacts.WebApi.Models
{
    public class ImportContactsDto : IMapWith<ImportContactsCommand>
    {
        public List<UpdateContactDto> Contacts { get; set; } = null!;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ImportContactsDto, ImportContactsCommand>()
                .ForMember(noteCommand => noteCommand.Contacts,
                    opt => opt.MapFrom(noteDto => noteDto.Contacts)).ReverseMap();
        }
    }
}
