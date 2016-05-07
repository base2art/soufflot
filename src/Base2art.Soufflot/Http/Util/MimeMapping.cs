namespace Base2art.Soufflot.Http.Util
{
    using System;
    using System.Collections.Generic;

    public class MimeMapping : IMimeMapping
    {
        private readonly IDictionary<string, string> mapping = new Dictionary<string, string>();

        private readonly ILazy<bool> ensurer;

        public MimeMapping()
        {
            this.ensurer = new RetryLazy<bool>(() =>
            {
                                             this.Populate();
                                             return true;
                                         });
        }
        
        protected virtual string NoContentType
        {
            get { return "text/plain"; }
        }
        
        protected virtual string DefaultContentType
        {
            get { return "application/octet-stream"; }
        }
        
        public string GetMimeMapping(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return this.NoContentType;
            }
            
            var ext = System.IO.Path.GetExtension(fileName);
            if (string.IsNullOrWhiteSpace(ext))
            {
                return this.DefaultContentType;
            }
            
            this.EnsurePopulation();
            
            return this.mapping.ContainsKey(ext) ? this.mapping[ext] : this.DefaultContentType;
        }

        protected virtual void Populate()
        {
            this.RegisterMapping(".323", "text/h323");
            this.RegisterMapping(".aaf", "application/octet-stream");
            this.RegisterMapping(".aca", "application/octet-stream");
            this.RegisterMapping(".accdb", "application/msaccess");
            this.RegisterMapping(".accde", "application/msaccess");
            this.RegisterMapping(".accdt", "application/msaccess");
            this.RegisterMapping(".acx", "application/internet-property-stream");
            this.RegisterMapping(".afm", "application/octet-stream");
            this.RegisterMapping(".ai", "application/postscript");
            this.RegisterMapping(".aif", "audio/x-aiff");
            this.RegisterMapping(".aifc", "audio/aiff");
            this.RegisterMapping(".aiff", "audio/aiff");
            this.RegisterMapping(".application", "application/x-ms-application");
            this.RegisterMapping(".art", "image/x-jg");
            this.RegisterMapping(".asd", "application/octet-stream");
            this.RegisterMapping(".asf", "video/x-ms-asf");
            this.RegisterMapping(".asi", "application/octet-stream");
            this.RegisterMapping(".asm", "text/plain");
            this.RegisterMapping(".asr", "video/x-ms-asf");
            this.RegisterMapping(".asx", "video/x-ms-asf");
            this.RegisterMapping(".atom", "application/atom+xml");
            this.RegisterMapping(".au", "audio/basic");
            this.RegisterMapping(".avi", "video/x-msvideo");
            this.RegisterMapping(".axs", "application/olescript");
            this.RegisterMapping(".bas", "text/plain");
            this.RegisterMapping(".bcpio", "application/x-bcpio");
            this.RegisterMapping(".bin", "application/octet-stream");
            this.RegisterMapping(".bmp", "image/bmp");
            this.RegisterMapping(".c", "text/plain");
            this.RegisterMapping(".cab", "application/octet-stream");
            this.RegisterMapping(".calx", "application/vnd.ms-office.calx");
            this.RegisterMapping(".cat", "application/vnd.ms-pki.seccat");
            this.RegisterMapping(".cdf", "application/x-cdf");
            this.RegisterMapping(".chm", "application/octet-stream");
            this.RegisterMapping(".class", "application/x-java-applet");
            this.RegisterMapping(".clp", "application/x-msclip");
            this.RegisterMapping(".cmx", "image/x-cmx");
            this.RegisterMapping(".cnf", "text/plain");
            this.RegisterMapping(".cod", "image/cis-cod");
            this.RegisterMapping(".cpio", "application/x-cpio");
            this.RegisterMapping(".cpp", "text/plain");
            this.RegisterMapping(".crd", "application/x-mscardfile");
            this.RegisterMapping(".crl", "application/pkix-crl");
            this.RegisterMapping(".crt", "application/x-x509-ca-cert");
            this.RegisterMapping(".csh", "application/x-csh");
            this.RegisterMapping(".css", "text/css");
            this.RegisterMapping(".csv", "application/octet-stream");
            this.RegisterMapping(".cur", "application/octet-stream");
            this.RegisterMapping(".dcr", "application/x-director");
            this.RegisterMapping(".deploy", "application/octet-stream");
            this.RegisterMapping(".der", "application/x-x509-ca-cert");
            this.RegisterMapping(".dib", "image/bmp");
            this.RegisterMapping(".dir", "application/x-director");
            this.RegisterMapping(".disco", "text/xml");
            this.RegisterMapping(".dll", "application/x-msdownload");
            this.RegisterMapping(".dll.config", "text/xml");
            this.RegisterMapping(".dlm", "text/dlm");
            this.RegisterMapping(".doc", "application/msword");
            this.RegisterMapping(".docm", "application/vnd.ms-word.document.macroEnabled.12");
            this.RegisterMapping(".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            this.RegisterMapping(".dot", "application/msword");
            this.RegisterMapping(".dotm", "application/vnd.ms-word.template.macroEnabled.12");
            this.RegisterMapping(".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template");
            this.RegisterMapping(".dsp", "application/octet-stream");
            this.RegisterMapping(".dtd", "text/xml");
            this.RegisterMapping(".dvi", "application/x-dvi");
            this.RegisterMapping(".dwf", "drawing/x-dwf");
            this.RegisterMapping(".dwp", "application/octet-stream");
            this.RegisterMapping(".dxr", "application/x-director");
            this.RegisterMapping(".eml", "message/rfc822");
            this.RegisterMapping(".emz", "application/octet-stream");
            this.RegisterMapping(".eot", "application/octet-stream");
            this.RegisterMapping(".eps", "application/postscript");
            this.RegisterMapping(".etx", "text/x-setext");
            this.RegisterMapping(".evy", "application/envoy");
            this.RegisterMapping(".exe", "application/octet-stream");
            this.RegisterMapping(".exe.config", "text/xml");
            this.RegisterMapping(".fdf", "application/vnd.fdf");
            this.RegisterMapping(".fif", "application/fractals");
            this.RegisterMapping(".fla", "application/octet-stream");
            this.RegisterMapping(".flr", "x-world/x-vrml");
            this.RegisterMapping(".flv", "video/x-flv");
            this.RegisterMapping(".gif", "image/gif");
            this.RegisterMapping(".gtar", "application/x-gtar");
            this.RegisterMapping(".gz", "application/x-gzip");
            this.RegisterMapping(".h", "text/plain");
            this.RegisterMapping(".hdf", "application/x-hdf");
            this.RegisterMapping(".hdml", "text/x-hdml");
            this.RegisterMapping(".hhc", "application/x-oleobject");
            this.RegisterMapping(".hhk", "application/octet-stream");
            this.RegisterMapping(".hhp", "application/octet-stream");
            this.RegisterMapping(".hlp", "application/winhlp");
            this.RegisterMapping(".hqx", "application/mac-binhex40");
            this.RegisterMapping(".hta", "application/hta");
            this.RegisterMapping(".htc", "text/x-component");
            this.RegisterMapping(".htm", "text/html");
            this.RegisterMapping(".html", "text/html");
            this.RegisterMapping(".htt", "text/webviewhtml");
            this.RegisterMapping(".hxt", "text/html");
            this.RegisterMapping(".ico", "image/x-icon");
            this.RegisterMapping(".ics", "application/octet-stream");
            this.RegisterMapping(".ief", "image/ief");
            this.RegisterMapping(".iii", "application/x-iphone");
            this.RegisterMapping(".inf", "application/octet-stream");
            this.RegisterMapping(".ins", "application/x-internet-signup");
            this.RegisterMapping(".isp", "application/x-internet-signup");
            this.RegisterMapping(".IVF", "video/x-ivf");
            this.RegisterMapping(".jar", "application/java-archive");
            this.RegisterMapping(".java", "application/octet-stream");
            this.RegisterMapping(".jck", "application/liquidmotion");
            this.RegisterMapping(".jcz", "application/liquidmotion");
            this.RegisterMapping(".jfif", "image/pjpeg");
            this.RegisterMapping(".jpb", "application/octet-stream");
            this.RegisterMapping(".jpe", "image/jpeg");
            this.RegisterMapping(".jpeg", "image/jpeg");
            this.RegisterMapping(".jpg", "image/jpeg");
            this.RegisterMapping(".js", "application/x-javascript");
            this.RegisterMapping(".jsx", "text/jscript");
            this.RegisterMapping(".latex", "application/x-latex");
            this.RegisterMapping(".lit", "application/x-ms-reader");
            this.RegisterMapping(".lpk", "application/octet-stream");
            this.RegisterMapping(".lsf", "video/x-la-asf");
            this.RegisterMapping(".lsx", "video/x-la-asf");
            this.RegisterMapping(".lzh", "application/octet-stream");
            this.RegisterMapping(".m13", "application/x-msmediaview");
            this.RegisterMapping(".m14", "application/x-msmediaview");
            this.RegisterMapping(".m1v", "video/mpeg");
            this.RegisterMapping(".m3u", "audio/x-mpegurl");
            this.RegisterMapping(".man", "application/x-troff-man");
            this.RegisterMapping(".manifest", "application/x-ms-manifest");
            this.RegisterMapping(".map", "text/plain");
            this.RegisterMapping(".mdb", "application/x-msaccess");
            this.RegisterMapping(".mdp", "application/octet-stream");
            this.RegisterMapping(".me", "application/x-troff-me");
            this.RegisterMapping(".mht", "message/rfc822");
            this.RegisterMapping(".mhtml", "message/rfc822");
            this.RegisterMapping(".mid", "audio/mid");
            this.RegisterMapping(".midi", "audio/mid");
            this.RegisterMapping(".mix", "application/octet-stream");
            this.RegisterMapping(".mmf", "application/x-smaf");
            this.RegisterMapping(".mno", "text/xml");
            this.RegisterMapping(".mny", "application/x-msmoney");
            this.RegisterMapping(".mov", "video/quicktime");
            this.RegisterMapping(".movie", "video/x-sgi-movie");
            this.RegisterMapping(".mp2", "video/mpeg");
            this.RegisterMapping(".mp3", "audio/mpeg");
            this.RegisterMapping(".mpa", "video/mpeg");
            this.RegisterMapping(".mpe", "video/mpeg");
            this.RegisterMapping(".mpeg", "video/mpeg");
            this.RegisterMapping(".mpg", "video/mpeg");
            this.RegisterMapping(".mpp", "application/vnd.ms-project");
            this.RegisterMapping(".mpv2", "video/mpeg");
            this.RegisterMapping(".ms", "application/x-troff-ms");
            this.RegisterMapping(".msi", "application/octet-stream");
            this.RegisterMapping(".mso", "application/octet-stream");
            this.RegisterMapping(".mvb", "application/x-msmediaview");
            this.RegisterMapping(".mvc", "application/x-miva-compiled");
            this.RegisterMapping(".nc", "application/x-netcdf");
            this.RegisterMapping(".nsc", "video/x-ms-asf");
            this.RegisterMapping(".nws", "message/rfc822");
            this.RegisterMapping(".ocx", "application/octet-stream");
            this.RegisterMapping(".oda", "application/oda");
            this.RegisterMapping(".odc", "text/x-ms-odc");
            this.RegisterMapping(".ods", "application/oleobject");
            this.RegisterMapping(".one", "application/onenote");
            this.RegisterMapping(".onea", "application/onenote");
            this.RegisterMapping(".onetoc", "application/onenote");
            this.RegisterMapping(".onetoc2", "application/onenote");
            this.RegisterMapping(".onetmp", "application/onenote");
            this.RegisterMapping(".onepkg", "application/onenote");
            this.RegisterMapping(".osdx", "application/opensearchdescription+xml");
            this.RegisterMapping(".p10", "application/pkcs10");
            this.RegisterMapping(".p12", "application/x-pkcs12");
            this.RegisterMapping(".p7b", "application/x-pkcs7-certificates");
            this.RegisterMapping(".p7c", "application/pkcs7-mime");
            this.RegisterMapping(".p7m", "application/pkcs7-mime");
            this.RegisterMapping(".p7r", "application/x-pkcs7-certreqresp");
            this.RegisterMapping(".p7s", "application/pkcs7-signature");
            this.RegisterMapping(".pbm", "image/x-portable-bitmap");
            this.RegisterMapping(".pcx", "application/octet-stream");
            this.RegisterMapping(".pcz", "application/octet-stream");
            this.RegisterMapping(".pdf", "application/pdf");
            this.RegisterMapping(".pfb", "application/octet-stream");
            this.RegisterMapping(".pfm", "application/octet-stream");
            this.RegisterMapping(".pfx", "application/x-pkcs12");
            this.RegisterMapping(".pgm", "image/x-portable-graymap");
            this.RegisterMapping(".pko", "application/vnd.ms-pki.pko");
            this.RegisterMapping(".pma", "application/x-perfmon");
            this.RegisterMapping(".pmc", "application/x-perfmon");
            this.RegisterMapping(".pml", "application/x-perfmon");
            this.RegisterMapping(".pmr", "application/x-perfmon");
            this.RegisterMapping(".pmw", "application/x-perfmon");
            this.RegisterMapping(".png", "image/png");
            this.RegisterMapping(".pnm", "image/x-portable-anymap");
            this.RegisterMapping(".pnz", "image/png");
            this.RegisterMapping(".pot", "application/vnd.ms-powerpoint");
            this.RegisterMapping(".potm", "application/vnd.ms-powerpoint.template.macroEnabled.12");
            this.RegisterMapping(".potx", "application/vnd.openxmlformats-officedocument.presentationml.template");
            this.RegisterMapping(".ppam", "application/vnd.ms-powerpoint.addin.macroEnabled.12");
            this.RegisterMapping(".ppm", "image/x-portable-pixmap");
            this.RegisterMapping(".pps", "application/vnd.ms-powerpoint");
            this.RegisterMapping(".ppsm", "application/vnd.ms-powerpoint.slideshow.macroEnabled.12");
            this.RegisterMapping(".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow");
            this.RegisterMapping(".ppt", "application/vnd.ms-powerpoint");
            this.RegisterMapping(".pptm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12");
            this.RegisterMapping(".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation");
            this.RegisterMapping(".prf", "application/pics-rules");
            this.RegisterMapping(".prm", "application/octet-stream");
            this.RegisterMapping(".prx", "application/octet-stream");
            this.RegisterMapping(".ps", "application/postscript");
            this.RegisterMapping(".psd", "application/octet-stream");
            this.RegisterMapping(".psm", "application/octet-stream");
            this.RegisterMapping(".psp", "application/octet-stream");
            this.RegisterMapping(".pub", "application/x-mspublisher");
            this.RegisterMapping(".qt", "video/quicktime");
            this.RegisterMapping(".qtl", "application/x-quicktimeplayer");
            this.RegisterMapping(".qxd", "application/octet-stream");
            this.RegisterMapping(".ra", "audio/x-pn-realaudio");
            this.RegisterMapping(".ram", "audio/x-pn-realaudio");
            this.RegisterMapping(".rar", "application/octet-stream");
            this.RegisterMapping(".ras", "image/x-cmu-raster");
            this.RegisterMapping(".rf", "image/vnd.rn-realflash");
            this.RegisterMapping(".rgb", "image/x-rgb");
            this.RegisterMapping(".rm", "application/vnd.rn-realmedia");
            this.RegisterMapping(".rmi", "audio/mid");
            this.RegisterMapping(".roff", "application/x-troff");
            this.RegisterMapping(".rpm", "audio/x-pn-realaudio-plugin");
            this.RegisterMapping(".rtf", "application/rtf");
            this.RegisterMapping(".rtx", "text/richtext");
            this.RegisterMapping(".scd", "application/x-msschedule");
            this.RegisterMapping(".sct", "text/scriptlet");
            this.RegisterMapping(".sea", "application/octet-stream");
            this.RegisterMapping(".setpay", "application/set-payment-initiation");
            this.RegisterMapping(".setreg", "application/set-registration-initiation");
            this.RegisterMapping(".sgml", "text/sgml");
            this.RegisterMapping(".sh", "application/x-sh");
            this.RegisterMapping(".shar", "application/x-shar");
            this.RegisterMapping(".sit", "application/x-stuffit");
            this.RegisterMapping(".sldm", "application/vnd.ms-powerpoint.slide.macroEnabled.12");
            this.RegisterMapping(".sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide");
            this.RegisterMapping(".smd", "audio/x-smd");
            this.RegisterMapping(".smi", "application/octet-stream");
            this.RegisterMapping(".smx", "audio/x-smd");
            this.RegisterMapping(".smz", "audio/x-smd");
            this.RegisterMapping(".snd", "audio/basic");
            this.RegisterMapping(".snp", "application/octet-stream");
            this.RegisterMapping(".spc", "application/x-pkcs7-certificates");
            this.RegisterMapping(".spl", "application/futuresplash");
            this.RegisterMapping(".src", "application/x-wais-source");
            this.RegisterMapping(".ssm", "application/streamingmedia");
            this.RegisterMapping(".sst", "application/vnd.ms-pki.certstore");
            this.RegisterMapping(".stl", "application/vnd.ms-pki.stl");
            this.RegisterMapping(".sv4cpio", "application/x-sv4cpio");
            this.RegisterMapping(".sv4crc", "application/x-sv4crc");
            this.RegisterMapping(".swf", "application/x-shockwave-flash");
            this.RegisterMapping(".t", "application/x-troff");
            this.RegisterMapping(".tar", "application/x-tar");
            this.RegisterMapping(".tcl", "application/x-tcl");
            this.RegisterMapping(".tex", "application/x-tex");
            this.RegisterMapping(".texi", "application/x-texinfo");
            this.RegisterMapping(".texinfo", "application/x-texinfo");
            this.RegisterMapping(".tgz", "application/x-compressed");
            this.RegisterMapping(".thmx", "application/vnd.ms-officetheme");
            this.RegisterMapping(".thn", "application/octet-stream");
            this.RegisterMapping(".tif", "image/tiff");
            this.RegisterMapping(".tiff", "image/tiff");
            this.RegisterMapping(".toc", "application/octet-stream");
            this.RegisterMapping(".tr", "application/x-troff");
            this.RegisterMapping(".trm", "application/x-msterminal");
            this.RegisterMapping(".tsv", "text/tab-separated-values");
            this.RegisterMapping(".ttf", "application/octet-stream");
            this.RegisterMapping(".txt", "text/plain");
            this.RegisterMapping(".u32", "application/octet-stream");
            this.RegisterMapping(".uls", "text/iuls");
            this.RegisterMapping(".ustar", "application/x-ustar");
            this.RegisterMapping(".vbs", "text/vbscript");
            this.RegisterMapping(".vcf", "text/x-vcard");
            this.RegisterMapping(".vcs", "text/plain");
            this.RegisterMapping(".vdx", "application/vnd.ms-visio.viewer");
            this.RegisterMapping(".vml", "text/xml");
            this.RegisterMapping(".vsd", "application/vnd.visio");
            this.RegisterMapping(".vss", "application/vnd.visio");
            this.RegisterMapping(".vst", "application/vnd.visio");
            this.RegisterMapping(".vsto", "application/x-ms-vsto");
            this.RegisterMapping(".vsw", "application/vnd.visio");
            this.RegisterMapping(".vsx", "application/vnd.visio");
            this.RegisterMapping(".vtx", "application/vnd.visio");
            this.RegisterMapping(".wav", "audio/wav");
            this.RegisterMapping(".wax", "audio/x-ms-wax");
            this.RegisterMapping(".wbmp", "image/vnd.wap.wbmp");
            this.RegisterMapping(".wcm", "application/vnd.ms-works");
            this.RegisterMapping(".wdb", "application/vnd.ms-works");
            this.RegisterMapping(".wks", "application/vnd.ms-works");
            this.RegisterMapping(".wm", "video/x-ms-wm");
            this.RegisterMapping(".wma", "audio/x-ms-wma");
            this.RegisterMapping(".wmd", "application/x-ms-wmd");
            this.RegisterMapping(".wmf", "application/x-msmetafile");
            this.RegisterMapping(".wml", "text/vnd.wap.wml");
            this.RegisterMapping(".wmlc", "application/vnd.wap.wmlc");
            this.RegisterMapping(".wmls", "text/vnd.wap.wmlscript");
            this.RegisterMapping(".wmlsc", "application/vnd.wap.wmlscriptc");
            this.RegisterMapping(".wmp", "video/x-ms-wmp");
            this.RegisterMapping(".wmv", "video/x-ms-wmv");
            this.RegisterMapping(".wmx", "video/x-ms-wmx");
            this.RegisterMapping(".wmz", "application/x-ms-wmz");
            this.RegisterMapping(".wps", "application/vnd.ms-works");
            this.RegisterMapping(".wri", "application/x-mswrite");
            this.RegisterMapping(".wrl", "x-world/x-vrml");
            this.RegisterMapping(".wrz", "x-world/x-vrml");
            this.RegisterMapping(".wsdl", "text/xml");
            this.RegisterMapping(".wvx", "video/x-ms-wvx");
            this.RegisterMapping(".x", "application/directx");
            this.RegisterMapping(".xaf", "x-world/x-vrml");
            this.RegisterMapping(".xaml", "application/xaml+xml");
            this.RegisterMapping(".xap", "application/x-silverlight-app");
            this.RegisterMapping(".xbap", "application/x-ms-xbap");
            this.RegisterMapping(".xbm", "image/x-xbitmap");
            this.RegisterMapping(".xdr", "text/plain");
            this.RegisterMapping(".xla", "application/vnd.ms-excel");
            this.RegisterMapping(".xlam", "application/vnd.ms-excel.addin.macroEnabled.12");
            this.RegisterMapping(".xlc", "application/vnd.ms-excel");
            this.RegisterMapping(".xlm", "application/vnd.ms-excel");
            this.RegisterMapping(".xls", "application/vnd.ms-excel");
            this.RegisterMapping(".xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12");
            this.RegisterMapping(".xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12");
            this.RegisterMapping(".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            this.RegisterMapping(".xlt", "application/vnd.ms-excel");
            this.RegisterMapping(".xltm", "application/vnd.ms-excel.template.macroEnabled.12");
            this.RegisterMapping(".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template");
            this.RegisterMapping(".xlw", "application/vnd.ms-excel");
            this.RegisterMapping(".xml", "text/xml");
            this.RegisterMapping(".xof", "x-world/x-vrml");
            this.RegisterMapping(".xpm", "image/x-xpixmap");
            this.RegisterMapping(".xps", "application/vnd.ms-xpsdocument");
            this.RegisterMapping(".xsd", "text/xml");
            this.RegisterMapping(".xsf", "text/xml");
            this.RegisterMapping(".xsl", "text/xml");
            this.RegisterMapping(".xslt", "text/xml");
            this.RegisterMapping(".xsn", "application/octet-stream");
            this.RegisterMapping(".xtp", "application/octet-stream");
            this.RegisterMapping(".xwd", "image/x-xwindowdump");
            this.RegisterMapping(".z", "application/x-compress");
            this.RegisterMapping(".zip", "application/x-zip-compressed");
        }

        protected void RegisterMapping(string ext, string mimeType)
        {
            this.mapping[ext] = mimeType;
        }
        
        protected void EnsurePopulation()
        {
            this.ensurer.Value.ToString();
        }
    }
}
