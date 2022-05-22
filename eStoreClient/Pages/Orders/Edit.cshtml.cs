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

namespace eStoreClient.Pages.Orders
{
    public class EditModel : PageModel
    {

        public EditModel()
        {
        }

        [BindProperty] public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpClient client = new HttpClient();

            //Get Order
            HttpResponseMessage response = await client.GetAsync("http://localhost:5000/api/Order/" + id);
            HttpContent content = response.Content;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var order = await JsonSerializer.DeserializeAsync<Order>(content.ReadAsStream(), options);
            if (order == null)
            {
                return NotFound();
            }
            Order = order;
            
            //Get member list
            response = await client.GetAsync("http://localhost:5000/api/Member");
            content = response.Content;
            var list =  await JsonSerializer.DeserializeAsync<IEnumerable<Member>>(content.ReadAsStream(), options);
            
            ViewData["MemberId"] = new SelectList(list, "MemberId", "Email");
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
            var json = JsonSerializer.Serialize(Order);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PutAsync("http://localhost:5000/api/Order/"+Order.OrderId, new StringContent(json, Encoding.UTF8, "application/json"));
            // HttpContent content = response.Content;
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
        
            return RedirectToPage("./Index");
        }
        
    }
}