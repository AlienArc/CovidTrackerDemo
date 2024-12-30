﻿using AutoMapper;

namespace CovidTracker.Core.Tests;

public abstract class MapperTestBase<T> where T : AutoMapper.Profile, new()
{
    protected MapperConfiguration Config;
    protected IMapper Mapper;

    public MapperTestBase()
    {

        Config = new MapperConfiguration(mce =>
        {
            mce.AddProfile(new T());
            ConfigureAdditionalMappers(mce);
        });
        Mapper = Config.CreateMapper();
    }

    [Test]
    public virtual void Configuration_IsValid()
    {
        Config.AssertConfigurationIsValid();
    }

    protected virtual void ConfigureAdditionalMappers(IMapperConfigurationExpression configExpression)
    {
    }
}
