using Grpc.Net.Client;
using GrpcService = GrpcCustomersService;
using Cilibia_Malina_Lab2Context.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cilibia_Malina_Lab2.Controllers
{
    public class CustomersGrpcController : Controller
    {
        private readonly GrpcChannel channel;

        public CustomersGrpcController()
        {
            channel = GrpcChannel.ForAddress("https://localhost:7274");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var client = new GrpcService.CustomerService.CustomerServiceClient(channel);

            GrpcService.CustomerList cust = client.GetAll(new GrpcService.Empty());

            return View(cust);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var client = new GrpcService.CustomerService.CustomerServiceClient(channel);

                var grpcCustomer = new GrpcService.Customer
                {
                    CustomerId = customer.CustomerID,
                    Name = customer.Name,
                    Adress = customer.Adress,
                    Birthdate = customer.BirthDate.ToString("yyyy-MM-dd")
                };

                var createdCustomer = client.Insert(grpcCustomer);

                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = new GrpcCustomersService.CustomerService.CustomerServiceClient(channel);

            var customer = client.Get(new GrpcCustomersService.CustomerId { Id = (int)id });

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var client = new GrpcCustomersService.CustomerService.CustomerServiceClient(channel);

            client.Delete(new GrpcCustomersService.CustomerId { Id = id });

            return RedirectToAction(nameof(Index));
        }

        // GET: Deschide formularul de editare
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // 1. Cerem datele actuale de la serverul gRPC
            var client = new GrpcCustomersService.CustomerService.CustomerServiceClient(channel);
            var grpcData = client.Get(new GrpcCustomersService.CustomerId { Id = (int)id });

            if (grpcData == null)
            {
                return NotFound();
            }

            // 2. Convertim datele din format gRPC in formatul Local (Modelul tau)
            // Acest pas este necesar pentru ca View-ul se asteapta la Cilibia_Malina_Lab2Context.Models.Customer
            var localModel = new Cilibia_Malina_Lab2Context.Models.Customer
            {
                CustomerID = grpcData.CustomerId,
                Name = grpcData.Name,
                Adress = grpcData.Adress,
                BirthDate = DateTime.Parse(grpcData.Birthdate)
            };

            return View(localModel);
        }

        // POST: Trimite modificarile la server
        [HttpPost]
        public IActionResult Edit(int id, Cilibia_Malina_Lab2Context.Models.Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var client = new GrpcCustomersService.CustomerService.CustomerServiceClient(channel);

                var grpcCustomer = new GrpcCustomersService.Customer
                {
                    CustomerId = customer.CustomerID,
                    Name = customer.Name,
                    Adress = customer.Adress,
                    Birthdate = customer.BirthDate.ToString("yyyy-MM-dd")
                };

                client.Update(grpcCustomer);

                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
    }
}