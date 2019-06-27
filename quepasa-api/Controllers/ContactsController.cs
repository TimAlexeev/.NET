using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using quepasa_api.Models;
using quepasa_api.Services;

namespace quepasa_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsService _contactsService;

        public ContactsController(ContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        // GET api/contacts
        [HttpGet(Name = "GetAllContacts")]
        public ActionResult<List<Contact>> Get()
        {
            return _contactsService.Get();
        }

        // GET api/contacts/5
        [HttpGet("{id:length(24)}", Name = "GetContact")]
        public ActionResult<Contact> Get(string id)
        {
            var contact = _contactsService.Get(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        [HttpPost]
        public ActionResult<Contact> Create (Contact newContact)
        {
            _contactsService.Create(newContact);

            return CreatedAtRoute("GetContact", new { id = newContact.Id.ToString() }, newContact);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Contact c)
        {
            var book = _contactsService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _contactsService.Update(id, c);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var contact = _contactsService.Get(id);

            if (contact == null)
            {
                return NotFound();
            }

            _contactsService.Remove(contact.Id);

            return NoContent();
        }

        [HttpGet("{sourceContactId}/{targetContactId}")]
        public IActionResult Associate([FromQuery()] string sourceContactId, [FromQuery()] string targetContactId)
        {
            _contactsService.AssociateOneToOne(sourceContactId, targetContactId);

            return NoContent();
        }
    }
}
