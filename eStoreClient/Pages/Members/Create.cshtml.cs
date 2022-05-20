using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;

namespace eStoreClient.Pages.Members
{
    public class CreateModel : PageModel
    {

        public CreateModel()
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Member Member { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || Member == null)
            {
                return Page();
            }
          
            var memberJson = JsonSerializer.Serialize(Member);
            HttpClient client = new HttpClient();
            await client.PostAsync("http://localhost:5000/api/Member", new StringContent(memberJson, Encoding.UTF8, "application/json"));
            // HttpContent content = response.Content;
            return RedirectToPage("./Index");
        }
    }
}
