using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Domain;
using ProjectManager.Core;
using ProjectManager.Model.Domain;

namespace ProjectManager.WPFClient
{
    public class MainWindowViewModel
    {
        private IServiceClient<IUsersService> usersService;

        public MainWindowViewModel(IServiceClient<IUsersService> usersService)
        {
            this.usersService = usersService;
            InitializeAsync();
        }

        public async Task InitializeAsync()
        {
            try
            {
                IAsyncServiceResult result = await usersService.TryAsync(x => x.SaveUser(new User { Name = "User 1", Password = "x", IsActive = true }));
                IAsyncServiceResult<User> userResult = await usersService.TryAsync(x => x.GetUser("User 1", "x"));
            }
            catch (Exception ex)
            {
                string y = ex.Message;
            }
        }
    }
}
