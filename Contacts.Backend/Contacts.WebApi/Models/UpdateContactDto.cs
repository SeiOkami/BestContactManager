using AutoMapper;
using System;
using Contacts.Application.Common.Mappings;
using Contacts.Application.Contacts.Commands.UpdateContact;

namespace Contacts.WebApi.Models
{
    public class UpdateContactDto : IMapWith<UpdateContactCommand>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateContactDto, UpdateContactCommand>()
                .ForMember(noteCommand => noteCommand.Id,
                    opt => opt.MapFrom(noteDto => noteDto.Id))
                .ForMember(noteCommand => noteCommand.FirstName,
                    opt => opt.MapFrom(noteDto => noteDto.FirstName));
        }
    }
}