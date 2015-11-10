using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PropertyManager.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(PropertyManager.Startup))]
namespace PropertyManager
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            ConfigureOAuth(app);
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);            
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                //AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthorizationServerProvider()               
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}