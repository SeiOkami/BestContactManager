using AutoMapper;
using System;
using Contacts.Application.Common.Mappings;
using Contacts.Application.Contacts.Commands.UpdateContact;

namespace Contacts.WebApi.Models
{
    public class UpdateContactDto : IMapWith<UpdateContactCommand>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = "";
        public string? LastName { get; set; } = "";

        public string? MiddleName { get; set; } = "";

        public string? Phone { get; set; } = "";

        public string? Email { get; set; } = "";

        public string? Description { get; set; } = "";

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateContactDto, UpdateContactCommand>()
                .ForMember(noteCommand => noteCommand.Id,
                    opt => opt.MapFrom(noteDto => noteDto.Id))
                .ForMember(noteCommand => noteCommand.FirstName,
                    opt => opt.MapFrom(noteDto => noteDto.FirstName))
                .ForMember(noteCommand => noteCommand.LastName,
                    opt => opt.MapFrom(noteDto => noteDto.LastName))
                .ForMember(noteCommand => noteCommand.MiddleName,
                    opt => opt.MapFrom(noteDto => noteDto.MiddleName))
                .ForMember(noteCommand => noteCommand.Phone,
                    opt => opt.MapFrom(noteDto => noteDto.Phone))
                .ForMember(noteCommand => noteCommand.Email,
                    opt => opt.MapFrom(noteDto => noteDto.Email))
                .ForMember(noteCommand => noteCommand.Description,
                    opt => opt.MapFrom(noteDto => noteDto.Description));
        }
    }
}