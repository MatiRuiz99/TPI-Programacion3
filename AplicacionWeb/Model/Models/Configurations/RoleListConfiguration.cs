﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Models;
using System;
using System.Collections.Generic;

namespace Model.Models.Configurations
{
    public partial class RoleListConfiguration : IEntityTypeConfiguration<RoleList>
    {
        public void Configure(EntityTypeBuilder<RoleList> entity)
        {
            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Authority).HasMaxLength(250);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<RoleList> entity);
    }
}
