namespace Base2art.Soufflot.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Http.Util;

    public class AssetsController : IRenderingRouted
    {
        IMimeMapping mapper;

        public AssetsController()
            : this(null)
        {
        }
        
        public AssetsController(IMimeMapping mimeMapper)
        {
            this.mapper = mimeMapper ?? new MimeMapping();
        }
        
        private readonly IPositionedRenderingRouted[] positionedRenderingRoutedItems = new IPositionedRenderingRouted[0];

        private readonly INonRenderingRouted[] nonRenderingRoutedItems = new INonRenderingRouted[0];

        public IPositionedRenderingRouted[] RenderingRoutedItems
        {
            get { return this.positionedRenderingRoutedItems; }
        }

        public INonRenderingRouted[] NonRenderingRoutedItems
        {
            get { return this.nonRenderingRoutedItems; }
        }

        public IResult Execute(IHttpContext httpContext, List<PositionedResult> childResults)
        {
            return httpContext.NoContent();
        }

        public IResult At(IHttpContext httpContext, List<PositionedResult> childResults, string diskFolder, string relativeFileName)
        {
            var result = this.NormalizePath(relativeFileName);
            if (string.IsNullOrWhiteSpace(result))
            {
                return httpContext.NotFound(new SimpleContent
                {
                    BodyContent = "File Not Found",
                    ContentType = "text/plain"
                });
            }
            
            string path = Path.Combine(diskFolder, result);
            if (!File.Exists(path))
            {
                return httpContext.NotFound(new SimpleContent
                {
                    BodyContent = string.Format("File '{0}' Not Found", result),
                    ContentType = "text/plain"
                });
            }
            
            return httpContext.Ok(new SimpleContent
            {
                Body = File.ReadAllBytes(path),
                ContentType = this.mapper.GetMimeMapping(path)
            });
        }

        public string NormalizePath(string path)
        {
            var parts = new LinkedList<string>();
            var dirName = path;
            var currentFName = Path.GetFileName(dirName);
            
            while (!string.IsNullOrWhiteSpace(dirName))
            {
                parts.AddLast(currentFName);
                dirName = Path.GetDirectoryName(dirName);
                currentFName = Path.GetFileName(dirName);
            }
            
            Stack<string> clean1 = new Stack<string>();
            
            return Process(parts, clean1)
                ? Path.Combine(clean1.ToArray())
                : null;
        }

        private static bool Process(LinkedList<string> parts, Stack<string> clean, int i = 0)
        {
            if (parts.Count == 0)
            {
                return i <= 0;
            }
            
            var part = parts.First.Value;
            
            if (part == ".")
            {
                parts.RemoveFirst();
                return Process(parts, clean, i);
            }
            
            if (part == "..")
            {
                parts.RemoveFirst();
                return Process(parts, clean, i + 1);
            }
            
            if (i > 0)
            {
                // SWALLOW!
                parts.RemoveFirst();
                
                return Process(parts, clean, i - 1);
            }
            
            parts.RemoveFirst();
            clean.Push(part);
            
            return Process(parts, clean, i);
        }
    }
}
