using Microsoft.EntityFrameworkCore;
using QMS.Core.Entities;

namespace QMS.Data.Context;

public class QmsDbContext : DbContext
{
    public QmsDbContext(DbContextOptions<QmsDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<RolePermission> RolePermissions { get; set; } = null!;
    public DbSet<Queue> Queues { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<Counter> Counters { get; set; } = null!;
    public DbSet<Ticket> Tickets { get; set; } = null!;
    public DbSet<TicketHistory> TicketHistories { get; set; } = null!;
    public DbSet<TicketCall> TicketCalls { get; set; } = null!;
    public DbSet<Device> Devices { get; set; } = null!;
    public DbSet<SystemSettings> SystemSettings { get; set; } = null!;
    public DbSet<AuditLog> AuditLogs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User -> Role (Many-to-One)
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // User -> Counter (One-to-Many)
        modelBuilder.Entity<User>()
            .HasOne(u => u.Counter)
            .WithMany(c => c.Users)
            .HasForeignKey(u => u.CounterId)
            .OnDelete(DeleteBehavior.SetNull);

        // Queue -> Service (One-to-Many)
        modelBuilder.Entity<Service>()
            .HasOne(s => s.Queue)
            .WithMany(q => q.Services)
            .HasForeignKey(s => s.QueueId)
            .OnDelete(DeleteBehavior.Cascade);

        // Service -> Counter (Many-to-Many)
        modelBuilder.Entity<Counter>()
            .HasOne(c => c.Service)
            .WithMany(s => s.Counters)
            .HasForeignKey(c => c.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        // Queue -> Ticket (One-to-Many)
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Queue)
            .WithMany(q => q.Tickets)
            .HasForeignKey(t => t.QueueId)
            .OnDelete(DeleteBehavior.Cascade);

        // Service -> Ticket (One-to-Many)
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Service)
            .WithMany(s => s.Tickets)
            .HasForeignKey(t => t.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        // Ticket -> TicketHistory (One-to-Many)
        modelBuilder.Entity<TicketHistory>()
            .HasOne(th => th.Ticket)
            .WithMany(t => t.Histories)
            .HasForeignKey(th => th.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        // Counter -> TicketHistory (One-to-Many)
        modelBuilder.Entity<TicketHistory>()
            .HasOne(th => th.Counter)
            .WithMany(c => c.TicketHistories)
            .HasForeignKey(th => th.CounterId)
            .OnDelete(DeleteBehavior.SetNull);

        // Ticket -> TicketCall (One-to-Many)
        modelBuilder.Entity<TicketCall>()
            .HasOne(tc => tc.Ticket)
            .WithMany(t => t.Calls)
            .HasForeignKey(tc => tc.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        // Counter -> TicketCall (One-to-Many)
        modelBuilder.Entity<TicketCall>()
            .HasOne(tc => tc.Counter)
            .WithMany(c => c.TicketCalls)
            .HasForeignKey(tc => tc.CounterId)
            .OnDelete(DeleteBehavior.Restrict);

        // Role -> RolePermission (One-to-Many)
        modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Role)
            .WithMany(r => r.Permissions)
            .HasForeignKey(rp => rp.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        // AuditLog -> User (Many-to-One)
        modelBuilder.Entity<AuditLog>()
            .HasOne(al => al.User)
            .WithMany()
            .HasForeignKey(al => al.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        // Add indexes
        modelBuilder.Entity<Ticket>()
            .HasIndex(t => t.TicketNumber)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<Queue>()
            .HasIndex(q => q.QueueName)
            .IsUnique();
    }
}
