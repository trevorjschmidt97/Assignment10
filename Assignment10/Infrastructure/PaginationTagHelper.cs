﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Assignment10.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-info")]

    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory urlInfo;

        public PaginationTagHelper(IUrlHelperFactory uhf)
        {
            urlInfo = uhf;
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public PageNumberingInfo PageInfo { get; set; }

        public string TeamName { get; set; }

        // Our own dictionary that we're creating
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> KeyValuePairs { get; set; } = new Dictionary<string, object>();

        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlInfo.GetUrlHelper(ViewContext);

            TagBuilder finishedTag = new TagBuilder("div");

            for (int i = 1; i <= PageInfo.NumPages; i++)
            {
                TagBuilder individualTag = new TagBuilder("a");

                KeyValuePairs["pageNum"] = i;

                individualTag.Attributes["href"] = urlHelper.Action("Index", KeyValuePairs);


                if (PageClassesEnabled)
                {
                    individualTag.AddCssClass(PageClass);
                    individualTag.AddCssClass(i == PageInfo.CurrentPage
                        ? PageClassSelected : PageClassNormal);
                }

                individualTag.InnerHtml.Append(i.ToString());
                finishedTag.InnerHtml.AppendHtml(individualTag);
            }

            

            

            output.Content.AppendHtml(finishedTag.InnerHtml);
        }
    }
}
