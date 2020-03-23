using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;


namespace EmployeeService
{
    public class BasicAuthentication : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else {
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                string encodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                string[] userPass = encodedAuthenticationToken.Split(':');
                string username = userPass[0];
                string password = userPass[1];

                if (EmployeeSecurity.Login(username, password))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                }
                else {

                    actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }


        
            }

        }
    }
}