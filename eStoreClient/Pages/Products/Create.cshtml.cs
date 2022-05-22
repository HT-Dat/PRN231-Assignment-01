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
    public class CreateModel : PageModel
    {

        public CreateModel()
        {
        }

        public async Task<IActionResult> OnGet()
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync("http://localhost:5000/api/Category");
            HttpContent content = response.Content;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var categories =
                await JsonSerializer.DeserializeAsync<IEnumerable<Category>>(content.ReadAsStream(), options);
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
            return Page();
        }

        [BindProperty] public Product Product { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var json = JsonSerializer.Serialize(Product);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync("http://localhost:5000/api/Product", new StringContent(json, Encoding.UTF8, "application/json"));
            // HttpContent content = response.Content;
            return RedirectToPage("./Index");
        }
    }
}