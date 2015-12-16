using BlogEdenilsom.DB.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEdenilsom.DB.Mapeamentos
{
    public class ImagemConfig : EntityTypeConfiguration<Imagem>
    {
        public ImagemConfig()
        {
            ToTable("IMAGEM");
            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasColumnName("IDIMAGEM")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Extensao)
               .HasColumnName("EXTENSAO")
               .HasMaxLength(10)
               .IsRequired();

            Property(x => x.Bytes)
               .HasColumnName("BYTES")
               .IsRequired();

            Property(x => x.IdPost)
               .HasColumnName("IDPOST")
               .IsRequired();

            HasRequired(x => x.Post)
                 .WithMany()
                 .HasForeignKey(x => x.IdPost);

        }
    }
}
