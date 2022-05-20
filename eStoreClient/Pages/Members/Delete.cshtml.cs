using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;

namespace eStoreClient.Pages.Members
{
    public class DeleteModel : PageModel
    {

        public DeleteModel()
        {
        }

        [BindProperty]
      public Member Member { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:5000/api/Member/"+id);
            HttpContent content = response.Content;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var member = await JsonSerializer.DeserializeAsync<Member>(content.ReadAsStream(), options);
             
            if (member == null)
            {
                return NotFound();
            }
            Member = member;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:5000/api/Member/"+id);
            HttpContent content = response.Content;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var member = await JsonSerializer.DeserializeAsync<Member>(content.ReadAsStream(), options);

            if (member != null)
            {
                Member = member;
                response = await client.DeleteAsync("http://localhost:5000/api/Member/"+id);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
