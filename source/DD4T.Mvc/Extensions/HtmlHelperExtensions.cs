using DD4T.Core.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using DD4T.Mvc.Html;
using System.Linq.Expressions;
using System.Text;

namespace DD4T.Mvc.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString DisplayOrActionFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, List<IRenderableViewModel>>> expression)
        {
            StringBuilder result = new StringBuilder();
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            foreach (var model in metadata.Model as List<IRenderableViewModel>)
            {
                result.Append(htmlHelper.DisplayOrActionFor(m => model));
            }

            return new MvcHtmlString(result.ToString());
        }

        public static IHtmlString DisplayOrActionFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, IRenderableViewModel>> expression)
        {
            IHtmlString result;

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var model = metadata.Model as IRenderableViewModel;
            if (string.IsNullOrWhiteSpace(model.RenderData.Controller))
            {
                result = htmlHelper.DisplayFor(m => model, model.RenderData.View);
            }
            else
            {
                result = htmlHelper.Render(model);
            }

            return result;
        }
    }
}