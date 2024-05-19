﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models.ViewModels;

namespace SportsStore.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            this.urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; }

        public PagingInfo? PageModel { get; set; }
        
        public string? PageAction { get; set; }

        public bool PageClassesEnabled { get; set; } = false;
        
        public string PageClass { get; set; } = String.Empty;
        
        public string PageClassNormal { get; set; } = String.Empty;
        
        public string PageClassSelected { get; set; } = String.Empty;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (this.ViewContext != null && this.PageModel != null)
            {
                IUrlHelper urlHelper = this.urlHelperFactory.GetUrlHelper(this.ViewContext);
                TagBuilder result = new TagBuilder("div");
                for (int i = 1; i <= this.PageModel.TotalPages; i++)
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.Attributes["href"] = urlHelper.Action(
                        this.PageAction,
                        new { productPage = i });
                    if (PageClassesEnabled)
                    {
                        tag.AddCssClass(PageClass);
                        tag.AddCssClass(i == PageModel.CurrentPage
                         ? PageClassSelected : PageClassNormal);
                    }
                    _ = tag.InnerHtml.Append(i.ToString());
                    result.InnerHtml.AppendHtml(tag);
                }

                output.Content.AppendHtml(result.InnerHtml);
            }
        }
    }
}
