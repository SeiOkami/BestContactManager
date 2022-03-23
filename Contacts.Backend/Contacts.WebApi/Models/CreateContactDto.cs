using AutoMapper;
using Contacts.Application.Common.Mappings;
using Contacts.Application.Contacts.Commands.CreateContact;
using System.ComponentModel.DataAnnotations;

namespace Contacts.WebApi.Models
{
    public class CreateContactDto : IMapWith<CreateContactCommand>
    {
        [Required]
        public string FirstName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateContactDto, CreateContactCommand>()
                .ForMember(noteCommand => noteCommand.FirstName,
                    opt => opt.MapFrom(noteDto => noteDto.FirstName));
        }
    }
}