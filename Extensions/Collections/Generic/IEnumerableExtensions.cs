using System.Linq.Expressions;

using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Specialized;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace System.Collections.Generic
{
    public static class IEnumerableExtensions
    {

        public static IEnumerable<SelectListItem> ToSelectListItems<T>(
            this IEnumerable<T> items,
            Func<T, string> nameSelector,
            Func<T, string> valueSelector
            )
        {
            return items.OrderBy(item => nameSelector(item))
                   .Select(item =>
                           new SelectListItem
                           {
                               Text = nameSelector(item),
                               Value = valueSelector(item)
                           });
        }

        public static MvcHtmlString ToUL<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);


            if (metaData.Model != null)
            {

                List<string> l = (List<string>)metaData.Model;



                TagBuilder ul = new TagBuilder("ul");

                foreach (string s in l)
                {
                    TagBuilder li = new TagBuilder("li");
                    li.SetInnerText(s);
                    ul.InnerHtml += li.ToString();
                }

                return MvcHtmlString.Create(ul.ToString());
            }

            return null;

        }

    }
}
