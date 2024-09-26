using MicroservicesWithCQRSDesignPattern.AppDbContext;
using MicroservicesWithCQRSDesignPattern.Handlers;
using MicroservicesWithCQRSDesignPattern.Interfaces;
using MicroservicesWithCQRSDesignPattern.Models;
using MicroservicesWithCQRSDesignPattern.Queries.CommandModel;
using MicroservicesWithCQRSDesignPattern.Queries.QueryModel;
using MicroservicesWithCQRSDesignPattern.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connect to database service
builder.Services.AddDbContext<ApplicationDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLDb"));
});

// Quản lý các thao tác ghi và đọc dữ liệu từ cơ sở dữ liệu thông qua một repository cho đối tượng Product.
builder.Services.AddScoped<IRepository<int, Product>, ProductRepository>();

// Đăng ký command handler cho việc xử lý các lệnh tạo sản phẩm trong mô hình CQRS.
builder.Services.AddTransient<ICommandHandler<CreateProductCommand>, CreateProductCommandHandler>();

// Đăng ký query handler để xử lý truy vấn lấy danh sách sản phẩm trong mô hình CQRS.
builder.Services.AddTransient<IQueryHandler<GetProductsQuery, IEnumerable<GetAllProductsCommand>>, GetProductsQueryHandler>();

// Đăng ký command handler để xử lý các lệnh cập nhật sản phẩm.
builder.Services.AddTransient<ICommandHandler<UpdateProductCommand>, UpdateProductCommandHandler>();

// Đăng ký command handler để xử lý các lệnh xóa sản phẩm.
builder.Services.AddTransient<ICommandHandler<DeleteProductCommand>, DeleteProductCommandHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
