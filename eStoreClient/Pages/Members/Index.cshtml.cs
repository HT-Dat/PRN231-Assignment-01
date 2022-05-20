using System.Text.Json;
using BusinessObject;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eStoreClient.Pages.Members
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
        }

        public IList<Member> Member { get; set; } = default!;

        public async Task OnGetAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:5000/api/Member");
            HttpContent content = response.Content;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Member = await JsonSerializer.DeserializeAsync<List<Member>>(content.ReadAsStream(), options);
        }
    }
}