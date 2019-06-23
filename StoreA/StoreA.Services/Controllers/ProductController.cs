using StoeA.Models.Models;
using StoreA.Services.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace StoreA.Services.Controllers
{
    public class ProductController : ApiController
    {
        private StoreADBcontext _db = new StoreADBcontext();

     
        [HttpGet]
        public IHttpActionResult GettingAllProduct()
        {
            var products = _db.Product.AsNoTracking()
                .Select(c => new Products
                {
                    Id = c.Id,
                    BrandName = c.BrandName,
                    ModelNo = c.ModelNo,
                    CategoryName = c.Category.Name,
                    CompanyName = c.Company.Name,              
                }).ToList();

           
            return Ok(products);
        }

        // GET: api/Products/5
        [ResponseType(typeof(Products))]
        [HttpGet]
        public IHttpActionResult GettingProduct([FromUri]int id)
        {
            var product = _db.Product.
                Where(d => d.Id == id)
                .Select(d => new
                {
                    Id = d.Id,
                    BrandName = d.BrandName,
                    ModelNo = d.ModelNo,    
                    CategoryName = d.Category.Name,
                    CompanyName = d.Company.Name,
                   

                }).SingleOrDefault();

            if (product == null)
            {
                return NotFound();
            }


            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult EdittingProduct(int id, Products product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            _db.Entry(product).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Products))]
        [HttpPost]
        public IHttpActionResult AddingProduct([FromBody]Products product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Product.Add(product);
            _db.SaveChanges();

            return Ok("محصول جدید  ثبت شد");
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Products))]
        [HttpDelete]
        public IHttpActionResult DeleteingProduct([FromUri]int id)
        {
            var product = _db.Product.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            _db.Product.Remove(product);
            _db.SaveChanges();

            return Ok("محصول حذف شد");
        }



        [HttpGet]
        public IHttpActionResult GetCountries()
        {
            var country = _db.Countries.AsNoTracking()
                        .Select(s => new Country
                        {
                            Id = s.Id,
                            Name = s.Name

                        }).ToList();

            return Ok(country);
        }

        [HttpGet]
        public IHttpActionResult GetCategories()
        {

            var categories = _db.Categories.AsNoTracking()
                        .Select(c => new Category
                        {
                            Id = c.Id,
                            Name = c.Name

                        }).ToList();

            return Ok(categories);
        }

        [HttpGet]
        public IHttpActionResult GetComponies()
        {
            var companies = _db.Companies.AsNoTracking()
                       .Select(c => new Company
                       {
                           Id = c.Id,
                           Name = c.Name

                       }).ToList();

            return Ok(companies);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return _db.Product.Count(e => e.Id == id) > 0;
        }
    }
}
