﻿using AutoMapper;
using Contacts.Application.Common.Mappings;
using Contacts.Domain;

namespace Contacts.Application.Contacts.Queries.GetContactList
{
    public class ContactLookupDto : IMapWith<Contact>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Contact, ContactLookupDto>()
                .ForMember(contDto => contDto.Id,
                    opt => opt.MapFrom(cont => cont.Id))
                .ForMember(contDto => contDto.FirstName,
                    opt => opt.MapFrom(cont => cont.FirstName))
                .ForMember(contDto => contDto.LastName,
                    opt => opt.MapFrom(cont => cont.LastName))
                .ForMember(contDto => contDto.MiddleName,
                    opt => opt.MapFrom(cont => cont.MiddleName))
                .ForMember(contDto => contDto.Phone,
                    opt => opt.MapFrom(cont => cont.Phone))
                .ForMember(contDto => contDto.Email,
                    opt => opt.MapFrom(cont => cont.Email))
                ;
        }
    }
}
