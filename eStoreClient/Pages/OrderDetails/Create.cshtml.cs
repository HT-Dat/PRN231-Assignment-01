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

namespace eStoreClient.Pages.OrderDetails
{
    public class CreateModel : PageModel
    {
        [BindProperty] public int id { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            this.id = id;
            HttpClient client = new HttpClient();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            
            //Fetch list order
            HttpResponseMessage response = await client.GetAsync("http://localhost:5000/api/Order");
            HttpContent content = response.Content;
            var listOrder =  await JsonSerializer.DeserializeAsync<IEnumerable<Order>>(content.ReadAsStream(), options);
            ViewData["OrderId"] = new SelectList(listOrder, "OrderId", "OrderId");
            
            //Fetch list product
            response = await client.GetAsync("http://localhost:5000/api/Product");
            content = response.Content;
            var listProduct =  await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(content.ReadAsStream(), options);

            ViewData["ProductId"] = new SelectList(listProduct, "ProductId", "ProductName");
            return Page();
        }

        [BindProperty] public OrderDetail OrderDetail { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            OrderDetail.OrderId = id;
            var json = JsonSerializer.Serialize(OrderDetail);
            HttpClient client = new HttpClient();
            await client.PostAsync("http://localhost:5000/api/order-detail", new StringContent(json, Encoding.UTF8, "application/json"));
            // HttpContent content = response.Content;
            return RedirectToPage("./Index", "OnGet", new {id = id});
        }
    }
}