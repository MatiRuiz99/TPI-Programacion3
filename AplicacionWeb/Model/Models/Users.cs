﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public int RoleId { get; set; }

        public virtual RoleList Role { get; set; }
    }
}