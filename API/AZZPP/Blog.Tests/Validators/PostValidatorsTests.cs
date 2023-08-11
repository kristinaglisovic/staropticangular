using Blog.Application.UseCases.DTO.Post;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Blog.Implementation.Validators;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Blog.Tests.Validators
{
    public class PostValidatorsTests
    {
        [Fact]
        public void CreatePostTests()
        {

            var validator = new CreatePostDTOValidator(Context);

            var dto = new CreatePostDTO
            {
                Title = "a"
            };

            var result = validator.Validate(dto);
        }


        [Fact]
        public void CreatePostFail()
        {

            var validator = new CreatePostDTOValidator(Context);

            var dto = new CreatePostDTO
            {
                Title = "Dioptrijske naočare nisu zaštitne naočare"
            };

            var result = validator.Validate(dto);
        }



         [Fact]
         public void CreateRoleFail()
            {


            var role = new Role { Name = "Adminaaaaaaa" };

            Context.Roles.Add(role);
            Context.SaveChanges();


            //role.Id.Should().NotBe(0);
            var addedRole = Context.Roles.Find(role.Id);
         

        }
            

        private BlogDbContext Context
        {
            get
            {
                var conString = "Data Source=KIMIX\\SQLEXPRESS;Initial Catalog=Blog;Integrated Security=True";

                var optionsBuilder = new DbContextOptionsBuilder().UseSqlServer(conString);


                var options = optionsBuilder.Options;
                return  new BlogDbContext(options);
            }
        }
    }
}
