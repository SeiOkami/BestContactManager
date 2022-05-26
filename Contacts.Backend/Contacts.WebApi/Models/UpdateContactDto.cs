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
                .ForMember(command => command.Id,
                    opt => opt.MapFrom(dto => dto.Id))
                .ForMember(command => command.FirstName,
                    opt => opt.MapFrom(dto => dto.FirstName))
                .ForMember(command => command.LastName,
                    opt => opt.MapFrom(dto => dto.LastName))
                .ForMember(command => command.MiddleName,
                    opt => opt.MapFrom(dto => dto.MiddleName))
                .ForMember(command => command.Phone,
                    opt => opt.MapFrom(dto => dto.Phone))
                .ForMember(command => command.Email,
                    opt => opt.MapFrom(dto => dto.Email))
                .ForMember(command => command.Description,
                    opt => opt.MapFrom(dto => dto.Description));
        }
    }
}