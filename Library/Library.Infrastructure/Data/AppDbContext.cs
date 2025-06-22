using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Library.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderBook> OrderBooks => Set<OrderBook>();
    public DbSet<ReadingRoom> ReadingRooms => Set<ReadingRoom>();
    public DbSet<Seat> Seats => Set<Seat>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Phone)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .HasConversion<string>();

        modelBuilder.Entity<OrderBook>()
            .HasOne(ob => ob.Book)
            .WithMany(b => b.OrderBooks)
            .HasForeignKey(ob => ob.BookId);

        modelBuilder.Entity<OrderBook>()
            .HasOne(ob => ob.Order)
            .WithMany(o => o.OrderBooks)
            .HasForeignKey(ob => ob.OrderId);

        modelBuilder.Entity<Seat>()
            .HasOne(s => s.ReadingRoom)
            .WithMany(r => r.Seats)
            .HasForeignKey(s => s.ReadingRoomId);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Seat)
            .WithMany(s => s.Reservations)
            .HasForeignKey(r => r.SeatId);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reservations)
            .HasForeignKey(r => r.UserId);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Book)
            .WithMany(b => b.Reviews)
            .HasForeignKey(r => r.BookId);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId);
    }
}
