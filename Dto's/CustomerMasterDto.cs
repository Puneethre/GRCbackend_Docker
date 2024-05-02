namespace GRCServices.Dto_s
{
    public class GetCustomerMasterDto
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = null!;

        public string? Street { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string Country { get; set; } = null!;

        public string ContactName { get; set; } = null!;

        public string ContactEmail { get; set; } = null!;

        public string? Description { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public bool IsActive { get; set; }

        public string? ZipCode { get; set; }

        public string ContactPhone { get; set; } = null!;
    }
    public class AddCustomerMasterDto
    {
     //   public int CustomerId { get; set; }

        public string CustomerName { get; set; } = null!;

        public string? Street { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string Country { get; set; } = null!;

        public string ContactName { get; set; } = null!;

        public string ContactEmail { get; set; } = null!;

        public string? Description { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public bool IsActive { get; set; }

        public string? ZipCode { get; set; }

        public string ContactPhone { get; set; } = null!;
    }
    public class UpdateCustomerMasterDto
    {
       // public int CustomerId { get; set; }

       // public string CustomerName { get; set; } = null!;

        public string? Street { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string Country { get; set; } = null!;

        public string ContactName { get; set; } = null!;

        public string ContactEmail { get; set; } = null!;

        public string? Description { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public bool IsActive { get; set; }

        public string? ZipCode { get; set; }

        public string ContactPhone { get; set; } = null!;
    }
}
