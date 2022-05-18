using AutoMapper;
using System;
using Contacts.Application.Common.Mappings;
using Contacts.Application.Contacts.Commands.UpdateContact;
using Contacts.Application.Contacts.Commands.ImportContacts;

namespace Contacts.WebApi.Models
{
    public class ImportContactsDto : IMapWith<ImportContactsCommand>
    {
        public List<UpdateContactDto> Contacts { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ImportContactsDto, ImportContactsCommand>()
                .ForMember(noteCommand => noteCommand.Contacts,
                    opt => opt.MapFrom(noteDto => noteDto.Contacts)).ReverseMap();
                    //opt => opt.MapFrom< List < UpdateContactDto > , List < UpdateContactCommand >   >(dto => dto.contacts)).ReverseMap();
                //.AfterMap((src, dest) => //src is a list type
                //{
                //    foreach (var myrealclass in src.contacts)
                //        dest.Contacts.Add(new UpdateContactCommand()
                //        {
                //            Id = myrealclass.Id,
                //            FirstName = myrealclass.FirstName,
                //            LastName = myrealclass.LastName,
                //            MiddleName = myrealclass.MiddleName,
                //            Phone = myrealclass.Phone,
                //            Email = myrealclass.Email,
                //            Description = myrealclass.Description
                //        });
                //});
        }
    }
}
