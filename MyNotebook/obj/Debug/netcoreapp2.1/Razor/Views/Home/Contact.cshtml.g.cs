#pragma checksum "C:\Users\Egzon Thaqi\Desktop\MyNotebook-main\MyNotebook\Views\Home\Contact.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9fb768c5d4d615163e50d054d2a46cd8873b064c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Contact), @"mvc.1.0.view", @"/Views/Home/Contact.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Contact.cshtml", typeof(AspNetCore.Views_Home_Contact))]
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
#line 1 "C:\Users\Egzon Thaqi\Desktop\MyNotebook-main\MyNotebook\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 2 "C:\Users\Egzon Thaqi\Desktop\MyNotebook-main\MyNotebook\Views\_ViewImports.cshtml"
using MyNotebook;

#line default
#line hidden
#line 3 "C:\Users\Egzon Thaqi\Desktop\MyNotebook-main\MyNotebook\Views\_ViewImports.cshtml"
using MyNotebook.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9fb768c5d4d615163e50d054d2a46cd8873b064c", @"/Views/Home/Contact.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aff100ab65901cddaadd741cfc547fa521975acc", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Contact : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\Egzon Thaqi\Desktop\MyNotebook-main\MyNotebook\Views\Home\Contact.cshtml"
  
    ViewData["Title"] = "Contact";
    Layout = "~/Views/Shared/BootstrapLayout.cshtml";

#line default
#line hidden
            BeginContext(94, 4, true);
            WriteLiteral("<h2>");
            EndContext();
            BeginContext(99, 17, false);
#line 5 "C:\Users\Egzon Thaqi\Desktop\MyNotebook-main\MyNotebook\Views\Home\Contact.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(116, 10, true);
            WriteLiteral("</h2>\n<h3>");
            EndContext();
            BeginContext(127, 19, false);
#line 6 "C:\Users\Egzon Thaqi\Desktop\MyNotebook-main\MyNotebook\Views\Home\Contact.cshtml"
Write(ViewData["Message"]);

#line default
#line hidden
            EndContext();
            BeginContext(146, 365, true);
            WriteLiteral(@"</h3>

<address>
    Kolegji UBT<br />
    Lagjja Kalabria,10000 Prishtine, Kosovo<br />
    <abbr title=""Phone"">Phone:</abbr>
    +383 38 541 400
</address>

<address>
    <strong>Support:</strong> <a href=""mailto:Support@example.com"">info@ubt-uni.net</a><br />
    <strong>Marketing:</strong> <a href=""mailto:Marketing@example.com"">ubt@ubt-uni.net</a>
</address>
");
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
