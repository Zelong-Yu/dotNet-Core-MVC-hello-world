using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HelloWorld
{
    public class Hello : IHello
    {
        public Hello()
        {
            //throw new Exception();
        }
        public string sayHello()
        {
            return "Hello From Class";
        }
    }
}