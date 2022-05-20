using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;

namespace eStoreClient.Pages.Members
{
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.FStoreDBContext _context;

        public IndexModel(BusinessObject.FStoreDBContext context)
        {
            _context = context;
        }

        public IList<Member> Member { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Members != null)
            {
                using (HttpClient client = new HttpClient())
                {
                    // If any authorization available
                    using (HttpResponseMessage response = await client.GetAsync("http://localhost:5000/api/Member"))
                    {
                        using (HttpContent content = response.Content)
                        {
                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };
                            Member = JsonSerializer.Deserialize<List<Member>>(await content.ReadAsStringAsync(), options);
                        }
                    }
                } 
            }
        }
    }
}
