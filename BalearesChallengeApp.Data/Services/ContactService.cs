using AutoMapper;
using AutoMapper.QueryableExtensions;
using BalearesChallengeApp.Models.DTOs;
using BalearesChallengeApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BalearesChallengeApp.Data.Services
{
    public class ContactService : ServiceBase, IContactService
    {
        public ContactService(BalearesDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<IEnumerable<ContactDto>> GetAll()
        {
            return await _context.Contact.ProjectTo<ContactDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<ContactDto> GetContactById(int id)
        {
            return await _context.Contact.ProjectTo<ContactDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ContactDto> CreateContact(ContactDto contactDto)
        {
            var contactEntity = _mapper.Map<Contact>(contactDto);
            contactEntity.CreatedOnUtc = DateTime.UtcNow;

            _context.Contact.Add(contactEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ContactDto>(contactEntity);
        }

        public async Task<bool> UpdateContact(ContactDto contactDto)
        {
            var contactEntity = await _context.Contact.FindAsync(contactDto.Id);

            if (contactEntity == null)
            {
                return false;
            }

            _mapper.Map(_mapper.Map<Contact>(contactDto), contactEntity);
            contactEntity.UpdatedOnUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteContact(int id)
        {
            var contactEntity = await _context.Contact.FindAsync(id);

            if (contactEntity == null)
            {
                return false;
            }

            contactEntity.IsDeleted = true;
            contactEntity.UpdatedOnUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ContactDto>> Search(int? provinceId, int? cityId)
        {
            var searchedContacts = await _context.Contact
                .Where(x => x.CityId == cityId || x.City.ProvinceId == provinceId)
                .ProjectTo<ContactDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return searchedContacts;
        }

        public async Task<bool> EmailOrPhoneExists(ContactDto contactDto)
        {
            return await _context.Contact.AnyAsync(c => c.Email == contactDto.Email || c.PhoneNumber == contactDto.PhoneNumber);
        }

        public async Task<ContactDto> Find(string email, string phoneNumber)
        {
            var foundContact = await _context.Contact
                .ProjectTo<ContactDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(c => c.Email == email || c.PhoneNumber == phoneNumber);

            return foundContact;
        }
    }
}
