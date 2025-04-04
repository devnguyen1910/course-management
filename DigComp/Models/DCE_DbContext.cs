﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DigComp.Models;

public partial class DCE_DbContext : DbContext
{
    public DCE_DbContext()
    {
    }

    public DCE_DbContext(DbContextOptions<DCE_DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DceUser> DceUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=TUONGNGUYEN;Initial Catalog=DigCompDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DceUser>(entity =>
        {
            entity.ToTable("DCE_Users");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Fullname).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
