using DAL;
using Entities;
using Repo.Iservices;
using Repo.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TestimonialService : ITestimonialService
{
    private readonly TestimonialDAL _testimonialDAL;

    public TestimonialService(TestimonialDAL testimonialDAL)
    {
        _testimonialDAL = testimonialDAL;
    }

    public async Task<List<TestimonialModel>> GetAllTestimonialsAsync()
    {
        return await _testimonialDAL.GetAllAsync();
    }

    public async Task AddTestimonialAsync(TestimonialModel testimonial)
    {
        await _testimonialDAL.AddAsync(testimonial);
    }

    public async Task ApproveTestimonialAsync(int id)
    {
        await _testimonialDAL.ApproveTestimonialAsync(id);
    }
    public async Task RejectTestimonialAsync(int id)
    {
        await _testimonialDAL.RejectTestimonialAsync(id);
    }

}
