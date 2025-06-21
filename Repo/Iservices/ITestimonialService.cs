using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Iservices
{
    public interface ITestimonialService
    {
        Task<List<TestimonialModel>> GetAllTestimonialsAsync();
        Task AddTestimonialAsync(TestimonialModel testimonial);
        Task ApproveTestimonialAsync(int id); // if using admin approval
        Task RejectTestimonialAsync(int id);

    }
}
