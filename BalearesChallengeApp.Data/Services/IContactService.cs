using BalearesChallengeApp.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BalearesChallengeApp.Data.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDto>> GetAll();
        Task<ContactDto> CreateContact(ContactDto contactDto);
        Task<ContactDto> GetContactById(int id);
        Task<bool> UpdateContact(ContactDto contactDto);
        Task<IEnumerable<ContactDto>> Search(int? provinceId, int? cityId);
        Task<ContactDto> Find(string email, string phoneNumber);
        Task<bool> DeleteContact(int id);
        Task<bool> EmailOrPhoneExists(ContactDto contactDto);
    }
}