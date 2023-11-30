using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Agrisustain_Jamaica.Models;

namespace Agrisustain_Jamaica.Data
{
    public class Agrisustain_JamaicaContext : DbContext
    {
        public Agrisustain_JamaicaContext (DbContextOptions<Agrisustain_JamaicaContext> options)
            : base(options)
        {
        }

        public DbSet<Agrisustain_Jamaica.Models.PestDiseaseSubmission> PestDiseaseSubmission { get; set; } = default!;
    }
}
