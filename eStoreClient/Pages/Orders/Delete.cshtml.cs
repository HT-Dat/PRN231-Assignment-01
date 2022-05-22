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

namespace eStoreClient.Pages.Orders
{
    public class DeleteModel : PageModel
    {

        public DeleteModel()
        {
        }

        [BindProperty]
      public Order Order { get; set; } = default!;

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:5000/api/Order/"+id);
            HttpContent content = response.Content;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var order = await JsonSerializer.DeserializeAsync<Order>(content.ReadAsStream(), options);

            if (order != null)
            {
                Order = order;
                response = await client.DeleteAsync("http://localhost:5000/api/Order/"+id);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
