using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsAppChatDAL.Model;

namespace WhatsAppChatDAL
{
    public interface IWhatsAppService:IDisposable
    {

        Task<long> CreateUser(UserViewModel user);

        Task<bool> UpdateUser(int userId, UserViewModel user);

        Task<bool> DeleteUser(int UserId);

        Task<UserViewModel> GetUser(int id);

        Task<IEnumerable<UserViewModel>> GetUsers();
    }
}
