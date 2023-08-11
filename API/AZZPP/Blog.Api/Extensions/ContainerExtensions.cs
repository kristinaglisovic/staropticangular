using Blog.Api.Core;
using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Blog.Implementation.UseCases.Commands.Ef;
using Blog.Implementation.UseCases.Queries.Ef;
using Blog.Implementation.UseCases.Queries.SP;
using Blog.Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Api.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddJwt(this IServiceCollection services, AppSettings settings)
        {
            services.AddTransient(x =>
            {
                var context = x.GetService<BlogDbContext>();
                var settings = x.GetService<AppSettings>();

                return new JwtManager(context, settings.JwtSettings);
            });



            //registracija verifikacije 
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.JwtSettings.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSettings.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void AddUseCases(this IServiceCollection services)
        {
            #region Queries
            services.AddTransient<IGetPostsQuery, EfGetPostsQuery>();
            services.AddTransient<IGetPostQuery, EfGetPostQuery>();
            services.AddTransient<IGetUseCaseLogsQuery, GetUseCaseLogsQuery>();
            services.AddTransient<IGetImagesQuery, EfGetImagesQuery>();
            services.AddTransient<IGetImageQuery, EfGetImageQuery>();
            services.AddTransient<IGetRolesQuery, EfGetRolesQuery>();
            services.AddTransient<IGetRoleQuery, EfGetRoleQuery>();
            services.AddTransient<IGetTagsQuery, EfGetTagsQuery>();
            services.AddTransient<IGetTagQuery, EfGetTagQuery>();
            services.AddTransient<IGetCategoriesQuery, EfGetCategoriesQuery>();
            services.AddTransient<IGetCategoryQuery, EfGetCategoryQuery>();
            services.AddTransient<IGetLikesQuery, EfGetLikesQuery>();
            services.AddTransient<IGetLikeQuery, EfGetLikeQuery>();
            services.AddTransient<IGetCommentsQuery, EfGetCommentsQuery>();
            services.AddTransient<IGetCommentQuery, EfGetCommentQuery>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IGetUserQuery, EfGetUserQuery>();
            services.AddTransient<IGetPostTagsQuery, EfGetPostTagsQuery>();
            services.AddTransient<IGetPostImagesQuery, EfGetPostImagesQuery>();
            services.AddTransient<IGetPostCategoryQuery, EfGetPostCategoryQuery>();

            #endregion

            #region Commands
            //create
            services.AddTransient<ICreateImageCommand, EfCreateImageCommand>();
            services.AddTransient<ICreatePostCommand, EfCreatePostCommand>();
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<ICreateRoleCommand, EfCreateRoleCommand>();
            services.AddTransient<ICreateTagCommand, EfCreateTagCommand>();
            services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
            services.AddTransient<ICreateLikeCommand, EfCreateLikeCommand>();
            services.AddTransient<ICreateCommentCommand, EfCreateCommentCommand>();
            //delete
            services.AddTransient<IDeletePostUsingIntCommand, EfDeletePostCommand>();
            services.AddTransient<IDeleteImageUsingIntCommand, EfDeleteImageCommand>();
            services.AddTransient<IDeleteRoleUsingIntCommand, EfDeleteRoleCommand>();
            services.AddTransient<IDeleteTagUsingIntCommand, EfDeleteTagCommand>();
            services.AddTransient<IDeleteCategoryUsingIntCommand, EfDeleteCategoryCommand>();
            services.AddTransient<IDeleteLikeUsingIntCommand, EfDeleteLikeCommand>();
            services.AddTransient<IDeleteCommentUsingIntCommand, EfDeleteCommentCommand>();
            services.AddTransient<IDeleteUserUsingIntCommand, EfDeleteUserCommand>();
            //update
            services.AddTransient<IUpdateUserUseCasesCommand, EfUpdateUserUseCasesCommand>();
            services.AddTransient<IPostActivationCommand, EfPostActivationCommand>();
            services.AddTransient<ISoftDeleteImageUsingIntCommand, EfSoftDeleteImageCommand>();
            services.AddTransient<ISoftDeletePostUsingIntCommand, EfSoftDeletePostUsingIntCommand>();
            services.AddTransient<ISoftDeleteUserUsingIntCommand, EfSoftDeleteUserUsingIntCommand>();
            services.AddTransient<ISoftDeleteRoleUsingIntCommand, EfSoftDeleteRoleCommand>();
            services.AddTransient<ITagActivationCommand, EfTagActivationCommand>();
            services.AddTransient<ICategoryActivationCommand, EfCategoryActivationCommand>();
            services.AddTransient<ISoftDeleteLikeUsingIntCommand, EfSoftDeleteLikeCommand>();
            services.AddTransient<ICommentActivationCommand, EfCommentActivationCommand>();
            services.AddTransient<IUserActivationCommand, EfUserActivationCommand>();
            services.AddTransient<IUpdatePostLikeCommand, EfUpdatePostLikeCommand>();
            services.AddTransient<ITagEditCommand, EfTagEditCommand>();
            services.AddTransient<ICategoryEditCommand, EfEditCategoryCommand>();
            services.AddTransient<IEditUserCommand, EfEditUserCommand>();
            services.AddTransient<IEditPostCommand, EfEditPostCommand>();

           
            #endregion


            #region Validators
            services.AddTransient<CreatePostDTOValidator>();
            services.AddTransient<UpdateUserUseCaseValidator>();
            services.AddTransient<RegisterValidator>();
            services.AddTransient<SearchUseCaseLogsValidator>();
            services.AddTransient<CreateRoleDTOValidator>();
            services.AddTransient<CreateTagDTOValidator>();
            services.AddTransient<CreateCategoryDTOValidator>();
            services.AddTransient<CreateLikeDTOValidator>();
            services.AddTransient<CreateCommentDTOValidator>();
            services.AddTransient<EditTagDTOValidator>();
            services.AddTransient<EditCategoryDTOValidator>();
            services.AddTransient<EditUserValidator>();
            services.AddTransient<EditPostDTOValidator>();
            #endregion
        }


        public static void AddApplicationUser(this IServiceCollection services)
        {
            services.AddTransient<IApplicationUser>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var header = accessor.HttpContext.Request.Headers["Authorization"];

                var claims = accessor.HttpContext.User;

                if (claims == null || claims.FindFirst("UserId") == null)
                {
                    return new AnonimousUser();
                }

                var actor = new JwtUser
                {
                    Email = claims.FindFirst("Email").Value,
                    Id = Int32.Parse(claims.FindFirst("UserId").Value),
                    Identity = claims.FindFirst("Email").Value,
                    UseCaseIds = JsonConvert.DeserializeObject<List<int>>(claims.FindFirst("UseCases").Value)
                };

                return actor;
            });
        }


        public static void AddBlogDbContext(this IServiceCollection services)
        {
            services.AddTransient(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder();

                var conString = x.GetService<AppSettings>().ConnString;

                optionsBuilder.UseSqlServer(conString);

                var options = optionsBuilder.Options;

                
               return new BlogDbContext(options);
               //return new BlogDbContext();
            });
        }
    }
}
