﻿using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Microsoft.Extensions.DependencyInjection;
using MS2Project.Application.Configurations;
using MS2Project.Application.Services.Emails;
using MS2Project.Infrastructure.IoC.AutofacSettings;
using Serilog;

namespace MS2Project.Infrastructure;

public class ApplicationStartup
{
    public static IServiceProvider Initialize(
        IServiceCollection services,
        EmailsSettings emailsSettings,
        ILogger logger,
        IExecutionContextAccessor executionContextAccessor)
    {

        var serviceProvider = CreateAutofacServiceProvider(
            services,
            emailsSettings,
            logger,
            executionContextAccessor);

        return serviceProvider;
    }

    private static IServiceProvider CreateAutofacServiceProvider(
        IServiceCollection services,
        EmailsSettings emailsSettings,
        ILogger logger,
        IExecutionContextAccessor executionContextAccessor)
    {
        var container = new ContainerBuilder();

        container.Populate(services);

        container.RegisterModule(new LifeTimeModule());
        container.RegisterModule(new LoggingModule(logger));
        //container.RegisterModule(new MediatorModule());
        container.RegisterModule(new EmailModule(emailsSettings));


        container.RegisterModule(new ProcessingModule());

        container.RegisterInstance(executionContextAccessor);

        var buildContainer = container.Build();

        ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(buildContainer));

        AutofacServiceProvider serviceProvider = new(buildContainer);

        CompositionRoot.SetContainer(buildContainer);

        return serviceProvider;
    }
}
