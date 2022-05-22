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
using Microsoft.EntityFrameworkCore;
using BusinessObject;

namespace eStoreClient.Pages.Members
{
    public class EditModel : PageModel
    {
        public EditModel()
        {
        }

        [BindProperty]
        public Member Member { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("LoggedInUser")))
            {
                return Unauthorized();
            }
            Member loggedMember = JsonSerializer.Deserialize<Member>(HttpContext.Session.GetString("LoggedInUser"));
            if (loggedMember.isAdmin)
            {
                return Unauthorized();
            }
            if (id == null)
            {
                id = loggedMember.MemberId;
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var json = JsonSerializer.Serialize(Member);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PutAsync("http://localhost:5000/api/Member/"+Member.MemberId, new StringContent(json, Encoding.UTF8, "application/json"));
            // HttpContent content = response.Content;
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            Member loggedMember = JsonSerializer.Deserialize<Member>(HttpContext.Session.GetString("LoggedInUser"));
            if (loggedMember.isAdmin == false)
            {
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
