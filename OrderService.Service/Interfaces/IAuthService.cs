using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Service.Interfaces;

public interface IAuthService
{
    ValueTask<string> GenerateTokenAsync(string login, string password);
}
