﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Notifyd.Portal.Extensions
{
public static class HtmlExtensions
{
    public static MvcHtmlString ActionButton(this HtmlHelper html, string linkText, string action, string controllerName, string iconClass, string value = null)
    {
        //<a href="/@lLink.ControllerName/@lLink.ActionName" title="@lLink.LinkText"><i class="@lLink.IconClass"></i><span class="">@lLink.LinkText</span></a>
        var lURL = new UrlHelper(html.ViewContext.RequestContext);

        // build the <span class="">@lLink.LinkText</span> tag
        var lSpanBuilder = new TagBuilder("span");
        lSpanBuilder.MergeAttribute("class", "");
        lSpanBuilder.SetInnerText(linkText);
        string lSpanHtml = lSpanBuilder.ToString(TagRenderMode.Normal);

        // build the <i class="@lLink.IconClass"></i> tag
        var lIconBuilder = new TagBuilder("i");
        lIconBuilder.MergeAttribute("class", iconClass);
        string lIconHtml = lIconBuilder.ToString(TagRenderMode.Normal);

        // build the <a href="@lLink.ControllerName/@lLink.ActionName" title="@lLink.LinkText">...</a> tag
        var lAnchorBuilder = new TagBuilder("a");

        if (string.IsNullOrEmpty(value))
        {
            lAnchorBuilder.MergeAttribute("href", lURL.Action(action, controllerName));
        }
        else
        {
            lAnchorBuilder.MergeAttribute("href", lURL.Action(action, controllerName, new { id = value }));
        }
    
        lAnchorBuilder.InnerHtml = lIconHtml + lSpanHtml; // include the <i> and <span> tags inside
        string lAnchorHtml = lAnchorBuilder.ToString(TagRenderMode.Normal);

        return MvcHtmlString.Create(lAnchorHtml);
    }
}
}


