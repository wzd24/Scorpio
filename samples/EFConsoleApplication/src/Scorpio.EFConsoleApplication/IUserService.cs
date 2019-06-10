using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Scorpio.Domain.Repositories;

namespace Scorpio.EFConsoleApplication
{
    public interface IUserService:Application.Services.ICrudApplicationService<UserDto,int>
    {
        void Delete(Expression<Func<User, bool>> expression);
    }

    class UserService : Application.Services.CrudApplicationService<User, UserDto, int>, IUserService
    {
        public UserService(IServiceProvider serviceProvider, IRepository<User, int> repository) : base(serviceProvider, repository)
        {
        }

        public void Delete(Expression<Func<User, bool>> expression)
        {
            Repository.Delete(expression);
        }
    }
}
