using Microsoft.AspNetCore.Mvc;
using TriangleSolution.Models;
using System.Collections.Generic;
using System.Linq;

namespace TriangleSolution.Controllers
{
    public class TriangleController : Controller
    {
        // 1. Show the info about a triangle
        // /triangle/Info?side1=10&side2=20&side3=25
        [HttpGet]
        public IActionResult Info(Triangle triangle)
        {
            if (triangle == null || !triangle.IsValid())
                return BadRequest("Invalid triangle parameters.");

            return Content(triangle.GetInfo(), "text/plain");
        }

        // 2. Calculate the area of a triangle
        // /triangle/Area?side1=10&side2=20&side3=25
        [HttpGet]
        public IActionResult Area(Triangle triangle)
        {
            if (triangle == null || !triangle.IsValid())
                return BadRequest("Invalid triangle parameters.");

            return Content(triangle.Area().ToString(), "text/plain");
        }

        // 3. Calculate the perimeter of a triangle
        // /triangle/Perimeter?side1=10&side2=20&side3=25
        [HttpGet]
        public IActionResult Perimeter(Triangle triangle)
        {
            if (triangle == null || !triangle.IsValid())
                return BadRequest("Invalid triangle parameters.");

            return Content(triangle.Perimeter().ToString(), "text/plain");
        }

        // 4. Return True or False depending on whether the triangle is right-angled
        // /triangle/IsRightAngled?side1=3&side2=5&side3=4
        [HttpGet]
        public IActionResult IsRightAngled(Triangle triangle)
        {
            if (triangle == null || !triangle.IsValid())
                return BadRequest("Invalid triangle parameters.");

            return Content(triangle.IsRightAngled().ToString().ToLower(), "text/plain");
        }

        // 5. Return True or False depending on whether the triangle is equilateral
        // /triangle/IsEquilateral?side1=10&side2=10&side3=10
        [HttpGet]
        public IActionResult IsEquilateral(Triangle triangle)
        {
            if (triangle == null || !triangle.IsValid())
                return BadRequest("Invalid triangle parameters.");

            return Content(triangle.IsEquilateral().ToString().ToLower(), "text/plain");
        }

        // 6. Return True or False depending on whether the triangle is isosceles
        // /triangle/IsIsosceles?side1=15&side2=20&side3=15
        [HttpGet]
        public IActionResult IsIsosceles(Triangle triangle)
        {
            if (triangle == null || !triangle.IsValid())
                return BadRequest("Invalid triangle parameters.");

            return Content(triangle.IsIsosceles().ToString().ToLower(), "text/plain");
        }

        // 7. Return True or False depending on whether two triangles are congruent
        // /triangle/arecongruent?tr1.side1=10&tr1.side2=20&tr1.side3=15&tr2.side1=15&tr2.side2=10&tr2.side3=20
        [HttpGet]
        public IActionResult AreCongruent([FromQuery(Name = "tr1")] Triangle tr1, [FromQuery(Name = "tr2")] Triangle tr2)
        {
            if (tr1 == null || !tr1.IsValid() || tr2 == null || !tr2.IsValid())
                return BadRequest("Invalid triangle parameters.");

            return Content(tr1.IsCongruentTo(tr2).ToString().ToLower(), "text/plain");
        }

        // 8. Return True or False depending on whether two triangles are similar
        // /triangle/aresimilar?tr1.side1=10&tr1.side2=20&tr1.side3=15&tr2.side1=30&tr2.side2=20&tr2.side3=40
        [HttpGet]
        public IActionResult AreSimilar([FromQuery(Name = "tr1")] Triangle tr1, [FromQuery(Name = "tr2")] Triangle tr2)
        {
            if (tr1 == null || !tr1.IsValid() || tr2 == null || !tr2.IsValid())
                return BadRequest("Invalid triangle parameters.");

            return Content(tr1.IsSimilarTo(tr2).ToString().ToLower(), "text/plain");
        }

        [HttpGet]
        public IActionResult GreatesByPerimeter([FromQuery(Name = "tr")] List<Triangle> tr)
        {
            if (tr == null || !tr.Any() || tr.Any(t => !t.IsValid()))
                return BadRequest("Invalid triangle array or parameters.");

            var greatest = tr.OrderByDescending(t => t.Perimeter()).First();
            return Content(greatest.GetInfo(), "text/plain");
        }

        [HttpGet]
        public IActionResult GreatestByArea([FromQuery(Name = "tr")] List<Triangle> tr)
        {
            if (tr == null || !tr.Any() || tr.Any(t => !t.IsValid()))
                return BadRequest("Invalid triangle array or parameters.");

            var greatest = tr.OrderByDescending(t => t.Area()).First();
            return Content(greatest.GetInfo(), "text/plain");
        }

        [HttpGet]
        public IActionResult PairwiseNonSimilar([FromQuery(Name = "tr")] List<Triangle> tr)
        {
            if (tr == null || !tr.Any() || tr.Any(t => !t.IsValid()))
                return BadRequest("Invalid triangle array or parameters.");

            var uniqueTriangles = new List<Triangle>();
            foreach (var t in tr)
            {
                if (!uniqueTriangles.Any(existing => existing.IsSimilarTo(t)))
                {
                    uniqueTriangles.Add(t);
                }
            }

            string result = string.Join("\n\n--------------------------------------------\n\n", 
                uniqueTriangles.Select(t => t.GetInfo()));

            return Content(result, "text/plain");
        }
    }
}