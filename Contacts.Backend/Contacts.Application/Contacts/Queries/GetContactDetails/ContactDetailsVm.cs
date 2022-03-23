using System;
using AutoMapper;
using Contacts.Application.Common.Mappings;
using Contacts.Domain;

namespace Contacts.Application.Contacts.Queries.GetContactDetails
{
    public class ContactDetailsVm : IMapWith<Contact>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public string Description { get; set; } = "";

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Contact, ContactDetailsVm>()
                .ForMember(contVm => contVm.Id,
                    opt => opt.MapFrom(cont => cont.Id))
                .ForMember(contVm => contVm.FirstName,
                    opt => opt.MapFrom(cont => cont.FirstName))
                .ForMember(contVm => contVm.LastName,
                    opt => opt.MapFrom(cont => cont.LastName))
                .ForMember(contVm => contVm.MiddleName,
                    opt => opt.MapFrom(cont => cont.MiddleName))
                .ForMember(contVm => contVm.Phone,
                    opt => opt.MapFrom(cont => cont.Phone))
                .ForMember(contVm => contVm.Email,
                    opt => opt.MapFrom(cont => cont.Email))
                .ForMember(contVm => contVm.Description,
                    opt => opt.MapFrom(cont => cont.Description))
                ;
        }
    }
}
