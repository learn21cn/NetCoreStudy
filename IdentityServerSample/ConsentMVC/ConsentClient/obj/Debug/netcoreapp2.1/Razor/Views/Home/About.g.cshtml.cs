#pragma checksum "G:\NetCoreStudy\IdentityServerSample\ConsentMVC\ConsentClient\Views\Home\About.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a5cd4108e07b4a4279389df88da8e2ae7dfc066d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_About), @"mvc.1.0.view", @"/Views/Home/About.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/About.cshtml", typeof(AspNetCore.Views_Home_About))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "G:\NetCoreStudy\IdentityServerSample\ConsentMVC\ConsentClient\Views\_ViewImports.cshtml"
using ConsentClient;

#line default
#line hidden
#line 2 "G:\NetCoreStudy\IdentityServerSample\ConsentMVC\ConsentClient\Views\_ViewImports.cshtml"
using ConsentClient.Models;

#line default
#line hidden
#line 1 "G:\NetCoreStudy\IdentityServerSample\ConsentMVC\ConsentClient\Views\Home\About.cshtml"
using Microsoft.AspNetCore.Authentication;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a5cd4108e07b4a4279389df88da8e2ae7dfc066d", @"/Views/Home/About.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1e04efdd8a9046250bf35b8bc3579e690e6ee83c", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_About : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(45, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "G:\NetCoreStudy\IdentityServerSample\ConsentMVC\ConsentClient\Views\Home\About.cshtml"
  
    ViewData["Title"] = "About";

#line default
#line hidden
            BeginContext(88, 4, true);
            WriteLiteral("<h2>");
            EndContext();
            BeginContext(93, 17, false);
#line 6 "G:\NetCoreStudy\IdentityServerSample\ConsentMVC\ConsentClient\Views\Home\About.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(110, 11, true);
            WriteLiteral("</h2>\r\n<h3>");
            EndContext();
            BeginContext(122, 19, false);
#line 7 "G:\NetCoreStudy\IdentityServerSample\ConsentMVC\ConsentClient\Views\Home\About.cshtml"
Write(ViewData["Message"]);

#line default
#line hidden
            EndContext();
            BeginContext(141, 74, true);
            WriteLiteral("</h3>\r\n\r\n<p>Use this area to provide additional information.</p>\r\n\r\n<dl>\r\n");
            EndContext();
#line 12 "G:\NetCoreStudy\IdentityServerSample\ConsentMVC\ConsentClient\Views\Home\About.cshtml"
     foreach (var claim in User.Claims)
    {

#line default
#line hidden
            BeginContext(263, 15, true);
            WriteLiteral("        <dt>类型 ");
            EndContext();
            BeginContext(279, 10, false);
#line 14 "G:\NetCoreStudy\IdentityServerSample\ConsentMVC\ConsentClient\Views\Home\About.cshtml"
          Write(claim.Type);

#line default
#line hidden
            EndContext();
            BeginContext(289, 23, true);
            WriteLiteral("  : </dt>\r\n        <dt>");
            EndContext();
            BeginContext(313, 11, false);
#line 15 "G:\NetCoreStudy\IdentityServerSample\ConsentMVC\ConsentClient\Views\Home\About.cshtml"
       Write(claim.Value);

#line default
#line hidden
            EndContext();
            BeginContext(324, 7, true);
            WriteLiteral("</dt>\r\n");
            EndContext();
#line 16 "G:\NetCoreStudy\IdentityServerSample\ConsentMVC\ConsentClient\Views\Home\About.cshtml"
    }

#line default
#line hidden
            BeginContext(338, 37, true);
            WriteLiteral("\r\n    <dt>Access Token</dt>\r\n    <dd>");
            EndContext();
            BeginContext(376, 59, false);
#line 19 "G:\NetCoreStudy\IdentityServerSample\ConsentMVC\ConsentClient\Views\Home\About.cshtml"
   Write(await ViewContext.HttpContext.GetTokenAsync("access_token"));

#line default
#line hidden
            EndContext();
            BeginContext(435, 45, true);
            WriteLiteral("</dd>\r\n\r\n    <dt>refresh token</dt>\r\n    <dd>");
            EndContext();
            BeginContext(481, 60, false);
#line 22 "G:\NetCoreStudy\IdentityServerSample\ConsentMVC\ConsentClient\Views\Home\About.cshtml"
   Write(await ViewContext.HttpContext.GetTokenAsync("refresh_token"));

#line default
#line hidden
            EndContext();
            BeginContext(541, 40, true);
            WriteLiteral("</dd>\r\n\r\n    <dt>Id Token</dt>\r\n    <dd>");
            EndContext();
            BeginContext(582, 55, false);
#line 25 "G:\NetCoreStudy\IdentityServerSample\ConsentMVC\ConsentClient\Views\Home\About.cshtml"
   Write(await ViewContext.HttpContext.GetTokenAsync("id_token"));

#line default
#line hidden
            EndContext();
            BeginContext(637, 12, true);
            WriteLiteral("</dd>\r\n</dl>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
