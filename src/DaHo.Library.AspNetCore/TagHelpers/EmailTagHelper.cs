using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace DaHo.Library.AspNetCore.TagHelpers
{
    [HtmlTargetElement("email", Attributes = "to")]
    public class EmailTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var body = (await output.GetChildContentAsync()).GetContent();
            var to = context.AllAttributes["to"].Value.ToString();
            var subject = context.AllAttributes["subject"]?.Value?.ToString();

            var mailto = $"mailto:{to}";
            mailto = !string.IsNullOrWhiteSpace(subject) 
                ? $"{mailto}&subject={subject}&body={body}" 
                : $"{mailto}&body={body}";

            output.TagName = "a";
            output.Attributes.Clear();
            output.Attributes.SetAttribute("href", mailto);
            output.Content.Clear();
            output.Content.AppendFormat("Email {0}", to);
        }
    }
}
