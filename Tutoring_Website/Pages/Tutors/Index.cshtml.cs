using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tutoring_Website.Model;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Tutoring_Website.Pages.Tutors
{
    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        private readonly IHttpClientFactory _clientFactory;

        public IndexModel(ApplicationDbContext db, IHttpClientFactory clientFactory)
        {
            _db = db;
            _clientFactory = clientFactory;
            TutorList = new List<Tutor>();

        }

        public IEnumerable<Tutor> Tutors { get; set; }

        public List<Tutor> TutorList { get; set; }


        public Product json { get; set; }

        public async Task OnGet()
        {

            var request = new HttpRequestMessage(HttpMethod.Get,
            "https://projectrainforest.azurewebsites.net/api/VendorProduct/GetVendorProducts/25");

            var client = _clientFactory.CreateClient();


            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var readContent = await response.Content.ReadAsStringAsync();
                List<Product> result = JsonConvert.DeserializeObject<List<Product>>(readContent);
                //JObject cObject = (JObject)result[0];
                //JToken pI = cObject["productInfo"];

                Console.WriteLine("LOOK HERE MOTHERFUCKERS");
                Console.WriteLine(result[0].ProductId);
                Console.WriteLine(result[0].ProductInfo.DateAdded);
                Console.WriteLine(result[0].ProductInfo.ProductDescription);
                Console.WriteLine(result[0].ProductInfo.ProductId);
                Console.WriteLine(result[0].ProductInfo.ProductRating);

                foreach (var product in result)
                {
                    Tutor tutor = new Tutor();
                    tutor.tutor_id = product.ProductId;
                    tutor.tutor_name = product.ProductName;
                    tutor.tutor_description = product.ProductInfo.ProductDescription;
                    tutor.tutor_subjects = product.ProductInfo.ProductDescription;
                    tutor.tutor_rate = product.ProductInfo.ProductPrice;
                    tutor.tutor_img = product.ProductInfo.ProductImg;
                    tutor.tutor_rating = (int)product.ProductInfo.ProductRating;
                    tutor.tutor_date_joined = product.ProductInfo.DateAdded;
                    TutorList.Add(tutor);
                    Console.WriteLine(TutorList);
                }

                
            }



            Tutors = await _db.Tutors.ToListAsync();

        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var tutor = await _db.Tutors.FindAsync(id);
            if (tutor == null)
            {
                return NotFound();
            }
            _db.Tutors.Remove(tutor);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }

    }
}
