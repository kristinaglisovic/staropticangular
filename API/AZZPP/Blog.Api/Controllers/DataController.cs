using Blog.DataAccess;
using Blog.Domain;
using Blog.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        /// <summary>
        /// Insert data in db
        /// </summary>
        /// <param name="context"></param> 
        /// <returns>Inserted data</returns>
        /// 
        /// <response code="201">Created</response>
        /// <response code="409">Conflict.</response>
        /// <response code="500">Unexpected server error.</response>


        // GET: api /<DataController>
        [HttpGet]
        public IActionResult Get([FromServices] BlogDbContext context)
        {
            

            if (context.Users.Any())
            {
                return Conflict();
            }

            var roles = new List<Role>
            {
                new Role{ Name= "Administrator"},
                new Role{ Name= "User"},
            };

            var admin = new User { FirstName = "Admin", LastName = "ASP", Username = "adminASP", Email = "adminasp@gmail.com", Role = roles.First(), Password = BCrypt.Net.BCrypt.HashPassword("Lozinka123!") };

            var images = new List<Image>
            {
                new Image{ Path = "1.jpg"},
                new Image{ Path = "2.jpg"},
                new Image{ Path = "3.jpg"},
                new Image{ Path = "4.jpg"},
                new Image{ Path = "5.jpg"},
                new Image{ Path = "6.jpg"},
                new Image{ Path = "7.jpg"},
                new Image{ Path = "8.jpg"},
                new Image{ Path = "9.jpg"},
                new Image{ Path = "10.jpg"},
            };

            var users = new List<User>
            {
                new User { FirstName = "Petar", LastName = "Peric", Username = "pera" , Email ="pera@gmail.com", Role= roles.First(), Password=BCrypt.Net.BCrypt.HashPassword("Lozinka123!")},
                new User { FirstName = "Mika", LastName = "Mikic", Username = "mika", Email ="mika@gmail.com", Role= roles.ElementAt(1),Password=BCrypt.Net.BCrypt.HashPassword("Lozinka123!"),Image=images.Last()},
                new User { FirstName = "Lazar", LastName = "Lazic", Username = "laza", Email ="laza@gmail.com", Role= roles.ElementAt(1), Password=BCrypt.Net.BCrypt.HashPassword("Lozinka123!")},
                new User { FirstName = "Paja", LastName = "Patak", Username = "paja", Email ="paja@gmail.com", Role= roles.First(), Password=BCrypt.Net.BCrypt.HashPassword("Lozinka123!")},
            };


            

            var tags = new List<Tag>
            {
                new Tag { Name = "funny" },
                new Tag { Name = "educational" },
                new Tag { Name = "sad" },
                new Tag { Name = "cool" },
                new Tag { Name = "happy" },
                new Tag { Name = "artistic" },
            };

            var categories = new List<Category>
            {
                new Category { Name = "Computers", Desciption="LAPTOP, MAC"},
                new Category { Name = "Gaming" , Desciption="GTA 5, LOL"},
                new Category { Name = "Music" , Desciption="TECHNO, HOUSE"},
                new Category { Name = "Animals" , Desciption="CAT, DOG"},
                new Category { Name = "Art" , Desciption="PORTRAIT, PHOTO" },
            };

            var posts = new List<Post>
            {
                new Post{ Title="Music is good", Description="Yes yes", User=users.First()},
                new Post{ Title="I love dogs", Description="Something", User=users.ElementAt(1)},
                new Post{ Title="Artistic title", Description="Something2", User=users.ElementAt(2)},
                new Post{ Title="LOL is the best game", Description="Something3", User=users.ElementAt(1)},
                new Post{ Title="My computer doesnt work", Description="Something4", User=users.ElementAt(2)},
            };


            //dodati post
            var comments = new List<Comment>
            {
                new Comment{ Text= "Cool.", User=users.ElementAt(1), Post=posts.First()},
                new Comment{ Text= "Nice.", User=users.ElementAt(2), Post=posts.ElementAt(2)},
                new Comment{ Text= "Not bad.", User=users.ElementAt(1), Post=posts.First()},
                new Comment{ Text= "LOL.", User=users.ElementAt(2), Post=posts.ElementAt(3)},
                new Comment{ Text= "Funny.", User=users.ElementAt(1), Post=posts.ElementAt(4)}
            };


            var postTags = new List<PostTag>
            {
                new PostTag
                {
                    Post = posts.First(),
                    Tag = tags.ElementAt(1)
                }
                ,
                new PostTag
                {
                    Post = posts.First(),
                    Tag = tags.ElementAt(2)
                }
                ,
                new PostTag
                {
                    Post = posts.First(),
                    Tag = tags.ElementAt(3)
                }
                ,
                new PostTag
                {
                    Post = posts.ElementAt(1),
                    Tag = tags.ElementAt(1)
                }
                ,
               new PostTag
                {
                    Post = posts.ElementAt(2),
                    Tag = tags.ElementAt(3)
                }
                , new PostTag
                {
                    Post = posts.ElementAt(3),
                    Tag = tags.First()
                }
                    , new PostTag
                {
                    Post = posts.ElementAt(4),
                    Tag = tags.ElementAt(2)
                }
            };



            /*U REALNOM ZIVOTU SE NECE DESITI DA ISTA SLIKA PRIPADA I PROFILNOJ SLICI KORISNIKA
             I SAMOM POSTU, IAKO JE TO MIGRACIJOM ODRADJENO JER SU UNESENE NASUMICNE VREDNOSTI ZBOG POPUNJAVANJA BAZE
             Odradjena je validacija da li je ta slika koja se prosledjuje kreiranjem posta pripada korisniku ili je slobodna
             za koriscenje posta
             To se moze videti kada samo korisnik unese sliku gde ce se izlistavanjem svih slika post biti []*/
            var postImage = new List<PostImage>
            {
                new PostImage
                {
                    Post = posts.First(),
                    Image = images.ElementAt(1)
                },
                new PostImage
                {
                    Post = posts.First(),
                    Image = images.ElementAt(3)
                }
                ,new PostImage
                {
                    Post = posts.First(),
                    Image = images.ElementAt(2)
                },
                new PostImage
                {
                    Post = posts.ElementAt(1),
                    Image = images.ElementAt(2)
                },
                new PostImage
                {
                    Post = posts.ElementAt(2),
                    Image = images.ElementAt(3)
                },
                new PostImage
                {
                    Post = posts.ElementAt(2),
                    Image = images.ElementAt(4)
                },
                new PostImage
                {
                    Post = posts.ElementAt(3),
                    Image = images.ElementAt(1)
                },
            };


            var postCategory = new List<PostCategory> {
                new PostCategory {
                    Post= posts.ElementAt(1),
                    Category= categories.First()
                },
                new PostCategory {
                    Post= posts.ElementAt(2),
                    Category= categories.ElementAt(2)
                },
                new PostCategory {
                    Post= posts.ElementAt(3),
                    Category= categories.ElementAt(2)
                },
                new PostCategory {
                    Post= posts.ElementAt(4),
                    Category= categories.ElementAt(3)
                },
                new PostCategory {
                    Post= posts.ElementAt(0),
                    Category= categories.First()
                },
            };


            var likes = new List<Like>
            {
                new Like
                {
                    User = users.First(),
                    Post = posts.First()
                },
                new Like
                {
                    User = users.First(),
                    Post = posts.ElementAt(3)
                },
                new Like
                {
                    User = users.ElementAt(2),
                    Post = posts.ElementAt(3)
                },  new Like
                {
                    User = users.ElementAt(1),
                    Post = posts.ElementAt(2)
                },  new Like
                {
                    User = users.ElementAt(2),
                    Post = posts.ElementAt(1)
                },  new Like
                {
                    User = users.ElementAt(1),
                    Post = posts.ElementAt(1)
                },
            };


            var userUseCases = new List<UserUseCase>();
            for (int i = 1; i <= 45; i++)
            {
                userUseCases.Add(new UserUseCase
                {
                    User = admin,
                    UseCaseId = i,
                    UpdatedAt = System.DateTime.UtcNow
                });

            }

            context.Images.AddRange(images);
            context.Users.Add(admin);
            context.Categories.AddRange(categories);
            context.Comments.AddRange(comments);
            context.Likes.AddRange(likes);
            context.Posts.AddRange(posts);
            context.PostCategories.AddRange(postCategory);
            context.PostImages.AddRange(postImage);
            context.PostTags.AddRange(postTags);
            context.Roles.AddRange(roles);
            context.Tags.AddRange(tags);
            context.Users.AddRange(users);
            context.UserUseCases.AddRange(userUseCases);

            context.SaveChanges();

            return StatusCode(201);
        }
    }
}
