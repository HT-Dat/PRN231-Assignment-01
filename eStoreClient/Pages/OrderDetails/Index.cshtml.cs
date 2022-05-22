using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;

namespace eStoreClient.Pages.OrderDetails
{
    public class IndexModel : PageModel
    {

        public IndexModel()
        {
        }

        public IList<OrderDetail> OrderDetail { get;set; } = default!;
        public int id { get; set; }

        public async Task OnGetAsync(int id)
        {
            this.id = id;
            HttpClient client = new HttpClient();
            string uri = "http://localhost:5000/api/order-detail?orderId=" + id.ToString();
            HttpResponseMessage response = await client.GetAsync(uri);
            HttpContent content = response.Content;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            OrderDetail = await JsonSerializer.DeserializeAsync<List<OrderDetail>>(content.ReadAsStream(), options);

        }
    }
}
