using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TanteBazar.Core.DataServices;

namespace TanteBazar.WebApi.Middlewares
{
    public class AuthorisationMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger _logger;
        private ICustomerDataService _customerDataService;
        private const string API_SECRET_KEY = "X_API_SECRET";

        public AuthorisationMiddleware(
            RequestDelegate requestDelegate, 
            ILogger logger, 
            ICustomerDataService customerDataService)
        {
            _next = requestDelegate ?? throw new ArgumentNullException(nameof(requestDelegate));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _customerDataService = customerDataService ?? throw new ArgumentNullException(nameof(customerDataService));
        }

        public async Task Invoke(HttpContext context)
        {
            var headers = context.Request.Headers;
            var apiSecret = string.Empty;

            if (!headers.ContainsKey(API_SECRET_KEY))
            {
                _logger
                    .ForContext<AuthorisationMiddleware>()
                    .Warning("API secret not specified");
            }

            apiSecret = headers[API_SECRET_KEY];

            if (await _customerDataService.IsUserValid(apiSecret))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, apiSecret)
                };

                context.User.AddIdentity(new ClaimsIdentity(claims));
            }
            else
            {
                _logger
                  .ForContext<AuthorisationMiddleware>()
                  .Warning("API secret was not found in the DB.");
            }



            await _next(context);

        }
    }
}
