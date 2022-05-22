using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;

namespace eStoreClient.Pages.Orders
{
    public class CreateModel : PageModel
    {

        public CreateModel()
        {
        }

        public async Task<IActionResult> OnGet()
        {
            //Get member list
            HttpClient client = new HttpClient();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            HttpResponseMessage response = await client.GetAsync("http://localhost:5000/api/Member");
            HttpContent content = response.Content;
            var list =  await JsonSerializer.DeserializeAsync<IEnumerable<Member>>(content.ReadAsStream(), options);
            
            ViewData["MemberId"] = new SelectList(list, "MemberId", "Email");
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;
        

           // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var json = JsonSerializer.Serialize(Order);
            HttpClient client = new HttpClient();
            await client.PostAsync("http://localhost:5000/api/Order", new StringContent(json, Encoding.UTF8, "application/json"));
            // HttpContent content = response.Content;
            return RedirectToPage("../OrderDetails/Create", "OnGet", new { id = Order.OrderId });
        }
    }
}
