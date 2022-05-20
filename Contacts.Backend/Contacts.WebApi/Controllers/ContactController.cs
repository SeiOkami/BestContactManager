using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Contacts.WebApi.Models;
using Contacts.Application.Contacts.Queries.GetContactList;
using Contacts.Application.Contacts.Queries.GetContactDetails;
using Contacts.Application.Contacts.Commands.CreateContact;
using Contacts.Application.Contacts.Commands.UpdateContact;
using Contacts.Application.Contacts.Commands.DeleteContact;
using Contacts.Application.Contacts.Commands.ClearContacts;
using Contacts.Application.Contacts.Commands.GenerateContacts;
using Contacts.Application.Contacts.Commands.ImportContacts;

namespace Contacts.WebApi.Controllers
{

    [ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ContactController : BaseController
    {
        private readonly IMapper _mapper;

        public ContactController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Gets the list of contacts
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /contact
        /// </remarks>
        /// <returns>
        /// Returns ContactListVm
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ContactListVm>> GetAll()
        {
            var query = new GetContactListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the contact by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /contact/D34D349E-43B8-429E-BCA4-793C932FD580
        /// </remarks>
        /// <param name="id">Contact id (guid)</param>
        /// <returns>Returns ContactDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user in unauthorized</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ContactDetailsVm>> Get(Guid id)
        {
            var query = new GetContactDetailsQuery
            {
                UserId = UserId,
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the contact
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /contact
        /// {
        ///     title: "contact title",
        ///     details: "contact details"
        /// }
        /// </remarks>
        /// <param name="createContactDto">CreateContactDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateContactDto createContactDto)
        {
            var command = _mapper.Map<CreateContactCommand>(createContactDto);
            command.UserId = UserId;
            var contactId = await Mediator.Send(command);
            return Ok(contactId);
        }

        /// <summary>
        /// Updates the contact
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /contact
        /// {
        ///     title: "updated contact title"
        /// }
        /// </remarks>
        /// <param name="updateContactDto">UpdateContactDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UpdateContactDto updateContactDto)
        {
            var command = _mapper.Map<UpdateContactCommand>(updateContactDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes the contact by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /contact/88DEB432-062F-43DE-8DCD-8B6EF79073D3
        /// </remarks>
        /// <param name="id">Id of the contact (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteContactCommand
            {
                Id = id,
                UserId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }


        /// <summary>
        /// Clear all contacts
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /contact/clear
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete("clear")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Clear()
        {
            var command = new ClearContactsCommand
            {
                UserId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }


        /// <summary>
        /// Generate test contacts 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /contact/clear
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost("generate")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Generate()
        {
            var command = new GenerateContactsCommand
            {
                UserId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }



        /// <summary>
        /// Import contacts from file
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /contact/import
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost("import")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Import(ImportContactsDto model)
        {
            var command = _mapper.Map<ImportContactsCommand>(model);
            command.UserId = UserId;
            command.Mediator = Mediator;
            await Mediator.Send(command);

            return NoContent();
        }

    }
}
