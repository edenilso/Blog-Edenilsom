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
    public class PostConfig : EntityTypeConfiguration<Post>
    {
        public PostConfig()
        {
            ToTable("POST");
            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasColumnName("IDPOST")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Autor)
                .HasColumnName("AUTOR")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.DataPublicacao)
               .HasColumnName("DATAPUBLICACAO")
               .IsRequired();

            Property(x => x.Descricao)
               .HasColumnName("DESCRICAO")
               .IsMaxLength()
               .IsRequired();
                       
            Property(x => x.Resumo)
               .HasColumnName("RESUMO")
               .HasMaxLength(1000)
               .IsRequired();

            Property(x => x.Titulo)
               .HasColumnName("TITULO")
               .HasMaxLength(100)
               .IsRequired();

            Property(x => x.Visivel)
               .HasColumnName("VISIVEL")
               .IsRequired();

            HasMany(x => x.Arquivos)
                .WithOptional()
                .HasForeignKey(x => x.IdPost);

            HasMany(x => x.Comentarios)
                .WithOptional()
                .HasForeignKey(x => x.IdPost);

            HasMany(x => x.Imagens)
            .WithOptional()
            .HasForeignKey(x => x.IdPost);

            HasMany(x => x.visitas)
            .WithOptional()
            .HasForeignKey(x => x.IdPost);

            HasMany(x => x.TagPost)
           .WithOptional()
           .HasForeignKey(x => x.IdPost);


        }
    }
}
