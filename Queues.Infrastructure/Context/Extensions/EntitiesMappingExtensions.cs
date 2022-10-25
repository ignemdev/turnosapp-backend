using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Queues.Domain.Entities;

namespace Queues.Infrastructure.Context.Extensions;
public static class EntitiesMappingExtensions
{
    public static ModelBuilder AddPeopleMapping(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasIndex(e => e.IdentificationNumber)
                .IsUnique();

            entity.Property(e => e.Name)
                .AddRequiredProperty();

            entity.Property(e => e.LastName)
                .AddRequiredProperty(300);

            entity.Property(e => e.IdentificationNumber)
                .AddRequiredProperty(11);

            entity.Property(e => e.Gender)
                .AddRequiredProperty(1);

            entity.HasCheckConstraint("CK_People_Gender", $"Gender in ('M', 'F')");
        });

        return modelBuilder;
    }

    public static ModelBuilder AddQueuesMapping(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Queue>(entity =>
        {
            entity.Property(e => e.Name)
                .AddRequiredProperty();
        });

        return modelBuilder;
    }

    public static ModelBuilder AddPeopleQueuesMapping(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonQueue>(entity =>
        {
            entity.HasOne(d => d.Person)
                .WithMany()
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PeopleQueues_People_PersonId");

            entity.HasOne(d => d.Queue)
                .WithMany()
                .HasForeignKey(d => d.QueueId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PeopleQueues_Queues_QueueId");
        });

        return modelBuilder;
    }

    private static PropertyBuilder<TPropertyType> AddRequiredProperty<TPropertyType>(
        this PropertyBuilder<TPropertyType> propertyBuilder,
        int maxLength = 150)
    {
        propertyBuilder
            .HasMaxLength(maxLength)
            .IsRequired();

        return propertyBuilder;
    }
}
