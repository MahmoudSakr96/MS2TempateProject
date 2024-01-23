﻿namespace MS2Project.API.IntegrationTests;

[CollectionDefinition("MS2ProjectAPI - Full Integration Test #1")]
public class WebApplicationFactoryCollection : ICollectionFixture<CustomWebApplicationFactory<Program>>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}
