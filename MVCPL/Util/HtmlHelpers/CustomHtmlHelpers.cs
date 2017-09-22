using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVCPL.Util.HtmlHelpers
{
    //TODO: Refactor
    public static class CustomHtmlHelpers
    {
        public static IHtmlString CustomImage(this HtmlHelper htmlHelper, string image, string alt, int width, int height)
        {
            var sb = new StringBuilder();
            var src = image != null ? $"data:image/jpeg;base64,{image}" : "../../img/no-image.jpg";
            sb.AppendFormat("<img alt={0} width={1} height={2} src={3} ", alt, width, height, src);
            sb.AppendLine(">");

            return MvcHtmlString.Create(sb.ToString());
        }

        public static IHtmlString CustomImageActionLink(this HtmlHelper htmlHelper, int userId, string image, string alt, int width, int height, 
            string tooltip, string action, string controller, object routeValues)
        {
            //TODO: Refactor
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var url = urlHelper.Action(action, controller, routeValues);
            var sb = new StringBuilder();
            //sb.AppendFormat("<a onclick=rewardDetails({0}) href={1}>", userId, url);
            sb.AppendFormat("<a onclick=rewardDetails({0}) href=#>", userId, url);
            var src = image != null ? $"data:image/jpeg;base64,{image}" : "../../img/no-image.jpg";
            var dataToggle = "tooltip";
            sb.AppendFormat("<img alt={0} data-toggle={1} title={2}  width={3} height={4} src={5} ", alt, dataToggle, tooltip, width, height, src);
            sb.AppendLine(">");
            sb.Append("</a>");

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}