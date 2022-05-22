using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;

namespace eStoreClient.Pages.Orders
{
    public class DetailsModel : PageModel
    {

        public DetailsModel()
        {
        }

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
    }
}
