using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MyFirstApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			// Web API 設定和服務

			// Web API 路由
			config.MapHttpAttributeRoutes();
			config.Routes.MapHttpRoute(
				name: "DefaultApi2",
				routeTemplate: "api/{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional },
				constraints: new { action = @"^[a-zA-Z]+" } // 要加這個, 才不會跟下面的DefaultApi衝突
			);
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			// 設定 JSON 序列化使屬性名稱為駝峰式命名
			config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
		}
    }
}
