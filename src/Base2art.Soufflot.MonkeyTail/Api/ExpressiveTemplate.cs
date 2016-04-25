// <copyright company="Base2art">
// Copyright (c) 2014 All Rights Reserved
// </copyright>
// <author>Scott Youngblut</author>

namespace Base2art.MonkeyTail.Api
{
    using System.Collections.Generic;
    using System.Linq;
    using Base2art.Soufflot.Api;
    
    public static class ExpressiveTemplate
    {
        public static IEnumerable<string> Render(//Children<TAppendable, TFormattable>(
//            this ExpressiveTemplateBase<TAppendable, TFormattable> template,
            this IEnumerable<PositionedResult> results)
//            where TAppendable : class, IAppendable<TAppendable>, new()
//            where TFormattable : IFormat<TAppendable>
        {
            return results.OrderBy(x => x.ContainerPriority).Select(x => x.Result.Content.BodyAsString);
        }
        
        public static IEnumerable<string> RenderBox(//Children<TAppendable, TFormattable>(
//            this ExpressiveTemplateBase<TAppendable, TFormattable> template,
            this IEnumerable<PositionedResult> results,
            BoxModelGuidePost guidePost)
//            where TAppendable : class, IAppendable<TAppendable>, new()
//            where TFormattable : IFormat<TAppendable>
        {
            return results.Render(guidePost.Value);
//            return template.RenderChildren(results.Where(x => x.Container == guidePost.Value));
        }
        
        public static IEnumerable<string> Render(//Children<TAppendable, TFormattable>(
//            this ExpressiveTemplateBase<TAppendable, TFormattable> template,
            this IEnumerable<PositionedResult> results,
            int containerId)
//            where TAppendable : class, IAppendable<TAppendable>, new()
//            where TFormattable : IFormat<TAppendable>
        {
            return results.Where(x => x.Container == containerId).Render();
//            return template.RenderChildren(results.Where(x => x.Container == containerId));
        }
    }
}
