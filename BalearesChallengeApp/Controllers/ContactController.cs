using BalearesChallengeApp.Models.DTOs;
using BalearesChallengeApp.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BalearesChallengeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        /// <summary>
        /// Retrieves all contacts
        /// </summary>
        /// <remarks>
        /// Sample response:
        ///     
        ///     [
        ///         {
        ///             "id": 1,
        ///             "name": "Lucas",
        ///             "company": "TestCompany",
        ///             "email": "lucas@testcompany.com",
        ///             "dateOfBirth": "1992-02-14",
        ///             "phoneNumber": "01112345678",
        ///             "address": "Calle falsa 123",
        ///             "cityId": 1,
        ///             "cityName": "Villa Luzuriaga",
        ///             "provinceName": "Buenos Aires"
        ///         },
        ///         {
        ///           "id": 2,
        ///           "name": "Name",
        ///           "company": "Company",
        ///           "email": "name@email.net",
        ///           "dateOfBirth": "1993-10-10",
        ///           "phoneNumber": "1112345678",
        ///           "address": "Calle 123",
        ///           "cityId": 5,
        ///           "cityName": "Cosquin",
        ///           "provinceName": "Cordoba"
        ///         }
        ///     ]
        /// 
        /// </remarks>
        /// <response code="200">List of contacts</response>
        // GET: api/<ContactController>
        [HttpGet]
        public async Task<IEnumerable<ContactDto>> Get()
        {
            var contacts = await _contactService.GetAll();

            return contacts;
        }

        /// <summary>
        /// Retrieves a specific contact by unique id
        /// </summary>
        /// <remarks>
        /// Sample response:
        /// 
        ///     {
        ///         "id": 1,
        ///         "name": "Lucas",
        ///         "company": "TestCompany",
        ///         "email": "lucas@testcompany.com",
        ///         "dateOfBirth": "1992-02-14",
        ///         "phoneNumber": "01112345678",
        ///         "address": "Calle falsa 123",
        ///         "cityId": 1,
        ///         "cityName": "Villa Luzuriaga",
        ///         "provinceName": "Buenos Aires"
        ///     }
        ///         
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="200">Contact</response>
        /// <response code="404">Contact not found</response>
        // GET api/<ContactController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDto>> GetById(int id)
        {
            var contactDto = await _contactService.GetContactById(id);

            if (contactDto == null)
            {
                return NotFound();
            }

            return contactDto;
        }

        /// <summary>
        /// Creates a contact
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST 
        ///     {
        ///       "name": "Lucas",
        ///       "company": "TestCompany",
        ///       "email": "lucas@testcompany.com",
        ///       "dateOfBirth": "1992-02-14",
        ///       "phoneNumber": "01112345678",
        ///       "address": "Calle falsa 123",
        ///       "cityId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="contactDto"></param>
        /// <response code="201">Contact created</response>
        /// <response code="409">Conflict, contact already exist</response>
        // POST api/<ContactController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactDto contactDto)
        {
            var emailOrPhoneExists = await _contactService.EmailOrPhoneExists(contactDto);
            if (emailOrPhoneExists)
            {
                return Conflict("Email or Phone Number already in use!");
            }

            var contactCreated = await _contactService.CreateContact(contactDto);

            return CreatedAtAction(nameof(GetById), new { id = contactCreated.Id }, contactCreated);
        }

        /// <summary>
        /// Updates a contact
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     PUT 
        ///     {
        ///       "name": "Lucas",
        ///       "company": "TestCompany",
        ///       "email": "lucas@testcompany.com",
        ///       "dateOfBirth": "1992-02-14",
        ///       "phoneNumber": "01112345678",
        ///       "address": "Calle falsa 123",
        ///       "cityId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="contactDto"></param>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad request</response>
        // PUT api/<ContactController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ContactDto contactDto)
        {
            if (id != contactDto.Id)
            {
                return BadRequest();
            }

            var contactExists = await _contactService.UpdateContact(contactDto);

            return contactExists ? NoContent() : NotFound();
        }

        /// <summary>
        /// Deletes a contact
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">No Content</response>
        /// <response code="404">Not Found</response>
        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contactExists = await _contactService.DeleteContact(id);

            return contactExists ? NoContent() : NotFound();
        }

        /// <summary>
        /// Finds a contact by email or phone
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /actions/find?email=lucas@testcompany.com
        /// 
        /// Sample response:
        /// 
        ///     {
        ///         "id": 1,
        ///         "name": "Lucas",
        ///         "company": "TestCompany",
        ///         "email": "lucas@testcompany.com",
        ///         "dateOfBirth": "1992-02-14",
        ///         "phoneNumber": "01112345678",
        ///         "address": "Calle falsa 123",
        ///         "cityId": 1,
        ///         "cityName": "Villa Luzuriaga",
        ///         "provinceName": "Buenos Aires"
        ///     }
        /// 
        /// </remarks>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <response code="200">Contact found</response>
        // GET: api/<ContactController>/actions/find
        [HttpGet("actions/find")]
        public async Task<ContactDto> Find(string email, string phone)
        {
            var contact = await _contactService.Find(email, phone);

            return contact;
        }

        /// <summary>
        /// Search contacts by province or city
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /actions/search?provinceId=1
        ///     
        /// Sample response:
        /// 
        ///     [
        ///         {
        ///             "id": 1,
        ///             "name": "Lucas",
        ///             "company": "TestCompany",
        ///             "email": "lucas@testcompany.com",
        ///             "dateOfBirth": "1992-02-14",
        ///             "phoneNumber": "01112345678",
        ///             "address": "Calle falsa 123",
        ///             "cityId": 1,
        ///             "cityName": "Villa Luzuriaga",
        ///             "provinceName": "Buenos Aires"
        ///         },
        ///         {
        ///           "id": 3,
        ///           "name": "NameTest",
        ///           "company": "Comp",
        ///           "email": "user@testemail.com",
        ///           "dateOfBirth": "1995-10-18",
        ///           "phoneNumber": "879456123",
        ///           "address": "Cale 321",
        ///           "cityId": 2,
        ///           "cityName": "La Plata",
        ///           "provinceName": "Buenos Aires"
        ///         }
        ///     ]
        /// 
        /// </remarks>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <response code="200">List of matched contacts</response>
        // GET: api/<ContactController>/actions/search
        [HttpGet("actions/search")]
        public async Task<IEnumerable<ContactDto>> Search(int? provinceId, int? cityId)
        {
            var contacts = await _contactService.Search(provinceId, cityId);

            return contacts;
        }
    }
}
