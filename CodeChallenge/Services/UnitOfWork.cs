using CodeChallenge.Context;
using CodeChallenge.Interfaces;
using CodeChallenge.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CodeChallenge.Services
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private CodeContext _context;
        private bool _disposed = false;

        public UnitOfWork(CodeContext context)
        {
            _context = context;
        }

        // Repositorios
        private IRepository<Product> _productRepository;
        private IRepository<Order> _orderRepository;
        private IRepository<Customer> _customerRepository;


        public virtual IRepository<Customer> CustomerRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new Repository<Product>(_context);
                }
                return _customerRepository;
            }
        }
        public virtual IRepository<Product> ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new Repository<Product>(_context);
                }
                return _productRepository;
            }
        }


        public virtual IRepository<Order> OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new Repository<Order>(_context);
                }
                return _orderRepository;
            }
        }
        // Agrega otros repositorios según sea necesario para tus entidades

        public virtual  bool CreateOrderWithTotalCost(int customerId, int productId, int quantity)
        {
          
            // Check for valid inputs
            if (customerId <= 0 || productId <= 0 || quantity <= 0)
            {
                throw new ArgumentException("Invalid input values. Customer ID, Product ID, and Quantity must be greater than zero.");
            }

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Call the stored procedure
                    var customerIdParam = new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customerId };
                    var productIdParam = new SqlParameter("@ProductId", SqlDbType.Int) { Value = productId };
                    var quantityParam = new SqlParameter("@Quantity", SqlDbType.Int) { Value = quantity };

                    _context.Database.ExecuteSqlRaw("EXEC CreateOrder @CustomerId, @ProductId, @Quantity",
                        customerIdParam, productIdParam, quantityParam);

                    // Commit the transaction
                    dbContextTransaction.Commit();

                    return true;
                }
                catch (Exception)
                {
                    // Handle exceptions and roll back the transaction if needed
                    dbContextTransaction.Rollback();
                    return false;
                    throw;
                }
            }
        }


        public virtual void Save()
        {
            _context.SaveChanges();
        }

        // Implementación de IDisposable para liberar recursos
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

      
    }
}
