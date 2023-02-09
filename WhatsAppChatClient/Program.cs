using WhatsAppChatDAL;
using WhatsAppChatDAL.Model;

namespace WhatsAppChatClient
{
    internal class Program
    {
        static  async Task Main(string[] args)
        {
            Console.WriteLine("***************WhatsApp Database ************");

            using (IWhatsAppService whatsAppService  = new WhatsAppService(new WhatsAppDbContext()))
            {
                //var userData = new UserViewModel
                //{
                //    Name = "Amaka",
                //    Email = "amaka.disapoint@outlook.com",
                //    PhoneNumber = "09095002355",
                //    ProfilePhoto = "goat&noodles.jpg"
                //};

                //var createdUserId = await whatsAppService.CreateUser(userData);

                //Console.WriteLine(createdUserId);



                //var updateResult = await whatsAppService.UpdateUser(5, new UserViewModel()
                //{
                //    Name = "AmakaJesus",
                //    Email = "amaka.Blessed@outlook.com",
                //    PhoneNumber = "09095002666",
                //    ProfilePhoto = "goat&noodles.jpg"
                //});


                //if (updateResult)
                //{
                //    Console.WriteLine($"Successfully Updated");
                //}
                //else
                //{
                //    Console.WriteLine($"Not Successfully Updated");
                //}


                //var deleteUser = await whatsAppService.DeleteUser(5);

                //Console.WriteLine(deleteUser ? $"Successfully Deleted" : $"Not Successfully Deleted");



                //var user = await whatsAppService.GetUser(4);

                //Console.WriteLine($"Name : {user.Name} \t Email : {user.Email} \t Phone : {user.PhoneNumber} \t Photo : {user.ProfilePhoto}");


                var allUsers = await whatsAppService.GetUsers();

                foreach (var user in allUsers)
                {
                    Console.WriteLine($"Name : {user.Name} \t Email : {user.Email} \t Phone : {user.PhoneNumber} \t Photo : {user.ProfilePhoto}");
                }




            }
        }
    }
}