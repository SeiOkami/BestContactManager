using AutoMapper;
using Contacts.Application.Common.Mappings;
using Contacts.Application.Contacts.Commands.CreateContact;
using System.ComponentModel.DataAnnotations;

namespace Contacts.WebApi.Models
{
    public class CreateContactDto : IMapWith<CreateContactCommand>
    {
        [Required]
        public string FirstName { get; set; } = String.Empty;

        public string? LastName { get; set; } = "";
        
        public string? MiddleName { get; set; } = "";

        public string? Phone { get; set; } = "";

        public string? Email { get; set; } = "";

        public string? Description { get; set; } = "";

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateContactDto, CreateContactCommand>()
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