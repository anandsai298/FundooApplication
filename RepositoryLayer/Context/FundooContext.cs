using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions option):base(option) 
        {
          
        }
        public DbSet<UserEntity>Users { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
        public DbSet<FundooNoteEntity> FundooNotes { get; set; }

        public DbSet<LabelEntity> Labels { get; set; }
    }
}
