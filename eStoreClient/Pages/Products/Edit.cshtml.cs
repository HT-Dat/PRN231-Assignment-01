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

namespace eStoreClient.Pages.Products
{
    public class EditModel : PageModel
    {

        public EditModel()
        {
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpClient client = new HttpClient();
            
            //Get Product
            HttpResponseMessage response = await client.GetAsync("http://localhost:5000/api/Product/"+id);
            HttpContent content = response.Content;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var product =  await JsonSerializer.DeserializeAsync<Product>(content.ReadAsStream(), options);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
            
            //Get category
            response = await client.GetAsync("http://localhost:5000/api/Category");
            content = response.Content;
            var categories =  await JsonSerializer.DeserializeAsync<IEnumerable<Category>>(content.ReadAsStream(), options);
           ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
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
            var json = JsonSerializer.Serialize(Product);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PutAsync("http://localhost:5000/api/Product/"+Product.ProductId, new StringContent(json, Encoding.UTF8, "application/json"));
            // HttpContent content = response.Content;
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
        
            return RedirectToPage("./Index");
        }
        
    }
}
