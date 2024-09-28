using System.Reflection;
using DesafioMottu.Application.Abstractions.Messaging;
using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Infrastructure;

namespace DesafioMottu.ArchitectureTests.Infrastructure;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(IBaseCommand).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(Entity).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
}
