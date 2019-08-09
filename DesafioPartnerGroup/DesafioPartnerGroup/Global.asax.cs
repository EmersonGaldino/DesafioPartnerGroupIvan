using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DesafioPartnerGroup
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            /**
             * Usado para configurar a conexao do banco de dados inicial, o contexto de interfaces e servicos inicial
             * estes podem ser alterados em tempo de execucao caso o contexto necessite ser alterado
             */
            string ConnectionString = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString;
            string localservices = Server.MapPath("\\localservices.config");
            Ivan.Services.LocalServiceActivator.Configure(localservices);
            Ivan.SQL.Database.SetSettings("connection1", ConnectionString, "System.Data.SqlClient");
        }
    }
}
